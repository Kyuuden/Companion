using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.JetsOfTime.Data;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FF.Rando.Companion.Games.JetsOfTime.Rendering;
internal class WorldMaps : IDisposable
{
    private readonly Dictionary<MapType, WorldMap> _worldMapCache = [];

    private readonly WorldSprites _worldSprites;

    private readonly Seed _seed;

    public WorldMaps(Seed seed)
    {
        _seed = seed;
        _worldSprites = new WorldSprites(seed.Rom);

    }

    public WorldMap Get(MapType type)
    {
        if (_worldMapCache.TryGetValue(type, out WorldMap map)) return map;

        map = new WorldMap(_seed.Rom, (int)type);
        _worldMapCache.Add(type, map);
        return map;
    }

    //public Bitmap GetSpriteTiles(int page)
    //{
    //    if (page >= _spriteTiles.Count)
    //        return null;

    //    var palette = page switch
    //    {
    //        1 => _spritePalettes[0],
    //        8 => _spritePalettes[5],

    //        11 => _spritePalettes[9],
    //        12 => _spritePalettes[9],
    //        _ => _spritePalettes[page],
    //    };

    //    var bmpData = BitmapDataFactory.CreateBitmapData(16*8, 16*8, KnownPixelFormat.Format8bppIndexed, palette);

    //    for (int y = 0; y < 16; y++)
    //        for (int x = 0; x < 16; x++)
    //            if ((y * 16 + x) < _spriteTiles[page].Count)
    //                _spriteTiles[page][y * 16 + x].DrawInto(bmpData, x * 8, y * 8, colorOffset:
    //                    page switch
    //                    {
    //                        0 => (y / 2) * 16,
    //                        _ => (_paletteOffsets[page]) * 8,
    //                    }
    //                    );

    //    _paletteOffsets[page] = (_paletteOffsets[page] + 1) % (palette.Count / 8);

    //    return bmpData.ToBitmap();
    //}

    public void Dispose()
    {
        foreach (var map in _worldMapCache.Values)
            map.Dispose();

        _worldMapCache.Clear();
        _worldSprites.Dispose();
    }
}

internal class WorldSprites : IDisposable
{
    private readonly List<byte[,]> _tiles = [];
    private readonly byte[] _scriptData;
    private Dictionary<int, int> _spriteOffsets = [];
    private Dictionary<int, List<BlockInfo>> _spriteBlocks = [];

    public WorldSprites(IMemorySpace rom)
    {
        foreach (var range in Addresses.ROM.WorldMaps.SpriteTileData)
        {
            var decompressed = Utils.DecompressData(rom.ReadBytes(range));
            _tiles.AddRange(decompressed.ReadMany<byte[]>(0x20 * 8).Select(b => b.DecodeTile(4)));
        }

        _scriptData = rom.ReadBytes(Addresses.ROM.WorldMaps.SpriteScriptData);

        var offsetnum = 0;
        foreach (var offset in MemoryMarshal.Cast<byte, ushort>(_scriptData)[..166])
        {
            _spriteOffsets[offsetnum++] = offset - 0xe000;
        }

        foreach (var kvp in _spriteOffsets)
        {
            var data = _scriptData.AsSpan()[kvp.Value..];
            if (data[0] != 4)
                continue;

            var assemblyOffset = BinaryPrimitives.ReadUInt16LittleEndian(data[1..]) - 0xe000;
            var assemblyData = _scriptData.AsSpan()[assemblyOffset..];

            var tileCount = assemblyData.ReadByte();
            for(int i = 0; i < tileCount; i++)
            {
                var x = assemblyData.ReadByte();
                var y = assemblyData.ReadByte();
                var tileData = assemblyData.ReadUShort();
            }
        }
    }

    public void Dispose()
    {
    }

    public record class TileInfo
    {
        public int Index { get; }
        public bool FlipHoriztonal { get; }
        public bool FlipVertical { get; }
        public bool Priority { get; }

        public TileInfo(ushort value)
        {
            Index = value & 0x3ff;
            Priority = (value & 0x2000) != 0;
            FlipHoriztonal = (value & 0x4000) != 0;
            FlipVertical = (value & 0x8000) != 0;
        }

