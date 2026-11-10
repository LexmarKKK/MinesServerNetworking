using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct MaxDepthPacket(ushort Depth) : IRootServerPacket<MaxDepthPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<MaxDepthPacket>();

    public readonly int Size => Unsafe.SizeOf<MaxDepthPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static MaxDepthPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<MaxDepthPacket>();
}
