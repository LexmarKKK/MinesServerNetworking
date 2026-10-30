using MinesServer.Data;
using MinesServer.Utils;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MinesServer.Networking.Server.Packets.Inventory;

public readonly record struct InventoryPacket(IDictionary<ItemType, long> Changes) : IRootServerPacket<InventoryPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<InventoryPacket>();

    public int Size =>
        (sizeof(ItemType) + sizeof(long)) * Changes.Count; // Changes

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        foreach (var kvp in Changes)
        {
            writer.Write(kvp.Key);
            writer.Write(kvp.Value);
        }
        return writer.Position;
    }

    public static InventoryPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var changes = new Dictionary<ItemType, long>();
        while (reader.CanRead)
            changes.Add(reader.Read<ItemType>(), reader.Read8());
        return new InventoryPacket(changes);
    }

    public bool Equals(InventoryPacket obj)
    {
        return Changes.SequenceEqual(obj.Changes);
    }
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        foreach (var change in Changes)
            hash.Add(change);
        return hash.ToHashCode();
    }

}
