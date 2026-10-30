using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Chat;

public readonly record struct ChatMutePacket(long Timestamp, long EndsAt, string Reason, int ModeratorId, string ModeratorName) : IRootServerPacket<ChatMutePacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<ChatMutePacket>();

    public int Size =>
        sizeof(long) + // Timestamp
        sizeof(long) + // EndsAt
        sizeof(byte) + // Reason.Length
        Reason.Length * 2 + // Reason
        sizeof(int) + // ModeratorId
        sizeof(byte) + // ModeratorName.Length
        ModeratorName.Length * 2; // ModeratorName

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(Timestamp);
        writer.Write(EndsAt);
        writer.WriteU1PrefixedUtf16(Reason);
        writer.Write(ModeratorId);
        writer.WriteU1PrefixedUtf16(ModeratorName);
        return writer.Position;
    }

    public static ChatMutePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.Read8(),
            reader.Read8(),
            reader.ReadU1PrefixedUtf16(out _),
            reader.Read4(),
            reader.ReadU1PrefixedUtf16(out _));
    }
}
