using MinesServer.Networking.Server.Packets.GUI.Components.Containers;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;

namespace MinesServer.Networking.Tests.Server.GUI.Containers;

internal sealed class CanvasTest : PacketTest<CanvasPacket>
{
    public override CanvasPacket Packet => new()
    {
        Children = [
            new ImagePacket() {
                URI = "ftp://localhost:21/test.png",
                AttachedProperties = [
                    new("Canvas.X", "123"),
                    new("Canvas.Y", "456"),
                    new("Canvas.Width", "789"),
                    new("Canvas.Height", "012")
                ]
            }
        ]
    };
}