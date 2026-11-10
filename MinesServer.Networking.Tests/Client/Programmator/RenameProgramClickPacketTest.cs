using MinesServer.Networking.Client.Packets.Programmator;

namespace MinesServer.Networking.Tests.Client.Programmator;

internal sealed class RenameProgramClickPacketTest : RootClientPacketTest<RenameProgramClickPacket>
{
    public override RenameProgramClickPacket Packet => new();
}
