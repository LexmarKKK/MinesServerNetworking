using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Client.Packets.Chat;

/// <summary>
/// Клик по строке сообщения (легаси-аналог Cpri): сервер открывает окно
/// отправителя — кнопка «Личное сообщение» находит/создаёт PrivateChat.
/// UserId — идентификатор аккаунта отправителя из ChatMessagePacket.PlayerId.
/// </summary>
public readonly record struct ChatMessageClickPacket(string Tag, int UserId) : IRootClientPacket<ChatMessageClickPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.Cache<ChatMessageClickPacket>.Code;

    public int Size =>
        sizeof(byte) // Tag.Length
        + Tag.Length // Tag
        + sizeof(int); // UserId

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(Tag);
        writer.Write(UserId);
        return writer.Position;
    }

    public static ChatMessageClickPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.ReadU1PrefixedASCII(out _), reader.Read4());
    }
}
