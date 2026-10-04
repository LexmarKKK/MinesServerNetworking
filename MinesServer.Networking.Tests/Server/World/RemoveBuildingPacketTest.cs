using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal class RemoveBuildingPacketTest : PacketTest<RemoveBuildingPacket>
{
    public override RemoveBuildingPacket Packet => new(789, 987);
}