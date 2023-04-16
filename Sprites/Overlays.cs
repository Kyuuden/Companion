using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.RomUtilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{
    public class Overlays : IDisposable
    {
        private readonly List<byte[,]> _stickers;
        private readonly List<Color> _stickerPalette;
        private Bitmap? _customMissBitmap;
        private readonly Bitmap _missBitmap;
        private readonly Font _font;

        public Bitmap MissBitmap => _customMissBitmap ?? _missBitmap;
        public Bitmap FingerBitmap { get; }

        public Overlays(IMemorySpace rom, Font font)
        {
            var processor = new TileProcessor();
            _stickers = rom
                .ReadBytes(CARTROMAddresses.BattleStatusStickers, CARTROMAddresses.BattleStatusStickersBytes)
                .ReadMany<byte[]>(0, 0x18 * 8, 64)
                .Select(data => processor.GetTile(data, 3))
                .ToList();

            _stickerPalette = rom
                .ReadBytes(CARTROMAddresses.BattleStatusStickersGreyScalePalette, 16)
                .ReadMany<uint>(0, 16, 8).Select(c => ColorProcessor.GetColor(c))
                .ToList();

            _stickerPalette[0] = Color.Transparent;

            _missBitmap = GetMiss();
            FingerBitmap = GetFinger();
            _font = font;
        }

        public List<Size?> MissOffsets { get; } = new List<Size?>
        {
            new Size(0,0),new Size(0,0),new Size(0,0),new Size(0,0),new Size(0,-3),new Size(0,-6),new Size(0,-9),new Size(0,-12),new Size(0,-14),new Size(0,-15),
            new Size(0,-15),new Size(0,-16),new Size(0,-16),new Size(0,-16),new Size(0,-15),new Size(0,-15),new Size(0,-14),new Size(0,-12),new Size(0,-9),new Size(0,-6),
            new Size(0,-3),new Size(0,0),new Size(0,-2),new Size(0,-4),new Size(0,-4),new Size(0,-6),new Size(0,-6),new Size(0,-4),new Size(0,-4),new Size(0,-2),
            new Size(0,-1),new Size(0,0),new Size(0,0),new Size(0,0),new Size(0,0),new Size(0,0),new Size(0,0),new Size(0,0),new Size(0,0),new Size(0,0), null
        };

        public List<ColorMatrix> FadeoutOffsets { get; } = new List<ColorMatrix>
        {
           new ColorMatrix { Matrix33 = 1.0f },new ColorMatrix { Matrix33 = 1.0f },   new ColorMatrix { Matrix33 = 1.0f },
           new ColorMatrix { Matrix33 = 1.0f },new ColorMatrix { Matrix33 = 1.0f },   new ColorMatrix { Matrix33 = 1.0f },
           new ColorMatrix { Matrix33 = 1.0f }, 
           new ColorMatrix { Matrix33 = 0.9f },new ColorMatrix { Matrix33 = 0.9f },   new ColorMatrix { Matrix33 = 0.9f },
           new ColorMatrix { Matrix33 = 0.9f },new ColorMatrix { Matrix33 = 0.9f },  
           new ColorMatrix { Matrix33 = 0.8f },new ColorMatrix { Matrix33 = 0.8f },   new ColorMatrix { Matrix33 = 0.8f },
           new ColorMatrix { Matrix33 = 0.8f },new ColorMatrix { Matrix33 = 0.8f },   
           new ColorMatrix { Matrix33 = 0.7f },new ColorMatrix { Matrix33 = 0.7f },   new ColorMatrix { Matrix33 = 0.7f },
           new ColorMatrix { Matrix33 = 0.7f },new ColorMatrix { Matrix33 = 0.7f },   
           new ColorMatrix { Matrix33 = 0.6f },new ColorMatrix { Matrix33 = 0.6f },   new ColorMatrix { Matrix33 = 0.6f },
           new ColorMatrix { Matrix33 = 0.5f },new ColorMatrix { Matrix33 = 0.5f },   new ColorMatrix { Matrix33 = 0.5f },
           new ColorMatrix { Matrix33 = 0.4f },new ColorMatrix { Matrix33 = 0.4f },   new ColorMatrix { Matrix33 = 0.4f },
           new ColorMatrix { Matrix33 = 0.3f },new ColorMatrix { Matrix33 = 0.3f },   new ColorMatrix { Matrix33 = 0.3f },
           new ColorMatrix { Matrix33 = 0.2f },new ColorMatrix { Matrix33 = 0.2f },   new ColorMatrix { Matrix33 = 0.2f },
           new ColorMatrix { Matrix33 = 0.1f },new ColorMatrix { Matrix33 = 0.1f },   new ColorMatrix { Matrix33 = 0.1f },
           new ColorMatrix { Matrix33 = 0.0f } ,                                      new ColorMatrix { Matrix33 = 0.0f }
        };

        private Bitmap GetMiss()
        {
            var bitmap = new Bitmap(32, 8);
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    byte[,] tile = x switch
                    {
                        int xPos when xPos < 8 => _stickers[28],
                        int xPos when xPos < 16 => _stickers[29],
                        int xPos when xPos < 24 => _stickers[30],
                        _ => _stickers[31]
                    };

                    var color = _stickerPalette[tile[x % 8, y % 8]];
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }

        public void SetCustomMissMessage(string? missText)
        {
            _customMissBitmap?.Dispose();
            _customMissBitmap = null;

            if (string.IsNullOrEmpty(missText))
                return;

            _customMissBitmap = new Bitmap(missText!.Length*8, 8);
            using var gr = Graphics.FromImage(_customMissBitmap);
            _font.RenderTextUnscaled(gr, 0, 0, missText, TextMode.Normal);
        }

        private Bitmap GetFinger()
        {
            var bitmap = new Bitmap(16, 16);
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    byte[,] tile = x switch
                    {
                        int _ when x < 8  && y < 8 => _stickers[10],
                        int _ when x < 8  && y < 16 => _stickers[26],
                        int _ when x < 16 && y < 8 => _stickers[11],
                        _ => _stickers[27]
                    };

                    var color = _stickerPalette[tile[x % 8, y % 8]];
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }

        public void Dispose()
        {
            _missBitmap?.Dispose();
            MissBitmap?.Dispose();
            FingerBitmap?.Dispose();
        }
    }
}
