using MinesServer.Networking.Server.Packets.GUI.Components.Containers;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;

namespace MinesServer.Networking.Tests.Server.GUI.Containers;

internal sealed class GridTest : PacketTest<GridPacket>
{
    public override GridPacket Packet => new() {
        Columns = [1, 2, 3],
        Rows = [4, 5, 6],
        Children = [
            new TextPacket() {
                Text = "asdsa фывыф",
                AttachedProperties = [
                    new("Grid.ColumnSpan", "2"),
                    new("Grid.Row", "1"),
                    new("Grid.Column", "2")
                ]
            }
        ]
    };
}