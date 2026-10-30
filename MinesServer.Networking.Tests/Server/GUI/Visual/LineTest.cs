using MinesServer.Data;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.GUI.Visual;

internal sealed class LineTest : PacketTest<LinePacket>
{
    public override LinePacket Packet => new() {
        Direction = LineDirection.ReverseDiagonal,
        Style = new() {
            BorderWidth = 5,
            Border = Color.Brown
        }
    };
}