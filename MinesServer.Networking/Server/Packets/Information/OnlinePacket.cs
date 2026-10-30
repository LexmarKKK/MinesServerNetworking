using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct OnlinePacket(uint Players, uint Programmator) : IRootServerPacket<OnlinePacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<OnlinePacket>();

    public readonly int Size => Unsafe.SizeOf<OnlinePacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static OnlinePacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<OnlinePacket>();
}
