using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.World;

public readonly record struct LocalChatMessagePacket(uint BotId, ushort FallbackX, ushort FallbackY, string Text) : IRootServerPacket<LocalChatMessagePacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<LocalChatMessagePacket>();

    public int Size =>
        sizeof(uint) + // BotId
        sizeof(ushort) + // FallbackX
        sizeof(ushort) + // FallbackY
        Text.Length * 2; // Nickname

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(BotId);
        writer.Write(FallbackX);
        writer.Write(FallbackY);
        writer.WriteUtf16(Text);
        return writer.Position;
    }

    public static LocalChatMessagePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new LocalChatMessagePacket(
            reader.ReadU4(),
            reader.ReadU2(),
            reader.ReadU2(),
            reader.ReadRemainingAsUtf16());
    }
}
