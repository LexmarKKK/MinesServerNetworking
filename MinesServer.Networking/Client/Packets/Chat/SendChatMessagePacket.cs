using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Client.Packets.Chat;

public readonly record struct SendChatMessagePacket(string Tag, string Message) : IRootClientPacket<SendChatMessagePacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<SendChatMessagePacket>();

    public int Size =>
        sizeof(byte) // Tag.Length
        + Tag.Length // Tag
        + Message.Length * 2; // Message

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(Tag);
        writer.WriteUtf16(Message);
        return writer.Position;
    }

    public static SendChatMessagePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.ReadU1PrefixedASCII(out _), reader.ReadRemainingAsUtf16());
    }
}
