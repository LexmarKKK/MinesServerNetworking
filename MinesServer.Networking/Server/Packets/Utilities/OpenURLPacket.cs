using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Utilities;

public readonly record struct OpenURLPacket(string URL) : IRootServerPacket<OpenURLPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<OpenURLPacket>();

    public int Size =>
        URL.Length; // URL

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteASCII(URL);
        return writer.Position;
    }

    public static OpenURLPacket Decode(ReadOnlySpan<byte> input) => new(input.Reader().ReadRemainingAsASCII());
}
