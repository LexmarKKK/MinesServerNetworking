using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Programmator;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct OpenProgrammatorPacket : IRootServerPacket<OpenProgrammatorPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<OpenProgrammatorPacket>();

    public readonly int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static OpenProgrammatorPacket Decode(ReadOnlySpan<byte> input) => new();
}