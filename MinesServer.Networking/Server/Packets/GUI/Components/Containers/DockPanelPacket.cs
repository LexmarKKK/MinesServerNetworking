using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Containers;

public record class DockPanelPacket : ContainerComponentPacket, IGUIComponentPacket<DockPanelPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<DockPanelPacket>();

    public static DockPanelPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected DockPanelPacket(ref MemoryReader reader) : base(ref reader) { }

    public DockPanelPacket() : base() { }
}
