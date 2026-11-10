using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal sealed class RobotInfoPacketTest : RootServerPacketTest<RobotInfoPacket>
{
    public override RobotInfoPacket Packet => new(
    101,
    3,
    5,
    "fatty",
    "rainbow",
    "Darkar25");
}