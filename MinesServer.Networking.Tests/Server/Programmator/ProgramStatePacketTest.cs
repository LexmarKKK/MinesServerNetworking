using MinesServer.Data;
using MinesServer.Networking.Server.Packets.Programmator;

namespace MinesServer.Networking.Tests.Server.Programmator;

internal class ProgramStatePacketTest : RootServerPacketTest<ProgramStatePacket>
{
    public override ProgramStatePacket Packet => new(ProgramState.Running, true);
}
