using MinesServer.Networking.Client.Packets.Programmator;

namespace MinesServer.Networking.Tests.Client.Programmator;

internal class DeleteProgramClickPacketTest : RootClientPacketTest<DeleteProgramClickPacket>
{
    public override DeleteProgramClickPacket Packet => new(42);
}
