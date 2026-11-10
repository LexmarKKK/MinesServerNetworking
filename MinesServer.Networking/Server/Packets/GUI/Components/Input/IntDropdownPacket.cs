#nullable enable
using MinesServer.Utils;
using MinesServer.Networking.Server.Packets.GUI;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Input;

public record class IntDropdownPacket : DropdownComponentPacket<int>, IGUIComponentPacket<IntDropdownPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<IntDropdownPacket>();

    public override int DefaultValue { get; init; }
    public override int[] Values { get; init; } = new[] { 0 };

    public override int Size => base.Size +
        sizeof(int) + // DefaultValue
        sizeof(byte) + // Values.Length
        sizeof(int) * Values.Length; // Values

    public override int Encode(Span<byte> output)
    {
        if (Values is null)
        {
            throw new ArgumentNullException(nameof(Values));
        }

        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.Write(DefaultValue);
        writer.WriteU1PrefixedArray(Values);
        return writer.Position;
    }

    public static IntDropdownPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected IntDropdownPacket(ref MemoryReader reader) : base(ref reader)
    {
        DefaultValue = reader.Read4();
        Values = reader.ReadU1PrefixedArray<int>(out _);
    }

    public IntDropdownPacket() { }

    public virtual bool Equals(IntDropdownPacket? other) => base.Equals(other);
    public override int GetHashCode() => base.GetHashCode();

}
