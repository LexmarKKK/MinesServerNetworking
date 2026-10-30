using MinesServer.Data;
using MinesServer.Networking.Client.Packets.Programmator;

namespace MinesServer.Networking.Tests.Client.Programmator;

internal sealed class SaveProgramPacketTest : RootClientPacketTest<SaveProgramPacket>
{
    public override SaveProgramPacket Packet => new(42, true, [(ProgAction.BooleanAND, "x", "y")], [3, 5, 1, 6, 43]);
}
