using BizHawk.Client.Common;
using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Drawing;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{

    public class TileProcessor
    {
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
    }
}

