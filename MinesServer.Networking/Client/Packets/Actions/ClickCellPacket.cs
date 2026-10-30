using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Actions;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct ClickCellPacket(ushort X, ushort Y) : IActionClientPacket<ClickCellPacket>
{
    public byte PacketCode => ActionClientPacketCodeProvider.GetPacketCode<ClickCellPacket>();

    public int Size => Unsafe.SizeOf<ClickCellPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static ClickCellPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<ClickCellPacket>();
}
