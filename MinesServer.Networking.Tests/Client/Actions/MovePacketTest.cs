using MinesServer.Networking.Client.Packets.Movement;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class MovePacketTest : PacketTest<MovePacket>
{
    public override MovePacket Packet => new(100, 200);
}
