using System.Collections.Generic;

namespace FF.Rando.Companion.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Yield<T>(this T obj)
    {
        yield return obj;
    }
}

