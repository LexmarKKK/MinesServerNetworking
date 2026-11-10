#nullable enable
using MinesServer.Utils;
using System;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.Connection;

public readonly record struct WorldInitPacket(string CodeName, string DisplayName, ushort Width, ushort Height, CellConfigurationPacket[] Cells, byte[][] TileGroups, AssetsDefinitionPacket[]? AssetsManifest = null, CellDecalEntry[]? DecalMap = null) : IRootServerPacket<WorldInitPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<WorldInitPacket>();

    public int Size =>
        sizeof(byte) + // CodeName.Length
        CodeName.Length + // CodeName
        sizeof(byte) + // DisplayName.Length
        DisplayName.Length * 2 + // DisplayName
        sizeof(ushort) + // Width
        sizeof(ushort) + // Height
        sizeof(byte) + // TileGroups.Length
        sizeof(byte) * TileGroups.Length + // TileGroups[].Length
        TileGroups.Sum(x => x.Length) + // TileGroups[]
        sizeof(ushort) + // AssetsManifest.Length
        (AssetsManifest?.Sum(x => x.Size) ?? 0) + // AssetsManifest
        sizeof(ushort) + // Cells.Length
        Cells.Sum(x => x.Size) + // Cells
        sizeof(ushort) + // DecalMap.Length
        (DecalMap?.Sum(x => x.Size) ?? 0); // DecalMap

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        AssetsDefinitionPacket[] assets = AssetsManifest ?? Array.Empty<AssetsDefinitionPacket>();
        CellDecalEntry[] decals = DecalMap ?? Array.Empty<CellDecalEntry>();
        if (assets.Length > ushort.MaxValue || Cells.Length > ushort.MaxValue || decals.Length > ushort.MaxValue)
        {
            throw new InvalidOperationException("WorldInitPacket contains too many world asset definitions, cell configurations or decal entries.");
        }

        writer.WriteU1PrefixedASCII(CodeName);
        writer.WriteU1PrefixedUtf16(DisplayName);
        writer.Write(Width);
        writer.Write(Height);
        writer.Write((byte)TileGroups.Length);
        foreach (var group in TileGroups)
            writer.WriteU1PrefixedArray(group);
        writer.Write((ushort)assets.Length);
        foreach (AssetsDefinitionPacket asset in assets)
            writer.Advance(asset.Encode(output[writer.Position..]));
        writer.Write((ushort)Cells.Length);
        foreach (CellConfigurationPacket cell in Cells)
            writer.Advance(cell.Encode(output[writer.Position..]));
        writer.Write((ushort)decals.Length);
        foreach (CellDecalEntry decal in decals)
            writer.Advance(decal.Encode(output[writer.Position..]));
        return writer.Position;
    }

    public static WorldInitPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var codeName = reader.ReadU1PrefixedASCII(out _);
        var name = reader.ReadU1PrefixedUtf16(out _);
        var width = reader.ReadU2();
        var height = reader.ReadU2();
        var tileGroups = new byte[reader.ReadU1()][];
        for (int i = 0; i < tileGroups.Length; i++)
            tileGroups[i] = reader.ReadU1PrefixedArray<byte>(out _);
        var assets = new AssetsDefinitionPacket[reader.ReadU2()];
        for (int i = 0; i < assets.Length; i++)
        {
            assets[i] = AssetsDefinitionPacket.Decode(reader.Remaining, out int bytesRead);
            reader.Advance(bytesRead);
        }
        var cellCount = reader.ReadU2();
        var renderConfig = new CellConfigurationPacket[cellCount];
        for (int i = 0; i < renderConfig.Length; i++)
        {
            renderConfig[i] = CellConfigurationPacket.Decode(reader.Remaining, out int bytesRead);
            reader.Advance(bytesRead);
        }
        var decals = new CellDecalEntry[reader.ReadU2()];
        for (int i = 0; i < decals.Length; i++)
        {
            decals[i] = CellDecalEntry.Decode(reader.Remaining, out int bytesRead);
            reader.Advance(bytesRead);
        }
        return new WorldInitPacket(
            codeName,
            name,
            width,
            height,
            renderConfig,
            tileGroups,
            assets,
            decals);
    }

    public bool Equals(WorldInitPacket other) =>
        CodeName == other.CodeName &&
        DisplayName == other.DisplayName &&
        Width == other.Width &&
        Height == other.Height &&
        Cells.SequenceEqual(other.Cells) &&
        (AssetsManifest ?? Array.Empty<AssetsDefinitionPacket>()).SequenceEqual(other.AssetsManifest ?? Array.Empty<AssetsDefinitionPacket>()) &&
        (DecalMap ?? Array.Empty<CellDecalEntry>()).SequenceEqual(other.DecalMap ?? Array.Empty<CellDecalEntry>()) &&
        TileGroups.Length == other.TileGroups.Length &&
        TileGroups.SelectMany(x => x).SequenceEqual(other.TileGroups.SelectMany(x => x));
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(CodeName);
        hash.Add(DisplayName);
        hash.Add(Width);
        hash.Add(Height);
        foreach (var cell in Cells)
            hash.Add(cell);
        foreach (var tile in TileGroups.SelectMany(group => group))
            hash.Add(tile);
        foreach (var asset in AssetsManifest ?? Array.Empty<AssetsDefinitionPacket>())
            hash.Add(asset);
        foreach (CellDecalEntry decal in DecalMap ?? Array.Empty<CellDecalEntry>())
            hash.Add(decal);
        return hash.ToHashCode();
    }

}
