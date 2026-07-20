using BizHawk.Common;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.JetsOfTime.Rendering;

internal class SpriteCollection : IDisposable
{
    private readonly Container _container;

    private readonly Dictionary<int, BasicSprite> _sprites = [];
    private readonly Dictionary<int, FrameInfo> _frameInfo = [];

    //private IReadOnlyList<FrameInfo>? _allFrameInfo;
    private IReadOnlyList<byte[,]>? _tiles;
    private readonly HashSet<int> _emptyTiles = [];
    private byte[]? _assemblyData;
    private Palette? _palette;
    private bool disposedValue;
    private readonly Range<long> _tilesLocation;
    private readonly Range<long> _assemblyLocation;
    private readonly Range<long> _paletteLocation;
    private readonly Range<long> _animationLocation;
    private readonly byte _assemblyFlags;
    private readonly bool _compressed;

    private readonly int _groupsPerFrame;
    private readonly int _blocksPerGroup;

    private readonly byte[] _headerData;

    public SpriteCollection(Container container, Span<byte> data)
    {
        _container = container;
        if (data == null) throw new ArgumentNullException(nameof(data));
        if (data.Length is not 5 and not 10) throw new ArgumentException("Sprite Headers must be 5 or 10 bytes long");

        _tilesLocation = Data.Addresses.ROM.Sprites.SpriteTileData[data[0]];
        _compressed = data[0] > 6;

        _assemblyLocation = Data.Addresses.ROM.Sprites.SpriteAssemblyData[data[1]];
        _paletteLocation = Data.Addresses.ROM.Sprites.PaletteData[data[2]];
        _animationLocation = Data.Addresses.ROM.Sprites.SpriteAnimationData[data[3]];
        _assemblyFlags = data[4];

        _headerData = data.ToArray();

        (_groupsPerFrame, _blocksPerGroup) = (_assemblyFlags & 0x03) switch
        {
            0 => (1, 4),
            1 => (1, 8),
            2 => (3, 4),
            3 => (3, 8),
            _ => (1, 4),
        };
    }

    private IReadOnlyList<byte[,]> Tiles
    {
        get
        {
            ParseTiles();
            return _tiles ?? [];
        }
    }

    private byte[] AssemblyData => _assemblyData ??= _container.Rom.ReadBytes(_assemblyLocation);

    public Palette Palette => _palette ??= _container.Rom.ReadBytes(_paletteLocation).DecodePalette(new Color32());

    //private IReadOnlyList<FrameInfo> Frames => _allFrameInfo ??= ParseAssemblyData();

    public int Count => (int)(_assemblyLocation.Length() / (_groupsPerFrame * _blocksPerGroup * 10));

    public ISprite? Get(int frameIndex)
    {
        ParseTiles();

        if (frameIndex < 0 || frameIndex >= Count)
            return null;

        if (_sprites.TryGetValue(frameIndex, out var sprite))
            return sprite;

        if (!_frameInfo.TryGetValue(frameIndex, out var frameInfo))
        {
            frameInfo = ParseFrame(frameIndex);
            if (frameInfo == null)
                return null;

            _frameInfo[frameIndex] = frameInfo;
        }

        //var frameInfo = Frames[frameIndex];
        var bitmap = BitmapDataFactory.CreateBitmapData(frameInfo.Size, KnownPixelFormat.Format8bppIndexed, Palette);

        foreach (var block in frameInfo.Groups.SelectMany(g => g.Blocks))
        {
            Tiles[block.Tiles[0].Index].DrawInto(bitmap, block.X, block.Y, block.Tiles[0].FlipHoriztonal, block.Tiles[0].FlipVertical);
            Tiles[block.Tiles[1].Index].DrawInto(bitmap, block.X + 8, block.Y, block.Tiles[1].FlipHoriztonal, block.Tiles[1].FlipVertical);
            Tiles[block.Tiles[2].Index].DrawInto(bitmap, block.X, block.Y + 8, block.Tiles[2].FlipHoriztonal, block.Tiles[2].FlipVertical);
            Tiles[block.Tiles[3].Index].DrawInto(bitmap, block.X + 8, block.Y + 8, block.Tiles[3].FlipHoriztonal, block.Tiles[3].FlipVertical);
        }

        sprite = new BasicSprite(bitmap);
        _sprites[frameIndex] = sprite;
        return sprite;
    }

