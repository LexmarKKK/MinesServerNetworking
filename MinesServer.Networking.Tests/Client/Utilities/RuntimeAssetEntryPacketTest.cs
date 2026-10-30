using MinesServer.Networking.Client.Packets.Utilities;

namespace MinesServer.Networking.Tests.Client.Utilities;

internal sealed class RuntimeAssetEntryPacketTest : PacketTest<RuntimeAssetEntryPacket>
{
    public override RuntimeAssetEntryPacket Packet => new("test.txt", "abc123");
}
