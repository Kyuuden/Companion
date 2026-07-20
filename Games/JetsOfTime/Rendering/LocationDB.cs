using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FF.Rando.Companion.Games.JetsOfTime.Rendering;
internal class LocationDB : IDisposable
{
    private readonly Dictionary<int, Location> _locationCache = [];

    private readonly Container _container;
    private readonly LocationTileCache _locationTileCache;

    public LocationDB(Container container)
    {
        _container = container;
        _locationTileCache = new LocationTileCache(_container);
    }

    public Location Get(int index)
    {
        if (_locationCache.TryGetValue(index, out var location)) return location;

        location = new Location(_container, _locationTileCache, index);
        _locationCache.Add(index, location);
        return location;
    }

    public void Dispose()
    {
        foreach (var loc in _locationCache.Values)
            loc.Dispose();
        _locationCache.Clear();
    }
}

internal class LocationTileCache(Container container)
{
    private readonly Dictionary<int, List<byte[,]>> _tiles = [];

    public List<byte[,]> Get(int index)
    {
        if (index >= 0xFF) return [];

        if (_tiles.TryGetValue(index, out var tiles)) return tiles;

        var compressedTiles = container.Rom.ReadBytes(Data.Addresses.ROM.LocationMaps.TileData[index]);
        var decompressed = Utils.DecompressData(compressedTiles);

        tiles = decompressed.ReadMany<byte[]>(0x20 * 8).Select(b => b.DecodeTile(4)).ToList();
        _tiles[index] = tiles;
        return tiles;
    }
}

internal class Location : IDisposable
{
    private readonly Palette _palette;
    private readonly List<byte[,]> _allLayer12TileData = [];
    private readonly List<byte[,]> _layer12AnimatedTileData;
    private readonly List<BlockInfo> _layer12Blocks = [];
    private readonly List<int> _layer1BlockIds = [];
    private readonly List<int> _layer2BlockIds = [];
    private readonly List<int> _layer3BlockIds = [];

    private readonly Size _size;
    private readonly Size _layer1Size;
    private readonly Size _layer2Size;
    private readonly Size _layer3Size;

    private readonly Dictionary<SpriteKey, ISprite> _renderedSprites = [];

