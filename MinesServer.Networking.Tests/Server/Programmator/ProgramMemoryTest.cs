using MinesServer.Networking.Server.Packets.Programmator;

namespace MinesServer.Networking.Tests.Server.Programmator;

internal sealed class ProgramMemoryTest : RootServerPacketTest<ProgramMemoryPacket>
{
    public override ProgramMemoryPacket Packet => new([1, 2, 3, 4, 5], [6, 7, 8, 9]);
}
