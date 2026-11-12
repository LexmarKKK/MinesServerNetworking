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
        MaxLength = 16,
    };

    [Test]
    public void Defaults_DecodeAsNoPlaceholderNoMaskNoLimit()
    {
        var minimal = new TextBoxPacket { Name = "f", DefaultValue = "" };
        var buffer = new byte[minimal.Size];
        minimal.Encode(buffer);
        var decoded = TextBoxPacket.Decode(buffer);

        Assert.Multiple(() =>
        {
            Assert.That(decoded.Placeholder, Is.EqualTo(""));
            Assert.That(decoded.IsPassword, Is.False);
            Assert.That(decoded.MaxLength, Is.EqualTo(0), "0 = лимита нет");
        });
    }
}
