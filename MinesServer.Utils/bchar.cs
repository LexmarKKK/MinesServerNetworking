#nullable enable
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MinesServer.Utils
{

    /// <summary>
    /// Обёртка для <see cref="char"/>, хранится в 1 байте, работает правильно только если ты не рукожоп и используешь этот тип данных только с ASCII символами
    /// </summary>
    /// <remarks>
    /// Эта штука предназначена исключительно для маршалинга, не надо юзать её на постоянке. Лучше как все адекватные люди используй <see cref="char"/>
    /// </remarks>
    // Darkar25 @ 2024
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [DebuggerDisplay("{(char)val}")]
#pragma warning disable CS8981
#pragma warning disable IDE1006
    public readonly struct bchar : IComparable, IConvertible, IComparable<bchar>, IComparable<char>, IEquatable<bchar>, IEquatable<char>
#pragma warning restore IDE1006
#pragma warning restore CS8981
    {
        private bchar(byte val) => this.val = val;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly byte val;

        public static implicit operator bchar(char c) => new((byte)c);

        public static implicit operator char(bchar c) => (char)c.val;

        public int CompareTo(object? obj)
        {
            return obj switch
            {
                null => 1,
                char c => this - c,
                bchar b => val - b.val,
                _ => throw new ArgumentException("Arg_MustBeChar")
            };
        }

        public override string ToString() => ((char)val).ToString();

        public TypeCode GetTypeCode() => TypeCode.Char;

        public string ToString(IFormatProvider? provider) => ((char)val).ToString(provider);

        short IConvertible.ToInt16(IFormatProvider? provider) => Convert.ToInt16(val);

        int IConvertible.ToInt32(IFormatProvider? provider) => Convert.ToInt32(val);

        long IConvertible.ToInt64(IFormatProvider? provider) => Convert.ToInt64(val);

        sbyte IConvertible.ToSByte(IFormatProvider? provider) => Convert.ToSByte(val);

        ushort IConvertible.ToUInt16(IFormatProvider? provider) => Convert.ToUInt16(val);

        uint IConvertible.ToUInt32(IFormatProvider? provider) => Convert.ToUInt32(val);

        ulong IConvertible.ToUInt64(IFormatProvider? provider) => Convert.ToUInt64(val);

        float IConvertible.ToSingle(IFormatProvider? provider) => throw new ArgumentException("InvalidCast_FromTo");

        bool IConvertible.ToBoolean(IFormatProvider? provider) => throw new ArgumentException("InvalidCast_FromTo");

        DateTime IConvertible.ToDateTime(IFormatProvider? provider) => throw new ArgumentException("InvalidCast_FromTo");

        decimal IConvertible.ToDecimal(IFormatProvider? provider) => throw new ArgumentException("InvalidCast_FromTo");

        double IConvertible.ToDouble(IFormatProvider? provider) => throw new ArgumentException("InvalidCast_FromTo");

        object IConvertible.ToType(Type type, IFormatProvider? provider) => Convert.ChangeType(val, type, provider);

        char IConvertible.ToChar(IFormatProvider? provider) => (char)val;

        byte IConvertible.ToByte(IFormatProvider? provider) => val;

        public int CompareTo(bchar obj) => val - obj.val;

        public int CompareTo(char obj) => this - obj;

        public override bool Equals(object? obj) => (obj is char c && this == c) || (obj is bchar b && this == b);

        public bool Equals(char obj) => (char)this == obj;

        public bool Equals(bchar obj) => val == obj.val;

        public override int GetHashCode() => val.GetHashCode();

        public static bool operator ==(bchar left, bchar right) => left.Equals(right);

        public static bool operator !=(bchar left, bchar right) => !(left == right);

        public static bool operator <(bchar left, bchar right) => left.CompareTo(right) < 0;

        public static bool operator <=(bchar left, bchar right) => left.CompareTo(right) <= 0;

        public static bool operator >(bchar left, bchar right) => left.CompareTo(right) > 0;

        public static bool operator >=(bchar left, bchar right) => left.CompareTo(right) >= 0;
    }
}
