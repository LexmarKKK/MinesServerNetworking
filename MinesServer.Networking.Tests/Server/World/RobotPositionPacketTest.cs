using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal sealed class RobotPositionPacketTest : PacketTest<RobotPositionPacket>
{
    public override RobotPositionPacket Packet => new(202, 1000, 2000, 3);
}