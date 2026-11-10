using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Connection;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct PingPacket(long SentAt, int PreviousPing) : IRootServerPacket<PingPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<PingPacket>();

    public readonly int Size => Unsafe.SizeOf<PingPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static PingPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<PingPacket>();
}