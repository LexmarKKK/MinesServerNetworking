using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.GUI;

public readonly record struct ModalWindowPacket(string Title, string Description, string ButtonText, string IconURI) : IRootServerPacket<ModalWindowPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<ModalWindowPacket>();

    public readonly int Size =>
        sizeof(byte) + // Title.Length
        Title.Length * 2 + // Title
        sizeof(ushort) + // Description.Length
        Description.Length * 2 + // Description
        sizeof(byte) + // ButtonText.Length
        ButtonText.Length * 2 + // ButtonText
        IconURI.Length; // IconURI

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU1PrefixedUtf16(Title);
        writer.WriteU2PrefixedUtf16(Description);
        writer.WriteU1PrefixedUtf16(ButtonText);
        writer.WriteASCII(IconURI);
        return writer.Position;
    }

    public static ModalWindowPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(
            reader.ReadU1PrefixedUtf16(out _),
            reader.ReadU2PrefixedUtf16(out _),
            reader.ReadU1PrefixedUtf16(out _),
            reader.ReadRemainingAsASCII());
    }
}
