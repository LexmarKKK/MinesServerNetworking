using MinesServer.Utils;
using System;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.Programmator;

public readonly record struct ProgramMemoryPacket(int[] RequestedVariables, int[] RequestedArraySlice) : IRootServerPacket<ProgramMemoryPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<ProgramMemoryPacket>();

    public int Size =>
        sizeof(byte) + // RequestedVariables.Length
        RequestedVariables.Length * sizeof(int) + // RequestedVariables
        RequestedArraySlice.Length * sizeof(int); // RequestedArraySlice

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedArray(RequestedVariables);
        writer.WriteArray(RequestedArraySlice);
        return writer.Position;
    }

    public static ProgramMemoryPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.ReadU1PrefixedArray<int>(out _), reader.ReadRemainingAsArray<int>());
    }

    public bool Equals(ProgramMemoryPacket other) =>
        RequestedVariables.SequenceEqual(other.RequestedVariables) &&
        RequestedArraySlice.SequenceEqual(other.RequestedArraySlice);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        foreach (var variable in RequestedVariables)
            hash.Add(variable);
        foreach (var value in RequestedArraySlice)
            hash.Add(value);
        return hash.ToHashCode();
    }

}
