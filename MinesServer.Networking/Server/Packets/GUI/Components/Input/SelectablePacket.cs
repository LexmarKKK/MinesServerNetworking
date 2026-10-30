using MinesServer.Networking.Server.Packets.GUI.Components.Visual;
using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Input;

public record class SelectablePacket : InputComponentPacket<bool>, IGUIComponentPacket<SelectablePacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<SelectablePacket>();

    public override bool DefaultValue { get; init; }

    public ImagePacket Checked { get; init; } = new() { URI = "gui/check/generic_on.png" };

    public ImagePacket Unchecked { get; init; } = new() { URI = "gui/check/generic_off.png" };

    public override int Size => base.Size
        + sizeof(bool) // DefaultValue
        + Checked.Size // Checked
        + Unchecked.Size; // Unchecked

    public override int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.Write(DefaultValue);
        writer.Write(Checked);
        writer.Write(Unchecked);
        return writer.Position;
    }

    public static SelectablePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected SelectablePacket(ref MemoryReader reader) : base(ref reader)
    {
        DefaultValue = reader.Read<bool>();
        Checked = ImagePacket.Decode(reader.Remaining);
        reader.Advance(Checked.Size);
        Unchecked = ImagePacket.Decode(reader.Remaining);
        reader.Advance(Unchecked.Size);
    }

    public SelectablePacket() : base() { }
}
