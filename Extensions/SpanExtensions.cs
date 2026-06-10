using System;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Extensions;

public static class SpanExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] array)
    {
        return new ReadOnlySpan<T>(array);
    }
}