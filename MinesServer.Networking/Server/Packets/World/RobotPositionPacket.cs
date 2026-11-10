using System;
using System.Runtime.CompilerServices;

namespace MinesServer.Networking.Server.Packets.World;

public readonly record struct RobotPositionPacket(ushort BotId, ushort X, ushort Y, byte Rotation) : IHBPacket<RobotPositionPacket>
{
    public byte PacketCode => HBPacketCodeProvider.GetPacketCode<RobotPositionPacket>();

    public int Size => Unsafe.SizeOf<RobotPositionPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static RobotPositionPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<RobotPositionPacket>();
}