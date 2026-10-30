using MinesServer.Data;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct SkillProgressPacket(SkillType Skill, long Current, long Max) : IRootServerPacket<SkillProgressPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<SkillProgressPacket>();

    public readonly int Size => Unsafe.SizeOf<SkillProgressPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static SkillProgressPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<SkillProgressPacket>();
}
