using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class HealthTest : RootServerPacketTest<HealthPacket>
{
    public override HealthPacket Packet => new(95, 120);
}
