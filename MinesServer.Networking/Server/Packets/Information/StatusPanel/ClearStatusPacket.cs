using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinesServer.Networking.Server.Packets.Information.StatusPanel;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct ClearStatusPacket : IRootServerPacket<ClearStatusPacket>
{
    public ushort PacketCode => RootServerPacketCodeProvider.Cache<ClearStatusPacket>.Code;

    // Пустой пакет: payload нет. Явный 0 — Unsafe.SizeOf пустой структуры
    // на Mono (Unity) даёт 0, на .NET 1; wire обязан не зависеть от рантайма.
    public readonly int Size => 0;

    public int Encode(Span<byte> output) => 0;

    public static ClearStatusPacket Decode(ReadOnlySpan<byte> input) => new();
}