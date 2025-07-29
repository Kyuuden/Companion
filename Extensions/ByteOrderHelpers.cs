using System;

namespace FF.Rando.Companion.Extensions;

public static class ByteOrderHelpers
{
    public static ByteOrder Native
    {
        get
        {
            return BitConverter.IsLittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian;
        }
    }

    public static UInt32 ToByteOrder(this UInt32 v, ByteOrder order)
    {
        if ((order == ByteOrder.LittleEndian) == BitConverter.IsLittleEndian)
            return v;
        else
            return v.ReverseByteOrder();
    }

    public static UInt16 ToByteOrder(this UInt16 v, ByteOrder order)
    {
        if ((order == ByteOrder.LittleEndian) == BitConverter.IsLittleEndian)
            return v;
        else
            return v.ReverseByteOrder();
    }

    public static UInt32 ReverseByteOrder(this UInt32 v)
    {
        return ((v & 0x000000FF) << 24) |
               ((v & 0x0000FF00) << 8) |
               ((v & 0x00FF0000) >> 8) |
               ((v & 0xFF000000) >> 24);
    }

    public static UInt16 ReverseByteOrder(this UInt16 v)
    {
        return (UInt16)(
               ((v & 0x00FF) << 8) |
               ((v & 0xFF00) >> 8));
    }
}


