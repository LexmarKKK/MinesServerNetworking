using MinesServer.Data;
using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class GeologyTest : RootServerPacketTest<GeologyPacket>
{
    public override GeologyPacket Packet => new(50, 100, CellType.HypnoRock, "Гипноскал");
}
