using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct DailyBonusStatePacket(bool Enabled) : IRootServerPacket<DailyBonusStatePacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<DailyBonusStatePacket>();

    public readonly int Size => Unsafe.SizeOf<DailyBonusStatePacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static DailyBonusStatePacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<DailyBonusStatePacket>();
}