    public Location(Container container, LocationTileCache tileCache, int index)
    {
        var header = container.Rom.ReadBytes(Data.Addresses.ROM.LocationMaps.Headers[index]);

        var layer12Index = header[1];
        var layer3Index = header[2];
        var paletteIndex = header[3];
        var mapIndex = BinaryPrimitives.ReadUInt16LittleEndian(header.AsSpan()[4..]);

        var paletteData = container.Rom.ReadBytes(Data.Addresses.ROM.LocationMaps.PaletteData[paletteIndex]);
        var colors = new List<Color32>([new(), new(), new(), new(), new(), new(), new(), new(), new(), new(), new(), new(), new(), new(), new(), new()]);
        foreach (var color in MemoryMarshal.Cast<byte, ushort>(paletteData))
        {
            if ((colors.Count % 16) == 0) colors.Add(new Color32());
            colors.Add(color.ToColor());
        }
        _palette = new Palette(colors);

        var layer12TileSets = container.Rom.ReadBytes(Data.Addresses.ROM.LocationMaps.TileSets[layer12Index]);

        _layer12AnimatedTileData = tileCache.Get(layer12TileSets[6]);
        _allLayer12TileData.AddRange(tileCache.Get(layer12TileSets[0]));
        _allLayer12TileData.AddRange(tileCache.Get(layer12TileSets[1]));
        _allLayer12TileData.AddRange(tileCache.Get(layer12TileSets[2]));
        _allLayer12TileData.AddRange(tileCache.Get(layer12TileSets[3]));
        _allLayer12TileData.AddRange(tileCache.Get(layer12TileSets[4]));
        _allLayer12TileData.AddRange(tileCache.Get(layer12TileSets[5]));
        _allLayer12TileData.AddRange(tileCache.Get(layer12TileSets[7]));

        var assemblyData = Utils.DecompressData(container.Rom.ReadBytes(Data.Addresses.ROM.LocationMaps.Layer12AssemblyData[layer12Index])).AsSpan();

        var currentBlock = new BlockInfo();
        for (int i = 0; i < assemblyData.Length; i+=2)
        {
            currentBlock.Tiles.Add(new TileInfo(BinaryPrimitives.ReadUInt16LittleEndian(assemblyData[i..])));
            if (currentBlock.Tiles.Count == 4)
            {
                _layer12Blocks.Add(currentBlock);
                currentBlock = new BlockInfo();
            }
        }

        var mapData = Utils.DecompressData(container.Rom.ReadBytes(Data.Addresses.ROM.LocationMaps.MapData[mapIndex])).AsReadOnlySpan();
        var layer12Size = mapData[0];
        var flags = mapData[1];
        mapData = mapData[6..];

        _layer1Size = new Size(((layer12Size >> 0) & 0x03) * 16 + 16, ((layer12Size >> 2) & 0x03) * 16 + 16);
        var layer1BlockCount = _layer1Size.Width * _layer1Size.Height;
        for (int i = 0; i < layer1BlockCount; i++) 
            _layer1BlockIds.Add(mapData[i]);

        mapData = mapData[layer1BlockCount..];

        _layer2Size = new Size(((layer12Size >> 4) & 0x03) * 16 + 16, ((layer12Size >> 6) & 0x03) * 16 + 16);
        var layer2BlockCount = _layer2Size.Width * _layer2Size.Height;
        for (int i = 0; i < layer2BlockCount; i++)
            _layer2BlockIds.Add(mapData[i]);

        mapData = mapData[layer2BlockCount..];

        _layer3Size = new Size(((flags >> 0) & 0x03) * 16 + 16, ((flags >> 2) & 0x03) * 16 + 16);
        var layer3BlockCount = _layer3Size.Width * _layer3Size.Height;
        var layer3BlockIds = new List<int>();

        if ((flags & 0x80) != 0)
        {
            for (int i = 0; i < layer3BlockCount; i++)
                _layer3BlockIds.Add(mapData[i]);

            mapData = mapData[layer3BlockCount..];
        }

        _size = new Size(Math.Max(_layer1Size.Width, _layer2Size.Width), Math.Max(_layer1Size.Height, _layer2Size.Height));
        var properties = new List<BlockProperties>();

        while (mapData.Length > 3)
        {
            var prop = (BlockProperties)((mapData[2] << 16 | mapData[1] << 8 | mapData[0]) & 0xFCF083);
            mapData = mapData[3..];

            if (prop.IsFlagSet(BlockProperties.Compressed))
            {
                int count = mapData[0];
                mapData = mapData[1..];
                count = count > 0 ? count : 256;
                properties.AddRange(Enumerable.Repeat(prop, count));
            }
            else
            {
                properties.Add(prop);
            }
        }

        while (properties.Count < (_size.Width * _size.Height))
            properties.Add(BlockProperties.None);

        while (properties.Count > (_size.Width * _size.Height))
            properties.RemoveAt(properties.Count - 1);

        for (int y = 0; y < _layer1Size.Height; y++)
        {
            for (int x = 0; x < _layer1Size.Width; x++)
            {
                var props = properties[y * _size.Width + x];
                if (props.IsFlagSet(BlockProperties.Layer1_HighBank))
                   _layer1BlockIds[y * _layer1Size.Width + x] |= 256;// = Math.Min(layer1BlockIds[y * layer1Width + x] + 256, _layer12Blocks.Count);
            }
        }

        for (int y = 0; y < _layer2Size.Height; y++)
        {
            for (int x = 0; x < _layer2Size.Width; x++)
            {
                var props = properties[y * _size.Width + x];
                if (props.IsFlagSet(BlockProperties.Layer2_HighBank))
                    _layer2BlockIds[y * _layer2Size.Width + x] |= 256;// = Math.Min(layer2BlockIds[y * layer2Width + x] + 256, _layer12Blocks.Count);
            }
        }
    }

    public ISprite? Render(bool includeLayer1, bool includeLayer2, bool includeLayer3)
    {
        //width = 16 * 8;
        //height = (_allLayer12TileData.Count / 16) * 8;
        //var test = BitmapDataFactory.CreateBitmapData(width, height, KnownPixelFormat.Format8bppIndexed, _palette);
        //for (int i = 0; i < _allLayer12TileData.Count; i++)
        //{
        //    _allLayer12TileData[i].DrawInto(test, (i % 16) * 8, (i / 16) * 8, colorOffset: 2);
        //}

        //test.MakeGrayscale();
        //return new BasicSprite(test);

        if (_renderedSprites.TryGetValue(new SpriteKey(includeLayer1, includeLayer2, includeLayer3), out var sprite))
            return sprite;

        var location = BitmapDataFactory.CreateBitmapData(_size.Width * 16, _size.Height * 16, KnownPixelFormat.Format8bppIndexed, _palette);

        if (includeLayer2)
            RenderLayer(location, _layer2Size, _layer2BlockIds, _layer12Blocks, false);

        if (includeLayer1)
            RenderLayer(location, _layer1Size, _layer1BlockIds, _layer12Blocks, false);

        if (includeLayer2)
            RenderLayer(location, _layer2Size, _layer2BlockIds, _layer12Blocks, true);

        if (includeLayer1)
            RenderLayer(location, _layer1Size, _layer1BlockIds, _layer12Blocks, true);

        sprite = new BasicSprite(location);
        _renderedSprites[new SpriteKey(includeLayer1, includeLayer2, includeLayer3)] = sprite;
        return sprite;
    }

