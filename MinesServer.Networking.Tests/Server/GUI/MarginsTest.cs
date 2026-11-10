using MinesServer.Networking.Server.Packets.GUI;

namespace MinesServer.Networking.Tests.Server.GUI;

internal sealed class MarginsTest : PacketTest<Margins>
{
    public override Margins Packet => new(1, 2, 3, 4);
}
