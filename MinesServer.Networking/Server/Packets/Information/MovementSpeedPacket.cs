using MinesServer.Data;
using MinesServer.Utils;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MinesServer.Networking.Server.Packets.Information;

public readonly record struct MovementSpeedPacket(IDictionary<CellType, ushort> CooldownMap) : IRootServerPacket<MovementSpeedPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<MovementSpeedPacket>();

    public int Size => CooldownMap.Count * (sizeof(CellType) + sizeof(ushort));

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        foreach (var a in CooldownMap)
        {
            writer.Write(a.Key);
            writer.Write(a.Value);
        }
        return writer.Position;
    }

    public static MovementSpeedPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var map = new Dictionary<CellType, ushort>();
        while (reader.CanRead)
            map[reader.Read<CellType>()] = reader.ReadU2();
        return new(map);
    }

    public bool Equals(MovementSpeedPacket other) =>
        CooldownMap.SequenceEqual(other.CooldownMap);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        foreach (var entry in CooldownMap)
            hash.Add(entry);
        return hash.ToHashCode();
    }

}
