using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.GUI;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct OpenProgramListClickPacket() : IRootClientPacket<OpenProgramListClickPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<OpenProgramListClickPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static OpenProgramListClickPacket Decode(ReadOnlySpan<byte> input) => new();
}
