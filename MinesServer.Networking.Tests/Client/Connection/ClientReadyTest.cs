using MinesServer.Networking.Client.Packets.Connection;

namespace MinesServer.Networking.Tests.Client.Connection;

internal sealed class ClientReadyTest : RootClientPacketTest<ClientReadyPacket>
{
    public override ClientReadyPacket Packet => new();
}
