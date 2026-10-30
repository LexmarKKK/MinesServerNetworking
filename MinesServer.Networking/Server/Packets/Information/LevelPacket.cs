using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct LevelPacket(long Level) : IRootServerPacket<LevelPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<LevelPacket>();

    public readonly int Size => Unsafe.SizeOf<LevelPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static LevelPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<LevelPacket>();
}
