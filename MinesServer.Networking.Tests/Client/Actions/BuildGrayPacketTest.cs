using MinesServer.Networking.Client.Packets.Actions;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class BuildGrayPacketTest : PacketTest<BuildGrayPacket>
{
    public override BuildGrayPacket Packet => new();
}
