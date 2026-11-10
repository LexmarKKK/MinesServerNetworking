using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Actions;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct BuildCyanPacket() : IActionClientPacket<BuildCyanPacket>
{
    public byte PacketCode => ActionClientPacketCodeProvider.GetPacketCode<BuildCyanPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static BuildCyanPacket Decode(ReadOnlySpan<byte> input) => new();
}