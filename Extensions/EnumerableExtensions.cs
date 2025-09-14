using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Yield<T>(this T obj)
    {
        yield return obj;
    }

    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
    {
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value));
    }
}

