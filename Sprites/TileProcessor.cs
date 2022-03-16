using BizHawk.Client.Common;
using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Drawing;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{
    class TileProcessor
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
                    byte color = 0;

                    for (var p = 0; p < bitDepth; p++)
                    {
                        color |= (byte)(data.Read<byte>(y * 16 + (7 - x) + BitPlaneOffset(p), 1) << p);
                    }

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
                    byte color = 0;

                    for (var p = 0; p < bitDepth; p++)
                    {
                        color |= (byte)(data.Read<byte>(y * 16 + (7 - x) + BitPlaneOffset(p), 1) << p);
                    }

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

        public Color GetColor(uint colordata)
        {
            var red = colordata & 0x1F;
            var green = (colordata >> 5) & 0x1F;
            var blue = (colordata >> 10) & 0x1F;
            return Color.FromArgb((int)(red * 255 / 31.0), (int)(green * 255 / 31.0), (int)(blue * 255 / 31.0));
        }
    }
}

