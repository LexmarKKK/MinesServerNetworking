using MinesServer.Networking.Contracts.Packets;

namespace MinesServer.Networking.Tests.Shared;

internal sealed class StringPairTest : PacketTest<StringPairPacket>
{
    public override StringPairPacket Packet => new("test_key", "Test value with some unicode chars: привет");
}
