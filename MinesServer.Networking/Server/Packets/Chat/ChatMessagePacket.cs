using MinesServer.Utils;
using System;
using System.Drawing;

namespace MinesServer.Networking.Server.Packets.Chat;

public readonly record struct ChatMessagePacket(long Id, long Timestamp, int PlayerId, byte ClanId, Color NicknameColor, string PlayerName, Color MessageColor, string Message) : IServerPacket<ChatMessagePacket>
{
    public int Size =>
        sizeof(long) + // Id
        sizeof(long) + // Timestamp
        sizeof(int) + // PlayerId
        sizeof(byte) + // ClanId
        sizeof(int) + // NicknameColor
        sizeof(byte) + // PlayerName.Length
        PlayerName.Length * 2 + // PlayerName
        sizeof(int) + // MessageColor
        sizeof(byte) + // Message.Length
        Message.Length * 2; // Message

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(Id);
        writer.Write(Timestamp);
        writer.Write(PlayerId);
        writer.Write(ClanId);
        writer.Write(NicknameColor.ToArgb());
        writer.WriteU1PrefixedUtf16(PlayerName);
        writer.Write(MessageColor.ToArgb());
        writer.WriteU1PrefixedUtf16(Message);
        return writer.Position;
    }

    public static ChatMessagePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(reader.Read8(), 
            reader.Read8(),
            reader.Read4(),
            reader.ReadU1(), 
            Color.FromArgb(reader.Read4()),
            reader.ReadU1PrefixedUtf16(out _),
            Color.FromArgb(reader.Read4()),
            reader.ReadU1PrefixedUtf16(out _));
    }

    public bool Equals(ChatMessagePacket other) =>
        Id == other.Id &&
        Timestamp == other.Timestamp &&
        PlayerId == other.PlayerId &&
        ClanId == other.ClanId &&
        NicknameColor.ToArgb() == other.NicknameColor.ToArgb() &&
        PlayerName == other.PlayerName &&
        MessageColor.ToArgb() == other.MessageColor.ToArgb() &&
        Message == other.Message;
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(Id);
        hash.Add(Timestamp);
        hash.Add(PlayerId);
        hash.Add(ClanId);
        hash.Add(NicknameColor.ToArgb());
        hash.Add(PlayerName);
        hash.Add(MessageColor.ToArgb());
        hash.Add(Message);
        return hash.ToHashCode();
    }

}
