using MinesServer.Networking.Server.Packets.GUI.Components.Visual;

namespace MinesServer.Networking.Tests.Server.GUI.Visual;

internal sealed class PanelTest : PacketTest<PanelPacket>
{
    public override PanelPacket Packet => new();
}