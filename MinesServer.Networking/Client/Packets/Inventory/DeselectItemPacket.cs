using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Inventory;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct DeselectItemPacket() : IRootClientPacket<DeselectItemPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<DeselectItemPacket>();

    public int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static DeselectItemPacket Decode(ReadOnlySpan<byte> input) => new();
}
