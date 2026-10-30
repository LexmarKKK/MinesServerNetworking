using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Inventory;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct UseItemPacket() : IRootClientPacket<UseItemPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<UseItemPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static UseItemPacket Decode(ReadOnlySpan<byte> input) => new();
}
