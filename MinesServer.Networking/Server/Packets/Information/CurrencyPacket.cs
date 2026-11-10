using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct CurrencyPacket(long Money, long Creds) : IRootServerPacket<CurrencyPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<CurrencyPacket>();

    public readonly int Size => Unsafe.SizeOf<CurrencyPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static CurrencyPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<CurrencyPacket>();
}
