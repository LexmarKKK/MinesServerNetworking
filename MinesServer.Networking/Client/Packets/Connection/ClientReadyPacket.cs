using System;

namespace MinesServer.Networking.Client.Packets.Connection;

/// <summary>
/// Клиент показал мир: ассеты скачаны, позиция, террейн и интерфейс готовы. До этого пакета
/// сервер держит робота скрытым и неуязвимым; шлётся один раз на каждый вход в мир.
/// </summary>
public readonly record struct ClientReadyPacket() : IRootClientPacket<ClientReadyPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<ClientReadyPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static ClientReadyPacket Decode(ReadOnlySpan<byte> input) => new();
}
