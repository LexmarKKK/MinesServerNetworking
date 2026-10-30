using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Mission;

public readonly record struct MissionInitPacket(string ImageURI, ushort Width, ushort Height, string Title, string Description) : IRootServerPacket<MissionInitPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<MissionInitPacket>();

    public int Size =>
        sizeof(ushort) + // ImageURI.Length
        ImageURI.Length + // ImageURI
        sizeof(ushort) + // Width
        sizeof(ushort) + // Height
        sizeof(byte) + // Title.Length
        Title.Length * 2 + // Title
        Description.Length * 2; // Description

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteU2PrefixedASCII(ImageURI);
        writer.Write(Width);
        writer.Write(Height);
        writer.WriteU1PrefixedUtf16(Title);
        writer.WriteUtf16(Description);
        return writer.Position;
    }

    public static MissionInitPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new MissionInitPacket(
            reader.ReadU2PrefixedASCII(out _), 
            reader.ReadU2(),
            reader.ReadU2(),
            reader.ReadU1PrefixedUtf16(out _),
            reader.ReadRemainingAsUtf16());
    }
}