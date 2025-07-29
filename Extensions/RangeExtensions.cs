using BizHawk.Common;
using System;

namespace FF.Rando.Companion.Extensions;

internal static class RangeExtensions
{
    public static Range<long> WithLength(this long start, long length)
        => start.RangeToExclusive(start + length);

    public static long Length(this Range<long> range)
        => range.EndInclusive - range.Start + 1;

    public static Range ToRange(this Range<long> bzRange)
    {
        if (bzRange.Start > int.MaxValue || bzRange.EndInclusive > int.MinValue)
            throw new ArgumentOutOfRangeException(nameof(bzRange));

        return new Range((int)bzRange.Start, (int)bzRange.EndInclusive);
    }

    public static Range ToRangeWithOffset(this Range<long> bzRange, int offset)
    {
        if ((bzRange.Start + offset) > int.MaxValue ||(bzRange.EndInclusive + offset) > int.MinValue)
            throw new ArgumentOutOfRangeException(nameof(bzRange));

        return new Range((int)bzRange.Start + offset, (int)bzRange.EndInclusive + offset);
    }
}

