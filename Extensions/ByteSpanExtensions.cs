using BizHawk.Common.CollectionExtensions;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace FF.Rando.Companion.Extensions;

public static class ByteSpanExtensions
{
    public static T Read<T>(this ReadOnlySpan<byte> span, int startBitIndex)
    {
        int numBits = (int)(typeof(T).Equals(typeof(bool)) ? 1 : Marshal.SizeOf(typeof(T)) * 8);
        return span.Read<T>(startBitIndex, numBits);
    }

    public static T Read<T>(this ReadOnlySpan<byte> span, int startBitIndex, int numBits)
    {
        if (typeof(T).Equals(typeof(ushort))) return (T)(object)BitConverter.ToUInt16(span.ReadData(startBitIndex, numBits, 16), 0);
        if (typeof(T).Equals(typeof(short))) return (T)(object)BitConverter.ToInt16(span.ReadData(startBitIndex, numBits, 16).SignExtend(numBits), 0);
        if (typeof(T).Equals(typeof(uint))) return (T)(object)BitConverter.ToUInt32(span.ReadData(startBitIndex, numBits, 32), 0);
        if (typeof(T).Equals(typeof(int))) return (T)(object)BitConverter.ToInt32(span.ReadData(startBitIndex, numBits, 32).SignExtend(numBits), 0);
        if (typeof(T).Equals(typeof(ulong))) return (T)(object)BitConverter.ToUInt64(span.ReadData(startBitIndex, numBits, 64), 0);
        if (typeof(T).Equals(typeof(long))) return (T)(object)BitConverter.ToInt64(span.ReadData(startBitIndex, numBits, 64).SignExtend(numBits), 0);
        if (typeof(T).Equals(typeof(byte))) return (T)(object)span.ReadBits(startBitIndex, numBits, 8)[0];
        if (typeof(T).Equals(typeof(sbyte))) return (T)(object)(sbyte)span.ReadBits(startBitIndex, numBits, 8).SignExtend(numBits)[0];
        if (typeof(T).Equals(typeof(bool))) return (T)(object)((span.ReadBits(startBitIndex, 1, 1)[0] & 0x01) == 0x01);
        if (typeof(T).Equals(typeof(float))) return (T)(object)BitConverter.ToSingle(span.ReadBits(startBitIndex, numBits, 32), 0);
        if (typeof(T).Equals(typeof(byte[]))) return (T)(object)span.ReadBits(startBitIndex, numBits, numBits);
        if (typeof(T).Equals(typeof(string))) return (T)(object)Encoding.ASCII.GetString(span.ReadBits(startBitIndex, numBits, numBits), 0, (int)numBits / 8).TrimEnd('\0');
        if (typeof(T).IsEnum)
        {
            Type enumType = Enum.GetUnderlyingType(typeof(T));
            if (enumType.Equals(typeof(byte))) return (T)(object)span.Read<byte>(startBitIndex, numBits);
            if (enumType.Equals(typeof(sbyte))) return (T)(object)span.Read<sbyte>(startBitIndex, numBits);
            if (enumType.Equals(typeof(ushort))) return (T)(object)span.Read<ushort>(startBitIndex, numBits);
            if (enumType.Equals(typeof(uint))) return (T)(object)span.Read<uint>(startBitIndex, numBits);
            if (enumType.Equals(typeof(ulong))) return (T)(object)span.Read<ulong>(startBitIndex, numBits);
            if (enumType.Equals(typeof(short))) return (T)(object)span.Read<short>(startBitIndex, numBits);
            if (enumType.Equals(typeof(int))) return (T)(object)span.Read<int>(startBitIndex, numBits);
            if (enumType.Equals(typeof(long))) return (T)(object)span.Read<long>(startBitIndex, numBits);
        }
        else if (typeof(T).IsValueType)
        {
            int size = Marshal.SizeOf(typeof(T));

            if (size != numBits / 8)
                throw new ArgumentException("struct size doesn't match field width");

            byte[] buffer = span.Read<byte[]>(startBitIndex, numBits);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(buffer, 0, ptr, size);
            T result = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);

            return result;
        }
        throw new NotSupportedException();
    }

    private static byte[] ReadData(this ReadOnlySpan<byte> b, int inOffset, int inWidth, int outWidth)
    {
        byte[] result = new byte[outWidth / 8 + (outWidth % 8 == 0 ? 0 : 1)];
        MoveBits(b, inOffset, inWidth, result, 0, outWidth);
        return result;
    }

    private static byte[] ReadBits(this ReadOnlySpan<byte> b, int inOffset, int bitsRemaining, int outputWidth)
    {
        byte[] result = new byte[outputWidth / 8 + (outputWidth % 8 == 0 ? 0 : 1)];
        MoveBits(b, inOffset, bitsRemaining, result, 0, outputWidth);
        return result;
    }

    private static void MoveBits(this ReadOnlySpan<byte> input, int inOffset, int inWidth, byte[] output, int outOffset, int outWidth)
    {
        if (inOffset % 8 == 0 && outOffset % 8 == 0 && inWidth % 8 == 0 && outWidth % 8 == 0)
        {
            input.Slice(inOffset / 8, (int)Math.Min(inWidth / 8, outWidth / 8)).ToArray().CopyTo(output, outOffset);
            return;
        }

        int remaining = Math.Min(inWidth, outWidth);

        while (remaining > 0)
        {
            int inByte = inOffset / 8;
            byte inBit = (byte)(inOffset % 8);
            int outByte = outOffset / 8;
            byte outBit = (byte)(outOffset % 8);
            int chunkSize = (int)Math.Min(remaining, Math.Min(8 - inBit, 8 - outBit));

            output[outByte] &= (byte)~(BitMasks8[chunkSize] << outBit);
            output[outByte] |= (byte)((input[inByte] >> inBit & BitMasks8[chunkSize]) << outBit);

            remaining -= chunkSize;
            outOffset += chunkSize;
            inOffset += chunkSize;
        }
    }

    private static readonly byte[] BitMasks8 = [0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F, 0xFF];

    private static byte[] SignExtend(this byte[] b, int valueWidth)
    {
        byte[] BitMasks = [0xFF, 0xFE, 0xFC, 0xF8, 0xF0, 0xE0, 0xC0, 0x80, 0x00];

        if (valueWidth == b.Length * 8) // no room to extend
            return b;

        if (valueWidth == 0)
            return b;

        int highBitByteOffset = (int)((valueWidth - 1) / 8);
        int highBitBitOffset = (int)((valueWidth - 1) % 8);

        if ((b[highBitByteOffset] >> highBitBitOffset & 0x01) == 0x00) // no need to extend (it's positive)
            return b;

        int byteOffset = (int)(valueWidth / 8);
        int bitOffset = (int)(valueWidth % 8);

        b[byteOffset++] |= BitMasks[bitOffset]; // extend the first byte
        while (byteOffset < b.Length)
            b[byteOffset++] = 0xFF; // extend the remaining bytes

        return b;
    }

    public static Palette DecodePalette(this ReadOnlySpan<byte> paletteData, Color32? colorZero = null)
    {
        bool first = true;
        List<Color32> colors = [];
        foreach (var color in MemoryMarshal.Cast<byte, ushort>(paletteData))
        {
            if (first && colorZero.HasValue)
            {
                colors.Add(colorZero.Value);
                first = false;
            }
            else
            {
                colors.Add(color.ToColor());
            }
        }
        return new Palette(colors);
    }

    public static Palette DecodePalette(this Span<byte> paletteData, Color32? colorZero = null)
    {
        bool first = true;
        List<Color32> colors = [];
        foreach (var color in MemoryMarshal.Cast<byte, ushort>(paletteData))
        {
            if (first && colorZero.HasValue)
            {
                colors.Add(colorZero.Value);
                first = false;
            }
            else
            {
                colors.Add(color.ToColor());
            }
        }
        return new Palette(colors);
    }

    public static byte[,] DecodeTile(this Span<byte> data, int bitsPerPixel, uint width = 8, uint height = 8)
    {
        if (width % 8 != 0 || height % 8 != 0)
            throw new InvalidOperationException("Dimensions must be multiple of 8");

        var tile = new byte[width, height];
        uint x = 0;
        uint y = 0;
        int bitPlane3Offset = (int)(width / 4 * height);


        switch (bitsPerPixel)
        {
            case 1:
                foreach (var row in data)
                {
                    for (var p = 0; p < 8; p++)
                    {
                        tile[x + p, y] = row.GetPixel(p);
                    }

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }
                break;
            case 2:
                foreach (var row in MemoryMarshal.Cast<byte, ushort>(data))
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] = row.GetPixel(p);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }
                break;
            case 3:
                foreach (var row in MemoryMarshal.Cast<byte, ushort>(data[..bitPlane3Offset]))
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] = row.GetPixel(p);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }

                x = y = 0;
                foreach (var row in data[bitPlane3Offset..])
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] += (byte)(row.GetPixel(p) << 2);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }
                break;

            case 4:
                foreach (var row in MemoryMarshal.Cast<byte, ushort>(data[..bitPlane3Offset]))
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] = row.GetPixel(p);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }

                x = y = 0;
                foreach (var row in MemoryMarshal.Cast<byte, ushort>(data[bitPlane3Offset..]))
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] |= (byte)(row.GetPixel(p) << 2);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }

                break;
            default:
                throw new InvalidOperationException();
        }

        return tile;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte GetPixel(this ushort spriteRow, int x)
    => (byte)(((spriteRow >> (7 - x)) & 0x01) | ((spriteRow >> (14 - x)) & 0x02));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte GetPixel(this byte spriteRow, int x)
        => (byte)((spriteRow >> (7 - x)) & 0x01);
}

