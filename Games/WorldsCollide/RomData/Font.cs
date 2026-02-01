using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Palette = KGySoft.Drawing.Imaging.Palette;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;
public class Font : IDisposable
{
    private readonly List<byte[,]> _tiles;
    private readonly TextEncoding _encoding = new();
    private readonly List<IReadWriteBitmapData> _cachedData = [];
    private bool disposedValue;

    public Palette Palette { get; private set; }

    public Font(IMemorySpace rom)
    {
        _tiles = rom.ReadBytes(Addresses.ROM.Font)
            .ReadMany<byte[]>(0x10 * 8)
            .Select(data => data.DecodeTile(2))
            .ToList();

        Palette = rom.ReadBytes(Addresses.ROM.FontPalettes)
            .Read<byte[]>(0, 8 * 8)
            .DecodePalette(new Color32());

        _cachedData.AddRange(BuildBitmaps(Palette));
    }

    public void UpdateFontColor(Color32 color)
    {
        if (disposedValue) return;
        var existing = Palette;
        List<Color32> colors = [existing[0], existing[1], existing[2], color];

        Palette = new Palette(colors);

        _cachedData.ForEach(b => b.TrySetPalette(Palette));
    }

    private List<IReadWriteBitmapData> BuildBitmaps(Palette palette)
    {
        var list = new List<IReadWriteBitmapData>();
        foreach (var tile in _tiles)
        {
            var data = BitmapDataFactory.CreateBitmapData(new Size(8, 8), KnownPixelFormat.Format8bppIndexed, palette);
            tile.DrawInto(data);
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

    public IReadableBitmapData RenderText(string text, int? cwidth = null)
    {
        var lines = cwidth.HasValue
            ? text.Split('\n').SelectMany(l => Breakup(l, cwidth.Value)).ToList()
            : [.. text.Split('\n')];

        var height = lines.Count * 8;
        var width = cwidth.HasValue ? cwidth.Value * 8 : Math.Max(1, lines.Max(x => x.Length)) * 8;

        var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, Palette);
        for (var y = 0; y < lines.Count(); y++)
        {
            var linesBytes = _encoding.GetBytes(lines[y]);
            for (var x = 0; x < linesBytes.Length; x++)
            {
                var bmp = _cachedData[linesBytes[x]];
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
                _cachedData.ForEach(x => x.Dispose());
                _cachedData.Clear();
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
