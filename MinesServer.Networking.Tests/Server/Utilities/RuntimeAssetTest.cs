using MinesServer.Networking.Server.Packets.Utilities;

namespace MinesServer.Networking.Tests.Server.Utilities;

internal sealed class RuntimeAssetTest : RootServerPacketTest<RuntimeAssetPacket>
{
    public override RuntimeAssetPacket Packet => new(
        "assets/logo.png",
        "v1.2.3-abc",
        [0xDE, 0xAD, 0xBE, 0xEF]);
}