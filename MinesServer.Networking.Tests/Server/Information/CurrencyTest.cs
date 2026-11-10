using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class CurrencyTest : RootServerPacketTest<CurrencyPacket>
{
    public override CurrencyPacket Packet => new(123456789, 98765);
}
