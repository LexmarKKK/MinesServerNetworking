using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class OnlineTest : RootServerPacketTest<OnlinePacket>
{
    public override OnlinePacket Packet => new(123, 456);
}
