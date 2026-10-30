#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MinesServer.Utils;

public class BitArrayEqualityComparer : IEqualityComparer<BitArray>
{
    public static BitArrayEqualityComparer Default { get; } = new();

    public bool Equals(BitArray? x, BitArray? y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (x == null || y == null)
            return false;
        if (x!.Length != y!.Length)
            return false;

        return !new BitArray(x).Xor(y).Cast<bool>().Contains(true);
    }

    public int GetHashCode(BitArray obj)
    {
        if (obj == null)
            return 0;

        HashCode hash = new();
        hash.Add(obj.Length);

        int[] intArray = new int[(obj.Length + 31) / 32];
        obj.CopyTo(intArray, 0);

        for (int i = 0; i < intArray.Length; i++)
            hash.Add(intArray[i]);

        return hash.ToHashCode();
    }
}
