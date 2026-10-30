using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using MinesServer.Networking.Client;
using MinesServer.Networking.Client.Packets;
using MinesServer.Networking.Client.Packets.Programmator;
using MinesServer.Networking.Connection;
using MinesServer.Networking.Connection.Server;
using MinesServer.Networking.Exceptions;
using MinesServer.Networking.Contracts;
using NetCoreServer;
using SocketClient = System.Net.Sockets.TcpClient;

namespace MinesServer.Networking.Tests;

/// <summary>
/// Приёмный путь целиком: кадрирование, декодирование, счётчик ошибок и выдача
/// событий контракта. До этого он жил в копии внутри сервера и не был покрыт ни
/// одним тестом — а сессия поверх него поднимается на настоящем сокете, и ошибка
/// обнаружилась бы только в проде.
///
/// Сокет настоящий, а не заглушка: PacketBuffer и ReceiveLoop — ровно то, что
/// здесь проверяется. Порт свободный, чтобы тесты не дрались за один номер.
/// </summary>
internal sealed class ServerTcpConnectionTests
{
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Клиентский код 200 заведомо не зарегистрирован: таблица корневых
    /// клиентских пакетов на 200 не заходит.
    /// </summary>
    private const byte UnregisteredCode = 200;

    private static int FreePort()
    {
        var probe = new TcpListener(IPAddress.Loopback, 0);
        probe.Start();
        int port = ((IPEndPoint)probe.LocalEndpoint).Port;
        probe.Stop();
        return port;
    }

    private sealed class ProbeServer : TcpServer
    {
        private readonly TaskCompletionSource<TcpConnection> _sessionCreated =
            new(TaskCreationOptions.RunContinuationsAsynchronously);

        public TcpConnection? Session { get; private set; }
        public Task<TcpConnection> SessionCreated => _sessionCreated.Task;

        public ProbeServer(int port) : base(IPAddress.Loopback, port) { }

        protected override TcpSession CreateSession()
        {
            var session = new TcpConnection(this);
            Session = session;
            _sessionCreated.TrySetResult(session);
            return session;
        }
    }

    /// <summary>
    /// Хостовая половина разговора: контракт и сырой провод тестового клиента.
    /// Писать приходится в обход контракта — на том и проверка.
    /// </summary>
    private sealed class Host(IClientConnection connection, Stream wire)
    {
        public IClientConnection Connection { get; } = connection;

        public void Send(byte[] frame)
        {
            wire.Write(frame, 0, frame.Length);
            wire.Flush();
        }
    }

    private static byte[] Frame(IRootClientPacket payload, uint timestamp = 12345)
    {
        var packet = new ClientPacket(timestamp, payload);
        var buffer = new byte[packet.Size];
        int written = packet.Encode(buffer);
        return buffer.AsSpan(0, written).ToArray();
    }

    /// <summary>Кадр с корректной длиной и заголовком, но несуществующим кодом.</summary>
    private static byte[] FrameWithUnregisteredCode()
    {
        var frame = new byte[sizeof(int) + sizeof(uint) + sizeof(byte)];
        BinaryPrimitives.WriteInt32LittleEndian(frame, frame.Length);
        BinaryPrimitives.WriteUInt32LittleEndian(frame.AsSpan(sizeof(int)), 12345);
        frame[sizeof(int) + sizeof(uint)] = UnregisteredCode;
        return frame;
    }

    private static async Task WithConnection(Func<Host, Task> body)
    {
        var server = new ProbeServer(FreePort());
        Assert.That(server.Start(), Is.True, "the probe server must bind before a client connects");
        try
        {
            using var client = new SocketClient();
            await client.ConnectAsync(IPAddress.Loopback, server.Port);

            // CreateSession вызывается из accept-потока: ограниченно ждём его,
            // чтобы при проблеме accept-пути тест завершился диагностикой, а не завис.
            var session = await WaitFor(server.SessionCreated);
            await body(new Host(session, client.GetStream()));
        }
        finally
        {
            server.Stop();
        }
    }

