using MinesServer.Networking.Server.Packets.GUI.Components.Input;

namespace MinesServer.Networking.Tests.Server.GUI.Input;

internal sealed class TextBoxTest : PacketTest<TextBoxPacket>
{
    public override TextBoxPacket Packet => new()
    {
        Name = "username",
        DefaultValue = "Player123",
        Regex = "^[a-zA-Z0-9_]{3,16}$"
    };
}