using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Extensions;

public static class ByteArrayExtensions
{
    private static Dictionary<byte, int>? bitCounts;

    public static int CountBits(this byte[] bytes)
    {
        bitCounts ??= GenerateBitCounts();
        return bytes.Sum(b => bitCounts[b]);
    }

    private static Dictionary<byte, int> GenerateBitCounts()
    {
        Dictionary<byte, int> counts = [];

        for (int i = 0; i < 256; i++)
        {
            if (i == 0)
                counts[(byte)i] = 0;
            else
                counts[(byte)i] = counts[(byte)(i >> 1)] + (i & 1);
        }

        return counts;
    }
}
