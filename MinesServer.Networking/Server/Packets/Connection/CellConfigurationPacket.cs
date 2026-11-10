using MinesServer.Data;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MinesServer.Utils;

namespace MinesServer.Networking.Server.Packets.Connection;

/// <summary>
/// Описание клетки фиксированного размера: только значения, без строк.
/// Имя текстуры выводится из типа клетки (<c>Visual/Cells/&lt;тип&gt;.png</c>),
/// а декали едут отдельной картой в <see cref="WorldInitPacket"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct CellConfigurationPacket(
    CellConfigProperties Properties,
    CellRenderLayer DrawLayer,
    CellOutlineKind Outline,
    byte ReliefGroup,
    CellAnimationType AnimationType,
    byte AnimationPalette,
    float FrameAnimationSpeed,
    int MapColor,
    CellTextureAnchorKind TextureAnchor = CellTextureAnchorKind.Cell) : IServerPacket<CellConfigurationPacket>
{
    public readonly int Size => Unsafe.SizeOf<CellConfigurationPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static CellConfigurationPacket Decode(ReadOnlySpan<byte> input) =>
        input.UnsafeRead<CellConfigurationPacket>();

    public static CellConfigurationPacket Decode(ReadOnlySpan<byte> input, out int bytesRead)
    {
        bytesRead = Unsafe.SizeOf<CellConfigurationPacket>();
        return input.UnsafeRead<CellConfigurationPacket>();
    }
}
