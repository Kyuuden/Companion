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

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public enum Icons
{
    Cursor,
    UpArrow,
    DownArrow,
    BothArrow,
    VS,
    Float,
    Petrify,
    Imp,
    Invisible,
    Poison,
    Zombie,
    Darkness
}

public class Font : IDisposable
{
    private readonly List<byte[,]> _tiles;
    private readonly TextEncoding _encoding = new();
    private readonly Dictionary<TextMode, List<IReadWriteBitmapData>> _cachedData = [];
    private readonly List<Palette> _palettes;
    private bool disposedValue;
    private readonly Dictionary<Icons, IReadableBitmapData> _iconData = [];

    public Font(IMemorySpace rom)
    {
        _tiles = rom.ReadBytes(Addresses.ROM.Font)
            .ReadMany<byte[]>(0x10 * 8)
            .Select(data => data.DecodeTile(2))
            .ToList();

        _palettes = rom.ReadBytes(Addresses.ROM.FontPalettes)
            .ReadMany<byte[]>(0, 8 * 8, 4)
            .Select(b => b.DecodePalette(new Color32())).ToList();

        for (int i = 0; i < _palettes.Count; i ++)
            _cachedData[(TextMode)i] = BuildBitmaps(_palettes[i]);

        var iconTiles = rom.ReadBytes(Addresses.ROM.IconsAndCursors)
            .ReadMany<byte[]>(0x20 * 8)
            .Select(d => d.DecodeTile(4))
            .ToList();

        var iconPalette = rom.ReadBytes(Addresses.ROM.IconsAndCursorsPalette).DecodePalette(new Color32());

        _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(16, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[0]).DrawTile(iconTiles[1], destinationX: 8).DrawTile(iconTiles[16], destinationY: 8).DrawTile(iconTiles[17], 8, 8);

        _iconData[Icons.UpArrow] = BitmapDataFactory.CreateBitmapData(8, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[18]).DrawTile(iconTiles[2], 0, 8, false, true);

        _iconData[Icons.DownArrow] = BitmapDataFactory.CreateBitmapData(8, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[2]).DrawTile(iconTiles[18], 0, 8, false, true);

        _iconData[Icons.BothArrow] = BitmapDataFactory.CreateBitmapData(8, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[18]).DrawTile(iconTiles[18], 0, 8, false, true);

        _iconData[Icons.VS] = _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(32, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[8]).DrawTile(iconTiles[9], 8).DrawTile(iconTiles[10], 16).DrawTile(iconTiles[11], 24)
            .DrawTile(iconTiles[24]).DrawTile(iconTiles[25], 8, 8).DrawTile(iconTiles[26], 16, 8).DrawTile(iconTiles[27], 24, 8);

        _iconData[Icons.Float] = _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(16, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[32]).DrawTile(iconTiles[33], destinationX: 8).DrawTile(iconTiles[48], destinationY: 8).DrawTile(iconTiles[49], 8, 8);

        _iconData[Icons.Petrify] = _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(16, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[34]).DrawTile(iconTiles[35], destinationX: 8).DrawTile(iconTiles[50], destinationY: 8).DrawTile(iconTiles[51], 8, 8);

        _iconData[Icons.Imp] = _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(16, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[36]).DrawTile(iconTiles[37], destinationX: 8).DrawTile(iconTiles[52], destinationY: 8).DrawTile(iconTiles[53], 8, 8);

        _iconData[Icons.Invisible] = _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(16, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[38]).DrawTile(iconTiles[39], destinationX: 8).DrawTile(iconTiles[54], destinationY: 8).DrawTile(iconTiles[55], 8, 8);

        _iconData[Icons.Poison] = _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(16, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[40]).DrawTile(iconTiles[41], destinationX: 8).DrawTile(iconTiles[56], destinationY: 8).DrawTile(iconTiles[57], 8, 8);

        _iconData[Icons.Zombie] = _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(16, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[42]).DrawTile(iconTiles[43], destinationX: 8).DrawTile(iconTiles[58], destinationY: 8).DrawTile(iconTiles[59], 8, 8);

        _iconData[Icons.Darkness] = _iconData[Icons.Cursor] = BitmapDataFactory.CreateBitmapData(16, 16, KnownPixelFormat.Format8bppIndexed, iconPalette)
            .DrawTile(iconTiles[44]).DrawTile(iconTiles[45], destinationX: 8).DrawTile(iconTiles[60], destinationY: 8).DrawTile(iconTiles[61], 8, 8);
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

    public IReadableBitmapData GetIcon(Icons icon) => _iconData[icon];

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var mode in _cachedData.Values)
                    mode.ForEach(x => x.Dispose());

                foreach (var icon in  _iconData.Values)
                    icon.Dispose();

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
