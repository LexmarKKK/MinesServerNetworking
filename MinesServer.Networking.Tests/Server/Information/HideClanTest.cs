using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class HideClanTest : RootServerPacketTest<HideClanPacket>
{
    public override HideClanPacket Packet => new();
}
