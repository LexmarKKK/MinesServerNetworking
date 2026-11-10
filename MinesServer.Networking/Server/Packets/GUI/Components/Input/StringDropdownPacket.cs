#nullable enable
using MinesServer.Utils;
using MinesServer.Networking.Server.Packets.GUI;
using System;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Input;

public record class StringDropdownPacket : DropdownComponentPacket<string>, IGUIComponentPacket<StringDropdownPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<StringDropdownPacket>();

    public override string DefaultValue { get; init; } = "NULL";
    public override string[] Values { get; init; } = new[] { "NULL" };

    public override int Size => base.Size +
        sizeof(byte) + // DefaultValue.Length
        DefaultValue.Length * 2 + // DefaultValue
        sizeof(byte) + // Values.Length
        sizeof(byte) * Values.Length + // Values[].Length
        Values.Sum(x => x.Length) * 2; // Values

    public override int Encode(Span<byte> output)
    {
        if (Values is null)
        {
            throw new ArgumentNullException(nameof(Values));
        }

        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.WriteU1PrefixedUtf16(DefaultValue);
        writer.Write((byte)Values.Length);
        foreach (var val in Values)
            writer.WriteU1PrefixedUtf16(val);
        return writer.Position;
    }

    public static StringDropdownPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected StringDropdownPacket(ref MemoryReader reader) : base(ref reader)
    {
        DefaultValue = reader.ReadU1PrefixedUtf16(out _);
        Values = new string[reader.ReadU1()];
        for (int i = 0; i < Values.Length; i++)
            Values[i] = reader.ReadU1PrefixedUtf16(out _);
    }

    public StringDropdownPacket() { }

    public virtual bool Equals(StringDropdownPacket? other) => base.Equals(other);
    public override int GetHashCode() => base.GetHashCode();

}
