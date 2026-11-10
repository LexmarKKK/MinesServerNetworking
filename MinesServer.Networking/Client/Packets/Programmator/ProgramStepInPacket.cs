using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Programmator;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct ProgramStepInPacket() : IRootClientPacket<ProgramStepInPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<ProgramStepInPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static ProgramStepInPacket Decode(ReadOnlySpan<byte> input) => new();
}
