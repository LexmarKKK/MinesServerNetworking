using MinesServer.Data;
using System;
using MinesServer.Utils;

namespace MinesServer.Networking.Server.Packets.Connection;

public readonly record struct CellConfigurationPacket(
    CellConfigProperties Properties,
    CellRenderLayer DrawLayer,
    CellOutlineKind Outline,
    string TextureName,
    string DecalTextureName,
    byte ReliefGroup,
    CellAnimationType AnimationType,
    byte AnimationPalette,
    float FrameAnimationSpeed,
    int MapColor,
    CellTextureAnchorKind TextureAnchor = CellTextureAnchorKind.Cell) : IServerPacket<CellConfigurationPacket>
{
    private string SafeTextureName => TextureName ?? string.Empty;
    private string SafeDecalTextureName => DecalTextureName ?? string.Empty;

    public int Size => sizeof(byte) * 9 + sizeof(float) + sizeof(int) + SafeTextureName.Length + SafeDecalTextureName.Length;

    public int Encode(Span<byte> output)
    {
        string textureName = SafeTextureName;
        string decalTextureName = SafeDecalTextureName;
        if (textureName.Length > byte.MaxValue || decalTextureName.Length > byte.MaxValue)
        {
            throw new InvalidOperationException("Cell and decal texture names must fit in one-byte length prefixes.");
        }

        var writer = output.Writer();
        writer.Write((byte)Properties);
        writer.Write((byte)DrawLayer);
        writer.Write((byte)Outline);
        writer.WriteU1PrefixedASCII(textureName);
        writer.WriteU1PrefixedASCII(decalTextureName);
        writer.Write(ReliefGroup);
        writer.Write((byte)AnimationType);
        writer.Write(AnimationPalette);
        writer.Write(FrameAnimationSpeed);
        writer.Write(MapColor);
        writer.Write((byte)TextureAnchor);
        return writer.Position;
    }

    public static CellConfigurationPacket Decode(ReadOnlySpan<byte> input) => Decode(input, out _);

    public static CellConfigurationPacket Decode(ReadOnlySpan<byte> input, out int bytesRead)
    {
        var reader = input.Reader();
        var packet = new CellConfigurationPacket(
            Properties: (CellConfigProperties)reader.ReadU1(),
            DrawLayer: (CellRenderLayer)reader.ReadU1(),
            Outline: (CellOutlineKind)reader.ReadU1(),
            TextureName: reader.ReadU1PrefixedASCII(out _),
            DecalTextureName: reader.ReadU1PrefixedASCII(out _),
            ReliefGroup: reader.ReadU1(),
            AnimationType: (CellAnimationType)reader.ReadU1(),
            AnimationPalette: reader.ReadU1(),
            FrameAnimationSpeed: reader.Read<float>(),
            MapColor: reader.Read4(),
            TextureAnchor: (CellTextureAnchorKind)reader.ReadU1());
        bytesRead = reader.Position;
        return packet;
    }
}
