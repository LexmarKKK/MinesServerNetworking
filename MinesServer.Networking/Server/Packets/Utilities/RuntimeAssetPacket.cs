using MinesServer.Utils;
using System;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.Utilities;

public readonly record struct RuntimeAssetPacket(string Filename, string ETag, byte[] Contents) : IRootServerPacket<RuntimeAssetPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<RuntimeAssetPacket>();

    public int Size =>
        sizeof(byte) + // Filename.Length
        Filename.Length + // Filename
        sizeof(byte) + // ETag.Length
        ETag.Length + // ETag
        sizeof(int) + // Contents.Length
        Contents.Length; // Contents

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(Filename);
        writer.WriteU1PrefixedASCII(ETag);
        writer.WritePrefixedArray(Contents);
        return writer.Position;
    }

    public static RuntimeAssetPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(
            reader.ReadU1PrefixedASCII(out _),
            reader.ReadU1PrefixedASCII(out _),
            reader.ReadPrefixedArray<byte>(out _));
    }

    public bool Equals(RuntimeAssetPacket other) =>
        Filename == other.Filename &&
        ETag == other.ETag &&
        Contents.SequenceEqual(other.Contents);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(Filename);
        hash.Add(ETag);
        foreach (var value in Contents)
            hash.Add(value);
        return hash.ToHashCode();
    }

}
