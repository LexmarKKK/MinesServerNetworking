using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class AutoMineStateTest : RootServerPacketTest<AutoMineStatePacket>
{
    public override AutoMineStatePacket Packet => new(false);
}