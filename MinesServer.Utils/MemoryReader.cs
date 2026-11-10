using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace MinesServer.Utils;

/// <summary>
/// This class is based on the MemoryPack`s MemoryPackReader class. It has removed safety checks and unnecessary parts. It is aimed at fast unmanaged data reading for networking.
/// </summary>
/// <remarks>
/// Since this class has no safety checks you should know what you`re doing when using it.
/// </remarks>
// Darkar25 @ 2024
public unsafe ref struct MemoryReader
{
    private static readonly UTF8Encoding s_strictUtf8 = new(false, true);

    private ReadOnlySpan<byte> original;
    public ReadOnlySpan<byte> Remaining { get; private set; }

    public MemoryReader(Span<byte> source) : this((ReadOnlySpan<byte>)source)
    {
    }

    public MemoryReader(ref byte reference, int length) : this(MemoryMarshal.CreateReadOnlySpan(ref reference, length))
    {
    }

    public MemoryReader(ReadOnlySpan<byte> source)
    {
        original = source;
        Remaining = original;
    }

    public readonly int Position
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => original.Length - Remaining.Length;
    }

    public readonly int RemainingBytes
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Remaining.Length;
    }

    public readonly bool CanRead
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => RemainingBytes > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Advance(int advanceLength) => Remaining = Remaining[advanceLength..];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sbyte Read1() => Read<sbyte>(1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte ReadU1()
    {
        byte ret = Remaining[0];
        Advance(1);
        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public short Read2() => Read<short>(2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ushort ReadU2() => Read<ushort>(2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Read4() => Read<int>(4);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint ReadU4() => Read<uint>(4);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Read8() => Read<long>(8);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong ReadU8() => Read<ulong>(8);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadPrefixedArray<T>(out int length, int knownSize) where T : unmanaged => ReadArray<T>(length = Read4(), knownSize);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadPrefixedArray<T>(out int length) where T : unmanaged => ReadPrefixedArray<T>(out length, sizeof(T));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadU2PrefixedArray<T>(out ushort length, int knownSize) where T : unmanaged => ReadArray<T>(length = ReadU2(), knownSize);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadU2PrefixedArray<T>(out ushort length) where T : unmanaged => ReadU2PrefixedArray<T>(out length, sizeof(T));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] Read2PrefixedArray<T>(out short length, int knownSize) where T : unmanaged => ReadArray<T>(length = Read2(), knownSize);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] Read2PrefixedArray<T>(out short length) where T : unmanaged => Read2PrefixedArray<T>(out length, sizeof(T));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadU1PrefixedArray<T>(out byte length, int knownSize) where T : unmanaged => ReadArray<T>(length = ReadU1(), knownSize);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadU1PrefixedArray<T>(out byte length) where T : unmanaged => ReadU1PrefixedArray<T>(out length, sizeof(T));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] Read1PrefixedArray<T>(out sbyte length, int knownSize) where T : unmanaged => ReadArray<T>(length = Read1(), knownSize);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] Read1PrefixedArray<T>(out sbyte length) where T : unmanaged => Read1PrefixedArray<T>(out length, sizeof(T));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadRemainingAsArray<T>(int knownSize) where T : unmanaged => ReadArray<T>(RemainingBytes / knownSize, knownSize);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadRemainingAsArray<T>() where T : unmanaged => ReadRemainingAsArray<T>(sizeof(T));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte[] ReadRemainingAsArray()
    {
        var length = RemainingBytes;
        if (length <= 0)
            return Array.Empty<byte>();
        byte[] ret = Remaining.ToArray();
        Advance(length);
        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadArray<T>(int length) where T : unmanaged => ReadArray<T>(length, sizeof(T));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte[] ReadByteArray(int length)
    {
        if (length <= 0)
            return Array.Empty<byte>();
        var ret = Remaining[..length].ToArray();
        Advance(length);
        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ReadArray<T>(int length, int knownSize) where T : unmanaged
    {
        if (length <= 0)
            return Array.Empty<T>();
        var size = knownSize * length;
        var ret = MemoryMarshal.Cast<byte, T>(Remaining[..size]).ToArray();
        Advance(size);
        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Read<T>() where T : unmanaged => Read<T>(sizeof(T));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Read<T>(int knownSize) where T : unmanaged
    {
        var ret = MemoryMarshal.Read<T>(Remaining);
        Advance(knownSize);
        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadPrefixedUtf8(out int length) => ReadUtf8(length = Read4());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Read2PrefixedUtf8(out short length) => ReadUtf8(length = Read2());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadU2PrefixedUtf8(out ushort length) => ReadUtf8(length = ReadU2());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Read1PrefixedUtf8(out sbyte length) => ReadUtf8(length = Read1());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadU1PrefixedUtf8(out byte length) => ReadUtf8(length = ReadU1());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadRemainingAsUtf8()
    {
        var length = RemainingBytes;
        if (length <= 0)
            return string.Empty;
        return ReadUtf8(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadUtf8(int length)
    {
        string ret = s_strictUtf8.GetString(Remaining[..length]);
        Advance(length);
        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadPrefixedASCII(out int length) => ReadASCII(length = Read4());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Read2PrefixedASCII(out short length) => ReadASCII(length = Read2());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadU2PrefixedASCII(out ushort length) => ReadASCII(length = ReadU2());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Read1PrefixedASCII(out sbyte length) => ReadASCII(length = Read1());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadU1PrefixedASCII(out byte length) => ReadASCII(length = ReadU1());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadRemainingAsASCII() => ReadASCII(RemainingBytes);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadASCII(int length)
    {
        if (length <= 0)
            return string.Empty;
        Span<char> chars = stackalloc char[length];
        for (int i = 0; i < length; i++)
            chars[i] = (char)Remaining[i];
        Advance(length);
        return chars.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadPrefixedUtf16(out int length) => ReadUtf16(length = Read4());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Read2PrefixedUtf16(out short length) => ReadUtf16(length = Read2());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadU2PrefixedUtf16(out ushort length) => ReadUtf16(length = ReadU2());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Read1PrefixedUtf16(out sbyte length) => ReadUtf16(length = Read1());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadU1PrefixedUtf16(out byte length) => ReadUtf16(length = ReadU1());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadRemainingAsUtf16() => ReadUtf16(RemainingBytes / 2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ReadUtf16(int length)
    {
        if (length <= 0)
            return string.Empty;
        var byteLength = length * 2;
        ReadOnlySpan<char> chars = MemoryMarshal.Cast<byte, char>(Remaining[..byteLength]);
        Advance(byteLength);
        return chars.ToString();
    }
}
