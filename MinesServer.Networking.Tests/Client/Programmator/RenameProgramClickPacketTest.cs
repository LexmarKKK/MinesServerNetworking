using MinesServer.Networking.Client.Packets.Programmator;

namespace MinesServer.Networking.Tests.Client.Programmator;

internal class RenameProgramClickPacketTest : RootClientPacketTest<RenameProgramClickPacket>
{
    public override RenameProgramClickPacket Packet => new(42);
}
