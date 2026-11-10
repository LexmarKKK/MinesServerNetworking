using MinesServer.Networking.Client.Packets.Connection;

namespace MinesServer.Networking.Tests.Client.Connection;

internal sealed class PongTest : RootClientPacketTest<PongPacket>
{
    public override PongPacket Packet => new(0x1122334455667788);
}
