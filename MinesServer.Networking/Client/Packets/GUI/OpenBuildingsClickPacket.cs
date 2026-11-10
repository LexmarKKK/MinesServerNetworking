using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.GUI;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct OpenBuildingsClickPacket() : IRootClientPacket<OpenBuildingsClickPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<OpenBuildingsClickPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static OpenBuildingsClickPacket Decode(ReadOnlySpan<byte> input) => new();
}