        protected virtual bool PrintMembers(StringBuilder builder)
        {
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

internal class WorldMap : IDisposable
{
    private static readonly Size _layer12Size = new(1536, 1024);
    private static readonly Size _layer3Size = new(512, 256);
    private static readonly Size _layer12BlockSize = new(96, 64);
    private readonly Dictionary<SpriteKey, ISprite> _renderedSprites = [];

    private readonly List<BlockInfo> _layer12Blocks = [];
    private readonly List<BlockInfo> _layer3Blocks = [];
    private readonly List<byte[,]> _layer12Tiles = [];
    private readonly List<byte[,]> _layer3Tiles = [];
    private readonly List<int> _layer1BlockIds = [];
    private readonly List<int> _layer2BlockIds = [];
    private readonly List<int> _layer3BlockIds = [];
    private readonly Palette _palette;

    public WorldMap(IMemorySpace rom, int index)
    {
        var header = rom.ReadBytes(Addresses.ROM.WorldMaps.Headers[index]).AsSpan();
        var layer12TileSets = header[..8];
        var layer13TileSets = header.Slice(8, 2);
        var paletteIndex = header[10];
        var paletteAnimationIndex = header[11];
        var spriteSets = header.Slice(12, 4);
        var layer12AssemblyIndex = header[16];
        var mapIndex = header[17];
        var propertyIndex = header[18];
        var musicIndex = header[19];
        var layer3AssemblyIndex = header[20];
        var exitsIndex = header[21];
        var scriptIndex = header[22];

        foreach (var set in layer12TileSets)
        {
            if (set < Addresses.ROM.WorldMaps.TileSets.Count)
            {
                var compressedTiles = rom.ReadBytes(Addresses.ROM.WorldMaps.TileSets[set]);
                var decompressed = Utils.DecompressData(compressedTiles);
                _layer12Tiles.AddRange(decompressed.ReadMany<byte[]>(0x20 * 8).Select(b => b.DecodeTile(4)));
            }
        }

        foreach (var set in layer13TileSets)
        {
            if (set < Addresses.ROM.WorldMaps.TileSets.Count)
            {
                var compressedTiles = rom.ReadBytes(Addresses.ROM.WorldMaps.TileSets[set]);
                var decompressed = Utils.DecompressData(compressedTiles);
                _layer3Tiles.AddRange(decompressed.ReadMany<byte[]>(0x10 * 8).Select(b => b.DecodeTile(2)));
            }
        }

        var layer12AssemblyData = Utils.DecompressData(rom.ReadBytes(Addresses.ROM.WorldMaps.Layer12AssemblyData[layer12AssemblyIndex])).AsSpan();

        var currentBlock = new BlockInfo();
        for (int i = 0; i < layer12AssemblyData.Length; i += 2)
        {
            currentBlock.Tiles.Add(new TileInfo(BinaryPrimitives.ReadUInt16LittleEndian(layer12AssemblyData[i..])));
            if (currentBlock.Tiles.Count == 4)
            {
                _layer12Blocks.Add(currentBlock);
                currentBlock = new BlockInfo();
            }
        }

        var layer3AssemblyData = Utils.DecompressData(rom.ReadBytes(Addresses.ROM.WorldMaps.Layer3AssemblyData[layer3AssemblyIndex])).AsSpan();
        var layer3blocks = new SortedDictionary<int, BlockInfo>();

        var tileNum = 0;
        for (int i = 0; i < layer3AssemblyData.Length; i += 2)
        {
            var x = tileNum % 32;
            var y = tileNum / 32;
            var blockX = x / 2;
            var blockY = y / 2;
            var tilex = x % 2;
            var tiley = y % 2;

            if (!layer3blocks.TryGetValue(blockX + (blockY * 16), out currentBlock))
            {
                currentBlock = layer3blocks[blockX + (blockY * 16)] = new BlockInfo();
            }

            currentBlock.Tiles.Add(new TileInfo(BinaryPrimitives.ReadUInt16LittleEndian(layer3AssemblyData[i..])));
            tileNum ++;
        }

        _layer3Blocks = [.. layer3blocks.Values];

        var paletteData = Utils.DecompressData(rom.ReadBytes(Addresses.ROM.WorldMaps.PaletteData[paletteIndex])).AsReadOnlySpan();
        _palette = paletteData.DecodePalette(new Color32());

        var mapData = Utils.DecompressData(rom.ReadBytes(Addresses.ROM.WorldMaps.MapData[mapIndex])).AsSpan();

        for(int i = 0; i < _layer12BlockSize.Width * _layer12BlockSize.Height; i++)
            _layer1BlockIds.Add(mapData.ReadByte());

        for (int i = 0; i < _layer12BlockSize.Width * _layer12BlockSize.Height; i++)
            _layer2BlockIds.Add(mapData.ReadByte() + 256);
    }

    public ISprite Render(IList<IRenderInstruction> instructions)
    {
        var bmp = BitmapDataFactory.CreateBitmapData(_layer12Size, KnownPixelFormat.Format8bppIndexed, _palette);
        List<int> l1Ids = [.. _layer1BlockIds];
        List<int> l2Ids = [.. _layer2BlockIds];

        foreach (var instruction in instructions)
        {
            switch (instruction)
            {
                case SetBlock setBlock when setBlock.Layer == 1:
                    l1Ids[setBlock.Y * _layer12BlockSize.Height + setBlock.X] = setBlock.BlockId;
                    break;
                case SetBlock setBlock when setBlock.Layer == 2:
                    l2Ids[setBlock.Y * _layer12BlockSize.Height + setBlock.X] = setBlock.BlockId;
                    break;
                case CopyBlocks copyBlocks:
                    var src = copyBlocks.SourceLayer == 1 ? _layer1BlockIds : _layer2BlockIds;
                    var dest = copyBlocks.DestinationLayer == 1 ? l1Ids : l2Ids;
                    var srcIndex = copyBlocks.SourceY * _layer12BlockSize.Width + copyBlocks.SourceX;
                    var destIndex = copyBlocks.DestinationY * _layer12BlockSize.Width + copyBlocks.DestinationX;
                    for(var y = 0; y < copyBlocks.Height; y++)
                    {
                        for (var x = 0; x < copyBlocks.Width; x++)
                        {
                            dest[destIndex + x + (y * _layer12BlockSize.Width)] = src[srcIndex + x + (_layer12BlockSize.Width * y)];
                        }
                    }

                    break;
            }
        }

        RenderLayer(bmp, _layer12BlockSize, l2Ids, _layer12Blocks, false);
        RenderLayer(bmp, _layer12BlockSize, l1Ids, _layer12Blocks, false);
        RenderLayer(bmp, _layer12BlockSize, l2Ids, _layer12Blocks, true);
        RenderLayer(bmp, _layer12BlockSize, l1Ids, _layer12Blocks, true);
        return new BasicSprite(bmp);
    }

    public ISprite? Render(bool includeLayer1 = true, bool includeLayer2 = true, bool includeLayer3 = false)
    {
        if (_renderedSprites.TryGetValue(new SpriteKey(includeLayer1, includeLayer2, includeLayer3), out var sprite))
            return sprite;

        var location = BitmapDataFactory.CreateBitmapData(_layer12Size, KnownPixelFormat.Format8bppIndexed, _palette);

        if (includeLayer2)
            RenderLayer(location, _layer12BlockSize, _layer2BlockIds, _layer12Blocks, false);

        if (includeLayer1)
            RenderLayer(location, _layer12BlockSize, _layer1BlockIds, _layer12Blocks, false);

        if (includeLayer2)
            RenderLayer(location, _layer12BlockSize, _layer2BlockIds, _layer12Blocks, true);

        if (includeLayer1)
            RenderLayer(location, _layer12BlockSize, _layer1BlockIds, _layer12Blocks, true);

        sprite = new BasicSprite(location);
        _renderedSprites[new SpriteKey(includeLayer1, includeLayer2, includeLayer3)] = sprite;
        return sprite;
    }

    private void RenderLayer(IReadWriteBitmapData bmp, Size layerSize, List<int> blockIds, List<BlockInfo> blocks, bool priorityTiles)
    {
        for (int y = 0; y < bmp.Height / 16; y++)
        {
            for (int x = 0; x < bmp.Width / 16; x++)
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
        if (block.Tiles[0].Priority == priorityTiles && block.Tiles[0].Index < _layer12Tiles.Count)
            _layer12Tiles[block.Tiles[0].Index].DrawInto(bmp, x + 0, y + 0, block.Tiles[0].FlipHoriztonal, block.Tiles[0].FlipVertical, block.Tiles[0].Palette * 16);

        if (block.Tiles[1].Priority == priorityTiles && block.Tiles[1].Index < _layer12Tiles.Count)
            _layer12Tiles[block.Tiles[1].Index].DrawInto(bmp, x + 8, y + 0, block.Tiles[1].FlipHoriztonal, block.Tiles[1].FlipVertical, block.Tiles[1].Palette * 16);

        if (block.Tiles[2].Priority == priorityTiles && block.Tiles[2].Index < _layer12Tiles.Count)
            _layer12Tiles[block.Tiles[2].Index].DrawInto(bmp, x + 0, y + 8, block.Tiles[2].FlipHoriztonal, block.Tiles[2].FlipVertical, block.Tiles[2].Palette * 16);

        if (block.Tiles[3].Priority == priorityTiles && block.Tiles[3].Index < _layer12Tiles.Count)
            _layer12Tiles[block.Tiles[3].Index].DrawInto(bmp, x + 8, y + 8, block.Tiles[3].FlipHoriztonal, block.Tiles[3].FlipVertical, block.Tiles[3].Palette * 16);
    }

    public void Dispose()
    {
        foreach (var sprite in _renderedSprites.Values)
            sprite.Dispose();

        _renderedSprites.Clear();
    }

    private record SpriteKey(bool Layer1, bool Layer2, bool Layer3);

    public record class TileInfo
    {
        public int Index { get; }
        public int Palette { get; }
        public bool FlipHoriztonal { get; }
        public bool FlipVertical { get; }
        public bool Priority { get; }

        public TileInfo(ushort value)
        {
            Index = value & 0x3ff;
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