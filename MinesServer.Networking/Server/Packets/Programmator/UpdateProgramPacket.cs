using MinesServer.Data;
using MinesServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.Programmator;

public readonly record struct UpdateProgramPacket(int ProgramId, string DisplayName, IReadOnlyList<(ProgAction Operator, string Label, string Value)> Instructions, int[] Breakpoints) : IRootServerPacket<UpdateProgramPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<UpdateProgramPacket>();

    public int Size =>
        sizeof(int) + // ProgramId
        sizeof(byte) + DisplayName.Length * 2 + // DisplayName
        sizeof(ushort) + Breakpoints.Length * sizeof(int) + // Breakpoints
        Instructions.Sum(x => sizeof(ProgAction) + sizeof(byte) + x.Value.Length * 2 + sizeof(byte) + x.Label.Length * 2); // Instructions

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(ProgramId);
        writer.WriteU1PrefixedUtf16(DisplayName);
        writer.WriteU2PrefixedArray(Breakpoints);
        foreach(var instruction in Instructions)
        {
            writer.Write(instruction.Operator);
            writer.WriteU1PrefixedUtf16(instruction.Label);
            writer.WriteU1PrefixedUtf16(instruction.Value);
        }
        return writer.Position;
    }

    public static UpdateProgramPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var progId = reader.Read4();
        var progName = reader.ReadU1PrefixedUtf16(out _);
        var breakpoints = reader.ReadU2PrefixedArray<int>(out _);
        List<(ProgAction Operator, string Label, string Value)> instructions = new();
        while(reader.CanRead)
            instructions.Add((reader.Read<ProgAction>(), reader.ReadU1PrefixedUtf16(out _), reader.ReadU1PrefixedUtf16(out _)));
        return new(progId, progName, instructions, breakpoints);
    }

    public bool Equals(UpdateProgramPacket other) =>
        ProgramId == other.ProgramId &&
        DisplayName == other.DisplayName &&
        Instructions.SequenceEqual(other.Instructions) &&
        Breakpoints.SequenceEqual(other.Breakpoints);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(ProgramId);
        hash.Add(DisplayName);
        foreach (var instruction in Instructions)
            hash.Add(instruction);
        foreach (var breakpoint in Breakpoints)
            hash.Add(breakpoint);
        return hash.ToHashCode();
    }

}
