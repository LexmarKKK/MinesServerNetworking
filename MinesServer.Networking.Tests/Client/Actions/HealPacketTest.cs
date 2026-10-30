using MinesServer.Networking.Client.Packets.Actions;

namespace MinesServer.Networking.Tests.Client.Actions;

internal sealed class HealPacketTest : PacketTest<HealPacket>
{
    public override HealPacket Packet => new();
}
