using MinesServer.Data;
using MinesServer.Networking.Contracts.Packets;
using MinesServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.World;

public readonly record struct AudioPacket(AudioEventType EventType, ushort TargetBotId, ushort X, ushort Y, IReadOnlyList<StringPairPacket> Parameters) : IHBPacket<AudioPacket>
{
    public byte PacketCode => HBPacketCodeProvider.GetPacketCode<AudioPacket>();

    public int Size =>
        sizeof(AudioEventType) + // EventType
        sizeof(ushort) + // TargetBotId
        sizeof(ushort) + // X
        sizeof(ushort) + // Y
        sizeof(byte) + // Parameters.Length
        Parameters.Sum(x => x.Size); // Parameters

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(EventType);
        writer.Write(TargetBotId);
        writer.Write(X);
        writer.Write(Y);
        writer.Write((byte)Parameters.Count);
        foreach (var param in Parameters)
            writer.Write(param);
        return writer.Position;
    }

    public static AudioPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var eventType = reader.Read<AudioEventType>();
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
        return new(eventType, botid, x, y, parameters);
    }

    public bool Equals(AudioPacket other) =>
        EventType == other.EventType &&
        TargetBotId == other.TargetBotId &&
        X == other.X &&
        Y == other.Y &&
        Parameters.SequenceEqual(other.Parameters);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        hash.Add(EventType);
        hash.Add(TargetBotId);
        hash.Add(X);
        hash.Add(Y);
        foreach (var parameter in Parameters)
            hash.Add(parameter);
        return hash.ToHashCode();
    }

}
