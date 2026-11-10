using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Mission;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct MissionProgressPacket(long Current, long Max) : IRootServerPacket<MissionProgressPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<MissionProgressPacket>();

    public readonly int Size => Unsafe.SizeOf<MissionProgressPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static MissionProgressPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<MissionProgressPacket>();
}
