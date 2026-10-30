using MinesServer.Networking.Client.Packets.Chat;
using System.Drawing;

namespace MinesServer.Networking.Tests.Client.Chat;

internal sealed class ChangeChatColorTest : RootClientPacketTest<ChangeChatColorPacket>
{
    public override ChangeChatColorPacket Packet => new(Color.FromArgb(255, 128, 64, 255));
}

