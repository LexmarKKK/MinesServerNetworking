using MinesServer.Networking.Server.Packets.GUI.Components.Visual;

namespace MinesServer.Networking.Tests.Server.GUI.Visual;

internal sealed class ImageTest : PacketTest<ImagePacket>
{
    public override ImagePacket Packet => new() {
        URI = "http://localhost/test.png",
        Width = 12,
        Height = 34
    };
}
