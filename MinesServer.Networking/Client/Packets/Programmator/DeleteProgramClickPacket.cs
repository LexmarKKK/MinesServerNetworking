using MinesServer.Data;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Programmator;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct DeleteProgramClickPacket(int ProgramId) : IRootClientPacket<DeleteProgramClickPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.Cache<DeleteProgramClickPacket>.Code;

    public int Size => Unsafe.SizeOf<DeleteProgramClickPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static DeleteProgramClickPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<DeleteProgramClickPacket>();
}
