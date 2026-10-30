using MinesServer.Utils;
using System;
using System.Collections.Generic;

namespace MinesServer.Networking.Contracts.Packets;

public readonly record struct StringPairPacket(string Key, string Value) : INetworkPacket<StringPairPacket>, IKeyValuePairPacket<string, string>
{
    public int Size =>
        sizeof(byte) // Key.Length
        + Key.Length // Key
        + sizeof(ushort) // Value.Length
        + Value.Length * 2; // Value

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(Key);
        writer.WriteU2PrefixedUtf16(Value);
        return writer.Position;
    }

    public static StringPairPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.ReadU1PrefixedASCII(out _), reader.ReadU2PrefixedUtf16(out _));
    }

    public static implicit operator StringPairPacket(KeyValuePair<string, string> pair)
        => new(pair.Key, pair.Value);

    public static implicit operator KeyValuePair<string, string>(StringPairPacket packet)
        => new(packet.Key, packet.Value);
}
