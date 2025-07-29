using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.Shared;
using FF.Rando.Companion.MemoryManagement;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static FF.Rando.Companion.FreeEnterprise.Shared.Addresses;

namespace FF.Rando.Companion.FreeEnterprise.RomData;

public enum TextMode
{
    Normal,
    Disabled,
    Highlighted,
    Special
}

public class Font : IDisposable
{
    private readonly List<byte[,]> _tiles;
    private readonly Palette _palette;
    private readonly Dictionary<TextMode, List<IReadableBitmapData>> _bitmaps = [];
    private bool disposedValue;
    private readonly TextEncoding _encoding;

    public Font(IMemorySpace memorySpace)
    {
        var fontData = memorySpace.ReadBytes(ROM.Font);
        _encoding = new TextEncoding();

        _tiles = fontData.ReadMany<byte[]>(0, 128, 256).Select(bytes => bytes.DecodeTile(2)).ToList();
        _palette = new Palette((IEnumerable<Color>)
            [
                Color.Transparent,
                Color.Transparent,
                Color.FromArgb(115, 115, 115), Color.White,
                Color.FromArgb(66, 66, 66), Color.FromArgb(123, 123, 123),
                Color.FromArgb(0, 165, 0), Color.FromArgb(255, 222, 0),
                Color.FromArgb(255, 58, 132), Color.FromArgb(255, 156, 90)
            ]);

        BuildBitmaps();
    }

    private void BuildBitmaps()
    {
        foreach (var mode in Enum.GetValues(typeof(TextMode)).OfType<TextMode>())
        {
            _bitmaps.Add(mode, BuildBitmaps(mode));
        }
    }

    private int GetColorIndex(byte c, TextMode mode)
        => mode switch
        {
            TextMode.Normal => c,
            TextMode.Disabled => c > 1 ? c + 2 : c,
            TextMode.Highlighted => c > 1 ? c + 4 : c,
            TextMode.Special => c > 1 ? c + 6 : c,
            _ => c,
        };

    private List<IReadableBitmapData> BuildBitmaps(TextMode mode)
    {
        var list = new List<IReadableBitmapData>();
        foreach (var tile in _tiles)
        {
            var data = BitmapDataFactory.CreateBitmapData(new Size(8, 8), KnownPixelFormat.Format8bppIndexed, _palette);
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    data.SetColorIndex(x, y, GetColorIndex(tile[x, y], mode));
                }
            }

            list.Add(data);
        }

        return list;
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

    public IReadableBitmapData RenderText(string text, TextMode mode, int? cwidth = null)
    {
        var preprocessed = _encoding.ConvertTagsToSymbols(text);

        var lines = cwidth.HasValue
            ? preprocessed.Split('\n').SelectMany(l => Breakup(l, cwidth.Value)).ToList()
            : [.. preprocessed.Split('\n')];

        var height = lines.Count * 8;
        var width = cwidth.HasValue ? cwidth.Value * 8 : lines.Max(x => x.Length) * 8;

        var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, _palette);
        for (var y = 0; y < lines.Count(); y++)
        {
            var linesBytes = _encoding.GetBytes(lines[y]);
            for(var x = 0; x < linesBytes.Length; x++)
            {
                var bmp = _bitmaps![mode][linesBytes[x]];
                bmp.CopyTo(data, new Point(x * 8, y * 8));
            }
        }

        return data;
    }

    public IReadWriteBitmapData RenderBox(int width, int height, Palette? additionalColors = null)
    {
        var palette = _palette;
        if (additionalColors != null)
        {
            palette = new Palette(_palette.GetEntries().Concat(additionalColors.GetEntries()));
        }

        var data = BitmapDataFactory.CreateBitmapData(new Size(width * 8, height * 8), KnownPixelFormat.Format8bppIndexed, palette);

        for (int iy = 0; iy < height; iy++)
        {
            for (int ix = 0; ix < width; ix++)
            {
                var b = 0xFF;

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

                var bmp = _bitmaps![TextMode.Normal][b];

                bmp.CopyTo(data, new Point(ix * 8, iy * 8));
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
                foreach (var list in _bitmaps)
                    foreach (var bmp in list.Value)
                        bmp.Dispose();
            }

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
