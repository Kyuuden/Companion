using BizHawk.Common;
using System;

namespace FF.Rando.Companion.Extensions;

internal static class RangeExtensions
{
    public static Range<long> WithLength(this long start, long length)
        => start.RangeToExclusive(start + length);

    public static long Length(this Range<long> range)
        => range.EndInclusive - range.Start + 1;
}

