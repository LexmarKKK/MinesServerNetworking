using MinesServer.Networking.Server.Packets.Connection;

namespace MinesServer.Networking.Tests.Server.Connection;

internal sealed class OutdatedClientTest : RootServerPacketTest<OutdatedClientPacket>
{
    public override OutdatedClientPacket Packet => new(
    101,
    "Версия 1.01",
    "Please update your client to continue. Требуется обновление.",
    "http://example.com/v101.exe",
    "--silent");
}