using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.RomData;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Rendering;
internal class WorldMaps(Seed seed) : IDisposable
{
    private readonly Dictionary<WorldMapType, ISprite> _worldMapCache = [];
    private readonly Seed _seed = seed;

    public ISprite Get(WorldMapType type)
    {
        if (_worldMapCache.TryGetValue(type, out var map)) return map;

        map =  GenerateMap((int)type);
        _worldMapCache.Add(type, map);
        return map;
    }

    private ISprite GenerateMap(int index)
    {
        var mapData = Utils.Decompress(_seed.Rom.ReadBytes(Addresses.ROM.WorldMaps.MapData[index])).AsSpan();
        var tileData = Utils.Decompress(_seed.Rom.ReadBytes(Addresses.ROM.WorldMaps.TileData[index])).AsSpan();

        var tilesetData = tileData[0..0x400];
        var graphics = tileData[0x400..0x2400];
        var palettes = tileData[0x2400..];

        var paletteData = _seed.Rom.ReadBytes(Addresses.ROM.WorldMaps.PaletteData[index]);

        var palette = paletteData.DecodePalette();

        var tiles = new List<byte[,]>();
        for (int i = 0; i < graphics.Length / 0x20; i++)
        {
            tiles.Add(graphics.Slice(i * 0x20, 0x20).DecodeTile(4));
        }

        var tileset = new List<List<TileInfo>> { ([]) };

        foreach (var tile in tilesetData)
        {
            var pal = palettes[tile / 2];
            pal = (byte)(((tile % 2 == 0) ? pal : (pal >> 4)) & 0x07);

            if (tileset.Last().Count == 4)
                tileset.Add([]);

            tileset.Last().Add(new TileInfo(tile, pal));
        }

        var bmp = BitmapDataFactory.CreateBitmapData(new System.Drawing.Size(256 * 16, 256 * 16), KnownPixelFormat.Format8bppIndexed, palette);

        for (int i = 0; i < mapData.Length; i++)
        {
            var tileX = i % 256;
            var tileY = i / 256;

            var tile = tileset[mapData[i]];

            tiles[tile[0].Index].DrawInto(bmp, tileX * 16, tileY * 16, false, false, tile[0].Palette * 16);
            tiles[tile[1].Index].DrawInto(bmp, tileX * 16 + 8, tileY * 16, false, false, tile[1].Palette * 16);
            tiles[tile[2].Index].DrawInto(bmp, tileX * 16, tileY * 16 + 8, false, false, tile[2].Palette * 16);
            tiles[tile[3].Index].DrawInto(bmp, tileX * 16 + 8, tileY * 16 + 8, false, false, tile[3].Palette * 16);
        }

        return new BasicSprite(bmp);
    }

    public void Dispose()
    {
        foreach (var map in _worldMapCache.Values)
            map.Dispose();

        _worldMapCache.Clear();
    }

    private record TileInfo(int Index, int Palette);
}
