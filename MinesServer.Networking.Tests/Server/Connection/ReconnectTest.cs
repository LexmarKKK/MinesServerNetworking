using MinesServer.Networking.Server.Packets.Connection;

namespace MinesServer.Networking.Tests.Server.Connection;

internal sealed class ReconnectTest : RootServerPacketTest<ReconnectPacket>
{
    public override ReconnectPacket Packet => new("Server is restarting. Please reconnect.");
}
