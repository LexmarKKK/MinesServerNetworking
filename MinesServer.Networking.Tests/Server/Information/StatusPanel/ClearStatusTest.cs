using MinesServer.Networking.Server.Packets.Information.StatusPanel;

namespace MinesServer.Networking.Tests.Server.Information.StatusPanel;

internal sealed class ClearStatusTest : RootServerPacketTest<ClearStatusPacket>
{
    public override ClearStatusPacket Packet => new();
}