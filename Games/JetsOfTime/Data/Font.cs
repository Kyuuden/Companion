using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Palette = KGySoft.Drawing.Imaging.Palette;

namespace FF.Rando.Companion.Games.JetsOfTime.Data;
public class Font
{
    private readonly List<byte[,]> _tiles;
    private readonly TextEncoding _encoding = new();
    private readonly Dictionary<TextMode, List<IReadWriteBitmapData>> _cachedData = [];
    private readonly List<Palette> _palettes;
    private bool disposedValue;

    public Font(IMemorySpace rom)
    {
        _tiles = rom.ReadBytes(Addresses.ROM.Font)
            .ReadMany<byte[]>(0x10 * 8)
            .Select(data => data.DecodeTile(2))
            .ToList();

        _palettes = rom.ReadBytes(Addresses.ROM.FontPalettes)
            .ReadMany<byte[]>(8 * 8)
            .Select(b => b.DecodePalette(new Color32())).ToList();

        for (int i = 0; i < _palettes.Count; i++)
            _cachedData[(TextMode)i] = BuildBitmaps(_palettes[i]);
    }

    public void UpdateFontColor(Color32 color)
    {
        if (disposedValue) return;
        var existing = _palettes[0];
        List<Color32> colors = [existing[0], existing[1], existing[2], color];

        _palettes[0] = new Palette(colors);

        _cachedData[TextMode.Normal].ForEach(b => b.TrySetPalette(_palettes[0]));
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

    public void RenderText(IReadWriteBitmapData bitmapData, Point point, string text, TextMode mode)
    {
        var bytes = _encoding.GetBytes(text);
        for (var x = 0; x < bytes.Length; x++)
        {
            var bmp = _cachedData[mode][bytes[x]];
            bmp.DrawInto(bitmapData, new Point(point.X + (x * 8), point.Y));
        }
    }

    public IReadableBitmapData RenderText(string text, TextMode mode, int? cwidth = null)
    {
        var lines = cwidth.HasValue
            ? text.Split('\n').SelectMany(l => Breakup(l, cwidth.Value)).ToList()
            : [.. text.Split('\n')];

        var height = lines.Count * 8;
        var width = cwidth.HasValue ? cwidth.Value * 8 : Math.Max(1, lines.Max(x => x.Length)) * 8;

        var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, _palettes[(int)mode]);
        for (var y = 0; y < lines.Count(); y++)
        {
            var linesBytes = _encoding.GetBytes(lines[y]);
            for (var x = 0; x < linesBytes.Length; x++)
            {
                var bmp = _cachedData[mode][linesBytes[x]];
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
                foreach (var mode in _cachedData.Values)
                    mode.ForEach(x => x.Dispose());

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
