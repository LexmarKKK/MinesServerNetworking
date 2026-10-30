using MinesServer.Networking.Client.Packets.Programmator;

namespace MinesServer.Networking.Tests.Client.Programmator;

internal sealed class ProgramStepInPacketTest : RootClientPacketTest<ProgramStepInPacket>
{
    public override ProgramStepInPacket Packet => new();
}
