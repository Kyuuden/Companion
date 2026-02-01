using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.FreeEnterprise.RomData;

public class Sprites : IDisposable
{
    private readonly List<List<byte[,]>> _combatTiles;
    private readonly List<byte[,]> _stickers;
    private readonly List<byte[,]> _portraitTiles;
    private readonly List<byte[,]> _chestTiles;
    private readonly List<byte[,]> _npcTiles;
    private readonly List<byte[,]> _eggTiles;

    private readonly List<List<Palette>> _combatPalettes;
    private readonly Palette _stickerPalette;
    private readonly Palette _greyScaleStickerPalette;
    private readonly List<Palette> _portraitPalettes;
    private readonly Palette _chestPalette;
    private readonly List<Palette> _npcPalettes;
    private readonly Palette _eggPalette;

    private readonly Dictionary<Pose, List<Frame>> PoseFrames = [];
    private readonly Dictionary<BitmapKey, Bitmap> _frameCache = [];
    private readonly Dictionary<Arrow, IReadableBitmapData> arrowCache = [];
    private Bitmap? _blank;
    private bool disposedValue;

    // private Dictionary<AnimationKey, ObjectAnimationUsingKeyFrames> _animationCache = [];
    //private ObjectAnimationUsingKeyFrames? _blank;

    public Sprites(IMemorySpace memorySpace)
    {
        PoseFrames[Pose.Stand] = [new Frame(0, new byte?[3, 2] { { 0, 1 }, { 2, 3 }, { 4, 5 } })];
        PoseFrames[Pose.Walk] = [new Frame(0, new byte?[3, 2] { { 0, 1 }, { 2, 3 }, { 12, 13 } })];
        PoseFrames[Pose.Queued] = [new Frame(0, new byte?[3, 2] { { 6, 7 }, { 8, 9 }, { 10, 11 } })];
        PoseFrames[Pose.Crouch] =
            [
            new Frame(8, new byte?[3, 2] { { 14, 15 }, { 16, 17 }, { 18, 19 } }, new sbyte?[3, 2] { { 44, -44 }, { default, default }, { default, default } }),
            new Frame(8, new byte?[3, 2] { { 14, 15 }, { 16, 17 }, { 18, 19 } }, new sbyte?[3, 2] { { 45, -45 }, { default, default }, { default, default } })
            ];

        PoseFrames[Pose.Damaged] = [new Frame(0, new byte?[3, 2] { { 30, 31 }, { 32, 33 }, { 34, 35 } })];
        PoseFrames[Pose.Celebrate] =
            [
            new Frame(16, new byte?[3, 2] { { 36, 37 }, { 38, 39 }, { 40, 41 } }),
            new Frame(16, new byte?[3, 2] { { 0, 1 }, { 2, 3 }, { 4, 5 } })
            ];

        PoseFrames[Pose.Dead] = [new Frame(0, new byte?[3, 3] { { default, default, default }, { 42, 43, 44 }, { 45, 46, 47 } })];
        PoseFrames[Pose.Special] = [new Frame(0, new byte?[3, 3] { { 48, 49, 50 }, { 51, 52, 53 }, { default, 55, 56 } })];
        PoseFrames[Pose.Casting] =
            [
            new Frame(8, new byte?[3, 2] { { 57, 58 }, { 59, 60 }, { 61, 62 } }),
            new Frame(8, new byte?[3, 2] { { 57, 58 }, { 63, 60 }, { 61, 62 } })
            ];

        PoseFrames[Pose.Portrait] = [new Frame(0, new byte?[4, 4] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 }, { 12, 13, 14, 15 } })];

        _combatTiles = [];
        foreach (var range in Addresses.ROM.CharacterSprites)
            _combatTiles.Add(
                memorySpace.ReadBytes(range)
                .ReadMany<byte[]>(0, 0x20 * 8, 896)
                .Select(data => data.DecodeTile(4))
                .ToList());

        _combatPalettes = [];
        foreach (var range in Addresses.ROM.CharacterPalettes)
            _combatPalettes.Add(
                memorySpace.ReadBytes(range)
                .ReadMany<byte[]>(0, 32 * 8, 16) //split into palettes
                .Select(paletteData => new Palette(paletteData.ReadMany<uint>(0, 16, 16).Select((c, i) => i == 0 ? new Color32() : c.ToColor()))).ToList());

        _stickers = memorySpace.ReadBytes(Addresses.ROM.BattleStatusStickers)
            .ReadMany<byte[]>(0, 0x18 * 8, 64)
            .Select(data => data.DecodeTile(3))
            .ToList();

        _stickerPalette = new Palette(memorySpace.ReadBytes(Addresses.ROM.BattleStatusStickersPalette).ReadMany<uint>(0, 16, 8).Select((c, i) => i == 0 ? new Color32() : c.ToColor()));
        _greyScaleStickerPalette = new Palette(memorySpace.ReadBytes(Addresses.ROM.BattleStatusStickersGreyScalePalette).ReadMany<uint>(0, 16, 8).Select((c, i) => i == 0 ? new Color32() : c.ToColor()));

        _portraitTiles = memorySpace.ReadBytes(Addresses.ROM.CharacterPortraits)
            .ReadMany<byte[]>(0, 0x18 * 8, 224)
            .Select(data => data.DecodeTile(3))
            .ToList();

        _portraitPalettes = memorySpace.ReadBytes(Addresses.ROM.CharacterPortraitsPalettes)
            .ReadMany<byte[]>(0, 16 * 8, 14) //split into palettes
            .Select(paletteData => new Palette(paletteData.ReadMany<uint>(0, 16, 8).Select((c, i) => i == 0 ? new Color32() : c.ToColor()))).ToList();

        _chestTiles = memorySpace.ReadBytes(Addresses.ROM.ChestSprites)
            .ReadMany<byte[]>(0, 0x18 * 8, 6)
            .Select(d => d.DecodeTile(3))
            .ToList();

        _chestPalette = new Palette(memorySpace.ReadBytes(Addresses.ROM.ChestPalette).ReadMany<uint>(0, 16, 8).Select((c, i) => i == 0 ? new Color32() : c.ToColor()));

        _npcTiles = memorySpace.ReadBytes(Addresses.ROM.NpcSprites)
            .ReadMany<byte[]>(0, 0x18 * 8, 820)
            .Select(data => data.DecodeTile(3))
            .ToList();

        _npcPalettes = memorySpace.ReadBytes(Addresses.ROM.NpcPalettes)
            .ReadMany<byte[]>(0, 16 * 8, 18) //split into palettes
            .Select(paletteData => new Palette(paletteData.ReadMany<uint>(0, 16, 8).Select((c, i) => i == 0 ? new Color32() : c.ToColor()))).ToList();

        _eggTiles = memorySpace.ReadBytes(Addresses.ROM.EggSprites)
            .ReadMany<byte[]>(0, 0x18 * 8, 16)
            .Select(data => data.DecodeTile(3))
            .ToList();

        _eggPalette = new Palette(memorySpace.ReadBytes(Addresses.ROM.EggPalette)
            .ReadMany<uint>(0, 16, 8).Select((c, i) => i == 0 ? new Color32() : c.ToColor()));
    }

    public int GetFrameCount(Pose pose)
    {
        return PoseFrames[pose].Count;
    }

    public IReadableBitmapData GetNpcImage(int[,] tileIndexes, int paletteIndex)
    {
        int width = tileIndexes.GetLength(1) * 8;
        int height = tileIndexes.GetLength(0) * 8;
        var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, _npcPalettes[paletteIndex]);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var tileIndex = tileIndexes[y / 8, x / 8];
                data.SetColorIndex(x, y, _npcTiles[tileIndex][x % 8, y % 8]);
            }
        }

        return data;
    }

    public IReadableBitmapData GetChestImage(Chest chest)
    {
        byte[,] tileIndexes = chest == Chest.Closed
            ? new byte[2, 2] { { 0, 1 }, { 4, 5 } }
            : new byte[2, 2] { { 2, 3 }, { 4, 5 } };

        int width = 16;
        int height = 16;
        var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, _chestPalette);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var tileIndex = tileIndexes[y / 8, x / 8];
                data.SetColorIndex(x, y, _chestTiles[tileIndex][x % 8, y % 8]);
            }
        }

        return data;
    }

    public IReadableBitmapData GetEgg()
    {
        int width = 32;
        int height = 32;
        var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, _eggPalette);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                data.SetColorIndex(x, y, _eggTiles[4 * (y / 8) + x / 8][x % 8, y % 8]);
            }
        }

        return data;
    }

    public IReadableBitmapData GetArrow(Arrow arrow)
    {
        if (arrowCache.TryGetValue(arrow, out var cached))
            return cached;


        if (arrow == Arrow.Up || arrow == Arrow.Down)
        {
            int width = 8;
            int height = 16;
            var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, GreyScaleStickerPalette);
            var tail = _stickers[14];
            var head = _stickers[15];

            for (int x = 0; x < 8; x++)
                for (var y = 0; y < 8; y++)
                    data.SetColorIndex(x, arrow == Arrow.Up ? 15 - y : y, tail[x, y]);
            for (int x = 0; x < 8; x++)
                for (var y = 0; y < 8; y++)
                    data.SetColorIndex(x, arrow == Arrow.Up ? 7 - y : y + 8, head[x, y]);

            arrowCache.Add(arrow, data);
            return data;
        }
        else
        {
            int width = 16;
            int height = 8;
            var data = BitmapDataFactory.CreateBitmapData(new Size(width, height), KnownPixelFormat.Format8bppIndexed, GreyScaleStickerPalette);

            var tail = _stickers[12];
            var head = _stickers[13];

            for (int x = 0; x < 8; x++)
                for (var y = 0; y < 8; y++)
                    data.SetColorIndex(arrow == Arrow.Left ? 15 - x : x, y, tail[x, y]);
            for (int x = 0; x < 8; x++)
                for (var y = 0; y < 8; y++)
                    data.SetColorIndex(arrow == Arrow.Left ? 7 - x : x + 8, y, head[x, y]);

            arrowCache.Add(arrow, data);
            return data;
        }
    }

    public Bitmap GetBlankCharacter()
    {
        if (_blank != null)
            return _blank;

        _blank = BitmapDataFactory.CreateBitmapData(new Size(24, 24), KnownPixelFormat.Format8bppIndexed, new Palette((IEnumerable<Color>)[Color.Transparent])).ToBitmap();
        return _blank;
    }

    public Bitmap GetCharacterImage(CharacterType type, int fashionIndex, Pose pose)
    {
        if (fashionIndex >= _combatTiles.Count)
            fashionIndex = 0;

        var key = new BitmapKey(type, fashionIndex, pose);

        if (_frameCache.TryGetValue(key, out var existing))
            return existing;

        var (frame, _) = RenderFrame(type, fashionIndex, pose, 0);

        _frameCache.Add(key, frame.ToBitmap());

        return _frameCache[key];


        /*for (int i = 0; i < PoseFrames[pose].Count; i++)
        {
            var (frame, duration) = RenderFrame(type, fashionIndex, pose, i);
            keyFrames.Add(new DiscreteObjectKeyFrame(frame, totalDuration));

            totalDuration += duration;
        }

        _animationCache.Add(key, new ObjectAnimationUsingKeyFrames
        {
            KeyFrames = keyFrames,
            Duration = totalDuration,
            RepeatBehavior = RepeatBehavior.Forever,
            SpeedRatio = 1.0
        });

        return _animationCache[key];*/
    }

    public (IReadableBitmapData, TimeSpan) RenderFrame(CharacterType type, int fashion, Pose pose, int frameNum)
        => (
                PoseFrames[pose][frameNum].Render(
                    tileIndex => GetTile(type, fashion, pose, tileIndex),
                    GetPalette(type, fashion, pose),
                    stickerIndex => _stickers[stickerIndex],
                    _stickerPalette),
                TimeSpan.FromSeconds(PoseFrames[pose][frameNum].FrameDelay / 60.0)
        );

    private const int BattleSpritesPerCharacter = 64;
    private const int PortraitSpritesPerCharacter = 16;

    public Palette GreyScaleStickerPalette => _greyScaleStickerPalette;

    private byte[,] GetTile(CharacterType character, int version, Pose pose, int spriteIndex)
        => pose switch
        {
            Pose.Portrait => _portraitTiles[(int)character * PortraitSpritesPerCharacter + spriteIndex],
            _ => _combatTiles[version][(int)character * BattleSpritesPerCharacter + spriteIndex]
        };

    private Palette GetPalette(CharacterType character, int version, Pose pose)
        => pose switch
        {
            Pose.Portrait => _portraitPalettes[(int)character],
            _ => _combatPalettes[version][(int)character]
        };

    private record BitmapKey(CharacterType CharacterType, int FashionIndex, Pose Pose);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var bmp in _frameCache.Values)
                    bmp.Dispose();

                _blank?.Dispose();
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
