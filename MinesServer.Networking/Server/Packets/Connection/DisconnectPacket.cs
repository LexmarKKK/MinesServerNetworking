using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Connection;

public readonly record struct DisconnectPacket(string Reason) : IRootServerPacket<DisconnectPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<DisconnectPacket>();

    public int Size =>
        Reason.Length * 2; // Reason

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteUtf16(Reason);
        return writer.Position;
    }

    public static DisconnectPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new DisconnectPacket(reader.ReadRemainingAsUtf16());
    }
}
