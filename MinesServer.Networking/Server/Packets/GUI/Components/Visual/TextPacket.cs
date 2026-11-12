using MinesServer.Data;
using MinesServer.Utils;
using MinesServer.Networking.Server.Packets.GUI;
using System;
using System.Drawing;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Visual;

public record class TextPacket : GUIComponentPacket, IGUIComponentPacket<TextPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<TextPacket>();

    public string Text { get; init; } = "";

    /// Ноль альфы означает «цвет скина клиента»: полностью прозрачный текст
    /// бессмыслен, поэтому значение свободно для маркера наследования.
    public Color TextColor { get; init; }

    /// Ноль означает «кегль скина клиента».
    public byte FontSize { get; init; }

    public FontId FontId { get; init; } = FontId.Text;

    public bool IsBold { get; init; }

    /// Межбуквенный интервал в сотых кегля (0.02em = 2), как в USS клиента.
    public byte LetterSpacing { get; init; }

    public TextAlign TextAlign { get; init; } = TextAlign.Default;

    public override int Size => base.Size
        + sizeof(ushort) // Text.Length
        + Text.Length * 2 // Text
        + sizeof(int) // TextColor (ARGB)
        + sizeof(byte) // FontSize
        + sizeof(byte) // FontId
        + sizeof(bool) // IsBold
        + sizeof(byte) // LetterSpacing
        + sizeof(byte); // TextAlign

    public override int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.WriteU2PrefixedUtf16(Text);
        writer.Write(TextColor.ToArgb());
        writer.Write(FontSize);
        writer.Write((byte)FontId);
        writer.Write(IsBold);
        writer.Write(LetterSpacing);
        writer.Write((byte)TextAlign);
        return writer.Position;
    }

    public static TextPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected TextPacket(ref MemoryReader reader) : base(ref reader)
    {
        Text = reader.ReadU2PrefixedUtf16(out _);
        // ARGB 0 (альфа-маркер наследования) маппится в default: у FromArgb(0)
        // внутренний state отличен от дефолтного, и равенство записей бы сломалось.
        var textColor = reader.Read4();
        TextColor = textColor == 0 ? default : Color.FromArgb(textColor);
        FontSize = reader.ReadU1();
        FontId = (FontId)reader.ReadU1();
        IsBold = reader.Read<bool>();
        LetterSpacing = reader.ReadU1();
        TextAlign = (TextAlign)reader.ReadU1();
    }

    public TextPacket() : base() { }
}
