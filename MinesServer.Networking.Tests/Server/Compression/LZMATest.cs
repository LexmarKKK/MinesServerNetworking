using MinesServer.Networking.Server.Packets.Compression;
using MinesServer.Networking.Server.Packets.GUI;
using MinesServer.Networking.Server.Packets.GUI.Components.Containers;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;

namespace MinesServer.Networking.Tests.Server.Compression;

internal sealed class LZMATest : RootServerPacketTest<LzmaPacket>
{
    public override LzmaPacket Packet => new(new OpenWindowPacket("TestWindow", 50, 50, new DockPanelPacket()
    {
        Children = [new TextPacket() { Text = "asdsasd" }]
    }));
}
