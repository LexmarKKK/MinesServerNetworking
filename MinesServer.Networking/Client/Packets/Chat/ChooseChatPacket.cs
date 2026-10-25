using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Client.Packets.Chat;

/// <summary>
/// Выбор чата из списка (легаси-аналог Choo): сервер переключает CurrentChat
/// на глобальный канал или приватный чат по тегу и отдаёт его последнюю
/// страницу истории (ChatMessageListPacket).
/// </summary>
public readonly record struct ChooseChatPacket(string Tag) : IRootClientPacket<ChooseChatPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.Cache<ChooseChatPacket>.Code;

    public int Size =>
        sizeof(byte) // Tag.Length
        + Tag.Length; // Tag

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(Tag);
        return writer.Position;
    }

    public static ChooseChatPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.ReadU1PrefixedASCII(out _));
    }
}
