using MinesServer.Data;
using MinesServer.Networking.Server.Packets.GUI;
using MinesServer.Networking.Server.Packets.GUI.Components.Input;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.GUI;

/// <summary>
/// Независимые wire-векторы для полей, введённых под окно авторизации:
/// фиксируют не только round-trip, но и порядок байтов новых полей —
/// одинаковая симметричная ошибка с обеих сторон round-trip не ловится.
/// </summary>
internal sealed class WireOrderGoldenTests
{
    [Test]
    public void TextBoxPacket_PasswordFields_MatchGoldenBytes()
    {
        var packet = new TextBoxPacket
        {
            Name = "login",
            DefaultValue = "",
            Regex = "",
            Placeholder = "Пароль",
            IsPassword = true,
        };
        byte[] expected =
        [
            0x00, 0x00, 0x00, // Style нет, OnClickContext пуст, attached properties пусты
            0x05, (byte)'l', (byte)'o', (byte)'g', (byte)'i', (byte)'n', // Name
            0x01, // IsEnabled
            0x00, 0x00, // DefaultValue ""
            0x00, // Regex ""
            0x06, 0x00, // "Пароль" — длина
            0x1F, 0x04, 0x30, 0x04, 0x40, 0x04, 0x3E, 0x04, 0x3B, 0x04, 0x4C, 0x04, // "Пароль" UTF16LE
            0x01, // IsPassword
        ];

        var encoded = new byte[packet.Size];
        var written = packet.Encode(encoded);

        Assert.Multiple(() =>
        {
            Assert.That(written, Is.EqualTo(expected.Length));
            Assert.That(encoded, Is.EqualTo(expected));
        });
    }

    [Test]
    public void OpenWindowPacket_ThemeByte_BetweenHeightAndContentCode()
    {
        var packet = new OpenWindowPacket("w1", 440, 600, new TextPacket(), WindowTheme.Kern);
        byte[] expected =
        [
            0x02, (byte)'w', (byte)'1', // WindowTag
            0xB8, 0x01, // Width 440
            0x58, 0x02, // Height 600
            0x01, // WindowTheme.Kern
            GUIComponentPacketCodeProvider.GetPacketCode<TextPacket>(), // код содержимого
            0x00, 0x00, 0x00, // TextPacket: Style нет, OnClickContext пуст, attached properties пусты
            0x00, 0x00, // Text ""
        ];

        var encoded = new byte[packet.Size];
        var written = packet.Encode(encoded);

        Assert.Multiple(() =>
        {
            Assert.That(written, Is.EqualTo(expected.Length));
            Assert.That(encoded, Is.EqualTo(expected));
            Assert.That(OpenWindowPacket.Decode(expected).Theme, Is.EqualTo(WindowTheme.Kern));
        });
    }
}
