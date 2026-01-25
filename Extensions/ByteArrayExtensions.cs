using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FF.Rando.Companion.Extensions;

/// <summary>
/// Adds abilities to read/write from a byte array at arbitrary bit indexes
/// and arbitrary bit widths.  All methods write/read little endian.
/// </summary>
public static class ByteArrayExtensions
{
    public static T Read<T>(this byte[] b, uint startBitIndex)
    {
        uint numBits = (uint)(typeof(T).Equals(typeof(bool)) ? 1 : Marshal.SizeOf(typeof(T)) * 8);
        return b.Read<T>(startBitIndex, numBits, null);
    }

    public static T Read<T>(this byte[] b, uint startBitIndex, uint numBits)
    {
        return b.Read<T>(startBitIndex, numBits, null);
    }

    public static T Read<T>(this byte[] b, uint startBitIndex, uint numBits, ByteOrder? order)
    {
        if (typeof(T).Equals(typeof(ushort))) return (T)(object)BitConverter.ToUInt16(b.ReadData(startBitIndex, numBits, order, 16, ByteOrderHelpers.Native), 0);
        if (typeof(T).Equals(typeof(short))) return (T)(object)BitConverter.ToInt16(b.ReadData(startBitIndex, numBits, order, 16, ByteOrderHelpers.Native).SignExtend(numBits, ByteOrderHelpers.Native), 0);
        if (typeof(T).Equals(typeof(uint))) return (T)(object)BitConverter.ToUInt32(b.ReadData(startBitIndex, numBits, order, 32, ByteOrderHelpers.Native), 0);
        if (typeof(T).Equals(typeof(int))) return (T)(object)BitConverter.ToInt32(b.ReadData(startBitIndex, numBits, order, 32, ByteOrderHelpers.Native).SignExtend(numBits, ByteOrderHelpers.Native), 0);
        if (typeof(T).Equals(typeof(ulong))) return (T)(object)BitConverter.ToUInt64(b.ReadData(startBitIndex, numBits, order, 64, ByteOrderHelpers.Native), 0);
        if (typeof(T).Equals(typeof(long))) return (T)(object)BitConverter.ToInt64(b.ReadData(startBitIndex, numBits, order, 64, ByteOrderHelpers.Native).SignExtend(numBits, ByteOrderHelpers.Native), 0);
        if (typeof(T).Equals(typeof(byte))) return (T)(object)b.ReadBits(startBitIndex, numBits, 8)[0];
        if (typeof(T).Equals(typeof(sbyte))) return (T)(object)(sbyte)b.ReadBits(startBitIndex, numBits, 8).SignExtend(numBits, null)[0];
        if (typeof(T).Equals(typeof(bool))) return (T)(object)((b.ReadBits(startBitIndex, 1, 1)[0] & 0x01) == 0x01);
        if (typeof(T).Equals(typeof(float))) return (T)(object)BitConverter.ToSingle(b.ReadBits(startBitIndex, numBits, 32), 0);
        if (typeof(T).Equals(typeof(byte[]))) return (T)(object)b.ReadBits(startBitIndex, numBits, numBits);
        if (typeof(T).Equals(typeof(string))) return (T)(object)Encoding.ASCII.GetString(b.ReadBits(startBitIndex, numBits, numBits), 0, (int)numBits / 8).TrimEnd('\0');
        if (typeof(T).IsEnum)
        {
            Type enumType = Enum.GetUnderlyingType(typeof(T));
            if (enumType.Equals(typeof(byte))) return (T)(object)b.Read<byte>(startBitIndex, numBits, order);
            if (enumType.Equals(typeof(sbyte))) return (T)(object)b.Read<sbyte>(startBitIndex, numBits, order);
            if (enumType.Equals(typeof(ushort))) return (T)(object)b.Read<ushort>(startBitIndex, numBits, order);
            if (enumType.Equals(typeof(uint))) return (T)(object)b.Read<uint>(startBitIndex, numBits, order);
            if (enumType.Equals(typeof(ulong))) return (T)(object)b.Read<ulong>(startBitIndex, numBits, order);
            if (enumType.Equals(typeof(short))) return (T)(object)b.Read<short>(startBitIndex, numBits, order);
            if (enumType.Equals(typeof(int))) return (T)(object)b.Read<int>(startBitIndex, numBits, order);
            if (enumType.Equals(typeof(long))) return (T)(object)b.Read<long>(startBitIndex, numBits, order);
        }
        else if (typeof(T).IsValueType)
        {
            int size = Marshal.SizeOf(typeof(T));

            if (size != numBits / 8)
                throw new ArgumentException("struct size doesn't match field width");

            byte[] buffer = b.Read<byte[]>(startBitIndex, numBits, order);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(buffer, 0, ptr, size);
            T result = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);

            return result;
        }
        throw new NotSupportedException();
    }

    public static IEnumerable<T> ReadMany<T>(this byte[] b, uint elementBits)
    {
        return ReadMany<T>(b, 0, elementBits, b.Length / ((int)elementBits >> 3));
    }

    public static IEnumerable<T> ReadMany<T>(this byte[] b, uint startBitIndex, uint elementBits, int count)
    {
        return b.ReadMany<T>(startBitIndex, elementBits, count, null);
    }

    public static IEnumerable<T> ReadMany<T>(this byte[] b, uint startBitIndex, uint elementBits, int count, ByteOrder? order)
    {
        return Enumerable.Range(0, count).Select(i => b.Read<T>((uint)(startBitIndex + elementBits * i), elementBits, order)).ToArray();
    }

    public static ulong ReadBCD(this byte[] b, uint startByteIndex, uint numBytes)
    {
        return ulong.Parse(new string(b.Read<byte[]>(startByteIndex * 8, numBytes * 8).SelectMany(bcd => bcd.ToString("X2")).ToArray()));
    }

    public static byte[,] DecodeTile(this byte[] data, int bitsPerPixel, uint width = 8, uint height = 8)
    {
        if (width % 8 != 0 || height % 8 != 0)
            throw new InvalidOperationException("Dimensions must be multiple of 8");

        var tile = new byte[width, height];
        var spanData = data.AsSpan();
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
                foreach (var row in MemoryMarshal.Cast<byte, ushort>(spanData))
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] = row.GetPixel(p);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }
                break;
            case 3:
                foreach (var row in MemoryMarshal.Cast<byte, ushort>(spanData[..bitPlane3Offset]))
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] = row.GetPixel(p);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }

                x = y = 0;
                foreach (var row in spanData[bitPlane3Offset..])
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] += (byte)(row.GetPixel(p) << 2);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }
                break;

            case 4:
                foreach (var row in MemoryMarshal.Cast<byte, ushort>(spanData[..bitPlane3Offset]))
                {
                    for (var p = 0; p < 8; p++)
                        tile[x + p, y] = row.GetPixel(p);

                    y += (x + 8) / width;
                    x = (x + 8) % width;
                }

                x = y = 0;
                foreach (var row in MemoryMarshal.Cast<byte, ushort>(spanData[bitPlane3Offset..]))
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

    private static byte GetPixel(this ushort spriteRow, int x)
        => (byte)(((spriteRow >> (7 - x)) & 0x01) | ((spriteRow >> (14 - x)) & 0x02));

    private static byte GetPixel(this byte spriteRow, int x)
        => (byte)((spriteRow >> (7 - x)) & 0x01);

    public static Palette DecodePalette(this byte[] paletteData, Color32? colorZero = null)
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

    private static byte[] SignExtend(this byte[] b, uint valueWidth, ByteOrder? order)
    {
        byte[] BitMasks = [0xFF, 0xFE, 0xFC, 0xF8, 0xF0, 0xE0, 0xC0, 0x80, 0x00];

        order ??= ByteOrder.LittleEndian;

        if (valueWidth == b.Length * 8) // no room to extend
            return b;

        if (valueWidth == 0)
            return b;

        if (order.Value == ByteOrder.LittleEndian)
        {
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
        else
        {
            int highBitByteOffset = b.Length - (int)((valueWidth - 1) / 8) - 1;
            int highBitBitOffset = (int)((valueWidth - 1) % 8);

            if ((b[highBitByteOffset] >> highBitBitOffset & 0x01) == 0x00) // no need to extend (it's positive)
                return b;

            int byteOffset = b.Length - (int)(valueWidth / 8) - 1;
            int bitOffset = (int)(valueWidth % 8);

            b[byteOffset--] |= BitMasks[bitOffset]; // extend the first byte
            while (byteOffset >= 0)
                b[byteOffset--] = 0xFF; // extend the remaining bytes

            return b;
        }
    }

    private static byte[] ReadData(this byte[] b, uint inOffset, uint inWidth, ByteOrder? inOrder, uint outWidth, ByteOrder? outOrder)
    {
        byte[] result = new byte[outWidth / 8 + (outWidth % 8 == 0 ? 0 : 1)];
        MoveData(b, inOffset, inWidth, inOrder, result, 0, outWidth, outOrder);
        return result;
    }

    private static void MoveData(byte[] input, uint inOffset, uint inWidth, ByteOrder? inOrder, byte[] output, uint outOffset, uint outWidth, ByteOrder? outOrder)
    {
        inOrder ??= ByteOrder.LittleEndian;
        outOrder ??= ByteOrder.LittleEndian;

        if (inOrder.Value == ByteOrder.LittleEndian && outOrder.Value == ByteOrder.LittleEndian)
        {
            MoveBits(input, inOffset, inWidth, output, outOffset, outWidth);
        }
        else if (inOrder.Value == ByteOrder.BigEndian && outOrder.Value == ByteOrder.BigEndian)
        {
            if (outWidth > inWidth)
                MoveBits(input, inOffset, inWidth, output, outOffset + (outWidth - inWidth), outWidth);
            else
                MoveBits(input, inOffset + (inWidth - outWidth), inWidth, output, outOffset, outWidth);
        }
        else if (inOrder.Value == ByteOrder.LittleEndian && outOrder.Value == ByteOrder.BigEndian)
        {
            while (inWidth > 0 && outWidth > 0)
            {
                uint chunkSize = Math.Min(inWidth, 8);

                MoveBits(input, inOffset, chunkSize, output, outOffset + outWidth - chunkSize, chunkSize);

                inWidth -= chunkSize;
                outWidth -= chunkSize;
                inOffset += chunkSize;
            }
        }
        else if (inOrder.Value == ByteOrder.BigEndian && outOrder.Value == ByteOrder.LittleEndian)
        {
            while (inWidth > 0 && outWidth > 0)
            {
                uint chunkSize = Math.Min(inWidth, 8);

                MoveBits(input, inOffset + inWidth - chunkSize, chunkSize, output, outOffset, chunkSize);

                inWidth -= chunkSize;
                outWidth -= chunkSize;
                outOffset += chunkSize;
            }
        }
    }

    private static byte[] ReadBits(this byte[] b, uint inOffset, uint bitsRemaining, uint outputWidth)
    {
        byte[] result = new byte[outputWidth / 8 + (outputWidth % 8 == 0 ? 0 : 1)];
        MoveBits(b, inOffset, bitsRemaining, result, 0, outputWidth);
        return result;
    }

    private static void MoveBits(byte[] input, uint inOffset, uint inWidth, byte[] output, uint outOffset, uint outWidth)
    {
        if (inOffset % 8 == 0 && outOffset % 8 == 0 && inWidth % 8 == 0 && outWidth % 8 == 0)
        {
            Array.Copy(input, (int)inOffset / 8, output, (int)outOffset / 8, (int)Math.Min(inWidth / 8, outWidth / 8));
            return;
        }

        uint remaining = Math.Min(inWidth, outWidth);

        while (remaining > 0)
        {
            uint inByte = inOffset / 8;
            byte inBit = (byte)(inOffset % 8);
            uint outByte = outOffset / 8;
            byte outBit = (byte)(outOffset % 8);
            uint chunkSize = (uint)Math.Min(remaining, Math.Min(8 - inBit, 8 - outBit));

            output[outByte] &= (byte)~(BitMasks8[chunkSize] << outBit);
            output[outByte] |= (byte)((input[inByte] >> inBit & BitMasks8[chunkSize]) << outBit);

            remaining -= chunkSize;
            outOffset += chunkSize;
            inOffset += chunkSize;
        }
    }

    private static readonly byte[] BitMasks8 = [0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F, 0xFF];

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

    public static byte ReverseBitOrder(this byte b) => BitReverseTable[b];

    private static Dictionary<byte, int>? bitCounts;

    private static readonly byte[] BitReverseTable =
    [
        0x00, 0x80, 0x40, 0xc0, 0x20, 0xa0, 0x60, 0xe0,
        0x10, 0x90, 0x50, 0xd0, 0x30, 0xb0, 0x70, 0xf0,
        0x08, 0x88, 0x48, 0xc8, 0x28, 0xa8, 0x68, 0xe8,
        0x18, 0x98, 0x58, 0xd8, 0x38, 0xb8, 0x78, 0xf8,
        0x04, 0x84, 0x44, 0xc4, 0x24, 0xa4, 0x64, 0xe4,
        0x14, 0x94, 0x54, 0xd4, 0x34, 0xb4, 0x74, 0xf4,
        0x0c, 0x8c, 0x4c, 0xcc, 0x2c, 0xac, 0x6c, 0xec,
        0x1c, 0x9c, 0x5c, 0xdc, 0x3c, 0xbc, 0x7c, 0xfc,
        0x02, 0x82, 0x42, 0xc2, 0x22, 0xa2, 0x62, 0xe2,
        0x12, 0x92, 0x52, 0xd2, 0x32, 0xb2, 0x72, 0xf2,
        0x0a, 0x8a, 0x4a, 0xca, 0x2a, 0xaa, 0x6a, 0xea,
        0x1a, 0x9a, 0x5a, 0xda, 0x3a, 0xba, 0x7a, 0xfa,
        0x06, 0x86, 0x46, 0xc6, 0x26, 0xa6, 0x66, 0xe6,
        0x16, 0x96, 0x56, 0xd6, 0x36, 0xb6, 0x76, 0xf6,
        0x0e, 0x8e, 0x4e, 0xce, 0x2e, 0xae, 0x6e, 0xee,
        0x1e, 0x9e, 0x5e, 0xde, 0x3e, 0xbe, 0x7e, 0xfe,
        0x01, 0x81, 0x41, 0xc1, 0x21, 0xa1, 0x61, 0xe1,
        0x11, 0x91, 0x51, 0xd1, 0x31, 0xb1, 0x71, 0xf1,
        0x09, 0x89, 0x49, 0xc9, 0x29, 0xa9, 0x69, 0xe9,
        0x19, 0x99, 0x59, 0xd9, 0x39, 0xb9, 0x79, 0xf9,
        0x05, 0x85, 0x45, 0xc5, 0x25, 0xa5, 0x65, 0xe5,
        0x15, 0x95, 0x55, 0xd5, 0x35, 0xb5, 0x75, 0xf5,
        0x0d, 0x8d, 0x4d, 0xcd, 0x2d, 0xad, 0x6d, 0xed,
        0x1d, 0x9d, 0x5d, 0xdd, 0x3d, 0xbd, 0x7d, 0xfd,
        0x03, 0x83, 0x43, 0xc3, 0x23, 0xa3, 0x63, 0xe3,
        0x13, 0x93, 0x53, 0xd3, 0x33, 0xb3, 0x73, 0xf3,
        0x0b, 0x8b, 0x4b, 0xcb, 0x2b, 0xab, 0x6b, 0xeb,
        0x1b, 0x9b, 0x5b, 0xdb, 0x3b, 0xbb, 0x7b, 0xfb,
        0x07, 0x87, 0x47, 0xc7, 0x27, 0xa7, 0x67, 0xe7,
        0x17, 0x97, 0x57, 0xd7, 0x37, 0xb7, 0x77, 0xf7,
        0x0f, 0x8f, 0x4f, 0xcf, 0x2f, 0xaf, 0x6f, 0xef,
        0x1f, 0x9f, 0x5f, 0xdf, 0x3f, 0xbf, 0x7f, 0xff
    ];
}

