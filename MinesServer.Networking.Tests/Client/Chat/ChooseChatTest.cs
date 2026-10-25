using MinesServer.Networking.Client.Packets.Chat;

namespace MinesServer.Networking.Tests.Client.Chat;

internal class ChooseChatTest : RootClientPacketTest<ChooseChatPacket>
{
    public override ChooseChatPacket Packet => new("FED");
}
