using MinesServer.Networking.Client.Packets.GUI;

namespace MinesServer.Networking.Tests.Client.GUI;

internal sealed class OpenSettingsClickTest : RootClientPacketTest<OpenSettingsClickPacket>
{
    public override OpenSettingsClickPacket Packet => new();
}

