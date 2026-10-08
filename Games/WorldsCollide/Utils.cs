using System;
using System.Buffers.Binary;

namespace FF.Rando.Companion.Games.WorldsCollide;

public static class Utils
{
    private static void EnsureSpace(ref byte[] data, int length)
    {
        if (data.Length > length) return;

        var newBuffer = new byte[data.Length * 2];

        data.AsSpan().CopyTo(newBuffer.AsSpan());
        data = newBuffer;
    }

    public static byte[] Decompress(ReadOnlySpan<byte> data)
    {
        byte[] temp = new byte[data.Length];
        int tempPtr = 0;
        byte[] buf2 = new byte[2048];
        byte n, x, b;
        uint size, w, num, i;
        int bpos, bpos2;
        uint finalCount = 0;
        size = BinaryPrimitives.ReadUInt16LittleEndian(data);
        bpos = 2; bpos2 = 2014;
        do
        {
            n = data[bpos]; bpos++;
            for (x = 0; x < 8; x++)
            {
                if (((n >> x) & 1) == 1)
                {
                    b = data[bpos]; 
                    bpos++;
                    EnsureSpace(ref temp, tempPtr + 1);
                    temp[tempPtr++] = b;
                    finalCount++;
                    buf2[bpos2 & 2047] = b; bpos2++;
                }
                else
                {
                    w = (uint)(data[bpos] + (data[bpos + 1] << 8));
                    bpos += 2;
                    num = (w >> 11) + 3;
                    w &= 2047;
                    for (i = 0; i < num; i++)
                    {
                        b = buf2[(w + i) & 2047];
                        EnsureSpace(ref temp, tempPtr + 1);
                        temp[tempPtr++] = b;
                        finalCount++;
                        buf2[bpos2 & 2047] = b; bpos2++;
                    }
                }
                if (bpos >= size)
                    x = 8;
            }
        }
        while (bpos < size);

        return temp.AsSpan()[..tempPtr].ToArray();
    }
}