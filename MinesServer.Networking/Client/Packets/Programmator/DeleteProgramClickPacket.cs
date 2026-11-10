using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Programmator;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct DeleteProgramClickPacket() : IRootClientPacket<DeleteProgramClickPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<DeleteProgramClickPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static DeleteProgramClickPacket Decode(ReadOnlySpan<byte> input) => new();
}
