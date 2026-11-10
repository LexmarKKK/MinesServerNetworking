using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct HideClanPacket() : IRootServerPacket<HideClanPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<HideClanPacket>();

    public readonly int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static HideClanPacket Decode(ReadOnlySpan<byte> input) => new();
}