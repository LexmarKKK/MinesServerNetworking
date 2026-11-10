using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Visual;

public record class PanelPacket : GUIComponentPacket, IGUIComponentPacket<PanelPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<PanelPacket>();

    public static PanelPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    public PanelPacket() : base() { }

    public PanelPacket(ref MemoryReader reader) : base(ref reader) { }
}
