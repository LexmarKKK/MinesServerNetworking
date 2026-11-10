using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Containers;

public record class CanvasPacket : ContainerComponentPacket, IGUIComponentPacket<CanvasPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<CanvasPacket>();

    public static CanvasPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected CanvasPacket(ref MemoryReader reader) : base(ref reader) { }

    public CanvasPacket() : base() { }
}