using MinesServer.Networking.Client.Packets.Programmator;

namespace MinesServer.Networking.Tests.Client.Programmator;

internal sealed class StopProgramPacketTest : RootClientPacketTest<StopProgramPacket>
{
    public override StopProgramPacket Packet => new();
}
