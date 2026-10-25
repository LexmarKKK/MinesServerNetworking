using MinesServer.Networking.Server.Packets.Movement;

namespace MinesServer.Networking.Tests.Server.Movement;

internal class TeleportTest : RootServerPacketTest<TeleportPacket>
{
    public override TeleportPacket Packet => new(100, 200);
}
