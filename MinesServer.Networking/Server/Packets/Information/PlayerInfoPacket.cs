using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Information;

public readonly record struct PlayerInfoPacket(uint PlayerId, uint BotId, string Nickname) : IRootServerPacket<PlayerInfoPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<PlayerInfoPacket>();

    public int Size =>
        sizeof(uint) + // PlayerId
        sizeof(uint) + // BotId
        Nickname.Length * 2; // Nickname

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(PlayerId);
        writer.Write(BotId);
        writer.WriteUtf16(Nickname);
        return writer.Position;
    }

    public static PlayerInfoPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new PlayerInfoPacket(reader.ReadU4(), reader.ReadU4(), reader.ReadRemainingAsUtf16());
    }
}
