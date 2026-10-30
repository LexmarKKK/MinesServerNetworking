using MinesServer.Networking.Client.Packets.Inventory;

namespace MinesServer.Networking.Tests.Client.Inventory;

internal sealed class UseItemPacketTest : RootClientPacketTest<UseItemPacket>
{
    public override UseItemPacket Packet => new();
}
