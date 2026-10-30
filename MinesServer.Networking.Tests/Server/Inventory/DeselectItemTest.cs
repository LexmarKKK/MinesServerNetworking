using MinesServer.Networking.Server.Packets.Inventory;

namespace MinesServer.Networking.Tests.Server.Inventory;

internal sealed class DeselectItemTest : RootServerPacketTest<DeselectItemPacket>
{
    public override DeselectItemPacket Packet => new();
}
