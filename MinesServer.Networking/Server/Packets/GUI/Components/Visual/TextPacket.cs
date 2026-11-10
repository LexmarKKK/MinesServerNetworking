using MinesServer.Utils;
using MinesServer.Networking.Server.Packets.GUI;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Visual;

public record class TextPacket : GUIComponentPacket, IGUIComponentPacket<TextPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<TextPacket>();

    public string Text { get; init; } = "";

    public override int Size => base.Size
        + sizeof(ushort) // Text.Length
        + Text.Length * 2; // Text

    public override int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.WriteU2PrefixedUtf16(Text);
        return writer.Position;
    }

    public static TextPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected TextPacket(ref MemoryReader reader) : base(ref reader)
    {
        Text = reader.ReadU2PrefixedUtf16(out _);
    }

    public TextPacket() : base() { }
}
