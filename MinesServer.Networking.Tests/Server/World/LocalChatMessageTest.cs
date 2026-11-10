using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal sealed class LocalChatMessageTest : RootServerPacketTest<LocalChatMessagePacket>
{
    public override LocalChatMessagePacket Packet => new(
        1010,
        50,
        75,
        "This is a local message. Это локальное сообщение.");
}
