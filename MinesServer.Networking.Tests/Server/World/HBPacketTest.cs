using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal sealed class HBPacketTest : RootServerPacketTest<HBPacket>
{
    public override HBPacket Packet => new([]);
}
