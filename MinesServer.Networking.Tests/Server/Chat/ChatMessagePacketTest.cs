using MinesServer.Networking.Server.Packets.Chat;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.Chat;

internal sealed class ChatMessagePacketTest : PacketTest<ChatMessagePacket>
{
    public override ChatMessagePacket Packet => new(1, 1000, 42, 5, Color.Red, "PlayerName", Color.White, "Hello world");
}
