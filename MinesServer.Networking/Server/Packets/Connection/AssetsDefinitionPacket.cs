using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Connection;

/// <summary>
/// Ресурс мира, передаваемый через RuntimeAssetPacket.
/// ETag определяет требуемое содержимое файла в манифесте мира, SizeBytes — его вес (для показа «сколько из скольки»).
/// </summary>
public readonly record struct AssetsDefinitionPacket(string Filename, string ETag, uint SizeBytes)
{
    private string SafeFilename => Filename ?? string.Empty;
    private string SafeETag => ETag ?? string.Empty;

    public int Size =>
        sizeof(byte) * 2 + SafeFilename.Length + SafeETag.Length + sizeof(uint);

    public int Encode(Span<byte> output)
    {
        string filename = SafeFilename;
        string etag = SafeETag;
        if (filename.Length > byte.MaxValue || etag.Length > byte.MaxValue)
        {
            throw new InvalidOperationException("Asset filename and ETag must each fit in a one-byte length prefix.");
        }

        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(filename);
        writer.WriteU1PrefixedASCII(etag);
        writer.Write(SizeBytes);
        return writer.Position;
    }

    public static AssetsDefinitionPacket Decode(ReadOnlySpan<byte> input, out int bytesRead)
    {
        var reader = input.Reader();
        string filename = reader.ReadU1PrefixedASCII(out _);
        string etag = reader.ReadU1PrefixedASCII(out _);
        var definition = new AssetsDefinitionPacket(filename, etag, reader.ReadU4());
        bytesRead = reader.Position;
        return definition;
    }
}
