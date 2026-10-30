using MinesServer.Networking.Server.Packets.Mission;

namespace MinesServer.Networking.Tests.Server.Mission;

internal sealed class MissionArrowTest : RootServerPacketTest<MissionArrowPacket>
{
    public override MissionArrowPacket Packet => new(1234, 5678);
}
