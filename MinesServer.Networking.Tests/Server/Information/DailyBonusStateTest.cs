using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class DailyBonusStateTest : RootServerPacketTest<DailyBonusStatePacket>
{
    public override DailyBonusStatePacket Packet => new(true);
}