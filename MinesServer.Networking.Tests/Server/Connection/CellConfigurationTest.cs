using MinesServer.Data;
using MinesServer.Networking.Server.Packets.Connection;

namespace MinesServer.Networking.Tests.Server.Connection;

internal sealed class CellConfigurationTest : PacketTest<CellConfigurationPacket>
{
    public override CellConfigurationPacket Packet => new(
        Passable: true,
        DrawLayer: CellRenderLayer.Foreground,
        Outline: CellOutlineKind.Wavy,
        TextureName: "Cells/Rock.png",
        DecalTextureName: "terrain-decals-rock.png",
        ReliefGroup: 2,
        AnimationType: CellAnimationType.Prismatic,
        AnimationPalette: 2,
        FrameAnimationSpeed: 6f,
        MapColor: unchecked((int)0xFF112233));

    [Test]
    public void WireLayoutMatchesProtocolContract()
    {
        var packet = Packet;
        Assert.Multiple(() =>
        {
            Assert.That(packet.Size, Is.EqualTo(16 + "Cells/Rock.png".Length + "terrain-decals-rock.png".Length));
            var bytes = new byte[packet.Size];
            Assert.That(packet.Encode(bytes), Is.EqualTo(packet.Size));
            Assert.That(CellConfigurationPacket.Decode(bytes), Is.EqualTo(packet));
        });
    }
}
