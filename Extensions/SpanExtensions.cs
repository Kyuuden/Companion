using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Extensions;

public static class SpanExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] array)
    {
        return new ReadOnlySpan<T>(array);
    }

    public static ushort ReadUShort(this ref Span<byte> span)
    {
        var ret = BinaryPrimitives.ReadUInt16LittleEndian(span);
        span = span.Slice(2);
        return ret;
    }

    public static byte ReadByte(this ref Span<byte> span)
    {
        var ret = span[0];
        span = span.Slice(1);
        return ret;
    }
}