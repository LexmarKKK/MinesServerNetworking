using MinesServer.Data;
using MinesServer.Networking.Server.Packets.Information;

namespace MinesServer.Networking.Tests.Server.Information;

internal sealed class SkillProgressTest : RootServerPacketTest<SkillProgressPacket>
{
    public override SkillProgressPacket Packet => new(SkillType.Jewelry, 12345, 67890);
}
