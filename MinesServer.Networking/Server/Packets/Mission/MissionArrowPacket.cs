using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Mission;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct MissionArrowPacket(ushort X, ushort Y) : IRootServerPacket<MissionArrowPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<MissionArrowPacket>();

    public readonly int Size => Unsafe.SizeOf<MissionArrowPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static MissionArrowPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<MissionArrowPacket>();
}
