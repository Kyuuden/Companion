using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{
    public enum Pose
    {
        Stand,
        Queued,
        Crouch,
        Special,
        Celebrate,
        Dead
    }

    public class Characters : IDisposable
    {
        private readonly List<byte[,]> _tiles;
        private readonly List<List<Color>> _colorPalettes;

        private Dictionary<int, Bitmap> _characterCache = new Dictionary<int, Bitmap>();

        private Dictionary<Pose, byte[,]> Poses = new Dictionary<Pose, byte[,]>
        {
            { Pose.Stand,     new byte[3,3] { {0, 1, 255},{2, 3, 255},{4, 5, 255} } },
            { Pose.Queued,    new byte[3,3] { {6, 7, 255},{8, 9, 255},{10, 11, 255} } },
            { Pose.Crouch,    new byte[3,3] { {14, 15, 255},{16, 17, 255},{18, 19, 255} } },
            { Pose.Special,   new byte[3,3] { {48, 49, 50},{ 51, 52, 53 },{255, 55, 56} } },
            { Pose.Celebrate, new byte[3,3] { {36, 37, 255},{38, 39, 255},{40, 41, 255} } },
            { Pose.Dead,      new byte[3,3] { {255, 255, 255},{42, 43, 44},{45, 46, 47} } }
        };

        public Characters(MemorySpace rom)
        {
            var processor = new TileProcessor();

            _tiles = rom
                .ReadBytes(CARTROMAddresses.CharacterSpritesAddress, CARTROMAddresses.CharacterSpritesBytes)
                .ReadMany<byte[]>(0, 0x20 * 8, 896)
                .Select(data => processor.GetTile(data, 4))
                .ToList();

            _colorPalettes = rom
                .ReadBytes(CARTROMAddresses.CharacterPalettesAddress, CARTROMAddresses.CharacterPalettesBytes)
                .ReadMany<byte[]>(0, 32*8, 16) //split into palettes
                .Select(paletteData => paletteData.ReadMany<uint>(0, 16, 16).Select(c => processor.GetColor(c)).ToList())
                .ToList();

            _colorPalettes.ForEach(p => p[0] = Color.Transparent);
        }

        public void Dispose()
        {
            foreach (var b in _characterCache.Values)
                b.Dispose();
        }

        private const int SpritesPerCharacter = 64;

        private byte[,] GetCharacterTile(CharacterType character, int spriteIndex)
                => _tiles[(int)character * SpritesPerCharacter + spriteIndex];

        public Bitmap GetCharacterBitmap(byte id, CharacterType type, Pose pose)
        {
            var key = id == 0 ? 0 : (id << 16) | ((byte)type << 8) | ((byte)pose);

            if (_characterCache.TryGetValue(key, out var bitmap))
                return bitmap;

            bitmap = new Bitmap(24, 24);

            if (key != 0)
            {
                var poseData = Poses[pose];

                for (var y = 0; y < 24; y++)
                {
                    for (var x = 0; x < 24; x++)
                    {
                        var tileIndex = poseData[y / 8, x / 8];
                        if (tileIndex != 255)
                            bitmap.SetPixel(x, y, _colorPalettes[(int)type][GetCharacterTile(type, tileIndex)[x % 8, y % 8]]);  
                    }
                }
            }

            _characterCache[key] = bitmap;
            return bitmap;
        } 
    }
}
