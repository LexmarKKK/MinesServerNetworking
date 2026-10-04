using MinesServer.Data;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Programmator;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct ProgramStatePacket(ProgramState State, bool IsMovementAllowed) : IRootServerPacket<ProgramStatePacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.Cache<ProgramStatePacket>.Code;

    public int Size => Unsafe.SizeOf<ProgramStatePacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static ProgramStatePacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<ProgramStatePacket>();
}