using MinesServer.Utils;
using System;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.Information;

public readonly record struct BasketPacket(uint Capacity, long[] Contents) : IRootServerPacket<BasketPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<BasketPacket>();

    public int Size =>
        sizeof(uint) + // Capacity
        sizeof(long) * Contents.Length; // Contents

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(Capacity);
        writer.WriteArray(Contents);
        return writer.Position;
    }

    public static BasketPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new BasketPacket(reader.ReadU4(), reader.ReadRemainingAsArray<long>());
    }

    public bool Equals(BasketPacket other) =>
        Capacity == other.Capacity &&
        Contents.SequenceEqual(other.Contents);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(Capacity);
        foreach (var item in Contents)
            hash.Add(item);
        return hash.ToHashCode();
    }

}
