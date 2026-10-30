using MinesServer.Data;
using MinesServer.Utils;
using System;

namespace MinesServer.Networking.Server.Packets.Information;

public readonly record struct GeologyPacket(int Current, int Max, CellType Cell, string Text) : IRootServerPacket<GeologyPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<GeologyPacket>();

    public int Size =>
        sizeof(int) + // Current
        sizeof(int) + // Max
        sizeof(CellType) + // Cell
        Text.Length * 2; // Text

    public int Encode(Span<byte> output)
    {
        var writer = output.Writer();
        writer.Write(Current);
        writer.Write(Max);
        writer.Write(Cell);
        writer.WriteUtf16(Text);
        return writer.Position;
    }

    public static GeologyPacket Decode(ReadOnlySpan<byte> input)
    {
        var reader = input.Reader();
        return new GeologyPacket(
            reader.Read4(),
            reader.Read4(),
            reader.Read<CellType>(),
            reader.ReadRemainingAsUtf16());
    }
}
