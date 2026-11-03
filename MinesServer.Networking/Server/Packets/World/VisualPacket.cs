using MinesServer.Data;
using MinesServer.Networking.Contracts.Packets;
using MinesServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.World;

public readonly record struct VisualPacket(VisualEffect EffectType, ushort TargetBotId, ushort X, ushort Y, IReadOnlyList<StringPairPacket> Parameters) : IHBPacket<VisualPacket>
{
    public byte PacketCode => HBPacketCodeProvider.GetPacketCode<VisualPacket>();

    public int Size =>
        sizeof(VisualEffect) + // EffectType
        sizeof(ushort) + // TargetBotId
        sizeof(ushort) + // X
        sizeof(ushort) + // Y
        sizeof(byte) + // Parameters.Length
        Parameters.Sum(x => x.Size); // Parameters

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(EffectType);
        writer.Write(TargetBotId);
        writer.Write(X);
        writer.Write(Y);
        writer.Write((byte)Parameters.Count);
        foreach (var param in Parameters)
            writer.Write(param);
        return writer.Position;
    }

    public static VisualPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var effect = reader.Read<VisualEffect>();
        var botid = reader.ReadU2();
        var x = reader.ReadU2();
        var y = reader.ReadU2();
        var parameters = new StringPairPacket[reader.ReadU1()];
        for (int i = 0; i < parameters.Length; i++)
        {
            var item = StringPairPacket.Decode(reader.Remaining);
            reader.Advance(item.Size);
            parameters[i] = item;
        }
        return new(effect, botid, x, y, parameters);
    }

    public bool Equals(VisualPacket other) =>
        EffectType == other.EffectType &&
        TargetBotId == other.TargetBotId &&
        X == other.X &&
        Y == other.Y &&
        Parameters.SequenceEqual(other.Parameters);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(EffectType);
        hash.Add(TargetBotId);
        hash.Add(X);
        hash.Add(Y);
        foreach (var parameter in Parameters)
            hash.Add(parameter);
        return hash.ToHashCode();
    }

}
