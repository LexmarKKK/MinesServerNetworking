using MinesServer.Data;
using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal sealed class MapRegionTest : PacketTest<MapRegionPacket>
{
    public override MapRegionPacket Packet => new(100, 200, 1, 1, [CellType.Rock, CellType.RustySand, CellType.GrayAcid, CellType.Cyan]);
}