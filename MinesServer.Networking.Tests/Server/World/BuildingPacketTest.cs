using MinesServer.Data;
using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal class BuildingPacketTest : PacketTest<BuildingPacket>
{
    public override BuildingPacket Packet => new(123, 456, BuildingType.Clans, 2, 78);
}