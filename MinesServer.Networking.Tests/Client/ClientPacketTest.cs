using MinesServer.Networking.Client.Packets;
using MinesServer.Networking.Client.Packets.Actions;
using MinesServer.Networking.Client.Packets.Movement;

namespace MinesServer.Networking.Tests.Client;

internal sealed class ClientPacketTest : PacketTest<ClientPacket>
{
    public override ClientPacket Packet => new(12345, new ActionClientPacket(101, 200, new MovePacket(100, 200)));
}
