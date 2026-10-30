using MinesServer.Networking.Server.Packets.Chat;

namespace MinesServer.Networking.Tests.Server.Chat;

internal sealed class ChatMutePacketTest : RootServerPacketTest<ChatMutePacket>
{
    public override ChatMutePacket Packet => new(1000, 2000, "Spamming", 1, "Moderator");
}
