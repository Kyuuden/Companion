using BizHawk.Client.Common;
using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Drawing;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{

    public class TileProcessor
    {
        public Bitmap LoadTileFromROM(IMemoryApi memory, uint address, byte bitDepth) 
            => LoadTile(memory.ReadByteRange(address, 8 * bitDepth, "CARTROM").ToArray(), bitDepth);

        public Bitmap LoadTile(byte[] data, byte bitDepth)
        {
            var _bitmap = new Bitmap(8, 8, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            var bmpData = _bitmap.LockBits(new Rectangle(0, 0, 8, 8), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            var tile = new byte[bmpData.Stride * bmpData.Height];
            for (var y = 0u; y < 8; y++)
            {
                for (var x = 0u; x < 8; x++)
                {
                    byte color = bitDepth switch
                    {
                        1 => data.Read<byte>(y * 8 + (7 - x), 1),
                        2 => (byte)(data.Read<byte>(y * 16 + (7 - x), 1) | (data.Read<byte>(y * 16 + (7 - x) + 8, 1) << 1)),
                        3 => (byte)(data.Read<byte>(y * 16 + (7 - x), 1) | (data.Read<byte>(y * 16 + (7 - x) + 8, 1) << 1) | (data.Read<byte>(128 + y * 8 + (7 - x), 1) << 2)),
                        4 => (byte)(data.Read<byte>(y * 16 + (7 - x), 1) | (data.Read<byte>(y * 16 + (7 - x) + 8, 1) << 1) | (data.Read<byte>(128 + y * 16 + (7 - x), 1) << 2) | (data.Read<byte>(128 + y * 16 + (7 - x) + 8, 1) << 3)),
                        _ => 0,
                    };

                    tile[y * bmpData.Stride + x] = color;
                }
            }

            IntPtr ptr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(tile, 0, ptr, tile.Length);
            _bitmap.UnlockBits(bmpData);
            return _bitmap;
        }

        public byte[,] GetTile(byte[] data, byte bitDepth)
        {
            var tile = new byte[8, 8];

            for (var y = 0u; y < 8; y++)
            {
                for (var x = 0u; x < 8; x++)
                {
                    byte color = bitDepth switch
                    {
                        1 => data.Read<byte>(y * 8 + (7 - x), 1),
                        2 => (byte)(data.Read<byte>(y * 16 + (7 - x), 1) | (data.Read<byte>(y * 16 + (7 - x) + 8, 1) << 1)),
                        3 => (byte)(data.Read<byte>(y * 16 + (7 - x), 1) | (data.Read<byte>(y * 16 + (7 - x) + 8, 1) << 1) | (data.Read<byte>(128 + y * 8 + (7 - x), 1) << 2)),
                        4 => (byte)(data.Read<byte>(y * 16 + (7 - x), 1) | (data.Read<byte>(y * 16 + (7 - x) + 8, 1) << 1) | (data.Read<byte>(128 + y * 16 + (7 - x), 1) << 2) | (data.Read<byte>(128 + y * 16 + (7 - x) + 8, 1) << 3)),
                        _ => 0,
                    };

                    tile[x,y] = color;
                }
            }

            return tile;
        }

        private static uint BitPlaneOffset(int bitplane) => bitplane switch
        {
            0 => 0,
            1 => 1 *8,
            2 => 16 * 8,
            3 => 17 * 8,
            4 => 32 * 8,
            5 => 33 * 8,
            6 => 48 * 8,
            7 => 49 * 8,
            _ => 0
        };
    }
}

