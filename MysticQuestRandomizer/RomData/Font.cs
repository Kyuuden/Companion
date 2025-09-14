using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MemoryManagement;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.MysticQuestRandomizer.RomData;
internal class Font : IDisposable
{
    private readonly List<byte[,]> _tiles;
    private readonly List<byte[,]> _resistanceTiles;
    private readonly List<IReadableBitmapData> _bitmaps;    
    private readonly TextEncoding _encoding = new();
    private bool disposedValue;

    public Font(IMemorySpace rom)
    {
        _tiles = rom.ReadBytes(Addresses.ROM.Font)
            .ReadMany<byte[]>(512 * 8)
            .Select(data => data.DecodeTile(2, 256, 8))
            .ToList();

        _resistanceTiles = rom.ReadBytes(Addresses.ROM.Resitstances)
            .ReadMany<byte[]>(0x18 * 8)
            .Select(t => t.DecodeTile(3))
            .Concat(rom.ReadBytes(Addresses.ROM.AlternateResitances).ReadMany<byte[]>(0x18 * 8).Select(t => t.DecodeTile(3))).ToList();

        var resistancePalettes = rom.ReadBytes(Addresses.ROM.Palettes)
            .ReadMany<byte[]>(16 * 8)
            .Take(2)
            .Select(p => p.DecodePalette()).ToList();

        Palette = new Palette(new List<Color32> 
        {
            Color.Transparent.ToColor32(),
            Color.Transparent.ToColor32(),
            Color.FromArgb(255, 255, 255).ToColor32(),
            Color.FromArgb(0, 0, 0).ToColor32()
        }.Concat(resistancePalettes.SelectMany(p => p.GetEntries())));

        _bitmaps = BuildBitmaps();
    }

    public Palette Palette { get; }

    private List<IReadableBitmapData> BuildBitmaps()
    {
        var list = new List<IReadableBitmapData>();
        foreach (var tile in _tiles)
        {
            for (int i = 0; i < 32; i++)
            {
                var data = BitmapDataFactory.CreateBitmapData(new Size(8, 8), KnownPixelFormat.Format8bppIndexed, Palette);

                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        data.SetColorIndex(x, y, tile[x + (i * 8), y]);
                    }
                }

                list.Add(data);
            }
        }

        var rightBorder = BitmapDataFactory.CreateBitmapData(new Size(8, 8), KnownPixelFormat.Format8bppIndexed, Palette);
        var leftBorder = list[TextEncoding.BorderLeft];

        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                rightBorder.SetColorIndex(x, y, leftBorder.GetColorIndex(7 - x, y));
            }
        }

        list.Add(rightBorder);

        var bottomBorder = BitmapDataFactory.CreateBitmapData(new Size(8, 8), KnownPixelFormat.Format8bppIndexed, Palette);
        var topBorder = list[TextEncoding.BorderTop];

        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                bottomBorder.SetColorIndex(x, y, topBorder.GetColorIndex(x, 7 - y));
            }
        }

        list.Add(bottomBorder);

        for (var i = 0; i < _resistanceTiles.Count; i++)
        {
            var tile = _resistanceTiles[i];

            var colorOffset = i switch
            {
                (0 or 1 or 2 or 3 or 12 or 13 or 14 or 15 or 16) => 4,
                _ => 12
            };

            if (i is 4 or 5 or 6)
                continue;

            var data = BitmapDataFactory.CreateBitmapData(new Size(8, 8), KnownPixelFormat.Format8bppIndexed, Palette);
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    data.SetColorIndex(x, y, tile[x, y] + colorOffset);
                }
            }

            list.Add(data);
        }

        return list;
    }

    public IReadWriteBitmapData RenderBox(int width, int height, Palette? additionalColors = null)
    {
        var palette = Palette;
        if (additionalColors != null)
        {
            palette = new Palette(Palette.GetEntries().Concat(additionalColors.GetEntries()));
        }

        var data = BitmapDataFactory.CreateBitmapData(new Size(width * 8, height * 8), KnownPixelFormat.Format8bppIndexed, palette);

        for (int iy = 0; iy < height; iy++)
        {
            for (int ix = 0; ix < width; ix++)
            {
                var b = 0xBF;

                if (ix == 0 && iy == 0)
                    b = TextEncoding.BorderTopLeft;
                else if (ix == 0 && iy == height - 1)
                    b = TextEncoding.BorderBottomLeft;
                else if (ix == width - 1 && iy == 0)
                    b = TextEncoding.BorderTopRight;
                else if (ix == width - 1 && iy == height - 1)
                    b = TextEncoding.BorderBottomRight;
                else if (ix == 0)
                    b = TextEncoding.BorderLeft;
                else if (ix == width - 1)
                    b = TextEncoding.BorderRight;
                else if (iy == 0)
                    b = TextEncoding.BorderTop;
                else if (iy == height - 1)
                    b = TextEncoding.BorderBottom;

                var bmp = _bitmaps![b];

                bmp.CopyTo(data, new Point(ix * 8, iy * 8));
            }
        }

        return data;
    }

    public IEnumerable<string> Breakup(string text, int cwidth)
    {
        var line = string.Empty;
        foreach (var word in text.Split([' '], StringSplitOptions.RemoveEmptyEntries))
        {
            if (line.Length + word.Length + (line.Length == 0 ? 0 : 1) > cwidth)
            {
                yield return line;
                if (line.Contains('\n'))
                    yield return string.Empty;
                line = word;
            }
            else
            {
                line += (line.Length == 0 ? string.Empty : ' ') + word;
            }
        }
        if (line != string.Empty)
            yield return line;
        if (line.Contains('\n'))
            yield return string.Empty;
    }

    public IReadableBitmapData RenderText(string text, int? cwidth = null)
    {
        var preprocessed = _encoding.ConvertTagsToSymbols(text);

        var lines = cwidth.HasValue
            ? preprocessed.Split('\n').SelectMany(l => Breakup(l, cwidth.Value)).ToList()
            : [.. preprocessed.Split('\n')];

        var height = lines.Count * 8;
        var width = cwidth.HasValue ? cwidth.Value * 8 : Math.Max(1, lines.Max(x => x.Length)) * 8;

        var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, Palette);
        for (var y = 0; y < lines.Count(); y++)
        {
            var linesBytes = _encoding.GetBytes(lines[y]);
            for (var x = 0; x < linesBytes.Length; x++)
            {
                var bmp = _bitmaps![linesBytes[x]];
                bmp.CopyTo(data, new Point(x * 8, y * 8));
            }
        }

        return data;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var bmp in _bitmaps)
                        bmp.Dispose();
            }
            _bitmaps.Clear();

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
