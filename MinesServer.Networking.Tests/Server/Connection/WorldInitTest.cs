using MinesServer.Data;
using MinesServer.Networking.Server.Packets.Connection;

namespace MinesServer.Networking.Tests.Server.Connection;

internal sealed class WorldInitTest : RootServerPacketTest<WorldInitPacket>
{
    public override WorldInitPacket Packet => new("test", "Тестовый мир", 10, 10,
        [
            new(
                Properties: CellConfigProperties.Passable,
                DrawLayer: CellRenderLayer.Background,
                Outline: CellOutlineKind.Pliant,
                ReliefGroup: 0,
                AnimationType: CellAnimationType.None,
                AnimationPalette: 0,
                FrameAnimationSpeed: 6f,
                MapColor: 0),
            new(
                Properties: CellConfigProperties.None,
                DrawLayer: CellRenderLayer.Foreground,
                Outline: CellOutlineKind.Round,
                ReliefGroup: 1,
                AnimationType: CellAnimationType.Prismatic,
                AnimationPalette: 1,
                FrameAnimationSpeed: 0f,
                MapColor: unchecked((int)0xFF00FF00)),
        ],
        [
            [1, 2, 3],
            [4, 5, 6]
        ],
        [new AssetsDefinitionPacket("Visual/Cells/Rock.png", "0123456789abcdef0123456789abcdef", 4096)],
        [new CellDecalEntry("Visual/World/Terrain/Decals/terrain-decals-rock.png", [1])]);
}
