using MinesServer.Utils;
using MinesServer.Networking.Server.Packets.GUI;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Input;

public record class TextBoxPacket : InputComponentPacket<string>, IGUIComponentPacket<TextBoxPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<TextBoxPacket>();

    public override string DefaultValue { get; init; } = "";

    public string Regex { get; init; } = "";

    public override int Size => base.Size
        + sizeof(ushort) // DefaultValue.Length
        + DefaultValue.Length * 2 // DefaultValue
        + sizeof(byte) // Regex.Length
        + Regex.Length * 2; // Regex

    public override int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.WriteU2PrefixedUtf16(DefaultValue);
        writer.WriteU1PrefixedUtf16(Regex);
        return writer.Position;
    }

    public static TextBoxPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected TextBoxPacket(ref MemoryReader reader) : base(ref reader)
    {
        DefaultValue = reader.ReadU2PrefixedUtf16(out _);
        Regex = reader.ReadU1PrefixedUtf16(out _);
    }

    public TextBoxPacket() : base() { }
}
