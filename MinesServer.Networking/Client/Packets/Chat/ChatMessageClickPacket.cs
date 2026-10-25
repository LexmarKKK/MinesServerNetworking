using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Client.Packets.Chat;

/// <summary>
/// Клик по строке сообщения (легаси-аналог Cpri): сервер находит сообщение
/// по идентификатору и открывает/создаёт PrivateChat с его отправителем.
/// MessageId — идентификатор сообщения (ChatMessagePacket.Id); отправитель
/// из него извлекается однозначно, обратно (из UserId) — нет.
/// </summary>
public readonly record struct ChatMessageClickPacket(string Tag, long MessageId) : IRootClientPacket<ChatMessageClickPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.Cache<ChatMessageClickPacket>.Code;

    public int Size =>
        sizeof(byte) // Tag.Length
        + Tag.Length // Tag
        + sizeof(long); // MessageId

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(Tag);
        writer.Write(MessageId);
        return writer.Position;
    }

    public static ChatMessageClickPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.ReadU1PrefixedASCII(out _), reader.Read8());
    }
}
