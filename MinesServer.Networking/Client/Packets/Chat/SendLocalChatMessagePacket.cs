using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Client.Packets.Chat;

public readonly record struct SendLocalChatMessagePacket(string Message) : IRootClientPacket<SendLocalChatMessagePacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<SendLocalChatMessagePacket>();

    public int Size => Message.Length * 2; // Message

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteUtf16(Message);
        return writer.Position;
    }

    public static SendLocalChatMessagePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.ReadRemainingAsUtf16());
    }
}
