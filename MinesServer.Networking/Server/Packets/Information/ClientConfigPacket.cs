using MinesServer.Networking.Contracts.Packets;
using MinesServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesServer.Networking.Server.Packets.Information;

public readonly record struct ClientConfigPacket(IReadOnlyList<StringPairPacket> Keybinds, IReadOnlyList<string> UnrenderedTextures) : IRootServerPacket<ClientConfigPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<ClientConfigPacket>();

    public int Size =>
        sizeof(byte) + // Keybinds.Length
        Keybinds.Sum(x => x.Size) + // Keybinds
        UnrenderedTextures.Sum(x => sizeof(byte) + x.Length); // UnrenderedTextures

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write((byte)Keybinds.Count);
        foreach (var keybind in Keybinds)
            writer.Write(keybind);
        foreach (var texture in UnrenderedTextures)
            writer.WriteU1PrefixedASCII(texture);
        return writer.Position;
    }

    public static ClientConfigPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        var keybinds = new StringPairPacket[reader.ReadU1()];
        for(int i = 0; i < keybinds.Length; i++)
        {
            var keybind = StringPairPacket.Decode(reader.Remaining);
            reader.Advance(keybind.Size);
            keybinds[i] = keybind;
        }
        var unrender = new List<string>();
        while (reader.CanRead)
            unrender.Add(reader.ReadU1PrefixedASCII(out _));
        return new(keybinds, unrender);
    }

    public bool Equals(ClientConfigPacket other) =>
        Keybinds.SequenceEqual(other.Keybinds) &&
        UnrenderedTextures.SequenceEqual(other.UnrenderedTextures);
    public override int GetHashCode()
    {
        var hash = new System.HashCode();
        foreach (var keybind in Keybinds)
            hash.Add(keybind);
        foreach (var texture in UnrenderedTextures)
            hash.Add(texture);
        return hash.ToHashCode();
    }

}
