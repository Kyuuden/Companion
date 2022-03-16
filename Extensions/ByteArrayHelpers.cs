using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BizHawk.FreeEnterprise.Companion.Extensions
{
    /// <summary>
    /// Adds abilities to read/write from a byte array at arbitrary bit indexes
    /// and arbitrary bit widths.  All methods write/read little endian.
    /// </summary>
    public static class ByteArrayHelpers
    {
        public static T Read<T>(this byte[] b, UInt32 startBitIndex)
        {
            UInt32 numBits = (UInt32)((typeof(T).Equals(typeof(bool))) ? 1 : (Marshal.SizeOf(typeof(T)) * 8));
            return Read<T>(b, startBitIndex, numBits, null);
        }

        public static T Read<T>(this byte[] b, UInt32 startBitIndex, UInt32 numBits)
        {
            return Read<T>(b, startBitIndex, numBits, null);
        }

        public static T Read<T>(this byte[] b, UInt32 startBitIndex, UInt32 numBits, ByteOrder? order)
        {
            if (typeof(T).Equals(typeof(UInt16))) return (T)(object)BitConverter.ToUInt16(b.ReadData(startBitIndex, numBits, order, 16, ByteOrderHelpers.Native), 0);
            if (typeof(T).Equals(typeof(Int16))) return (T)(object)BitConverter.ToInt16(b.ReadData(startBitIndex, numBits, order, 16, ByteOrderHelpers.Native).SignExtend(numBits, ByteOrderHelpers.Native), 0);
            if (typeof(T).Equals(typeof(UInt32))) return (T)(object)BitConverter.ToUInt32(b.ReadData(startBitIndex, numBits, order, 32, ByteOrderHelpers.Native), 0);
            if (typeof(T).Equals(typeof(Int32))) return (T)(object)BitConverter.ToInt32(b.ReadData(startBitIndex, numBits, order, 32, ByteOrderHelpers.Native).SignExtend(numBits, ByteOrderHelpers.Native), 0);
            if (typeof(T).Equals(typeof(UInt64))) return (T)(object)BitConverter.ToUInt64(b.ReadData(startBitIndex, numBits, order, 64, ByteOrderHelpers.Native), 0);
            if (typeof(T).Equals(typeof(Int64))) return (T)(object)BitConverter.ToInt64(b.ReadData(startBitIndex, numBits, order, 64, ByteOrderHelpers.Native).SignExtend(numBits, ByteOrderHelpers.Native), 0);
            if (typeof(T).Equals(typeof(byte))) return (T)(object)b.ReadBits(startBitIndex, numBits, 8)[0];
            if (typeof(T).Equals(typeof(sbyte))) return (T)(object)(sbyte)b.ReadBits(startBitIndex, numBits, 8).SignExtend(numBits, null)[0];
            if (typeof(T).Equals(typeof(bool))) return (T)(object)((b.ReadBits(startBitIndex, 1, 1)[0] & 0x01) == 0x01);
            if (typeof(T).Equals(typeof(float))) return (T)(object)BitConverter.ToSingle(b.ReadBits(startBitIndex, numBits, 32), 0);
            if (typeof(T).Equals(typeof(byte[]))) return (T)(object)b.ReadBits(startBitIndex, numBits, numBits);
            if (typeof(T).Equals(typeof(string))) return (T)(object)Encoding.ASCII.GetString(b.ReadBits(startBitIndex, numBits, numBits), 0, (Int32)numBits / 8).TrimEnd('\0');
            if (typeof(T).IsEnum)
            {
                Type enumType = Enum.GetUnderlyingType(typeof(T));
                if (enumType.Equals(typeof(byte))) return (T)(object)Read<byte>(b, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(sbyte))) return (T)(object)Read<sbyte>(b, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(UInt16))) return (T)(object)Read<UInt16>(b, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(UInt32))) return (T)(object)Read<UInt32>(b, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(UInt64))) return (T)(object)Read<UInt64>(b, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(Int16))) return (T)(object)Read<Int16>(b, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(Int32))) return (T)(object)Read<Int32>(b, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(Int64))) return (T)(object)Read<Int64>(b, startBitIndex, numBits, order);
            }
            else if (typeof(T).IsValueType)
            {
                int size = Marshal.SizeOf(typeof(T));

                if (size != numBits / 8)
                    throw new ArgumentException("struct size doesn't match field width");

                byte[] buffer = Read<byte[]>(b, startBitIndex, numBits, order);
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.Copy(buffer, 0, ptr, size);
                T result = (T)Marshal.PtrToStructure(ptr, typeof(T));
                Marshal.FreeHGlobal(ptr);

                return result;
            }
            throw new NotSupportedException();
        }

        public static IEnumerable<T> ReadMany<T>(this byte[] b, UInt32 startBitIndex, UInt32 elementBits, int count)
        {
            return ReadMany<T>(b, startBitIndex, elementBits, count, null);
        }

        public static IEnumerable<T> ReadMany<T>(this byte[] b, UInt32 startBitIndex, UInt32 elementBits, int count, ByteOrder? order)
        {
            return Enumerable.Range(0, count).Select(i => b.Read<T>((UInt32)(startBitIndex + (elementBits * i)), elementBits, order)).ToArray();
        }

        public static UInt64 ReadBCD(this byte[] b, UInt32 startByteIndex, UInt32 numBytes)
        {
            return UInt64.Parse(new String(b.Read<byte[]>(startByteIndex * 8, numBytes * 8).SelectMany(bcd => bcd.ToString("X2")).ToArray()));
        }

        private static byte[] SignExtend(this byte[] b, UInt32 valueWidth, ByteOrder? order)
        {
            byte[] BitMasks = { 0xFF, 0xFE, 0xFC, 0xF8, 0xF0, 0xE0, 0xC0, 0x80, 0x00 };

            order = order ?? ByteOrder.LittleEndian;

            if (valueWidth == b.Length * 8) // no room to extend
                return b;

            if (valueWidth == 0)
                return b;

            if (order.Value == ByteOrder.LittleEndian)
            {
                int highBitByteOffset = (int)((valueWidth - 1) / 8);
                int highBitBitOffset = (int)((valueWidth - 1) % 8);

                if (((b[highBitByteOffset] >> highBitBitOffset) & 0x01) == 0x00) // no need to extend (it's positive)
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

                if (((b[highBitByteOffset] >> highBitBitOffset) & 0x01) == 0x00) // no need to extend (it's positive)
                    return b;

                int byteOffset = b.Length - (int)(valueWidth / 8) - 1;
                int bitOffset = (int)(valueWidth % 8);

                b[byteOffset--] |= BitMasks[bitOffset]; // extend the first byte
                while (byteOffset >= 0)
                    b[byteOffset--] = 0xFF; // extend the remaining bytes

                return b;
            }
        }

        private static byte[] ReadData(this byte[] b, UInt32 inOffset, UInt32 inWidth, ByteOrder? inOrder, UInt32 outWidth, ByteOrder? outOrder)
        {
            byte[] result = new byte[outWidth / 8 + ((outWidth % 8 == 0) ? 0 : 1)];
            ByteArrayHelpers.MoveData(b, inOffset, inWidth, inOrder, result, 0, outWidth, outOrder);
            return result;
        }

        private static void MoveData(byte[] input, UInt32 inOffset, UInt32 inWidth, ByteOrder? inOrder, byte[] output, UInt32 outOffset, UInt32 outWidth, ByteOrder? outOrder)
        {
            inOrder = inOrder ?? ByteOrder.LittleEndian;
            outOrder = outOrder ?? ByteOrder.LittleEndian;

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
                    UInt32 chunkSize = (UInt32)Math.Min(inWidth, 8);

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
                    UInt32 chunkSize = (UInt32)Math.Min(inWidth, 8);

                    MoveBits(input, inOffset + inWidth - chunkSize, chunkSize, output, outOffset, chunkSize);

                    inWidth -= chunkSize;
                    outWidth -= chunkSize;
                    outOffset += chunkSize;
                }
            }
        }

        private static byte[] ReadBits(this byte[] b, UInt32 inOffset, UInt32 bitsRemaining, UInt32 outputWidth)
        {
            byte[] result = new byte[outputWidth / 8 + ((outputWidth % 8 == 0) ? 0 : 1)];
            ByteArrayHelpers.MoveBits(b, inOffset, bitsRemaining, result, 0, outputWidth);
            return result;
        }

        private static void MoveBits(byte[] input, UInt32 inOffset, UInt32 inWidth, byte[] output, UInt32 outOffset, UInt32 outWidth)
        {
            if (inOffset % 8 == 0 && outOffset % 8 == 0 && inWidth % 8 == 0 && outWidth % 8 == 0)
            {
                Array.Copy(input, (int)inOffset / 8, output, (int)outOffset / 8, (int)Math.Min(inWidth / 8, outWidth / 8));
                return;
            }

            UInt32 remaining = Math.Min(inWidth, outWidth);

            while (remaining > 0)
            {
                UInt32 inByte = inOffset / 8;
                byte inBit = (byte)(inOffset % 8);
                UInt32 outByte = outOffset / 8;
                byte outBit = (byte)(outOffset % 8);
                UInt32 chunkSize = (UInt32)Math.Min(remaining, Math.Min(8 - inBit, 8 - outBit));

                output[outByte] &= (byte)~(BitMasks8[chunkSize] << outBit);
                output[outByte] |= (byte)(((input[inByte] >> inBit) & BitMasks8[chunkSize]) << outBit);

                remaining -= chunkSize;
                outOffset += chunkSize;
                inOffset += chunkSize;
            }
        }

        private static byte[] BitMasks8 = { 0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F, 0xFF };
    }
}

