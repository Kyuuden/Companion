using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FF.Rando.Companion.Extensions;
public static class ByteArrayWritingExtensions
{
    public static void Write<T>(this byte[] b, T v, uint startBitIndex)
    {
        uint numBits = v switch
        {
            null => throw new ArgumentNullException(),
            bool => 1,
            string s => (uint)Encoding.ASCII.GetByteCount(s) * 8,
            _ when v.GetType().IsEnum => (uint)Marshal.SizeOf(Enum.GetUnderlyingType(typeof(T))) * 8,
            _ => (uint)(Marshal.SizeOf(typeof(T)) * 8)
        };

        Write(b, v, startBitIndex, numBits, null);
    }

    public static void Write<T>(this byte[] b, T v, uint startBitIndex, uint numBits)
    {
        Write(b, v, startBitIndex, numBits, null);
    }

    public static void Write<T>(this byte[] b, T v, uint startBitIndex, uint numBits, ByteOrder? order)
    {
        switch (v)
        {
            case null:
                throw new ArgumentNullException();
            case ushort uint16:
                b.WriteData(startBitIndex, numBits, order, BitConverter.GetBytes(uint16), ByteOrderHelpers.Native);
                break;
            case short int16:
                b.WriteData(startBitIndex, numBits, order, BitConverter.GetBytes(int16), ByteOrderHelpers.Native);
                break;
            case uint uint32:
                b.WriteData(startBitIndex, numBits, order, BitConverter.GetBytes(uint32), ByteOrderHelpers.Native);
                break;
            case int int32:
                b.WriteData(startBitIndex, numBits, order, BitConverter.GetBytes(int32), ByteOrderHelpers.Native);
                break;
            case ulong uint64:
                b.WriteData(startBitIndex, numBits, order, BitConverter.GetBytes(uint64), ByteOrderHelpers.Native);
                break;
            case long int64:
                b.WriteData(startBitIndex, numBits, order, BitConverter.GetBytes(int64), ByteOrderHelpers.Native);
                break;
            case byte uint8:
                b.WriteBits(startBitIndex, numBits, [uint8]);
                break;
            case sbyte int8:
                b.WriteBits(startBitIndex, numBits, [(byte)int8]);
                break;
            case bool boolean:
                b.WriteBits(startBitIndex, 1, [(byte)(boolean ? 1 : 0)]);
                break;
            case float f:
                b.WriteBits(startBitIndex, numBits, BitConverter.GetBytes(f));
                break;
            case byte[] array:
                if (array.Length * 8 != numBits)
                    throw new ArgumentException("byte array length doesn't match field width");

                b.WriteBits(startBitIndex, numBits, array);
                break;
            case string s:
                Write(b, Encoding.ASCII.GetBytes(s.PadRight((int)numBits / 8, '\0')), startBitIndex, numBits, order);
                break;
            case Enum:
                Type enumType = Enum.GetUnderlyingType(typeof(T));
                if (enumType.Equals(typeof(byte))) Write(b, (byte)(object)v, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(sbyte))) Write(b, (sbyte)(object)v, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(ushort))) Write(b, (ushort)(object)v, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(uint))) Write(b, (uint)(object)v, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(ulong))) Write(b, (ulong)(object)v, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(short))) Write(b, (short)(object)v, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(int))) Write(b, (int)(object)v, startBitIndex, numBits, order);
                if (enumType.Equals(typeof(long))) Write(b, (long)(object)v, startBitIndex, numBits, order);
                break;
            default:
                if (typeof(T).IsValueType)
                {
                    int size = Marshal.SizeOf(typeof(T));

                    if (size != numBits / 8)
                        throw new ArgumentException("struct size doesn't match field width");

                    byte[] buffer = new byte[size];
                    IntPtr ptr = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr((object)v, ptr, true);
                    Marshal.Copy(ptr, buffer, 0, size);
                    Marshal.FreeHGlobal(ptr);

                    Write(b, buffer, startBitIndex, numBits, order);
                }
                break;
        };
    }

    private static void WriteData(this byte[] b, uint outOffset, uint outWidth, ByteOrder? outOrder, byte[] data, ByteOrder? inOrder)
    {
        ByteArrayExtensionHelpers.MoveData(data, 0, (uint)(data.Length * 8), inOrder, b, outOffset, outWidth, outOrder);
    }

    private static void WriteBits(this byte[] b, uint outOffset, uint bitsRemaining, byte[] data)
    {
        ByteArrayExtensionHelpers.MoveBits(data, 0, (uint)(data.Length * 8), b, outOffset, bitsRemaining);
    }
}
