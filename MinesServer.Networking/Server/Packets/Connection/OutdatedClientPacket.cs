using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Connection;

public readonly record struct OutdatedClientPacket(ushort NewVersionCode, string Name, string Description, string UpdateURL, string CLIArgs) : IRootServerPacket<OutdatedClientPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<OutdatedClientPacket>();

    public int Size =>
        sizeof(ushort) + // NewVersionCode
        sizeof(byte) + // Name.Length
        Name.Length * 2 + // Name
        sizeof(ushort) + // Description.Length
        Description.Length * 2 + // Description
        sizeof(ushort) + // UpdateURL.Length
        UpdateURL.Length + // UpdateURL
        CLIArgs.Length; // CLIArgs

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(NewVersionCode);
        writer.WriteU1PrefixedUtf16(Name);
        writer.WriteU2PrefixedUtf16(Description);
        writer.WriteU2PrefixedASCII(UpdateURL);
        writer.WriteASCII(CLIArgs);
        return writer.Position;
    }

    public static OutdatedClientPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new OutdatedClientPacket(
            reader.ReadU2(),
            reader.ReadU1PrefixedUtf16(out _),
            reader.ReadU2PrefixedUtf16(out _),
            reader.ReadU2PrefixedASCII(out _),
            reader.ReadRemainingAsASCII());
    }
}
