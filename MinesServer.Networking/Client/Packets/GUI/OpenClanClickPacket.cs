using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.GUI;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct OpenClanClickPacket() : IRootClientPacket<OpenClanClickPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<OpenClanClickPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static OpenClanClickPacket Decode(ReadOnlySpan<byte> input) => new();
}
