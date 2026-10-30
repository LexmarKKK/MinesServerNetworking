using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Information.StatusPanel;

public readonly record struct ClearStatusLinePacket(string Tag) : IRootServerPacket<ClearStatusLinePacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<ClearStatusLinePacket>();

    public int Size =>
        Tag.Length; // Tag

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.WriteASCII(Tag);
        return writer.Position;
    }

    public static ClearStatusLinePacket Decode(ReadOnlySpan<byte> input) => new(input.Reader().ReadRemainingAsASCII());
}
