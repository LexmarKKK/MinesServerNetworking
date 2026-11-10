using MinesServer.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.Information.StatusPanel;

public readonly record struct AddStatusLinePacket(byte BlinkRate, Color Color, string Tag, IReadOnlyList<string> Text) : IRootServerPacket<AddStatusLinePacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<AddStatusLinePacket>();

    public int Size =>
        sizeof(byte) + // BlinkRate
        sizeof(int) + // Color
        sizeof(byte) + // Tag.Length
        Tag.Length + // Tag
        sizeof(byte) * Text.Count + // Text[].Length
        Text.Sum(x => x.Length) * 2; // Text

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(BlinkRate);
        writer.Write(Color.ToArgb());
        writer.WriteU1PrefixedASCII(Tag);
        foreach (var x in Text)
            writer.WriteU1PrefixedUtf16(x);
        return writer.Position;
    }

    public static AddStatusLinePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var blinkRate = reader.ReadU1();
        var color = Color.FromArgb(reader.Read4());
        var tag = reader.ReadU1PrefixedASCII(out _);
        var text = new List<string>();
        while (reader.CanRead)
            text.Add(reader.ReadU1PrefixedUtf16(out _));
        return new AddStatusLinePacket(blinkRate, color, tag, text);
    }

    public bool Equals(AddStatusLinePacket other) =>
        BlinkRate == other.BlinkRate &&
        Color.ToArgb().Equals(other.Color.ToArgb()) &&
        Tag == other.Tag &&
        Text.SequenceEqual(other.Text);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(BlinkRate);
        hash.Add(Color.ToArgb());
        hash.Add(Tag);
        foreach (var part in Text)
            hash.Add(part);
        return hash.ToHashCode();
    }

}
