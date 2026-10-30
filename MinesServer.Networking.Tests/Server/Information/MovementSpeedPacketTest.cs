using MinesServer.Data;
using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class MovementSpeedPacketTest : PacketTest<MovementSpeedPacket>
{
    public override MovementSpeedPacket Packet => new(new Dictionary<CellType, ushort>
    {
        { CellType.Empty, 100 },
        { CellType.Road, 50 }
    });
}
