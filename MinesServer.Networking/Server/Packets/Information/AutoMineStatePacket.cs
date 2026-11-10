using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct AutoMineStatePacket(bool Enabled) : IRootServerPacket<AutoMineStatePacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<AutoMineStatePacket>();

    public readonly int Size => Unsafe.SizeOf<AutoMineStatePacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static AutoMineStatePacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<AutoMineStatePacket>();
}
