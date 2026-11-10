using MinesServer.Utils;
using MinesServer.Networking.Server.Packets.GUI;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Containers;

public record class ScrollViewerPacket : ContainerComponentPacket, IGUIComponentPacket<ScrollViewerPacket>, INamedComponentPacket
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<ScrollViewerPacket>();

    public string Name { get; init; } = "";
    public ScrollbarVisibility HorizontalScrollBar { get; init; }
    public ScrollbarVisibility VerticalScrollBar { get; init; }

    public override int Size => base.Size
        + sizeof(byte) // Name.Length
        + Name.Length // Name
        + sizeof(ScrollbarVisibility) // HorizontalScrollBar
        + sizeof(ScrollbarVisibility); // VerticalScrollBar

    public override int Encode(Span<byte> output)
    {
        if (!Enum.IsDefined(typeof(ScrollbarVisibility), HorizontalScrollBar) ||
            !Enum.IsDefined(typeof(ScrollbarVisibility), VerticalScrollBar))
        {
            throw new ArgumentOutOfRangeException(nameof(HorizontalScrollBar), "Scrollbar visibility is invalid.");
        }

        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.WriteU1PrefixedASCII(Name);
        writer.Write(HorizontalScrollBar);
        writer.Write(VerticalScrollBar);
        return writer.Position;
    }

    public static ScrollViewerPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected ScrollViewerPacket(ref MemoryReader reader) : base(ref reader)
    {
        Name = reader.ReadU1PrefixedASCII(out _);
        HorizontalScrollBar = reader.Read<ScrollbarVisibility>();
        VerticalScrollBar = reader.Read<ScrollbarVisibility>();
    }

    public ScrollViewerPacket() : base() { }
}
