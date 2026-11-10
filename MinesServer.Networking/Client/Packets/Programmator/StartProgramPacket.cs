using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Programmator;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct StartProgramPacket() : IRootClientPacket<StartProgramPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<StartProgramPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static StartProgramPacket Decode(ReadOnlySpan<byte> input) => new();
}
