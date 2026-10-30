using MinesServer.Networking.Server.Packets.GUI;

namespace MinesServer.Networking.Tests.Server.GUI;

internal sealed class CloseWindowTest : RootServerPacketTest<CloseWindowPacket>
{
    public override CloseWindowPacket Packet => new();
}
