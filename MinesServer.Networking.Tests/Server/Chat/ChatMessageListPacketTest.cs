using MinesServer.Networking.Server.Packets.Chat;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.Chat;

internal sealed class ChatMessageListPacketTest : RootServerPacketTest<ChatMessageListPacket>
{
    public override ChatMessageListPacket Packet => new("en", [new(1, 1000, 42, 5, Color.Red, "Player", Color.White, "Hello")]);
}
