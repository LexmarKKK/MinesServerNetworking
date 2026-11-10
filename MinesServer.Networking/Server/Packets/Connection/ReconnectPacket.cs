using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Connection;

public readonly record struct ReconnectPacket(string Reason) : IRootServerPacket<ReconnectPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<ReconnectPacket>();

    public int Size =>
        Reason.Length * 2; // Reason

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteUtf16(Reason);
        return writer.Position;
    }

    public static ReconnectPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new ReconnectPacket(reader.ReadRemainingAsUtf16());
    }
}
