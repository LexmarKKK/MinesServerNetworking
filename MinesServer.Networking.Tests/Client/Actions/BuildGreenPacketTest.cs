using MinesServer.Networking.Client.Packets.Actions;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class BuildGreenPacketTest : PacketTest<BuildGreenPacket>
{
    public override BuildGreenPacket Packet => new();
}