    private static async Task<T> WaitFor<T>(Task<T> task)
    {
        var finished = await Task.WhenAny(task, Task.Delay(Timeout));
        Assert.That(finished, Is.SameAs(task), $"the host was not notified within {Timeout}");
        return await task;
    }

    [Test]
    public async Task DecodableFrame_ReachesTheHostAsAPacket()
    {
        await WithConnection(async host =>
        {
            var received = new TaskCompletionSource<ClientPacket>();
            host.Connection.OnReceived += p => received.TrySetResult(p);

            host.Send(Frame(new DeleteProgramClickPacket()));
            var packet = await WaitFor(received.Task);

            Assert.Multiple(() =>
            {
                Assert.That(packet.Data, Is.InstanceOf<DeleteProgramClickPacket>());
                Assert.That(packet.Timestamp, Is.EqualTo(12345));
            });
        });
    }

    /// <summary>
    /// Два кадра в одном сетевом чтении — самая частая форма прихода данных, и
    /// именно на ней видна рассинхронизация кадрирования: если TryTake отдаёт кадры
    /// не по границам префикса, придут склейки или огрызки.
    /// </summary>
    [Test]
    public async Task TwoFramesInOneWrite_AreDeliveredSeparately()
    {
        await WithConnection(async host =>
        {
            var delivered = new TaskCompletionSource<ClientPacket[]>();
            var packets = new ConcurrentQueue<ClientPacket>();
            host.Connection.OnReceived += p =>
            {
                packets.Enqueue(p);
                if (packets.Count == 2)
                    delivered.TrySetResult(packets.ToArray());
            };

            host.Send(Frame(new DeleteProgramClickPacket()).Concat(Frame(new StopProgramPacket())).ToArray());
            var arrived = await WaitFor(delivered.Task);

            Assert.Multiple(() =>
            {
                Assert.That(arrived, Has.Length.EqualTo(2));
                Assert.That(arrived[0].Data, Is.InstanceOf<DeleteProgramClickPacket>());
                Assert.That(arrived[1].Data, Is.InstanceOf<StopProgramPacket>());
            });
        });
    }

    /// <summary>
    /// Одна битая рамка не должна рвать соединение: длина кадра осмысленная, поток
    /// после неё синхронный. Рвёт только счётчик, а порог принимает хост.
    /// </summary>
    [Test]
    public async Task UndecodableFrame_ReportsTheErrorAndKeepsTheStream()
    {
        await WithConnection(async host =>
        {
            var failed = new TaskCompletionSource<Exception>();
            host.Connection.OnError += e => failed.TrySetResult(e);

            host.Send(FrameWithUnregisteredCode());
            var error = await WaitFor(failed.Task);

            Assert.Multiple(() =>
            {
                Assert.That(error, Is.InstanceOf<PacketDecodeException>());
                Assert.That(host.Connection.DecodeErrorCount, Is.EqualTo(1));
                Assert.That(host.Connection.ConnectionStatus, Is.EqualTo(ConnectionStatus.Connected));
            });
        });
    }

    /// <summary>
    /// Счётчик копится, а не застревает на единице: на нём держится «N ошибок —
    /// дисконнект», и без накопления порог недостижим.
    /// </summary>
    [Test]
    public async Task DecodeErrors_Accumulate()
    {
        await WithConnection(async host =>
        {
            var seen = new ConcurrentQueue<Exception>();
            var third = new TaskCompletionSource<bool>();
            host.Connection.OnError += e =>
            {
                seen.Enqueue(e);
                if (seen.Count == 3)
                    third.TrySetResult(true);
            };

            for (int i = 0; i < 3; i++)
                host.Send(FrameWithUnregisteredCode());

            await WaitFor(third.Task);
            Assert.That(host.Connection.DecodeErrorCount, Is.EqualTo(3));
        });
    }
}
