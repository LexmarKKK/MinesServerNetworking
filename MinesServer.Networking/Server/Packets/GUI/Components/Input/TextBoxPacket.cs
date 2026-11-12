using MinesServer.Utils;
using MinesServer.Networking.Server.Packets.GUI;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Input;

public record class TextBoxPacket : InputComponentPacket<string>, IGUIComponentPacket<TextBoxPacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<TextBoxPacket>();

    public override string DefaultValue { get; init; } = "";

    public string Regex { get; init; } = "";

    /// Подсказка пустого поля; пустая строка — подсказки нет.
    public string Placeholder { get; init; } = "";

    /// Маскировать ввод (поле пароля).
    public bool IsPassword { get; init; }

    /// Потолок символов ввода; ноль — лимита нет.
    public ushort MaxLength { get; init; }

    public override int Size => base.Size
        + sizeof(ushort) // DefaultValue.Length
        + DefaultValue.Length * 2 // DefaultValue
        + sizeof(byte) // Regex.Length
        + Regex.Length * 2 // Regex
        + sizeof(ushort) // Placeholder.Length
        + Placeholder.Length * 2 // Placeholder
        + sizeof(bool) // IsPassword
        + sizeof(ushort); // MaxLength

    public override int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.WriteU2PrefixedUtf16(DefaultValue);
        writer.WriteU1PrefixedUtf16(Regex);
        writer.WriteU2PrefixedUtf16(Placeholder);
        writer.Write(IsPassword);
        writer.Write(MaxLength);
        return writer.Position;
    }

    public static TextBoxPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected TextBoxPacket(ref MemoryReader reader) : base(ref reader)
    {
        DefaultValue = reader.ReadU2PrefixedUtf16(out _);
        Regex = reader.ReadU1PrefixedUtf16(out _);
        Placeholder = reader.ReadU2PrefixedUtf16(out _);
        IsPassword = reader.Read<bool>();
        MaxLength = reader.ReadU2();
    }

    public TextBoxPacket() : base() { }

    // Синтезированное равенство записи не видит новых полей: они объявлены в
    // базовом InputComponentPacket с собственным Equals, поэтому без этого
    // переопределения декодер, потерявший Placeholder/IsPassword/MaxLength,
    // проходил бы round-trip тесты как ни в чём не бывало.
    public virtual bool Equals(TextBoxPacket? other) =>
        base.Equals(other) &&
        other.Placeholder == Placeholder &&
        other.IsPassword == IsPassword &&
        other.MaxLength == MaxLength;

    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(base.GetHashCode());
        hash.Add(Placeholder);
        hash.Add(IsPassword);
        hash.Add(MaxLength);
        return hash.ToHashCode();
    }
}
