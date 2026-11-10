using MinesServer.Networking.Client.Packets.GUI;

namespace MinesServer.Networking.Tests.Client.GUI;

internal sealed class OpenBuildingsClickTest : RootClientPacketTest<OpenBuildingsClickPacket>
{
    public override OpenBuildingsClickPacket Packet => new();
}
