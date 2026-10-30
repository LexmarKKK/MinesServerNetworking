using MinesServer.Data;
using MinesServer.Networking.Server.Packets.Inventory;

namespace MinesServer.Networking.Tests.Server.Inventory;

internal sealed class InventoryTest : RootServerPacketTest<InventoryPacket>
{
    public override InventoryPacket Packet => new(new Dictionary<ItemType, long>
    {
        { ItemType.Nano, 100 },
        { ItemType.Battery, 250 },
        { ItemType.Poly, 1000 }
    });
}
