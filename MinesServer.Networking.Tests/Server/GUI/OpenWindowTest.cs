using MinesServer.Networking.Server.Packets.GUI;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.GUI;

internal sealed class OpenWindowTest : RootServerPacketTest<OpenWindowPacket>
{
    public override OpenWindowPacket Packet => new("TestWindow", 800, 600, new PanelPacket()
    {
        Style = new()
        {
            Background = Color.Azure
        }
    });
}