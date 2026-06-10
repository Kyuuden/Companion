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

    public static uint ToByteOrder(this uint v, ByteOrder order)
    {
        if ((order == ByteOrder.LittleEndian) == BitConverter.IsLittleEndian)
            return v;
        else
            return v.ReverseByteOrder();
    }

    public static ushort ToByteOrder(this ushort v, ByteOrder order)
    {
        if ((order == ByteOrder.LittleEndian) == BitConverter.IsLittleEndian)
            return v;
        else
            return v.ReverseByteOrder();
    }

    public static uint ReverseByteOrder(this uint v)
    {
        return ((v & 0x000000FF) << 24) |
               ((v & 0x0000FF00) << 8) |
               ((v & 0x00FF0000) >> 8) |
               ((v & 0xFF000000) >> 24);
    }

    public static ushort ReverseByteOrder(this ushort v)
    {
        return (ushort)(
               ((v & 0x00FF) << 8) |
               ((v & 0xFF00) >> 8));
    }
}


