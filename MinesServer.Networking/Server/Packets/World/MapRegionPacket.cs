using MinesServer.Data;
using MinesServer.Utils;
using System;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.World;

public readonly record struct MapRegionPacket(ushort X, ushort Y, byte Width, byte Height, CellType[] Payload) : IHBPacket<MapRegionPacket>
{
    public byte PacketCode => HBPacketCodeProvider.GetPacketCode<MapRegionPacket>();

    public int Size =>
        sizeof(ushort) + // X
        sizeof(ushort) + // Y
        sizeof(byte) + // Width
        sizeof(byte) + // Height
        Payload.Length; // Payload

    public int Encode(Span<byte> output)
    {
        if(Payload.Length != (Width + 1) * (Height + 1))
            throw new InvalidOperationException();
        var writer = output.Writer();
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Width);
        writer.Write(Height);
        writer.WriteArray(Payload);
        return writer.Position;
    }

    public static MapRegionPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var x = reader.ReadU2();
        var y = reader.ReadU2();
        var w = reader.ReadU1();
        var h = reader.ReadU1();
        return new(x, y, w, h, reader.ReadArray<CellType>((w + 1) * (h + 1)));
    }

    public bool Equals(MapRegionPacket other) =>
        X == other.X &&
        Y == other.Y &&
        Width == other.Width &&
        Height == other.Height &&
        Payload.SequenceEqual(other.Payload);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(X);
        hash.Add(Y);
        hash.Add(Width);
        hash.Add(Height);
        foreach (var cell in Payload)
            hash.Add(cell);
        return hash.ToHashCode();
    }

}
