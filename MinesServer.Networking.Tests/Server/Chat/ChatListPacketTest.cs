using MinesServer.Networking.Server.Packets.Chat;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.Chat;

internal sealed class ChatListPacketTest : RootServerPacketTest<ChatListPacket>
{
    public override ChatListPacket Packet => new([("en", "English", new(1, 1000, 42, 5, Color.Red, "Player", Color.White, "Hello"))]);
}
