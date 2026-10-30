using MinesServer.Data;
using MinesServer.Networking.Client.Packets.Movement;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class RotatePacketTest : PacketTest<RotatePacket>
{
    public override RotatePacket Packet => new(Direction.Up);
}
