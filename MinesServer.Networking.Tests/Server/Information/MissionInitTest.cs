using MinesServer.Networking.Server.Packets.Mission;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class MissionInitTest : RootServerPacketTest<MissionInitPacket>
{
    public override MissionInitPacket Packet => new(
    "missions/intro.png",
    256,
    128,
    "Welcome!",
    "Complete the tutorial mission to learn the basics. Завершите обучение.");
}