    private void ParseTiles()
    {
        if (_tiles == null)
        {
            var data = _container.Rom.ReadBytes(_tilesLocation);

            if (_compressed)
            {
                data = Utils.DecompressData(data);
            }

            _tiles = data.ReadMany<byte[]>(0x20 * 8).Select(b => b.DecodeTile(4)).ToList();

            for (var i = 0; i < _tiles.Count; i++)
            {
                if (_tiles[i].IsBackground())
                    _emptyTiles.Add(i);
            }
        }
    }

    private FrameInfo? ParseFrame(int frameIndex)
    {
        var frameLength = _groupsPerFrame * _blocksPerGroup * 10;
        var data = AssemblyData.AsSpan().Slice(frameLength * frameIndex, frameLength);

        var frame = new FrameInfo();

        for (var groupNum = 0; groupNum < _groupsPerFrame; groupNum++)
        {
            var group = new GroupInfo();

            for (var blockNum = 0; blockNum < _blocksPerGroup; blockNum++)
            {
                var block = new BlockInfo();

                var tile = BinaryPrimitives.ReadUInt16LittleEndian(data);
                var tile2 = BinaryPrimitives.ReadUInt16LittleEndian(data[2..]);
                data = data[4..];

                block.Tiles.Add(new TileInfo(tile & 0x3FF, (tile & 0x4000) != 0, (tile & 0x8000) != 0));
                block.Tiles.Add(new TileInfo(tile2 & 0x3FF, (tile2 & 0x4000) != 0, (tile2 & 0x8000) != 0));
                group.Blocks.Add(block);
            }

            for (var blockNum = 0; blockNum < _blocksPerGroup; blockNum++)
            {
                var tile = BinaryPrimitives.ReadUInt16LittleEndian(data);
                var tile2 = BinaryPrimitives.ReadUInt16LittleEndian(data[2..]);
                data = data[4..];

                group.Blocks[blockNum].Tiles.Add(new TileInfo(tile & 0x3FF, (tile & 0x4000) != 0, (tile & 0x8000) != 0));
                group.Blocks[blockNum].Tiles.Add(new TileInfo(tile2 & 0x3FF, (tile2 & 0x4000) != 0, (tile2 & 0x8000) != 0));
            }

            frame.Groups.Add(group);
        }

        for (var groupNum = 0; groupNum < _groupsPerFrame; groupNum++)
        {
            var group = frame.Groups[groupNum];

            for (var blockNum = 0; blockNum < _blocksPerGroup; blockNum++)
            {
                var block = group.Blocks[blockNum];

                var x = (sbyte)data[0];
                var y = (sbyte)data[1];

                if (x > 0 && x % 4 != 0)
                {
                    Debug.WriteLine("");
                }

                block.X = x;
                block.Y = y;

                data = data[2..];
            }
        }

        return frame.Normalize(_emptyTiles) ? frame : null;
    }

    //private List<FrameInfo> ParseAssemblyData()
    //{
    //    ParseTiles();
    //    var frames = new List<FrameInfo>();

    //    var (groupsPerFrame, blocksPerGroup) = (_assemblyFlags & 0x03) switch
    //    {
    //        0 => (1, 4),
    //        1 => (1, 8),
    //        2 => (3, 4),
    //        3 => (3, 8),
    //        _ => (1, 4),
    //    };

    //    var data = AssemblyData.AsSpan();

    //    var blocksPerFrame = groupsPerFrame * blocksPerGroup;
    //    var frameCount = data.Length / (blocksPerFrame * 10);

    //    for (var frameNum = 0; frameNum < frameCount; frameNum++)
    //    {
    //        var frame = new FrameInfo();

    //        for (var groupNum = 0; groupNum < groupsPerFrame; groupNum++)
    //        {
    //            var group = new GroupInfo();

    //            for (var blockNum = 0; blockNum < blocksPerGroup; blockNum++)
    //            {
    //                var block = new BlockInfo();

    //                var tile = BinaryPrimitives.ReadUInt16LittleEndian(data);
    //                var tile2 = BinaryPrimitives.ReadUInt16LittleEndian(data[2..]);
    //                data = data[4..];

