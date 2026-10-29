using MinesServer.Data;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Programmator;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct RenameProgramClickPacket(int ProgramId) : IRootClientPacket<RenameProgramClickPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.Cache<RenameProgramClickPacket>.Code;

    public int Size => Unsafe.SizeOf<RenameProgramClickPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static RenameProgramClickPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<RenameProgramClickPacket>();
}
