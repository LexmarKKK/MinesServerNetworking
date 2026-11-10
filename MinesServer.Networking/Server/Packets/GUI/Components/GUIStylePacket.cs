using MinesServer.Utils;
using System;
using System.Drawing;

namespace MinesServer.Networking.Server.Packets.GUI.Components;

public readonly record struct GUIStylePacket : IServerPacket<GUIStylePacket>
{
    public Color Background { get; init; }
    public Color Border { get; init; }
    public byte BorderWidth { get; init; }
    public Margins Margin { get; init; }
    public Margins Padding { get; init; }

    public int Size =>
        sizeof(int) // Background
        + sizeof(int) // Border
        + sizeof(byte) // BorderWidth
        + Margin.Size // Margin
        + Padding.Size; // Padding

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(Background.ToArgb());
        writer.Write(Border.ToArgb());
        writer.Write(BorderWidth);
        writer.Write(Margin);
        writer.Write(Padding);
        return writer.Position;
    }

    public static GUIStylePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var background = Color.FromArgb(reader.Read4());
        var border = Color.FromArgb(reader.Read4());
        var borderWidth = reader.ReadU1();
        var margin = Margins.Decode(reader.Remaining);
        reader.Advance(margin.Size);
        var padding = Margins.Decode(reader.Remaining);
        reader.Advance(padding.Size);
        return new()
        {
            Background = background,
            Border = border,
            BorderWidth = borderWidth,
            Padding = padding,
            Margin = margin
        };
    }

    public bool Equals(GUIStylePacket other) =>
        Background.ToArgb().Equals(other.Background.ToArgb()) &&
        Border.ToArgb().Equals(other.Border.ToArgb()) &&
        BorderWidth == other.BorderWidth &&
        Margin == other.Margin &&
        Padding == other.Padding;
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(Background.ToArgb());
        hash.Add(Border.ToArgb());
        hash.Add(BorderWidth);
        hash.Add(Margin);
        hash.Add(Padding);
        return hash.ToHashCode();
    }

}
