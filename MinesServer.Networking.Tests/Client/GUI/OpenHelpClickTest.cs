using MinesServer.Networking.Client.Packets.GUI;

namespace MinesServer.Networking.Tests.Client.GUI;

internal sealed class OpenHelpClickTest : RootClientPacketTest<OpenHelpClickPacket>
{
    public override OpenHelpClickPacket Packet => new();
}

