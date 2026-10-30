using MinesServer.Networking.Client.Packets.GUI;

namespace MinesServer.Networking.Tests.Client.GUI;

internal sealed class OpenClanClickTest : RootClientPacketTest<OpenClanClickPacket>
{
    public override OpenClanClickPacket Packet => new();
}

