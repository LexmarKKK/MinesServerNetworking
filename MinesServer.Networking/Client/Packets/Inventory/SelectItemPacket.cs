using MinesServer.Data;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Client.Packets.Inventory;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct SelectItemPacket(ItemType Item) : IRootClientPacket<SelectItemPacket>
{
    public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<SelectItemPacket>();

    public int Size => Unsafe.SizeOf<SelectItemPacket>();

    public int Encode(Span<byte> output) => output.UnsafeWrite(this);

    public static SelectItemPacket Decode(ReadOnlySpan<byte> input) => input.UnsafeRead<SelectItemPacket>();
}
