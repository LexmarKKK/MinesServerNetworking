using MinesServer.Utils;
using MinesServer.Networking.Server.Packets.GUI;
using System;

namespace MinesServer.Networking.Server.Packets.GUI.Components.Visual;
public record class ImagePacket : GUIComponentPacket, IGUIComponentPacket<ImagePacket>
{
    public override byte PacketCode => GUIComponentPacketCodeProvider.GetPacketCode<ImagePacket>();

    public string URI { get; init; } = "";
    public byte Width { get; init; } = 16;
    public byte Height { get; init; } = 16;

    public override int Size => base.Size
        + sizeof(ushort) // URI.Length
        + URI.Length // URI
        + sizeof(byte) // Width
        + sizeof(byte); // Height

    public override int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Advance(base.Encode(output));
        writer.WriteU2PrefixedASCII(URI);
        writer.Write(Width);
        writer.Write(Height);
        return writer.Position;
    }

    public static ImagePacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(ref reader);
    }

    protected ImagePacket(ref MemoryReader reader) : base(ref reader)
    {
        URI = reader.ReadU2PrefixedASCII(out _);
        Width = reader.ReadU1();
        Height = reader.ReadU1();
    }

    public ImagePacket() : base() { }
}
