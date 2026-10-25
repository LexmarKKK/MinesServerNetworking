using MinesServer.Networking.Client.Packets.Chat;

namespace MinesServer.Networking.Tests.Client.Chat;

internal class ChatMessageClickTest : RootClientPacketTest<ChatMessageClickPacket>
{
    public override ChatMessageClickPacket Packet => new("FED", 123);
}
