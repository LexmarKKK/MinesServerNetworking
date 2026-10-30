using MinesServer.Networking.Client.Packets.Actions;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class BzPacketTest : PacketTest<BzPacket>
{
    public override BzPacket Packet => new();
}