    //                block.Tiles.Add(new TileInfo(tile & 0x3FF, (tile & 0x4000) != 0, (tile & 0x8000) != 0));
    //                block.Tiles.Add(new TileInfo(tile2 & 0x3FF, (tile2 & 0x4000) != 0, (tile2 & 0x8000) != 0));
    //                group.Blocks.Add(block);
    //            }

    //            for (var blockNum = 0; blockNum < blocksPerGroup; blockNum++)
    //            {
    //                var tile = BinaryPrimitives.ReadUInt16LittleEndian(data);
    //                var tile2 = BinaryPrimitives.ReadUInt16LittleEndian(data[2..]);
    //                data = data[4..];

    //                group.Blocks[blockNum].Tiles.Add(new TileInfo(tile & 0x3FF, (tile & 0x4000) != 0, (tile & 0x8000) != 0));
    //                group.Blocks[blockNum].Tiles.Add(new TileInfo(tile2 & 0x3FF, (tile2 & 0x4000) != 0, (tile2 & 0x8000) != 0));
    //            }

    //            frame.Groups.Add(group);
    //        }

    //        for (var groupNum = 0; groupNum < groupsPerFrame; groupNum++)
    //        {
    //            var group = frame.Groups[groupNum];

    //            for (var blockNum = 0; blockNum < blocksPerGroup; blockNum++)
    //            {
    //                var block = group.Blocks[blockNum];

    //                var x = (sbyte)data[0];
    //                var y = (sbyte)data[1];

    //                if (x > 0 && x % 4 != 0)
    //                {
    //                    Debug.WriteLine("");
    //                }

    //                block.X = x;
    //                block.Y = y;

    //                data = data[2..];
    //            }
    //        }

    //        if (frame.Normalize(_emptyTiles))
    //            frames.Add(frame);
    //        else
    //            Debug.WriteLine($"Skipping frame# {frameNum}");
    //    }

    //    return frames;
    //}

    private record TileInfo(int Index, bool FlipHoriztonal, bool FlipVertical);

    private class BlockInfo
    {
        public override string ToString() => $"X={X}, Y={Y}, Height=16, Width=16";
        public List<TileInfo> Tiles { get; } = [];
        public int X { get; set; }
        public int Y { get; set; }
    }

    private class GroupInfo
    {
        public override string ToString() => $"{Blocks.Count} blocks";
        public List<BlockInfo> Blocks { get; } = [];
    }

    private class FrameInfo
    {
        public override string ToString() => $"Size={Size}, {Groups.Count} block groups";
        public List<GroupInfo> Groups { get; } = [];
        public Size Size { get; private set; }

        public bool Normalize(HashSet<int> blankTiles)
        {
            for(var groupNum = Groups.Count -1; groupNum >= 0; groupNum--)
            {
                var group = Groups[groupNum];
                for (var blockNum = group.Blocks.Count - 1; blockNum >= 0; blockNum--)
                {
                    var block = group.Blocks [blockNum];
                    if (block.Tiles.All(t=> blankTiles.Contains(t.Index)))
                        group.Blocks.RemoveAt(blockNum);
                }

                if (group.Blocks.Count == 0)
                    Groups.RemoveAt(groupNum);
            }

            if (Groups.Count == 0)
                return false;

            var minX = Groups.SelectMany(g => g.Blocks.Select(b => b.X)).Min();
            var maxX = Groups.SelectMany(g => g.Blocks.Select(b => b.X)).Max();

            var minY = Groups.SelectMany(g => g.Blocks.Select(b => b.Y)).Min();
            var maxY = Groups.SelectMany(g => g.Blocks.Select(b => b.Y)).Max();

            var width = maxX + 16 - minX;
            var height = maxY + 16 - minY;

            var XOffset = minX < 0 ? -minX : 0;
            var YOffset = minY < 0 ? -minY : 0;

            foreach (var group in Groups)
            {
                foreach (var block in group.Blocks)
                {
                    block.X = block.X + XOffset;
                    block.Y = block.Y + YOffset;
                }
            }

            Size = new Size(width, height);
            return true;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var sprite in _sprites.Values)
                    sprite.Dispose();
                _sprites.Clear();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
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

