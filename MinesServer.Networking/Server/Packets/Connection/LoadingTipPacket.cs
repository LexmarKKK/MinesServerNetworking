using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Connection;

/// <summary>
/// Совет игроку на экране входа в мир. Текст описывает правила сервера (цены, навыки, рынок), поэтому клиент своих
/// советов не держит: показывает последний присланный, смену решает сервер. До 255 знаков, rich text клиента (&lt;b&gt;);
/// пустая строка — совета нет.
/// </summary>
public readonly record struct LoadingTipPacket(string Tip) : IRootServerPacket<LoadingTipPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<LoadingTipPacket>();

    public int Size =>
        sizeof(byte) + // Tip.Length
        Tip.Length * 2; // Tip

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedUtf16(Tip);
        return writer.Position;
    }

    public static LoadingTipPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.ReadU1PrefixedUtf16(out _));
    }
}
