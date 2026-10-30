using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct ShowClanPacket(ushort ClanId) : IRootServerPacket<ShowClanPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<ShowClanPacket>();

    public readonly int Size => Unsafe.SizeOf<ShowClanPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static ShowClanPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<ShowClanPacket>();
}
