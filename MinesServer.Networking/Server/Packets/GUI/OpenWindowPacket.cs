using MinesServer.Data;
using MinesServer.Networking.Exceptions;
using MinesServer.Networking.Server.Packets.GUI.Components;
using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.GUI;

public readonly record struct OpenWindowPacket(string WindowTag, ushort Width, ushort Height, IGUIComponentPacket Content, WindowTheme Theme = WindowTheme.SciFi) : IRootServerPacket<OpenWindowPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<OpenWindowPacket>();

    public readonly int Size =>
        sizeof(byte) // WindowTag.Length
        + WindowTag.Length // WindowTag
        + sizeof(ushort) // Width
        + sizeof(ushort) // Height
        + sizeof(byte) // Theme
        + sizeof(byte) // Content packet code
        + Content.Size; // Content

    public static OpenWindowPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var tag = reader.ReadU1PrefixedASCII(out _);
        var width = reader.ReadU2();
        var height = reader.ReadU2();
        var theme = (WindowTheme)reader.ReadU1();
        var packetCode = reader.ReadU1();
        if (!GUIComponentPacketCodeProvider.TryGetDecoder(packetCode, out var decoder))
            throw new PacketDecodeException("Invalid packet code");
        var content = decoder(reader.Remaining);
        return new(tag, width, height, content, theme);
    }

    public int Encode(Span<byte> output)
    {
        if (string.IsNullOrEmpty(WindowTag))
        {
            throw new ArgumentException("WindowTag must be set.", nameof(WindowTag));
        }
        if (Width == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Width));
        }
        if (Height == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Height));
        }
        if (Content is null)
        {
            throw new ArgumentNullException(nameof(Content));
        }

        var writer = output.Writer();
        writer.WriteU1PrefixedASCII(WindowTag);
        writer.Write(Width);
        writer.Write(Height);
        writer.Write((byte)Theme);
        writer.Write(Content.PacketCode);
        writer.Write(Content);
        return writer.Position;
    }
}
