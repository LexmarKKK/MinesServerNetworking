#nullable enable

using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Connection;

/// <summary>
/// Одна общая текстура декали и клетки, на которых она рисуется.
/// Декаль задаётся картой мира, а не полем каждой конфигурации клетки:
/// так одна текстура не дублируется в пакете для каждого типа.
/// </summary>
public readonly struct CellDecalEntry : IEquatable<CellDecalEntry>
{
    public CellDecalEntry(string textureName, byte[] cells)
    {
        TextureName = textureName;
        Cells = cells;
    }

    public string TextureName { get; }

    public byte[] Cells { get; }

    private string SafeTextureName => TextureName ?? string.Empty;

    private byte[] SafeCells => Cells ?? Array.Empty<byte>();

    public int Size => sizeof(byte) + SafeTextureName.Length + sizeof(ushort) + SafeCells.Length;

    public int Encode(Span<byte> output)
    {
        string textureName = SafeTextureName;
        byte[] cells = SafeCells;
        if (textureName.Length > byte.MaxValue)
        {
            throw new InvalidOperationException("Decal texture name must fit in a one-byte length prefix.");
        }

        if (cells.Length > ushort.MaxValue)
        {
            throw new InvalidOperationException("Decal texture cell list must fit in a two-byte length prefix.");
        }

        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(textureName);
        writer.Write((ushort)cells.Length);
        foreach (byte cell in cells)
        {
            writer.Write(cell);
        }

        return writer.Position;
    }

    public static CellDecalEntry Decode(ReadOnlySpan<byte> input, out int bytesRead)
    {
        var reader = input.Reader();
        string textureName = reader.ReadU1PrefixedASCII(out _);
        byte[] cells = new byte[reader.ReadU2()];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = reader.ReadU1();
        }

        bytesRead = reader.Position;
        return new CellDecalEntry(textureName, cells);
    }

    public bool Equals(CellDecalEntry other) =>
        string.Equals(SafeTextureName, other.SafeTextureName, StringComparison.Ordinal) &&
        SafeCells.AsSpan().SequenceEqual(other.SafeCells);

    public override bool Equals(object? obj) =>
        obj is CellDecalEntry other && Equals(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(SafeTextureName, StringComparer.Ordinal);
        foreach (byte cell in SafeCells)
        {
            hash.Add(cell);
        }

        return hash.ToHashCode();
    }
}
