using MinesServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesServer.Networking.Client.Packets.Programmator;

public readonly record struct QueryProgramMemoryPacket(IReadOnlyList<string> Variables, ushort ArrayStart, ushort ArrayStop) : IRootClientPacket<QueryProgramMemoryPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<QueryProgramMemoryPacket>();

    public int Size =>
        sizeof(ushort) + // ArrayStart
        sizeof(ushort) + // ArrayStop
        Variables.Sum(x => sizeof(byte) + x.Length * 2); // Variables

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(ArrayStart);
        writer.Write(ArrayStop);
        foreach (var variable in Variables)
            writer.WriteU1PrefixedUtf16(variable);
        return writer.Position;
    }

    public static QueryProgramMemoryPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var start = reader.ReadU2();
        var stop = reader.ReadU2();
        var variables = new List<string>();
        while (reader.CanRead)
            variables.Add(reader.ReadU1PrefixedUtf16(out _));
        return new(variables, start, stop);
    }

    public bool Equals(QueryProgramMemoryPacket other) =>
        ArrayStart == other.ArrayStart &&
        ArrayStop == other.ArrayStop &&
        Variables.SequenceEqual(other.Variables);

    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(ArrayStart);
        hash.Add(ArrayStop);
        foreach (var variable in Variables)
            hash.Add(variable);
        return hash.ToHashCode();
    }

}
