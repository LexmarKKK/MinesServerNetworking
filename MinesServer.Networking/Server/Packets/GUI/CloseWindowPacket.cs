using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.GUI;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct CloseWindowPacket() : IRootServerPacket<CloseWindowPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<CloseWindowPacket>();

    public readonly int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static CloseWindowPacket Decode(ReadOnlySpan<byte> input) => new();
}
