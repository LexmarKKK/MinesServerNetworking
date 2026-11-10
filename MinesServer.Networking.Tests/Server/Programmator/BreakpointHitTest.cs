using MinesServer.Networking.Server.Packets.Programmator;

namespace MinesServer.Networking.Tests.Server.Programmator;

internal sealed class BreakpointHitTest : RootServerPacketTest<BreakpointHitPacket>
{
    public override BreakpointHitPacket Packet => new([1, 5, 10, 25]);
}