    private void RenderLayer(IReadWriteBitmapData bmp, Size layerSize, List<int> blockIds, List<BlockInfo> blocks, bool priorityTiles)
    {
        for (int y = 0; y < bmp.Height / 16; y++)
        {
            for (int x = 0; x < bmp.Width/ 16; x++)
            {
                if (x >= layerSize.Width || y >= layerSize.Height)
                    continue;

                var block = blocks[blockIds[y * layerSize.Width + x]];
                RenderBlock(bmp, x * 16, y * 16, block, priorityTiles);
            }
        }
    }

    private void RenderBlock(IWritableBitmapData bmp, int x, int y, BlockInfo block, bool priorityTiles)
    {
        if (block.Tiles[0].Priority == priorityTiles && block.Tiles[0].Index < _allLayer12TileData.Count)
            _allLayer12TileData[block.Tiles[0].Index].DrawInto(bmp, x + 0, y + 0, block.Tiles[0].FlipHoriztonal, block.Tiles[0].FlipVertical, block.Tiles[0].Palette * 16);

        if (block.Tiles[1].Priority == priorityTiles && block.Tiles[1].Index < _allLayer12TileData.Count)
            _allLayer12TileData[block.Tiles[1].Index].DrawInto(bmp, x + 8, y + 0, block.Tiles[1].FlipHoriztonal, block.Tiles[1].FlipVertical, block.Tiles[1].Palette * 16);

        if (block.Tiles[2].Priority == priorityTiles && block.Tiles[2].Index < _allLayer12TileData.Count)
            _allLayer12TileData[block.Tiles[2].Index].DrawInto(bmp, x + 0, y + 8, block.Tiles[2].FlipHoriztonal, block.Tiles[2].FlipVertical, block.Tiles[2].Palette * 16);

        if (block.Tiles[3].Priority == priorityTiles && block.Tiles[3].Index < _allLayer12TileData.Count)
            _allLayer12TileData[block.Tiles[3].Index].DrawInto(bmp, x + 8, y + 8, block.Tiles[3].FlipHoriztonal, block.Tiles[3].FlipVertical, block.Tiles[3].Palette * 16);
    }

    public void Dispose()
    {
        foreach (var sprite in _renderedSprites.Values)
            sprite.Dispose();

        _renderedSprites.Clear();
    }

    private record SpriteKey(bool Layer1, bool Layer2, bool Layer3);

    [Flags]
    private enum BlockProperties
    {
        None = 0,
        Layer1_HighBank      = 0x000001,
        Layer2_HighBank      = 0x000002,
        Compressed           = 0x000080,

        Door                 = 0x001000,
        Uknown1              = 0x002000,
        Top_Above_All        = 0x004000,
        NPC_Collision_Battle = 0x008000,

        Collision_Ignore_Z   = 0x040000,
        Collision_Inverted   = 0x080000,
        Uknown2              = 0x100000,
        Z_Neutral            = 0x200000,
        Bottom_Above_All     = 0x400000,
        NPC_Collision        = 0x800000,
    }

    public record class TileInfo
    {
        public int Index { get; }
        public int Palette { get; }
        public bool FlipHoriztonal { get; }
        public bool FlipVertical { get; }
        public bool Priority { get; }

        public TileInfo(ushort value)
        {
            Index = Math.Max(0, (value & 0x3ff) - 256);
            Palette = ((value & 0x1c00) >> 10);
            Priority = (value & 0x2000) != 0;
            FlipHoriztonal = (value & 0x4000) != 0;
            FlipVertical = (value & 0x8000) != 0;
        }

        protected virtual bool PrintMembers(StringBuilder builder)
        {
            builder.Append($"Index = {Index:X}, Palette = {Palette:X}");
            if (Priority)
                builder.Append(", Priority");
            if (FlipHoriztonal)
                builder.Append(", FlipH");
            if (FlipVertical)
                builder.Append(", FlipV");
            return true;
        }
    }

    private class BlockInfo
    {
        public List<TileInfo> Tiles { get; } = [];
    }
}
