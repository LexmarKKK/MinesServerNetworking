using System;
using System.Runtime.CompilerServices;

namespace MinesServer.Networking.Server.Packets.World;

public readonly record struct RemoveBuildingPacket(ushort X, ushort Y) : IHBPacket<RemoveBuildingPacket>
{
    public byte PacketCode => HBPacketCodeProvider.Cache<RemoveBuildingPacket>.Code;

    public int Size => Unsafe.SizeOf<RemoveBuildingPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static RemoveBuildingPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<RemoveBuildingPacket>();
}