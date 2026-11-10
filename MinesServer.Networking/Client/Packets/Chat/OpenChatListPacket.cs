using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Chat;

/// <summary>
/// Запрос списка чатов (легаси-аналог Cmen): сервер шлёт ChatListPacket
/// — глобальные каналы плюс приватные чаты игрока (см. pSenders.SendChatList).
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct OpenChatListPacket() : IRootClientPacket<OpenChatListPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<OpenChatListPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static OpenChatListPacket Decode(ReadOnlySpan<byte> input) => new();
}
