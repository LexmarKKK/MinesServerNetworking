using MinesServer.Data;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Movement;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct RotatePacket(Direction Direction) : IActionClientPacket<RotatePacket>
{
    public byte PacketCode => ActionClientPacketCodeProvider.GetPacketCode<RotatePacket>();

    public int Size => Unsafe.SizeOf<RotatePacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static RotatePacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<RotatePacket>();
}
