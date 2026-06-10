using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FF.Rando.Companion.Extensions;
public static class TileExtensions
{
    public static byte[,] DecodeTile(this byte[] data, int bitsPerPixel, uint width = 8, uint height = 8)
    {
        return DecodeTile(data.AsReadOnlySpan(), bitsPerPixel, width, height);
    }

    public static byte[,] DecodeTile(this ReadOnlySpan<byte> data, int bitsPerPixel, uint width = 8, uint height = 8)
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
