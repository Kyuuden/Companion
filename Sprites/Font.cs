using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{
    public enum TextMode
    {
        Normal,
        Disabled,
        Highlighted
    }

    public class Font : IDisposable
    {
        private readonly List<byte[,]> _tiles;
        private readonly Color[,] _palettes;

        private List<List<Bitmap>>? bitmaps;

        public Font(MemorySpace rom)
        {
            var fontData = rom.ReadBytes(CARTROMAddresses.Font, CARTROMAddresses.FontBytes);
            var processor = new TileProcessor();

            _tiles = fontData.ReadMany<byte[]>(0, 128, 256).Select(bytes => processor.GetTile(bytes, 2)).ToList();

            _palettes = new Color[3, 4];
            _palettes[0, 0] = Color.Black;
            _palettes[0, 1] = Color.FromArgb(0, 0, 99);
            _palettes[0, 2] = Color.FromArgb(115, 115, 115);
            _palettes[0, 3] = Color.White;

            _palettes[1, 0] = Color.Black;
            _palettes[1, 1] = Color.FromArgb(0, 0, 99);
            _palettes[1, 2] = Color.FromArgb(66, 66, 66);
            _palettes[1, 3] = Color.FromArgb(123, 123, 123);

            _palettes[2, 0] = Color.Black;
            _palettes[2, 1] = Color.FromArgb(0, 0, 99);
            _palettes[2, 2] = Color.FromArgb(0, 165, 0);
            _palettes[2, 3] = Color.FromArgb(255, 222, 0);

            BuildBitmaps();
        }


        public bool UpdateBackgroundColor(Color backcolor)
        {
            if (_palettes[0, 1] != backcolor)
            {
                _palettes[0, 1] = backcolor;
                _palettes[1, 1] = backcolor;
                _palettes[2, 1] = backcolor;
                ClearBitmaps();
                BuildBitmaps();
                return true;
            }
            return false;
        }

        public Color GetBackColor()
            => _palettes[0, 1];

        private void ClearBitmaps()
        {
            if (bitmaps == null)
                return;

            foreach (var l in bitmaps)
                l.ForEach(b => b.Dispose());
        }

        public void Dispose() => ClearBitmaps();


        private void BuildBitmaps()
        {
            bitmaps = new List<List<Bitmap>>();

            for (int i = 0; i < 3; i++)
            {
                var list = new List<Bitmap>();
                bitmaps.Add(list);
                foreach (var tile in _tiles)
                {
                    var bitmap = new Bitmap(8, 8);
                    for (var y = 0; y < 8; y++)
                    {
                        for (var x = 0; x < 8; x++)
                        {
                            bitmap.SetPixel(x, y, _palettes[i, tile[x, y]]);
                        }
                    }
                    list.Add(bitmap);
                }
            }
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

        public int RenderText(Graphics gr, int x, int y, int cwidth, string text, TextMode mode)
        {
            var height = 0;
            foreach (var line in Breakup(text, cwidth))
            {
                RenderText(gr, x, y, line, mode);
                y += 10;
                height += 10;
            }
            return height;
        }

        public void RenderText(Graphics gr, int x, int y, string text, TextMode mode)
        {
            var originalX = x;
            foreach (var c in text)
            {
                if (c == '\n')
                {
                    x = originalX;
                    y += 10;
                    continue;
                }

                var bitmap = bitmaps![(int)mode][Map(c)];
                gr.DrawImage(bitmap, new Rectangle(x, y, 8, 8));
                x += 8;
            }
        }

        public void RenderBox(Graphics gr, int x, int y, int width, int height)
        {
            for (int iy = 0; iy < height; iy++)
            {
                var line = "";
                for (int ix = 0; ix < width; ix++)
                {
                    var character = ' ';

                    if (ix == 0 && iy == 0)
                        character = BorderTopLeft;
                    else if (ix == 0 && iy == height - 1)
                        character = BorderBottomLeft;
                    else if (ix == width - 1 && iy == 0)
                        character = BorderTopRight;
                    else if (ix == width - 1 && iy == height - 1)
                        character = BorderBottomRight;
                    else if (ix == 0)
                        character = BorderLeft;
                    else if (ix == width - 1)
                        character = BorderRight;
                    else if (iy == 0)
                        character = BorderTop;
                    else if (iy == height - 1)
                        character = BorderBottom;

                    line += character;
                }

                RenderText(gr, x, y + 8 * iy, line, TextMode.Normal);
            }
        }

        private static int Map(char c) => c switch
        {
            ' ' => 0xFF,
            '\'' => 0xC0,
            '.' => 0xC1,
            '-' => 0xC2,
            '_' => 0xC3,
            '!' => 0xC4,
            '?' => 0xC5,
            '%' => 0xC6,
            '/' => 0xC7,
            ':' => 0xC8,
            ',' => 0xC9,
            '&' => 0xCA,
            '+' => 0xCB,
            '(' => 0xCC,
            ')' => 0xCD,
            '0' => 0x80,
            '1' => 0x81,
            '2' => 0x82,
            '3' => 0x83,
            '4' => 0x84,
            '5' => 0x85,
            '6' => 0x86,
            '7' => 0x87,
            '8' => 0x88,
            '9' => 0x89,
            'A' => 0x42,
            'B' => 0x43,
            'C' => 0x44,
            'D' => 0x45,
            'E' => 0x46,
            'F' => 0x47,
            'G' => 0x48,
            'H' => 0x49,
            'I' => 0x4A,
            'J' => 0x4B,
            'K' => 0x4C,
            'L' => 0x4D,
            'M' => 0x4E,
            'N' => 0x4F,
            'O' => 0x50,
            'P' => 0x51,
            'Q' => 0x52,
            'R' => 0x53,
            'S' => 0x54,
            'T' => 0x55,
            'U' => 0x56,
            'V' => 0x57,
            'W' => 0x58,
            'X' => 0x59,
            'Y' => 0x5A,
            'Z' => 0x5B,
            'a' => 0x5C,
            'b' => 0x5D,
            'c' => 0x5E,
            'd' => 0x5F,
            'e' => 0x60,
            'f' => 0x61,
            'g' => 0x62,
            'h' => 0x63,
            'i' => 0x64,
            'j' => 0x65,
            'k' => 0x66,
            'l' => 0x67,
            'm' => 0x68,
            'n' => 0x69,
            'o' => 0x6A,
            'p' => 0x6B,
            'q' => 0x6C,
            'r' => 0x6D,
            's' => 0x6E,
            't' => 0x6F,
            'u' => 0x70,
            'v' => 0x71,
            'w' => 0x72,
            'x' => 0x73,
            'y' => 0x74,
            'z' => 0x75,
            '*' => 125, //Crystal
            '^' => 126, //Key
            '~' => 127, //Tail
            '$' => 54,  //Note
            '|' => 46,  //Sword
            '`' => 48,  //Dagger
            '•' => 64,
            'º' => 65,
            BorderTopLeft => 247, //Top Left Border
            BorderTop => 248, //Top border
            BorderTopRight => 249, //Top right border
            BorderLeft => 250, //Left border
            BorderRight => 251, //right border
            BorderBottomLeft => 252, //bottom left
            BorderBottom => 253, //bottom
            BorderBottomRight => 254, //bottom right
            _ => 125
        };

        public const char BorderTopLeft = '\xDA';
        public const char BorderTop = '\xC2';
        public const char BorderTopRight = '\xBF';
        public const char BorderLeft = '\xC3';
        public const char BorderRight = '\xb4';
        public const char BorderBottomLeft = '\xC0';
        public const char BorderBottom = '\xc1';
        public const char BorderBottomRight = '\xD9';
    }
}
