using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{
    public enum TextMode
    {
        Border,
        Normal,
        Disabled,
        Highlighted,
        Special       
    }

    public class Font : IDisposable
    {
        private readonly List<byte[,]> _tiles;
        private readonly Color[,] _palettes;
        private readonly Settings settings;
        private List<List<Bitmap>>? bitmaps;

        public Font(MemorySpace rom, Settings settings)
        {
            var fontData = rom.ReadBytes(CARTROMAddresses.Font, CARTROMAddresses.FontBytes);
            var processor = new TileProcessor();

            _tiles = fontData.ReadMany<byte[]>(0, 128, 256).Select(bytes => processor.GetTile(bytes, 2)).ToList();

            _palettes = new Color[5, 4];
            _palettes[0, 0] = Color.Black;
            _palettes[0, 1] = Color.FromArgb(0, 0, 99);
            _palettes[0, 2] = Color.FromArgb(115, 115, 115);
            _palettes[0, 3] = Color.White;

            _palettes[1, 0] = Color.Black;
            _palettes[1, 1] = Color.Transparent;
            _palettes[1, 2] = Color.FromArgb(115, 115, 115);
            _palettes[1, 3] = Color.White;

            _palettes[2, 0] = Color.Black;
            _palettes[2, 1] = Color.Transparent;
            _palettes[2, 2] = Color.FromArgb(66, 66, 66);
            _palettes[2, 3] = Color.FromArgb(123, 123, 123);

            _palettes[3, 0] = Color.Black;
            _palettes[3, 1] = Color.Transparent;
            _palettes[3, 2] = Color.FromArgb(0, 165, 0);
            _palettes[3, 3] = Color.FromArgb(255, 222, 0);

            _palettes[4, 0] = Color.Black;
            _palettes[4, 1] = Color.Transparent;
            _palettes[4, 2] = Color.FromArgb(255, 58, 132);
            _palettes[4, 3] = Color.FromArgb(255, 156, 90);

            BuildBitmaps();
            this.settings = settings;
        }


        public bool UpdateBackgroundColor(Color backcolor)
        {
            if (_palettes[0, 1] != backcolor && bitmaps != null)
            {
                _palettes[0, 1] = backcolor;
                ClearBitmaps(0);
                bitmaps[0] = BuildBitmaps(0);
                return true;
            }
            return false;
        }

        public Color GetBackColor()
            => _palettes[0, 1];

        public void Dispose() => ClearBitmaps();

        private void ClearBitmaps()
        {
            if (bitmaps == null)
                return;

            for (var i = 0; i < bitmaps.Count; i++)
                ClearBitmaps(i);
        }

        private void ClearBitmaps(int mode)
        {
            if (bitmaps == null || mode > bitmaps.Count)
                return;

            bitmaps[mode].ForEach(b => b.Dispose());
            bitmaps[mode].Clear();
        }

        private void BuildBitmaps()
        {
            bitmaps = new List<List<Bitmap>>();
            for (int i = 0; i < _palettes.GetLength(0); i++)
                bitmaps.Add(BuildBitmaps(i));
        }

        private List<Bitmap> BuildBitmaps(int mode)
        {
            var list = new List<Bitmap>();
            foreach (var tile in _tiles)
            {
                var bitmap = new Bitmap(8, 8);
                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        bitmap.SetPixel(x, y, _palettes[mode, tile[x, y]]);
                    }
                }
                list.Add(bitmap);
            }

            return list;
        }

        public IEnumerable<string> Breakup(string text, int cwidth)
        {
            var line = string.Empty;
            foreach (var word in text.Split(' '))
            {
                if (line.Length + word.Length + (line.Length == 0 ? 0 : 1) > cwidth)
                {
                    yield return line;
                    line = word;
                }
                else
                {
                    line += (line.Length == 0 ? string.Empty : ' ') + word;
                }
            }
            if (line != string.Empty)
                yield return line;
        }

        public int RenderText(Graphics gr, int x, int y, int cwidth, string text, TextMode mode, int extraSpacing = 2)
        {
            var height = 0;
            foreach (var line in Breakup(text, cwidth))
            {
                RenderText(gr, x, y, line, mode);
                y += settings.Scale(8 + extraSpacing);
                height += settings.Scale(8 + extraSpacing);
            }
            return height;
        }

        public void RenderText(Graphics gr, int x, int y, string text, TextMode mode)
        {
            float originalX = x, fx = x;
            float fy = y;
            gr.InterpolationMode = settings.InterpolationMode;
            foreach (var c in text)
            {
                if (c == '\n')
                {
                    fx = originalX;
                    fy += settings.ScaleF(10);
                    continue;
                }

                var bitmap = bitmaps![(int)mode][c.ToGame()];

                gr.DrawImage(bitmap, fx, fy, settings.TileSizeF, settings.TileSizeF);
                fx += settings.TileSizeF;
            }
        }

        public void RenderTextUnscaled(Graphics gr, int x, int y, string text, TextMode mode)
        {
            float originalX = x, fx = x;
            float fy = y;
            gr.InterpolationMode = settings.InterpolationMode;
            foreach (var c in text)
            {
                if (c == '\n')
                {
                    fx = originalX;
                    fy += settings.ScaleF(10);
                    continue;
                }

                var bitmap = bitmaps![(int)mode][c.ToGame()];

                gr.DrawImage(bitmap, fx, fy, 8, 8);
                fx += 8;
            }
        }

        public void RenderBox(Graphics gr, int x, int y, int width, int height)
        {
            if (height <= 0 || width <= 0)
                return;

            using (var boxBmp = new Bitmap(width * 8, height * 8))
            {
                using (var bmpGraphics = Graphics.FromImage(boxBmp))
                    for (int iy = 0; iy < height; iy++)
                    {
                        for (int ix = 0; ix < width; ix++)
                        {
                            var character = ' ';

                            if (ix == 0 && iy == 0)
                                character = CharacterMap.BorderTopLeft;
                            else if (ix == 0 && iy == height - 1)
                                character = CharacterMap.BorderBottomLeft;
                            else if (ix == width - 1 && iy == 0)
                                character = CharacterMap.BorderTopRight;
                            else if (ix == width - 1 && iy == height - 1)
                                character = CharacterMap.BorderBottomRight;
                            else if (ix == 0)
                                character = CharacterMap.BorderLeft;
                            else if (ix == width - 1)
                                character = CharacterMap.BorderRight;
                            else if (iy == 0)
                                character = CharacterMap.BorderTop;
                            else if (iy == height - 1)
                                character = CharacterMap.BorderBottom;

                            bmpGraphics.DrawImageUnscaled(bitmaps![(int)TextMode.Border][character.ToGame()], ix * 8, iy * 8);
                        }
                    }

                gr.InterpolationMode = settings.InterpolationMode;
                gr.DrawImage(boxBmp, x, y, width * settings.TileSize, height * settings.TileSize);
            }
        }
    }
}
