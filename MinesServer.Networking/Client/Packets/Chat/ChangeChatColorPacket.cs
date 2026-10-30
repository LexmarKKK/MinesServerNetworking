using MinesServer.Utils;
using System;
using System.Drawing;

namespace MinesServer.Networking.Client.Packets.Chat;

public readonly record struct ChangeChatColorPacket(Color Color) : IRootClientPacket<ChangeChatColorPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<ChangeChatColorPacket>();

    public int Size =>
        sizeof(int); // Color

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(Color.ToArgb());
        return writer.Position;
    }

    public static ChangeChatColorPacket Decode(ReadOnlySpan<byte> input) => new(Color.FromArgb(input.Reader().Read4()));

    public bool Equals(ChangeChatColorPacket other) => Color.ToArgb() == other.Color.ToArgb();
    public override int GetHashCode() => Color.ToArgb().GetHashCode();

}