using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Actions;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct SuicidePacket() : IActionClientPacket<SuicidePacket>
{
    public byte PacketCode => ActionClientPacketCodeProvider.GetPacketCode<SuicidePacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static SuicidePacket Decode(ReadOnlySpan<byte> input) => new();
}