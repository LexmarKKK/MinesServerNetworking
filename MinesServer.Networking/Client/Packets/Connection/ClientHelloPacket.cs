using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Client.Packets.Connection;

public readonly record struct ClientHelloPacket(int ClientVersion, string OS, int OSVersion, string HardwareFingerprint, string AuthToken) : IRootClientPacket<ClientHelloPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<ClientHelloPacket>();

    public int Size =>
        sizeof(int) + // ClientVersion
        sizeof(byte) + // OS.Length
        OS.Length + // OS
        sizeof(int) + // OSVersion
        sizeof(byte) + // HardwareFingerprint.Length
        HardwareFingerprint.Length + // HardwareFingerprint
        sizeof(ushort) + // AuthToken.Length
        AuthToken.Length; // AuthToken

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(ClientVersion);
        writer.WriteU1PrefixedASCII(OS);
        writer.Write(OSVersion);
        writer.WriteU1PrefixedASCII(HardwareFingerprint);
        writer.WriteU2PrefixedASCII(AuthToken);
        return writer.Position;
    }

    public static ClientHelloPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new(
            reader.Read4(),
            reader.ReadU1PrefixedASCII(out _),
            reader.Read4(),
            reader.ReadU1PrefixedASCII(out _),
            reader.ReadU2PrefixedASCII(out _));
    }
}
