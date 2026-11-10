using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Client.Packets.Chat;

public readonly record struct QueryChatHistoryPacket(string Tag, ulong StartFrom = 0) : IRootClientPacket<QueryChatHistoryPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<QueryChatHistoryPacket>();

    public int Size =>
        Tag.Length + // Tag
        sizeof(ulong); // StartFrom

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(StartFrom);
        writer.WriteASCII(Tag);
        return writer.Position;
    }

    public static QueryChatHistoryPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var startFrom = reader.ReadU8();
        return new QueryChatHistoryPacket(reader.ReadRemainingAsASCII(), startFrom);
    }
}
