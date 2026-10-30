using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Actions;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct UnmappedKeyPacket(byte Code, bool Control, bool Alt, bool Shift) : IActionClientPacket<UnmappedKeyPacket>
{
    public byte PacketCode => ActionClientPacketCodeProvider.GetPacketCode<UnmappedKeyPacket>();

    public int Size => Unsafe.SizeOf<UnmappedKeyPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static UnmappedKeyPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<UnmappedKeyPacket>();
}