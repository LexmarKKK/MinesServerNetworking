#nullable enable
using MinesServer.Networking.Exceptions;
using MinesServer.Networking.Server.Packets.GUI;
using MinesServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.GUI.Components;

public abstract record class ContainerComponentPacket : GUIComponentPacket, IContainerComponentPacket
{
    public IReadOnlyList<IGUIComponentPacket> Children { get; init; } = Array.Empty<IGUIComponentPacket>();

    public override int Size => base.Size
        + sizeof(ushort) // Children.Length
        + sizeof(byte) * Children.Count // Component code
        + Children.Sum(x => x.Size); // Children

    public override int Encode(Span<byte> output)
    {
        if (Children is null)
        {
            throw new ArgumentNullException(nameof(Children));
        }

        foreach (IGUIComponentPacket child in Children)
        {
            if (child is null)
            {
                throw new ArgumentException("GUI container children cannot contain null.", nameof(Children));
            }
        }

        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.Write((ushort)Children.Count);
        foreach (var child in Children)
        {
            writer.Write(child.PacketCode);
            writer.Write(child);
        }
        return writer.Position;
    }

    protected ContainerComponentPacket(ref MemoryReader reader) : base(ref reader)
    {
        var children = new IGUIComponentPacket[reader.ReadU2()];
        for (int i = 0; i < children.Length; i++)
        {
            var packetCode = reader.ReadU1();
            if (!GUIComponentPacketCodeProvider.TryGetDecoder(packetCode, out var decoder))
                throw new PacketDecodeException("Unknown component code");
            children[i] = decoder(reader.Remaining);
            reader.Advance(children[i].Size);
        }
        Children = children;
    }

    protected ContainerComponentPacket() : base() { }

    public virtual bool Equals(ContainerComponentPacket? other) =>
        base.Equals(other) &&
        Children.SequenceEqual(other.Children);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(base.GetHashCode());
        foreach (var child in Children)
            hash.Add(child);
        return hash.ToHashCode();
    }

}
