using MinesServer.Data;
using MinesServer.Networking.Server.Packets.Programmator;

namespace MinesServer.Networking.Tests.Server.Programmator;

internal sealed class UpdateProgramTest : RootServerPacketTest<UpdateProgramPacket>
{
    public override UpdateProgramPacket Packet => new(123, "test program", [(ProgAction.BooleanAND, "", ""), (ProgAction.AddNumberToVar, "asd", "456")], [4, 6, 3, 23]);
}
