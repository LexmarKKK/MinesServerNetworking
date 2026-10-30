using MinesServer.Networking.Client.Packets.Programmator;

namespace MinesServer.Networking.Tests.Client.Utilities;

internal sealed class QueryProgramMemoryPacketTest : RootClientPacketTest<QueryProgramMemoryPacket>
{
    public override QueryProgramMemoryPacket Packet => new(["testVar", "anotherVar"], 0, 100);
}
