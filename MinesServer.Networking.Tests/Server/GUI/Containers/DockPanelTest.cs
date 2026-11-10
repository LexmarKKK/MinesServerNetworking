using MinesServer.Networking.Server.Packets.GUI.Components.Containers;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;

namespace MinesServer.Networking.Tests.Server.GUI.Containers;

internal sealed class DockPanelTest : PacketTest<DockPanelPacket>
{
    public override DockPanelPacket Packet => new()
    {
        Children = [
            new ImagePacket() {
                URI = "ftp://localhost:21/test.png",
                AttachedProperties = [
                    new("DockPanel.Dock", "Right")
                ]
            }
        ]
    };
}
