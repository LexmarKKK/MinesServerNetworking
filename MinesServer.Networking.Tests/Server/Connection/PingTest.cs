using MinesServer.Networking.Server.Packets.Connection;

namespace MinesServer.Networking.Tests.Server.Connection;

internal sealed class PingTest : RootServerPacketTest<PingPacket>
{
    public override PingPacket Packet => new(0x1122334455667788, 123);
}
