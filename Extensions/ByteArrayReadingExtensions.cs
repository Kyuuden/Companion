using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FF.Rando.Companion.Extensions;
public static class ByteArrayReadingExtensions
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

    private static byte[] ReadData(this byte[] b, uint inOffset, uint inWidth, ByteOrder? inOrder, uint outWidth, ByteOrder? outOrder)
    {
        byte[] result = new byte[outWidth / 8 + (outWidth % 8 == 0 ? 0 : 1)];
        ByteArrayExtensionHelpers.MoveData(b, inOffset, inWidth, inOrder, result, 0, outWidth, outOrder);
        return result;
    }

    private static byte[] ReadBits(this byte[] b, uint inOffset, uint bitsRemaining, uint outputWidth)
    {
        byte[] result = new byte[outputWidth / 8 + (outputWidth % 8 == 0 ? 0 : 1)];
        ByteArrayExtensionHelpers.MoveBits(b, inOffset, bitsRemaining, result, 0, outputWidth);
        return result;
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
}
