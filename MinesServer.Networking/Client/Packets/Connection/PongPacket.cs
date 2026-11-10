using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Connection;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct PongPacket(long SentAt) : IRootClientPacket<PongPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<PongPacket>();

    public int Size => Unsafe.SizeOf<PongPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static PongPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<PongPacket>();
}