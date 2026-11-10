using MinesServer.Networking.Client.Packets.Actions;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class ClickCellPacketTest : PacketTest<ClickCellPacket>
{
    public override ClickCellPacket Packet => new(100, 200);
}
