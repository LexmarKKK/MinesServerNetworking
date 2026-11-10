using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace MinesServer.Utils
{
    /// <summary>
    /// This class is based on the MemoryPack`s MemoryPackWriter class. It has removed safety checks and unnecessary parts. It is aimed at fast unmanaged data writing for networking.
    /// </summary>
    /// <remarks>
    /// Since this class has no safety checks you should know what you`re doing when using it.
    /// </remarks>
    // Darkar25 @ 2024
    public ref struct MemoryWriter
    {
        private static readonly UTF8Encoding s_strictUtf8 = new(false, true);

        public MemoryWriter(Span<byte> destination)
        {
            _original = destination;
            Remaining = _original;
        }

        private readonly Span<byte> _original;
        public Span<byte> Remaining { get; private set; }

        public readonly int Position
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _original.Length - Remaining.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Advance(int advanceLength) => Remaining = Remaining[advanceLength..];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => Remaining = _original;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(T value, int sizeHint) where T : unmanaged
        {
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(Remaining), value);
            Advance(sizeHint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(T value) where T : unmanaged => Write(value, Unsafe.SizeOf<T>());

        #region Managed numerics

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(sbyte value) => Write(value, 1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(byte value)
        {
            Remaining[0] = value;
            Advance(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(short value) => Write(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(ushort value) => Write(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(int value) => Write(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(uint value) => Write(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(long value) => Write(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(ulong value) => Write(value, 8);

        #endregion

        #region Arrays

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray<T>(T[] value, int sizeHint) where T : unmanaged
        {
            var byteCount = value.Length * sizeHint;
            MemoryMarshal.AsBytes(value.AsSpan()).CopyTo(Remaining);
            Advance(byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray<T>(T[] value) where T : unmanaged => WriteArray(value, Unsafe.SizeOf<T>());

        #region Prefixed Arrays

        #region Prefixed [int]Arrays

        #region Managed numeric [int]arrays

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray(ulong[] value) => WritePrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray(long[] value) => WritePrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray(uint[] value) => WritePrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray(int[] value) => WritePrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray(ushort[] value) => WritePrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray(short[] value) => WritePrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray(byte[] value)
        {
            Write(value.Length);
            WriteArray(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray(sbyte[] value) => WritePrefixedArray(value, 1);

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray<T>(T[] value, int sizeHint) where T : unmanaged
        {
            Write(value.Length);
            WriteArray(value, sizeHint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedArray<T>(T[] value) where T : unmanaged => WritePrefixedArray(value, Unsafe.SizeOf<T>());

        #endregion

        #region Prefixed [ushort]Arrays

        #region Managed numeric [ushort]arrays

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray(ulong[] value) => WriteU2PrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray(long[] value) => WriteU2PrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray(uint[] value) => WriteU2PrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray(int[] value) => WriteU2PrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray(ushort[] value) => WriteU2PrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray(short[] value) => WriteU2PrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray(byte[] value)
        {
            Write((ushort)value.Length);
            WriteArray(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray(sbyte[] value) => WriteU2PrefixedArray(value, 1);

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray<T>(T[] value, int sizeHint) where T : unmanaged
        {
            Write((ushort)value.Length);
            WriteArray(value, sizeHint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedArray<T>(T[] value) where T : unmanaged => WriteU2PrefixedArray(value, Unsafe.SizeOf<T>());

        #endregion

        #region Prefixed [short]Arrays

        #region Managed numeric [short]arrays

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray(ulong[] value) => Write2PrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray(long[] value) => Write2PrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray(uint[] value) => Write2PrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray(int[] value) => Write2PrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray(ushort[] value) => Write2PrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray(short[] value) => Write2PrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray(byte[] value)
        {
            Write((short)value.Length);
            WriteArray(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray(sbyte[] value) => Write2PrefixedArray(value, 1);

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray<T>(T[] value, int sizeHint) where T : unmanaged
        {
            Write((short)value.Length);
            WriteArray(value, sizeHint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedArray<T>(T[] value) where T : unmanaged => Write2PrefixedArray(value, Unsafe.SizeOf<T>());

        #endregion

        #region Prefixed [byte]Arrays

        #region Managed numeric [byte]arrays

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray(ulong[] value) => WriteU1PrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray(long[] value) => WriteU1PrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray(uint[] value) => WriteU1PrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray(int[] value) => WriteU1PrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray(ushort[] value) => WriteU1PrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray(short[] value) => WriteU1PrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray(byte[] value)
        {
            Write((byte)value.Length);
            WriteArray(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray(sbyte[] value) => WriteU1PrefixedArray(value, 1);

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray<T>(T[] value, int sizeHint) where T : unmanaged
        {
            Write((byte)value.Length);
            WriteArray(value, sizeHint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedArray<T>(T[] value) where T : unmanaged => WriteU1PrefixedArray(value, Unsafe.SizeOf<T>());

        #endregion

        #region Prefixed [sbyte]Arrays

        #region Managed numeric [sbyte]arrays

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray(ulong[] value) => Write1PrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray(long[] value) => Write1PrefixedArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray(uint[] value) => Write1PrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray(int[] value) => Write1PrefixedArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray(ushort[] value) => Write1PrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray(short[] value) => Write1PrefixedArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray(byte[] value)
        {
            Write((sbyte)value.Length);
            WriteArray(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray(sbyte[] value) => Write1PrefixedArray(value, 1);

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray<T>(T[] value, int sizeHint) where T : unmanaged
        {
            Write((sbyte)value.Length);
            WriteArray(value, sizeHint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedArray<T>(T[] value) where T : unmanaged => Write1PrefixedArray(value, Unsafe.SizeOf<T>());

        #endregion

        #endregion

        #region Managed numeric arrays

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray(sbyte[] value) => WriteArray(value, 1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray(byte[] value) => WriteSpan(value.AsSpan());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray(short[] value) => WriteArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray(ushort[] value) => WriteArray(value, 2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray(int[] value) => WriteArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray(uint[] value) => WriteArray(value, 4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray(long[] value) => WriteArray(value, 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArray(ulong[] value) => WriteArray(value, 8);

        #endregion

        #endregion

        #region Spans

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSpan<T>(scoped Span<T> value) where T : unmanaged => WriteSpan(value, Unsafe.SizeOf<T>());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSpan(scoped Span<byte> value)
        {
            value.CopyTo(Remaining);
            Advance(value.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSpan<T>(scoped Span<T> value, int sizeHint) where T : unmanaged
        {
            var size = sizeHint * value.Length;
            MemoryMarshal.AsBytes(value).CopyTo(Remaining);
            Advance(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSpan<T>(scoped ReadOnlySpan<T> value) where T : unmanaged => WriteSpan(value, Unsafe.SizeOf<T>());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSpan<T>(scoped ReadOnlySpan<T> value, int sizeHint) where T : unmanaged
        {
            var size = sizeHint * value.Length;
            MemoryMarshal.AsBytes(value).CopyTo(Remaining);
            Advance(size);
        }

        #endregion

        #region Strings

        #region UTF8

        #region Prefixed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedUtf8(string value)
        {
            WriteUtf8AtOffsetNoAdvance(value, out var bytesWritten, 4);
            Write(bytesWritten);
            Advance(bytesWritten);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedUtf8(string value)
        {
            WriteUtf8AtOffsetNoAdvance(value, out var bytesWritten, 2);
            Write((ushort)bytesWritten);
            Advance(bytesWritten);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedUtf8(string value)
        {
            WriteUtf8AtOffsetNoAdvance(value, out var bytesWritten, 2);
            Write((short)bytesWritten);
            Advance(bytesWritten);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedUtf8(string value)
        {
            WriteUtf8AtOffsetNoAdvance(value, out var bytesWritten, 1);
            Write((byte)bytesWritten);
            Advance(bytesWritten);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedUtf8(string value)
        {
            WriteUtf8AtOffsetNoAdvance(value, out var bytesWritten, 1);
            Write((sbyte)bytesWritten);
            Advance(bytesWritten);
        }

        // До чего же UTF8 капризная херня
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8AtOffsetNoAdvance(string value, out int bytesWritten, int offset)
        {
            int maxByteCount = s_strictUtf8.GetMaxByteCount(value.Length);
            Span<byte> destination = Remaining.Slice(offset, maxByteCount);
            bytesWritten = s_strictUtf8.GetBytes(value.AsSpan(), destination);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8(string value) => WriteUtf8(value, out _);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8(string value, out int bytesWritten)
        {
            WriteUtf8AtOffsetNoAdvance(value, out bytesWritten, 0);
            Advance(bytesWritten);
        }

#endregion

        #region ASCII

        #region Prefixed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedASCII(string value)
        {
            Write(value.Length);
            WriteASCII(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedASCII(string value)
        {
            Write((ushort)value.Length);
            WriteASCII(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedASCII(string value)
        {
            Write((short)value.Length);
            WriteASCII(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedASCII(string value)
        {
            Write((byte)value.Length);
            WriteASCII(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedASCII(string value)
        {
            Write((sbyte)value.Length);
            WriteASCII(value);
        }

        #endregion

        public static Span<bchar> AsASCIISpan(string value)
        {
            Span<bchar> span = new bchar[value.Length];
            unsafe
            {
                fixed (char* chars = value)
                fixed (bchar* bytes = span)
                {
                    bchar* bytesRef = (bchar*)chars;
                    for (int i = 0; i < value.Length; i++)
                        bytes[i] = bytesRef[i * 2];
                }
            }
            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteASCII(string value) => WriteSpan(MemoryMarshal.AsBytes(AsASCIISpan(value)));

        #endregion

        #region UTF16

        #region Prefixed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePrefixedUtf16(string value)
        {
            Write(value.Length);
            WriteUtf16(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU2PrefixedUtf16(string value)
        {
            Write((ushort)value.Length);
            WriteUtf16(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write2PrefixedUtf16(string value)
        {
            Write((short)value.Length);
            WriteUtf16(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteU1PrefixedUtf16(string value)
        {
            Write((byte)value.Length);
            WriteUtf16(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write1PrefixedUtf16(string value)
        {
            Write((sbyte)value.Length);
            WriteUtf16(value);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16(string value) => WriteSpan(value.AsSpan(), 2);

        #endregion

        #endregion
    }
}
