using MinesServer.Networking.Client.Packets;
using MinesServer.Networking.Client.Packets.Actions;
using MinesServer.Networking.Client.Packets.Movement;
using MinesServer.Networking.Server.Packets;
using MinesServer.Networking.Server.Packets.Movement;

namespace MinesServer.Networking.Tests;

/// <summary>
/// Независимые wire-векторы фиксируют длину, порядок полей и little-endian
/// представление для кадров обоих направлений.
/// </summary>
internal sealed class WireFormatGoldenTests
{
    private static readonly byte[] ClientFrame =
    [
        0x12, 0x00, 0x00, 0x00,
        0x44, 0x33, 0x22, 0x11,
        0x18,
        0x02, 0x01, 0x04, 0x03,
        0x0B,
        0x06, 0x05, 0x08, 0x07,
    ];

    private static readonly byte[] ServerFrame =
    [
        0x0A, 0x00, 0x00, 0x00,
        0x01, 0x00,
        0x02, 0x01, 0x04, 0x03,
    ];

    [Test]
    public void ClientActionFrame_MatchesGoldenBytesAndDecodes()
    {
        var packet = new ClientPacket(
            0x11223344,
            new ActionClientPacket(0x0102, 0x0304, new MovePacket(0x0506, 0x0708)));
        var encoded = new byte[packet.Size];

        int written = packet.Encode(encoded);

        Assert.Multiple(() =>
        {
            Assert.That(written, Is.EqualTo(ClientFrame.Length));
            Assert.That(encoded, Is.EqualTo(ClientFrame));
            Assert.That(ClientPacket.Decode(ClientFrame), Is.EqualTo(packet));
        });
    }

    [Test]
    public void ServerTeleportFrame_MatchesGoldenBytesAndDecodes()
    {
        var packet = new ServerPacket(new TeleportPacket(0x0102, 0x0304));
        var encoded = new byte[packet.Size];

        int written = packet.Encode(encoded);

        Assert.Multiple(() =>
        {
            Assert.That(written, Is.EqualTo(ServerFrame.Length));
            Assert.That(encoded, Is.EqualTo(ServerFrame));
            Assert.That(ServerPacket.Decode(ServerFrame), Is.EqualTo(packet));
        });
    }
}
