using MinesServer.Data;
using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Visual;

public record class LinePacket : GUIComponentPacket, IGUIComponentPacket<LinePacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<LinePacket>();

    public LineDirection Direction { get; init; }

    public override int Size => base.Size
        + sizeof(LineDirection); // Direction

    public override int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.Write(Direction);
        return writer.Position;
    }

    public static LinePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    public LinePacket(ref MemoryReader reader) : base(ref reader)
    {
        Direction = reader.Read<LineDirection>();
    }

    public LinePacket() : base() { }
}
