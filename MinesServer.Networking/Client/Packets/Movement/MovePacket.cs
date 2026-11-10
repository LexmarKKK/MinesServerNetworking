using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Movement;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct MovePacket(ushort X, ushort Y) : IActionClientPacket<MovePacket>
{
    public byte PacketCode => ActionClientPacketCodeProvider.GetPacketCode<MovePacket>();

    public int Size => Unsafe.SizeOf<MovePacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static MovePacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<MovePacket>();
}
