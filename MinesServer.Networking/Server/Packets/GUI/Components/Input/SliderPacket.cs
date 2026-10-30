using MinesServer.Networking.Server.Packets.GUI.Components.Visual;
using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Input;

public record class SliderPacket : InputComponentPacket<float>, IGUIComponentPacket<SliderPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<SliderPacket>();

    public override float DefaultValue { get; init; }
    public float MinValue { get; init; }
    public float MaxValue { get; init; } = 100;
    public float Step { get; init; } = 1f;
    public ImagePacket Knob { get; init; } = new() { URI = "gui/slider/knob.png" };

    public override int Size => base.Size +
        sizeof(float) + // DefaultValue
        sizeof(float) + // MinValue
        sizeof(float) + // MaxValue
        sizeof(float) + // Step
        Knob.Size; // Knob

    public override int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.Write(DefaultValue);
        writer.Write(MinValue);
        writer.Write(MaxValue);
        writer.Write(Step);
        writer.Write(Knob);
        return writer.Position;
    }

    public static SliderPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected SliderPacket(ref MemoryReader reader) : base(ref reader)
    {
        DefaultValue = reader.Read<float>();
        MinValue = reader.Read<float>();
        MaxValue = reader.Read<float>();
        Step = reader.Read<float>();
        Knob = ImagePacket.Decode(reader.Remaining);
    }

    public SliderPacket() { }
}
