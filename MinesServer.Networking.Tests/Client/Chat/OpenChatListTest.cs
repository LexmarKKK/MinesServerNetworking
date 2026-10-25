using MinesServer.Networking.Client.Packets.Chat;

namespace MinesServer.Networking.Tests.Client.Chat;

internal class OpenChatListTest : RootClientPacketTest<OpenChatListPacket>
{
    public override OpenChatListPacket Packet => new();
}
