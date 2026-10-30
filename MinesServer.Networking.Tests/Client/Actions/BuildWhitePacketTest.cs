using MinesServer.Networking.Client.Packets.Actions;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class BuildWhitePacketTest : PacketTest<BuildWhitePacket>
{
    public override BuildWhitePacket Packet => new();
}
