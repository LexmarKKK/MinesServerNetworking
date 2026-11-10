using MinesServer.Networking.Server.Packets.GUI.Components;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.GUI;

internal sealed class GUIStyleTest : PacketTest<GUIStylePacket>
{
    public override GUIStylePacket Packet => new()
    {
        Background = Color.FromArgb(128, 255, 0, 255),
        Border = Color.Black,
        BorderWidth = 2,
        Margin = new(1, 2, 3, 4),
        Padding = new(5, 6, 7, 8)
    };
}