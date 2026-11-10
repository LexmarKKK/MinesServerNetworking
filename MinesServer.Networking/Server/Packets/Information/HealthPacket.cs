using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct HealthPacket(int Current, int Max) : IRootServerPacket<HealthPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<HealthPacket>();

    public readonly int Size => Unsafe.SizeOf<HealthPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static HealthPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<HealthPacket>();
}
