using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal sealed class VisualPacketTest : PacketTest<VisualPacket>
{
    public override VisualPacket Packet => new(
        Data.VisualEffect.C190,
        572,
        19,
        57,
        [
            new("clr", "F00"),
            new("radius", "15")
        ]);
}