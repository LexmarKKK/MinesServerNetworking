using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Movement;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct TeleportPacket(ushort X, ushort Y, bool SmoothTransition = true) : IRootServerPacket<TeleportPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.Cache<TeleportPacket>.Code;

    public readonly int Size => Unsafe.SizeOf<TeleportPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static TeleportPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<TeleportPacket>();
}
