using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.RomData;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Rendering;

public partial class CharacterSprites : IDisposable
{
    private readonly byte[] _tileData;
    private readonly List<Palette> _palettes;
    private readonly Dictionary<int, Tile> _tileCache = [];
    private bool disposedValue;
    private readonly Dictionary<CharacterEx, Dictionary<Pose, CharacterPose>> _actorPoses = [];
    private readonly Dictionary<CharacterPose, CharacterSprite> _spriteCache = [];

    public CharacterSprites(byte[] tileData, byte[] paletteData)
    {
        _tileData = tileData;
        _palettes = paletteData.ReadMany<byte[]>(0, 8 * 0x20, 0x20).Select(p => p.DecodePalette(new Color32())).ToList();

        foreach (CharacterEx actor in Enum.GetValues(typeof(CharacterEx)))
        {
            foreach (var actorPose in GetPoseDataFor(actor))
            {
                if (actorPose.SpriteSize.Width * actorPose.SpriteSize.Height != actorPose.TileIndicies.Count)
                    throw new InvalidOperationException($"{actorPose} is invalid;");
                
                if (!_actorPoses.TryGetValue(actor, out var poseDict))
                    _actorPoses[actor] = new Dictionary<Pose, CharacterPose>();

                _actorPoses[actor][actorPose.Pose] = actorPose;
            }
        }
    }

    public IEnumerable<Pose> GetPosesFor(CharacterEx actor)
    {
        return _actorPoses.TryGetValue(actor, out var poses) ? poses.Keys : ([]);
    }

    public ISprite? Get(CharacterEx actor, Pose pose)
    {
        if (!_actorPoses.TryGetValue(actor, out var poses) || !poses.TryGetValue(pose, out var actorPose))
            return null;

        if (_spriteCache.TryGetValue(actorPose, out var sprite))
            return sprite;

        var paletteIndex = PaletteIndexFor(actor);
        var palette = _palettes[paletteIndex];

        var bmp = BitmapDataFactory.CreateBitmapData(
            new Size(actorPose.SpriteSize.Width * 8, actorPose.SpriteSize.Height * 8),
            KnownPixelFormat.Format8bppIndexed,
            palette);

        for (int i = 0; i < actorPose.TileIndicies.Count; i++)
        {
            GetTile(actorPose.TileIndicies[i], paletteIndex).DrawInto(bmp, new Point((i % actorPose.SpriteSize.Width) * 8, (i / actorPose.SpriteSize.Width) * 8));
        }

        _spriteCache[actorPose] = sprite = new CharacterSprite(bmp);
        return sprite;
    }

    private IReadableBitmapData GetTile(int index, int palette)
    {
        if (_tileCache.TryGetValue(index, out var tile))
        {
            return tile.GetForPalette(palette);
        }

        if (_tileCache.TryGetValue(-1 * index, out tile))
        {
            tile = tile.GetFlipped();
            _tileCache[index] = tile;
            return tile.GetForPalette(palette);
        }

        var bitmapData = BitmapDataFactory.CreateBitmapData(8, 8, KnownPixelFormat.Format8bppIndexed, _palettes[palette]);
        _tileData.AsSpan(index * 0x20, 0x20).DecodeTile(4).DrawInto(bitmapData, flipHorizontal: index < 0);
        tile = new Tile(bitmapData, palette, i => _palettes[i]);
        _tileCache[index] = tile;
        return bitmapData;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var value in _tileCache.Values) value.Dispose();
                _tileCache.Clear();
                foreach (var sprite in _spriteCache.Values) sprite.Dispose();
                _spriteCache.Clear();
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

    private class Tile : IDisposable
    {
        private readonly Func<int, Palette> _paletteGetter;
        private readonly Dictionary<int, IReadableBitmapData> _bmpByPalette = [];

        public Tile(IReadableBitmapData bmp, int paletteIndex, Func<int, Palette> paletteGetter)
            : this(paletteGetter)
        {
            _bmpByPalette.Add(paletteIndex, bmp);
        }

        private Tile(Func<int, Palette> paletteGetter)
        {
            _paletteGetter = paletteGetter;
        }

        public IReadableBitmapData GetForPalette(int paletteIndex)
        {
            if (_bmpByPalette.TryGetValue(paletteIndex, out IReadableBitmapData bmp)) return bmp;

            var clone = _bmpByPalette.Values.First().Clone();
            clone.TrySetPalette(_paletteGetter(paletteIndex));
            _bmpByPalette[paletteIndex] = clone;
            return clone;
        }

        public Tile GetFlipped()
        {
            var flipped = new Tile(_paletteGetter);
            foreach (var item in _bmpByPalette)
            {
                flipped._bmpByPalette[item.Key] = item.Value.CopyRotateFlip(RotateFlipType.RotateNoneFlipX);
            }

            return flipped;
        }

        public void Dispose()
        {
            foreach (var bmp in _bmpByPalette.Values) bmp.Dispose();
            _bmpByPalette.Clear();
        }
    }

    private record CharacterPose
    {
        public CharacterEx Character { get; init; }
        public required Pose Pose { get; init; }
        public required IReadOnlyList<int> TileIndicies { get; init; }
        public required Size SpriteSize { get; init; }

        public CharacterPose OffsetFor(CharacterEx character, int tileOffset)
            => this with { Character = character, TileIndicies = this.TileIndicies.Select(i => i + tileOffset).ToList() };
    }
}
