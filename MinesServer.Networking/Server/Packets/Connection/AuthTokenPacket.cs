using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Connection;

public readonly record struct AuthTokenPacket(string Token) : IRootServerPacket<AuthTokenPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<AuthTokenPacket>();

    public int Size => Token.Length; // Token

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteASCII(Token);
        return writer.Position;
    }

    public static AuthTokenPacket Decode(ReadOnlySpan<byte> input) => new(input.Reader().ReadRemainingAsASCII());
}