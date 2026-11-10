using MinesServer.Networking.Server.Packets.Utilities;

namespace MinesServer.Networking.Tests.Server.Utilities;

internal sealed class OpenURLTest : RootServerPacketTest<OpenURLPacket>
{
    public override OpenURLPacket Packet => new("https://www.example.com");
}
