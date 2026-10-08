using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.RomData;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Rendering.SNES;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;


namespace FF.Rando.Companion.Games.WorldsCollide.Rendering;
internal class LocationMaps : IDisposable
{
    private readonly byte[,] _blankTile;
    private readonly Dictionary<int, Dictionary<int, IReadableBitmapData>> _l3TileSets = [];
    private readonly List<byte[,]> _animatedTiles = [];
    private readonly byte[] _locationGraphicsData;
    private readonly byte[] _locationL3GraphicsData;
    private readonly byte[] _locationTileSets;
    private readonly byte[] _locationTileMaps;
    private readonly byte[] _locationAnimationData;
    private readonly List<ColorMath> _colorMathModes;
    private readonly Dictionary<int, ISprite> _locationMapCache = [];
    private readonly Seed _seed;

    public LocationMaps(Seed seed)
    {
        _seed = seed;
        _locationGraphicsData = _seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationTileData);
        _locationL3GraphicsData = _seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationL3TileData);
        _locationTileSets = _seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationTileSets);
        _locationTileMaps = _seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationTileMaps);
        _locationAnimationData = _seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationAnimationData);
        _colorMathModes = _seed.Rom.ReadBytes(Addresses.ROM.Locations.ColorMathModes).ReadMany<byte[]>(3 * 8).Select(b => new ColorMath(b)).ToList();

        var animatedTileData = seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationAnimatedTileData).AsSpan();
        for (var i = 0; i < animatedTileData.Length; i += 0x20)
        {
            _animatedTiles.Add(animatedTileData.Slice(i, 0x20).DecodeTile(4));
        }

        _blankTile = new byte[8, 8];
    }

    public IReadableBitmapData GetL3TileSet(int graphicsSet, int paletteSet)
    {
        if (_l3TileSets.TryGetValue(graphicsSet, out var tilesets) && tilesets.TryGetValue(paletteSet, out var tileset))
            return tileset;

        var palette = _seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationPalettes[paletteSet]).DecodeMultiPalette(16);

        if (tilesets != null && tilesets.Count > 0)
        {
            var existing = tilesets.First().Value;
            var clone = existing.Clone();
            if (clone.TrySetPalette(palette))
            {
                tilesets[paletteSet] = clone;
                return clone;
            }
        }

        var l3data = Utils.Decompress(_locationL3GraphicsData.AsSpan()[Addresses.ROM.Locations.L3TileDataOffsets[graphicsSet]..]);
        var l3palettedata = l3data.AsSpan()[..0x40];
        var l3graphics = l3data.AsSpan()[0x40..];
        var tileSet  = BitmapDataFactory.CreateBitmapData(new Size(256*16, 16), KnownPixelFormat.Format8bppIndexed, palette);
        List<byte[,]> l3Tiles = [];
        DecodeTiles(l3graphics, l3Tiles, l3graphics.Length / 0x10, 2);

        for (var i = 0; i < 256; i++)
        {
            DrawTile(new MapTile((byte)((i * 4) + 0), l3palettedata[i % 64]), l3Tiles, false, false, false, tileSet, i * 16, 0);
            DrawTile(new MapTile((byte)((i * 4) + 1), l3palettedata[i % 64]), l3Tiles, false, false, false, tileSet, i * 16 + 8, 0);
            DrawTile(new MapTile((byte)((i * 4) + 2), l3palettedata[i % 64]), l3Tiles, false, false, false, tileSet, i * 16, 8);
            DrawTile(new MapTile((byte)((i * 4) + 3), l3palettedata[i % 64]), l3Tiles, false, false, false, tileSet, i * 16 + 8, 8);
        }

        if (tilesets == null)
        {
            tilesets = [];
            _l3TileSets.Add(graphicsSet, tilesets);
        }

        tilesets.Add(paletteSet, tileSet);
        return tileSet;
    }

    public ISprite Get(MapLocation location)
    {
        if (_locationMapCache.TryGetValue((int)location, out var map)) return map;

        map =  GenerateMap((int)location);
        _locationMapCache.Add((int)location, map);
        return map;
    }

    private ISprite GenerateMap(int index)
    {
        var properties = new LocationProperties(_seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationProperties[index]));

        var colorMath = _colorMathModes[properties.ColorMathIndex];
        var l12graphicsspan = _locationGraphicsData.AsSpan();
        var mapDataSpan = _locationTileMaps.AsSpan();

        byte[] l1Map, l2Map, l3Map;
        List<byte[,]> l12Tiles = [];

        var palette = _seed.Rom.ReadBytes(Addresses.ROM.Locations.LocationPalettes[properties.L12Palette]).DecodeMultiPalette(16);

        DecodeTiles(l12graphicsspan.Slice(Addresses.ROM.Locations.TileDataOffsets[properties.L12GraphicsSet1]), l12Tiles, 0x100);
        DecodeTiles(l12graphicsspan.Slice(Addresses.ROM.Locations.TileDataOffsets[properties.L12GraphicsSet2]), l12Tiles, 0x80);
        DecodeTiles(l12graphicsspan.Slice(Addresses.ROM.Locations.TileDataOffsets[properties.L12GraphicsSet3]), l12Tiles, 0x80);
        DecodeTiles(l12graphicsspan.Slice(Addresses.ROM.Locations.TileDataOffsets[properties.L12GraphicsSet4]), l12Tiles, 0x80);

        AssembleAnimationTiles(_locationAnimationData.AsSpan().Slice(Addresses.ROM.Locations.LocationAnimationOffsets[properties.L2AnimationSet]), l12Tiles);

        IReadWriteBitmapData? l1Priority0 = null, l1Priority1 = null, l2Priority0 = null, l2Priority1 = null, l3Priority0 = null;

        var l1Blocks = new List<MapBlock>();
        var l2Blocks = new List<MapBlock>();

        if (properties.L1TileMap != 0)
        {
            l1Blocks.AddRange(GetTileSetBlocks(Utils.Decompress(_locationTileSets.AsSpan()[Addresses.ROM.Locations.TileSetOffsets[properties.L1TileSet]..])));
            l1Map = Utils.Decompress(mapDataSpan[Addresses.ROM.Locations.TileMapOffsets[properties.L1TileMap]..]);

            l1Priority0 = BitmapDataFactory.CreateBitmapData(properties.L1Size, KnownPixelFormat.Format8bppIndexed, palette);
            l1Priority1 = BitmapDataFactory.CreateBitmapData(properties.L1Size, KnownPixelFormat.Format8bppIndexed, palette);

            DrawLayer(l1Priority0, l1Map, l1Blocks, l12Tiles, false);
            DrawLayer(l1Priority1, l1Map, l1Blocks, l12Tiles, true);
        }

        if (properties.L2TileMap != 0)
        {
            l2Blocks.AddRange(GetTileSetBlocks(Utils.Decompress(_locationTileSets.AsSpan()[Addresses.ROM.Locations.TileSetOffsets[properties.L2TileSet]..])));
            l2Map = Utils.Decompress(mapDataSpan[Addresses.ROM.Locations.TileMapOffsets[properties.L2TileMap]..]);

            l2Priority0 = BitmapDataFactory.CreateBitmapData(properties.L2Size, KnownPixelFormat.Format8bppIndexed, palette);
            l2Priority1 = BitmapDataFactory.CreateBitmapData(properties.L2Size, KnownPixelFormat.Format8bppIndexed, palette);

            DrawLayer(l2Priority0, l2Map, l2Blocks, l12Tiles, false);
            DrawLayer(l2Priority1, l2Map, l2Blocks, l12Tiles, true);
        }

        if (properties.L3TileMap != 0)
        {
            var l3TileSet = GetL3TileSet(properties.L3GraphicsSet, properties.L12Palette);
            l3Map = Utils.Decompress(mapDataSpan[Addresses.ROM.Locations.TileMapOffsets[properties.L3TileMap]..]);
            l3Priority0 = BitmapDataFactory.CreateBitmapData(properties.L3Size, KnownPixelFormat.Format8bppIndexed, palette);
            DrawL3(l3Priority0, l3Map, l3TileSet);
        }

        var result = BitmapDataFactory.CreateBitmapData(properties.Size);

        if (colorMath.HasSubscreen)
        {
            var subscreen = BitmapDataFactory.CreateBitmapData(properties.Size);
            if (!properties.TopPriorityL3 && colorMath.SubscreenL3)
                l3Priority0?.DrawInto(subscreen);

            if (colorMath.SubscreenL2) l2Priority0?.DrawInto(subscreen);
            if (colorMath.SubscreenL1) l1Priority0?.DrawInto(subscreen);
            if (colorMath.SubscreenL2) l2Priority1?.DrawInto(subscreen);
            if (colorMath.SubscreenL1) l1Priority1?.DrawInto(subscreen);

            if (properties.TopPriorityL3 && colorMath.SubscreenL3)
                l3Priority0?.DrawInto(subscreen);

            if (!properties.TopPriorityL3)
                DrawLayerWithColorMath(l3Priority0, subscreen, result, colorMath.MainscreenL3, colorMath.PerformColorMathOnL3, colorMath.Mode);

            DrawLayerWithColorMath(l2Priority0, subscreen, result, colorMath.MainscreenL2, colorMath.PerformColorMathOnL2, colorMath.Mode);
            DrawLayerWithColorMath(l1Priority0, subscreen, result, colorMath.MainscreenL1, colorMath.PerformColorMathOnL1, colorMath.Mode);
            DrawLayerWithColorMath(l2Priority1, subscreen, result, colorMath.MainscreenL2, colorMath.PerformColorMathOnL2, colorMath.Mode);
            DrawLayerWithColorMath(l1Priority1, subscreen, result, colorMath.MainscreenL1, colorMath.PerformColorMathOnL1, colorMath.Mode);

            if (properties.TopPriorityL3)
                DrawLayerWithColorMath(l3Priority0, subscreen, result, colorMath.MainscreenL3, colorMath.PerformColorMathOnL3, colorMath.Mode);
        }
        else
        {
            if (!properties.TopPriorityL3 && colorMath.MainscreenL3)
                l3Priority0?.DrawInto(result);

            if (colorMath.MainscreenL2) l2Priority0?.DrawInto(result);
            if (colorMath.MainscreenL1) l1Priority0?.DrawInto(result);
            if (colorMath.MainscreenL2) l2Priority1?.DrawInto(result);
            if (colorMath.MainscreenL1) l1Priority1?.DrawInto(result);

            if (properties.TopPriorityL3 && colorMath.MainscreenL3)
                l3Priority0?.DrawInto(result);
        }

        l1Priority0?.Dispose();
        l1Priority1?.Dispose();
        l2Priority0?.Dispose();
        l2Priority1?.Dispose();
        l3Priority0?.Dispose();

        return new BasicSprite(result);
    }

    private void DrawLayerWithColorMath(IReadWriteBitmapData? layer, IReadWriteBitmapData subscreen, IReadWriteBitmapData mainscreen, bool isMainScreen, bool performColorMath, ColorMathMode mode)
    {
        if (isMainScreen && layer != null)
        {
            if (performColorMath)
                PerformColorMath(mode, layer, subscreen, mainscreen);
            else
                layer.DrawInto(mainscreen);
        }
    }

    private void PerformColorMath(ColorMathMode mode, IReadWriteBitmapData layer, IReadWriteBitmapData subscreen, IReadWriteBitmapData mainscreen)
    {
        var layerCopy = BitmapDataFactory.CreateBitmapData(layer.Size);
        layer.CopyTo(layerCopy);

        var lRow = layerCopy.GetMovableRow(0);
        var sRow = subscreen.GetMovableRow(0);

        do
        {
            for (var x = 0; x < layer.Width; x++)
            {
                var sColor = sRow.GetColor32(x);
                var lColor = lRow.GetColor32(x);
                if (sColor.A == 0 || lColor.A == 0)
                    continue;

                var rColor = lColor.BlendWith(sColor, mode);
                lRow.SetColor32(x, rColor);
            }
        }
        while (lRow.MoveNextRow() && sRow.MoveNextRow());

        layerCopy.DrawInto(mainscreen);
    }

    private void DecodeTiles(ReadOnlySpan<byte> tileData, List<byte[,]> tiles, int tileCount, byte bpp = 4)
    {
        var size = 8 * bpp;
        var decoded = 0;

        for(var i = 0; i < tileData.Length; i += size)
        {
            tiles.Add(tileData.Slice(i, size).DecodeTile(bpp));
            decoded++;
            if (decoded == tileCount)
                break;
        }

        while (decoded < tileCount)
        {
            tiles.Add(_blankTile);
            decoded++;
        }
    }

    private void AssembleAnimationTiles(ReadOnlySpan<byte> frameData, List<byte[,]> tiles)
    {
        var frames = MemoryMarshal.Cast<byte, ushort>(frameData);
        for (var i = 0; i < frames.Length; i++)
        {
            if (i % 5 == 0)
                continue;

            var offset = frames[i] / 0x20;

            tiles.Add(_animatedTiles[offset]);
            tiles.Add(_animatedTiles[offset+1]);
            tiles.Add(_animatedTiles[offset+2]);
            tiles.Add(_animatedTiles[offset+3]);
        }
    }

    private static void DrawL3(IReadWriteBitmapData dst, byte[] blockIds, IReadableBitmapData tileSet)
    {
        var tileWidth = dst.Width / 16;
        for (int i = 0; i < blockIds.Length; i++)
        {
            var target = new Point((i % tileWidth) * 16, (i / tileWidth) * 16);

            var blockId = blockIds[i];
            var flipH = (blockId & 0x40) == 0x40;
            var flipV = (blockId & 0x80) == 0x80;
            blockId &= 0x3F;

            var sRect = new Rectangle(blockId * 16, 0, 16, 16);

            if (!flipH && !flipV)
            {
                tileSet.DrawInto(dst, sRect, target);
            }
            else
            {
                var op = (flipH && flipV) ? RotateFlipType.RotateNoneFlipXY
                    : flipH ? RotateFlipType.RotateNoneFlipX : RotateFlipType.RotateNoneFlipY;

                using var clone = tileSet.Clone(sRect);
                using var rot = clone.CopyRotateFlip(op);
                rot.DrawInto(dst, target);
            }
        }
    }

    private static void DrawLayer(IReadWriteBitmapData dst, byte[] blockIds, List<MapBlock> blocks, List<byte[,]> tiles, bool priority)
    {
        var tileWidth = dst.Width / 16;
        for (int i = 0; i < blockIds.Length; i++)
        {
            var tileX = i % tileWidth;
            var tileY = i / tileWidth;

            var blockId = blockIds[i];
            var block = blocks[blockId];
            DrawTile(block.UpperLeft, tiles, priority, dst, tileX * 16, tileY * 16);
            DrawTile(block.UpperRight, tiles, priority, dst, tileX * 16 + 8, tileY * 16);
            DrawTile(block.LowerLeft, tiles, priority, dst, tileX * 16, tileY * 16 + 8);
            DrawTile(block.LowerRight, tiles, priority, dst, tileX * 16 + 8, tileY * 16 + 8);
        }
    }

    private static void DrawTile(MapTile tile, List<byte[,]> tiles, bool priority, bool flipH, bool flipV, IReadWriteBitmapData dst, int x, int y)
    {
        if (tile.Priority != priority)
            return;

        tiles[tile.Index].DrawInto(dst, x, y, flipH, flipV, (tile.Palette / 4) * 16 + (tile.Palette % 4 * 4));
    }

    private static void DrawTile(MapTile tile, List<byte[,]> tiles, bool priority, IReadWriteBitmapData dst, int x, int y)
    {
        if (tile.Priority != priority)
            return;

        tiles[tile.Index].DrawInto(dst, x, y, tile.FlipHoriztonal, tile.FlipVertical, tile.Palette * 16);
    }

    private IEnumerable<MapBlock> GetTileSetBlocks(byte[] data)
    {
        var offset = 0;
        for (var i = 0; i < 256; i++)
        {
            yield return new MapBlock(
            [
                new MapTile(data[offset],       data[offset + 0x400]),
                new MapTile(data[offset+0x100], data[offset + 0x500]),
                new MapTile(data[offset+0x200], data[offset + 0x600]),
                new MapTile(data[offset+0x300], data[offset + 0x700])
            ]);

            offset += 1;
        }
    }

    public void Dispose()
    {
        foreach (var map in _locationMapCache.Values)
            map.Dispose();

        _locationMapCache.Clear();

        foreach (var graphics in _l3TileSets.Values)
            foreach (var set in graphics.Values)
                set.Dispose();

        _l3TileSets.Clear();
    }

    private class ColorMath
    {
        public ColorMath(byte[] data)
        {
            PerformColorMathOnL1 = data.Read<bool>(0);
            PerformColorMathOnL2 = data.Read<bool>(1);
            PerformColorMathOnL3 = data.Read<bool>(2);
            PerformColorMathOnSprites = data.Read<bool>(4);
            PerformColorMathOnBackground = data.Read<bool>(5);

            var mode = data.Read<byte>(6, 2);
            Mode = mode switch
            {
                1 => ColorMathMode.Average,
                2 => ColorMathMode.Subtractive,
                3 => ColorMathMode.SubtractiveAverage,
                _ => ColorMathMode.Additive,
            };

            MainscreenL1 = data.Read<bool>(8);
            MainscreenL2 = data.Read<bool>(9);
            MainscreenL3 = data.Read<bool>(10);
            MainscreenSprites = data.Read<bool>(12);

            SubscreenL1 = data.Read<bool>(16);
            SubscreenL2 = data.Read<bool>(17);
            SubscreenL3 = data.Read<bool>(18);
            SubscreenSprites = data.Read<bool>(20);
        }

        public bool MainscreenL1 { get; }
        public bool MainscreenL2 { get; }
        public bool MainscreenL3 { get; }
        public bool MainscreenSprites { get; }

        public bool SubscreenL1 { get; }
        public bool SubscreenL2 { get; }
        public bool SubscreenL3 { get; }
        public bool SubscreenSprites { get; }

        public bool HasSubscreen => SubscreenL1 || SubscreenL2 || SubscreenL3 || SubscreenSprites;

        public bool PerformColorMathOnL1 { get; }
        public bool PerformColorMathOnL2 { get; }
        public bool PerformColorMathOnL3 { get; }
        public bool PerformColorMathOnSprites { get; }
        public bool PerformColorMathOnBackground { get; }
        public ColorMathMode Mode { get; }
    }

    private class LocationProperties(byte[] data)
    {
        public int L12GraphicsSet1 { get; } = data.Read<byte>(56, 7);
        public int L12GraphicsSet2 { get; } = data.Read<byte>(63, 7);
        public int L12GraphicsSet3 { get; } = data.Read<byte>(70, 7);
        public int L12GraphicsSet4 { get; } = data.Read<byte>(77, 7);
        public int L3GraphicsSet { get; } = data.Read<byte>(84, 6);
        public int L2AnimationSet { get; } = data.Read<byte>(0x1B * 8, 5);
        public int L3AnimationSet { get; } = data.Read<byte>(0x1B * 8 + 5, 3);
        public int L1TileSet { get; } = data.Read<byte>(90, 7);
        public int L2TileSet { get; } = data.Read<byte>(97, 7);
        public int L1TileMap { get; } = data.Read<ushort>(104, 10);
        public int L2TileMap { get; } = data.Read<ushort>(114, 10);
        public int L3TileMap { get; } = data.Read<ushort>(124, 10);
        public Size L1Size { get; } = GetSize(data[0x17] >> 4);
        public Size L2Size { get; } = GetSize(data[0x17]);
        public Size L3Size { get; } = GetSize(data[0x18] >> 4);
        public int L12Palette { get; } = data[0x19];
        public byte L2LeftShift { get; } = data[0x12];
        public byte L2UpShift { get; } = data[0x13];
        public byte L3LeftShift { get; } = data[0x14];
        public byte L3UpShift { get; } = data[0x15];
        public byte ColorMathIndex { get; } = data[0x20];
        public bool TopPriorityL3 { get; } = data.Read<bool>(23);
        private static Size GetSize(int b)
            => (b & 0xF) switch
            {
                0x0 => new Size(256, 256),
                0x1 => new Size(256, 512),
                0x2 => new Size(256, 1024),
                0x3 => new Size(256, 2048),
                0x4 => new Size(512, 256),
                0x5 => new Size(512, 512),
                0x6 => new Size(512, 1024),
                0x7 => new Size(512, 2048),
                0x8 => new Size(1024, 256),
                0x9 => new Size(1024, 512),
                0xa => new Size(1024, 1024),
                0xc => new Size(2048, 256),
                0xd => new Size(2048, 512),
                0xe => new Size(2048, 1024),
                _ => new Size(2048, 2048),
            };
        public Size Size
        {
            get
            {
                return new Size(
                    ((IEnumerable<int>)[L1Size.Width, L2Size.Width, L3Size.Width]).Max(),
                    ((IEnumerable<int>)[L1Size.Height, L2Size.Height, L3Size.Height]).Max());
            }
        }
    }
}
