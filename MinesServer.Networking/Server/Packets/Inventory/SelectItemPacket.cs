using MinesServer.Data;
using MinesServer.Utils;
using System;
using System.Collections;

namespace MinesServer.Networking.Server.Packets.Inventory;

public readonly record struct SelectItemPacket(ItemType Item, string Name, string Description, byte DX, byte DY, byte Distance, bool Rotating, BitArray Highlight) : IRootServerPacket<SelectItemPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<SelectItemPacket>();

    public int Size =>
        sizeof(ItemType) + // Item
        sizeof(byte) + // Name.Length
        Name.Length * 2 + // Name
        sizeof(ushort) + // Description.Length
        Description.Length * 2 + // Description
        sizeof(byte) + // DX
        sizeof(byte) + // DY
        sizeof(byte) + // Distance
        sizeof(bool) + // Rotating
        (Highlight.Count + 31) / 32 * 4; // Highlight

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(Item);
        writer.WriteU1PrefixedUtf16(Name);
        writer.WriteU2PrefixedUtf16(Description);
        writer.Write(DX);
        writer.Write(DY);
        writer.Write(Distance);
        writer.Write(Rotating);
        int[] bitmask = new int[(int)Math.Ceiling(Highlight.Count / 32f)];
        Highlight.CopyTo(bitmask, 0);
        writer.WriteArray(bitmask);
        return writer.Position;
    }

    public static SelectItemPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new SelectItemPacket(
            reader.Read<ItemType>(),
            reader.ReadU1PrefixedUtf16(out _),
            reader.ReadU2PrefixedUtf16(out _),
            reader.ReadU1(),
            reader.ReadU1(),
            reader.ReadU1(),
            reader.Read<bool>(),
            new(reader.ReadRemainingAsArray<int>()));
    }

    public bool Equals(SelectItemPacket other)
    {
        return Item == other.Item &&
            Name == other.Name &&
            Description == other.Description &&
            DX == other.DX &&
            DY == other.DY &&
            Rotating == other.Rotating &&
            Distance == other.Distance &&
            Rotating == other.Rotating &&
            BitArrayEqualityComparer.Default.Equals(Highlight, other.Highlight);
    }

    public override int GetHashCode()
    {
        var hc = new HashCode();
        hc.Add(Item);
        hc.Add(Name);
        hc.Add(Description);
        hc.Add(DX);
        hc.Add(DY);
        hc.Add(Rotating);
        hc.Add(Distance);
        hc.Add(Rotating);
        hc.Add(Highlight, BitArrayEqualityComparer.Default);
        return hc.ToHashCode();
    }
}
