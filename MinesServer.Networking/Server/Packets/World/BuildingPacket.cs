using MinesServer.Data;
using System;
using System.Runtime.CompilerServices;

namespace MinesServer.Networking.Server.Packets.World;

public readonly record struct BuildingPacket(ushort X, ushort Y, BuildingType BuildingCode, byte Variant, byte LinkedClan) : IHBPacket<BuildingPacket>
{
    public byte PacketCode => HBPacketCodeProvider.Cache<BuildingPacket>.Code;

    public int Size => Unsafe.SizeOf<BuildingPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static BuildingPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<BuildingPacket>();
}
