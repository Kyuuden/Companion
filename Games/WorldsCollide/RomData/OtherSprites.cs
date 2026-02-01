using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public abstract class OtherSprites<T>(byte[] tileData, byte[] paletteData) : IDisposable where T : Enum
{
    private readonly List<Palette> _palettes = paletteData.ReadMany<byte[]>(8 * 0x20).Select(p => p.DecodePalette(new Color32())).ToList();
    private readonly Dictionary<T, OtherSprite> _spriteCache = [];

    public ISprite Get(T item)
    {
        if (_spriteCache.TryGetValue(item, out var sprite))
            return sprite;

        _spriteCache[item] = sprite = GenerateSprite(item, tileData, _palettes);
        return sprite;
    }

    private OtherSprite GenerateSprite(T item, ReadOnlySpan<byte> tileData, IList<Palette> palettes)
    {
        var info = GetInfo(item);
        var palette = palettes[info.PaletteIndex];

        var bmp = BitmapDataFactory.CreateBitmapData(info.Size, KnownPixelFormat.Format8bppIndexed, palette);

        for (int i = 0; i < info.TileIndicies.Count; i++)
        {
            var tile = info.TileIndicies[i];
            if (tile == null) continue;

            tileData.Slice(Math.Abs(tile.Value) * 0x20, 0x20).ToArray().DecodeTile(4).DrawInto(bmp, i % info.HorizontalTileCount * 8, i / info.HorizontalTileCount * 8, tile < 0);
        }

        return new OtherSprite(bmp);
    }

    protected abstract SpriteInfo GetInfo(T item);

    protected record SpriteInfo
    {
        public List<int?> TileIndicies { get; }
        public int PaletteIndex { get; }
        public int HorizontalTileCount { get; }
        public int VerticalTileCount { get; }

        public Size Size => new(HorizontalTileCount * 8, VerticalTileCount * 8);

        public SpriteInfo(List<int?> tileIndicies, int paletteIndex, int horizontalTileCount, int verticalTileCount)
        {
            TileIndicies = tileIndicies;
            PaletteIndex = paletteIndex;
            HorizontalTileCount = horizontalTileCount;
            VerticalTileCount = verticalTileCount;

            if (tileIndicies.Count != horizontalTileCount * verticalTileCount)
                throw new ArgumentException($"Tile count of {tileIndicies.Count} does not match tile size of {horizontalTileCount} by {verticalTileCount}.");
        }
    }

    public void Dispose()
    {
        foreach (var sprite in _spriteCache.Values) sprite.Dispose();
        _spriteCache.Clear();
    }
}
