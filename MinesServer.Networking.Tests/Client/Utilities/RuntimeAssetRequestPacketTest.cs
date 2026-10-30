using MinesServer.Networking.Client.Packets.Utilities;

namespace MinesServer.Networking.Tests.Client.Utilities;

internal sealed class RuntimeAssetRequestPacketTest : RootClientPacketTest<RuntimeAssetRequestPacket>
{
    public override RuntimeAssetRequestPacket Packet => new([new RuntimeAssetEntryPacket("test.txt", "abc123")]);
}
