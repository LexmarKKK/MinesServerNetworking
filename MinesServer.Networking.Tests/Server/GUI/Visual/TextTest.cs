using MinesServer.Data;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.GUI.Visual;

internal sealed class TextTest : PacketTest<TextPacket>
{
    public override TextPacket Packet => new()
    {
        Text = "asdsa фывыф",
        TextColor = Color.FromArgb(255, 216, 213, 238),
        FontSize = 32,
        FontId = FontId.Display,
        IsBold = true,
        LetterSpacing = 2,
        TextAlign = TextAlign.Center,
    };

    [Test]
    public void Defaults_DecodeAsInheritSentinels()
    {
        var minimal = new TextPacket { Text = "t" };
        var decoded = TextPacket.Decode(EncodeOf(minimal));

        Assert.Multiple(() =>
        {
            Assert.That(decoded.TextColor.A, Is.EqualTo(0), "alpha 0 = цвет скина клиента");
            Assert.That(decoded.FontSize, Is.EqualTo(0), "0 = кегль скина клиента");
            Assert.That(decoded.FontId, Is.EqualTo(FontId.Text));
            Assert.That(decoded.IsBold, Is.False);
            Assert.That(decoded.LetterSpacing, Is.EqualTo(0));
            Assert.That(decoded.TextAlign, Is.EqualTo(TextAlign.Default));
        });
    }

    private static byte[] EncodeOf(TextPacket packet)
    {
        var encoded = new byte[packet.Size];
        packet.Encode(encoded);
        return encoded;
    }
}
