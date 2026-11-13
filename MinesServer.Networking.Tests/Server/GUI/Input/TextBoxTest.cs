using MinesServer.Networking.Server.Packets.GUI.Components.Input;

namespace MinesServer.Networking.Tests.Server.GUI.Input;

internal sealed class TextBoxTest : PacketTest<TextBoxPacket>
{
    public override TextBoxPacket Packet => new()
    {
        Name = "username",
        DefaultValue = "Player123",
        Regex = "^[a-zA-Z0-9_]{3,16}$",
        Placeholder = "Логин",
        IsPassword = true,
    };

    [Test]
    public void Defaults_DecodeAsNoPlaceholderNoMask()
    {
        var minimal = new TextBoxPacket { Name = "f", DefaultValue = "" };
        var buffer = new byte[minimal.Size];
        minimal.Encode(buffer);
        var decoded = TextBoxPacket.Decode(buffer);

        Assert.Multiple(() =>
        {
            Assert.That(decoded.Placeholder, Is.EqualTo(""));
            Assert.That(decoded.IsPassword, Is.False);
        });
    }
}
