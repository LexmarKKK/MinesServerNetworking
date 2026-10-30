using MinesServer.Networking.Server.Packets.GUI.Components.Visual;

namespace MinesServer.Networking.Tests.Server.GUI.Visual;

internal sealed class TextTest : PacketTest<TextPacket>
{
    public override TextPacket Packet => new() { Text = "asdsa фывыф" };
}
