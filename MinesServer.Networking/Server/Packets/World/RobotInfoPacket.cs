using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.World;

public readonly record struct RobotInfoPacket(ushort BotId, int PlayerId, byte ClanId, string Skin, string Tail, string Name) : IRootServerPacket<RobotInfoPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<RobotInfoPacket>();

    public int Size =>
        sizeof(ushort) + // BotId
        sizeof(int) + // PlayerId
        sizeof(byte) + // ClanId
        sizeof(byte) + // Skin.Length
        Skin.Length + // Skin
        sizeof(byte) + // Tail.Length
        Tail.Length + // Tail
        sizeof(byte) + // Name.Length
        Name.Length * 2; // Name

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(BotId);
        writer.Write(PlayerId);
        writer.Write(ClanId);
        writer.WriteU1PrefixedASCII(Skin);
        writer.WriteU1PrefixedASCII(Tail);
        writer.WriteU1PrefixedUtf16(Name);
        return writer.Position;
    }

    public static RobotInfoPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(
            reader.ReadU2(),
            reader.Read4(),
            reader.ReadU1(),
            reader.ReadU1PrefixedASCII(out _),
            reader.ReadU1PrefixedASCII(out _),
            reader.ReadU1PrefixedUtf16(out _));
    }
}
