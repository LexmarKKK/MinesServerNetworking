using MinesServer.Utils;
using System;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.Programmator;

public readonly record struct BreakpointHitPacket(int[] CallStack) : IRootServerPacket<BreakpointHitPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<BreakpointHitPacket>();

    public int Size =>
        sizeof(int) * CallStack.Length; // CallStack

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteArray(CallStack);
        return writer.Position;
    }

    public static BreakpointHitPacket Decode(ReadOnlySpan<byte> input) => new(input.Reader().ReadRemainingAsArray<int>());

    public bool Equals(BreakpointHitPacket other) => CallStack.SequenceEqual(other.CallStack);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        foreach (var breakpoint in CallStack)
            hash.Add(breakpoint);
        return hash.ToHashCode();
    }

}
