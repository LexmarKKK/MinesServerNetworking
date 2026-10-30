using MinesServer.Networking.Client.Packets.Actions;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class BuildCyanPacketTest : PacketTest<BuildCyanPacket>
{
    public override BuildCyanPacket Packet => new();
}
