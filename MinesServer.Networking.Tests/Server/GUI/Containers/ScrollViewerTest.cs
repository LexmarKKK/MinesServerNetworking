using MinesServer.Networking.Server.Packets.GUI;
using MinesServer.Networking.Server.Packets.GUI.Components.Containers;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.GUI.Containers;

internal sealed class ScrollViewerTest : PacketTest<ScrollViewerPacket>
{
    public override ScrollViewerPacket Packet => new() {
        HorizontalScrollBar = ScrollbarVisibility.Hidden,
        VerticalScrollBar = ScrollbarVisibility.Visible,
        Children = [
            new PanelPacket() {
                Style = new() {
                    Background = Color.AliceBlue,
                    BorderWidth = 15,
                    Border = Color.Red
                }
            }
        ]
    };
}