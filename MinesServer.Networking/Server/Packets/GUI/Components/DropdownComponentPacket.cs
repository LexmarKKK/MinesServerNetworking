#nullable enable
using MinesServer.Utils;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.GUI.Components;

public abstract record class DropdownComponentPacket<TValue> : InputComponentPacket<TValue> where TValue : notnull
{
    public abstract TValue[] Values { get; init; }

    protected DropdownComponentPacket(ref MemoryReader reader) : base(ref reader) { }

    protected DropdownComponentPacket() : base() { }

    public virtual bool Equals(DropdownComponentPacket<TValue>? other) =>
        base.Equals(other) &&
        Values.SequenceEqual(other.Values);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(base.GetHashCode());
        foreach (var value in Values)
            hash.Add(value);
        return hash.ToHashCode();
    }

}