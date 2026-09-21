#if NET7_0_OR_GREATER
using System.Numerics;
#endif

using System;

namespace MinesServer.Utils
{
    public static class NumberExtensions
    {
        static long[] uintTable = new[] {
            4294967296,  8589934582,  8589934582,  8589934582,  12884901788,
            12884901788, 12884901788, 17179868184, 17179868184, 17179868184,
            21474826480, 21474826480, 21474826480, 21474826480, 25769703776,
            25769703776, 25769703776, 30063771072, 30063771072, 30063771072,
            34349738368, 34349738368, 34349738368, 34349738368, 38554705664,
            38554705664, 38554705664, 41949672960, 41949672960, 41949672960,
            42949672960, 42949672960
            };

#if NET7_0_OR_GREATER
        public static int WriteToSpanAsASCII<T>(this T number, Span<byte> destination) where T : IBinaryInteger<T>
        {
            if (T.IsZero(number))
            {
                destination[0] = (byte)'0';
                return 1;
            }

            bool isNegative = T.IsNegative(number);
            T absNumber = T.Abs(number);

            Span<byte> buffer = stackalloc byte[40];
            int index = buffer.Length;

            T ten = T.CreateChecked(10);

            do
            {
                (absNumber, var remainder) = T.DivRem(absNumber, ten);
                buffer[--index] = (byte)(byte.CreateTruncating(remainder) + '0');
            } while (!T.IsZero(absNumber));

            if (isNegative)
                buffer[--index] = (byte)'-';

            int length = buffer.Length - index;
            buffer.Slice(index, length).CopyTo(destination);
            return length;
        }
#endif

        public static int Digits(this Enum e) => Digits(Convert.ToInt32(e));

        public static int Digits(this short n)
        {
            if (n >= 0)
            {
                if (n < 10) return 1;
                if (n < 100) return 2;
                if (n < 1000) return 3;
                if (n < 10000) return 4;
                return 5;
            }
            else
            {
                if (n > -10) return 2;
                if (n > -100) return 3;
                if (n > -1000) return 4;
                if (n > -10000) return 5;
                return 6;
            }
        }

        public static int Digits(this ushort n)
        {
            if (n < 10) return 1;
            if (n < 100) return 2;
            if (n < 1000) return 3;
            if (n < 10000) return 4;
            return 5;
        }

        public static int Digits(this int n)
        {
            if (n >= 0)
            {
                if (n < 10) return 1;
                if (n < 100) return 2;
                if (n < 1000) return 3;
                if (n < 10000) return 4;
                if (n < 100000) return 5;
                if (n < 1000000) return 6;
                if (n < 10000000) return 7;
                if (n < 100000000) return 8;
                if (n < 1000000000) return 9;
                return 10;
            }
            else
            {
                if (n > -10) return 2;
                if (n > -100) return 3;
                if (n > -1000) return 4;
                if (n > -10000) return 5;
                if (n > -100000) return 6;
                if (n > -1000000) return 7;
                if (n > -10000000) return 8;
                if (n > -100000000) return 9;
                if (n > -1000000000) return 10;
                return 11;
            }
        }

        //  Old code used Math.Log(n, 2) -> -Infinity for n=0, overflowing uintTable.
        //  Restored server behaviour (Log2(0)=0) without net7-only uint.Log2 to keep net481.
        public static int Digits(this uint n) => (int)((n + uintTable[Log2(n)]) >> 32);

        static int Log2(uint n)
        {
            int r = 0;
            if (n >= 1u << 16) { r += 16; n >>= 16; }
            if (n >= 1u << 8) { r += 8; n >>= 8; }
            if (n >= 1u << 4) { r += 4; n >>= 4; }
            if (n >= 1u << 2) { r += 2; n >>= 2; }
            if (n >= 1u << 1) { r += 1; }
            return r;
        }

        public static int Digits(this long n)
        {
            if (n >= 0)
            {
                if (n < 10L) return 1;
                if (n < 100L) return 2;
                if (n < 1000L) return 3;
                if (n < 10000L) return 4;
                if (n < 100000L) return 5;
                if (n < 1000000L) return 6;
                if (n < 10000000L) return 7;
                if (n < 100000000L) return 8;
                if (n < 1000000000L) return 9;
                if (n < 10000000000L) return 10;
                if (n < 100000000000L) return 11;
                if (n < 1000000000000L) return 12;
                if (n < 10000000000000L) return 13;
                if (n < 100000000000000L) return 14;
                if (n < 1000000000000000L) return 15;
                if (n < 10000000000000000L) return 16;
                if (n < 100000000000000000L) return 17;
                if (n < 1000000000000000000L) return 18;
                return 19;
            }
            else
            {
                if (n > -10L) return 2;
                if (n > -100L) return 3;
                if (n > -1000L) return 4;
                if (n > -10000L) return 5;
                if (n > -100000L) return 6;
                if (n > -1000000L) return 7;
                if (n > -10000000L) return 8;
                if (n > -100000000L) return 9;
                if (n > -1000000000L) return 10;
                if (n > -10000000000L) return 11;
                if (n > -100000000000L) return 12;
                if (n > -1000000000000L) return 13;
                if (n > -10000000000000L) return 14;
                if (n > -100000000000000L) return 15;
                if (n > -1000000000000000L) return 16;
                if (n > -10000000000000000L) return 17;
                if (n > -100000000000000000L) return 18;
                if (n > -1000000000000000000L) return 19;
                return 20;
            }
        }

        public static int Digits(this ulong n)
        {
            if (n < 10UL) return 1;
            if (n < 100UL) return 2;
            if (n < 1000UL) return 3;
            if (n < 10000UL) return 4;
            if (n < 100000UL) return 5;
            if (n < 1000000UL) return 6;
            if (n < 10000000UL) return 7;
            if (n < 100000000UL) return 8;
            if (n < 1000000000UL) return 9;
            if (n < 10000000000UL) return 10;
            if (n < 100000000000UL) return 11;
            if (n < 1000000000000UL) return 12;
            if (n < 10000000000000UL) return 13;
            if (n < 100000000000000UL) return 14;
            if (n < 1000000000000000UL) return 15;
            if (n < 10000000000000000UL) return 16;
            if (n < 100000000000000000UL) return 17;
            if (n < 1000000000000000000UL) return 18;
            return 19;
        }
    }
}
