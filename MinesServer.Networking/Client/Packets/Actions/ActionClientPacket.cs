using MinesServer.Networking.Exceptions;
using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Client.Packets.Actions;

public readonly record struct ActionClientPacket(ushort X, ushort Y, IActionClientPacket Payload) : IRootClientPacket<ActionClientPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<ActionClientPacket>();

    public int Size => 
        sizeof(ushort) +  // X
        sizeof(ushort) + // Y
        sizeof(byte) + // PacketCode
        Payload.Size; // Payload

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Payload.PacketCode);
        writer.Write(Payload);
        return writer.Position;
    }

    public static ActionClientPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var x = reader.ReadU2();
        var y = reader.ReadU2();
        var packetCode = reader.ReadU1();
        if (!ActionClientPacketCodeProvider.TryGetDecoder(packetCode, out var decoder))
            throw new PacketDecodeException("Invalid packet code");
        return new(x, y, decoder(reader.Remaining));
    }
}
