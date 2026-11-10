using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Inventory;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct DeselectItemPacket() : IRootServerPacket<DeselectItemPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.GetPacketCode<DeselectItemPacket>();

    public readonly int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static DeselectItemPacket Decode(ReadOnlySpan<byte> input) => new();
}
