using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.Sprites
{
    public enum Pose
    {
        Stand,
        Walk,
        Queued,
        Crouch,
        Damaged,
        Casting,
        Special,
        Celebrate,
        Dead
    }

    public class Characters : IDisposable
    {
        private readonly List<byte[,]> _tiles;
        private readonly List<byte[,]> _stickers;

        private readonly List<List<Color>> _colorPalettes;
        private readonly List<Color> _stickerPalette;

        private Dictionary<Pose, List<Frame>> PoseFrames = new Dictionary<Pose, List<Frame>>();
        private Dictionary<BitmapKey, Bitmap> _frameCache = new Dictionary<BitmapKey, Bitmap>();

        public Characters(MemorySpace rom)
        {
            PoseFrames[Pose.Stand]     = new List<Frame>(new[] { new Frame( 0, new byte[3, 3] { {   0,   1, 255 }, {  2,  3, 255 }, {   4,  5, 255 } }) });
            PoseFrames[Pose.Walk]      = new List<Frame>(new[] { new Frame( 0, new byte[3, 3] { {   0,   1, 255 }, {  2,  3, 255 }, {  12, 13, 255 } }) });
            PoseFrames[Pose.Queued]    = new List<Frame>(new[] { new Frame( 0, new byte[3, 3] { {   6,   7, 255 }, {  8,  9, 255 }, {  10, 11, 255 } }) });
            PoseFrames[Pose.Crouch]    = new List<Frame>(new[] { new Frame( 8, new byte[3, 3] { {  14,  15, 255 }, { 16, 17, 255 }, {  18, 19, 255 } }, new sbyte?[3, 3] { { 44, -44, null }, { null, null, null }, { null, null, null } }), new Frame(8, new byte[3, 3] { { 14, 15, 255 }, { 16, 17, 255 }, { 18, 19, 255 } }, new sbyte?[3, 3] { { 45, -45, null }, { null, null, null }, { null, null, null } }) });
            PoseFrames[Pose.Damaged]   = new List<Frame>(new[] { new Frame( 0, new byte[3, 3] { {  30,  31, 255 }, { 32, 33, 255 }, {  34, 35, 255 } }) });
            PoseFrames[Pose.Celebrate] = new List<Frame>(new[] { new Frame(16, new byte[3, 3] { {  36,  37, 255 }, { 38, 39, 255 }, {  40, 41, 255 } }), new Frame(16, new byte[3, 3] { { 0, 1, 255 }, { 2, 3, 255 }, { 4, 5, 255 } }) });
            PoseFrames[Pose.Dead]      = new List<Frame>(new[] { new Frame( 0, new byte[3, 3] { { 255, 255, 255 }, { 42, 43,  44 }, {  45, 46,  47 } }) });
            PoseFrames[Pose.Special]   = new List<Frame>(new[] { new Frame( 0, new byte[3, 3] { {  48,  49,  50 }, { 51, 52,  53 }, { 255, 55,  56 } }) });
            PoseFrames[Pose.Casting]   = new List<Frame>(new[] { new Frame( 8, new byte[3, 3] { {  57,  58, 255 }, { 59, 60, 255 }, {  61, 62, 255 } }), new Frame( 8, new byte[3, 3] { { 57, 58, 255 }, { 63, 60, 255 }, { 61, 62, 255 } }) });

            var processor = new TileProcessor();

            _tiles = rom
                .ReadBytes(CARTROMAddresses.CharacterSpritesAddress, CARTROMAddresses.CharacterSpritesBytes)
                .ReadMany<byte[]>(0, 0x20 * 8, 896)
                .Select(data => processor.GetTile(data, 4))
                .ToList();

            _stickers = rom
                .ReadBytes(CARTROMAddresses.BattleStatusStickers, CARTROMAddresses.BattleStatusStickersBytes)
                .ReadMany<byte[]>(0, 0x18 * 8, 64)
                .Select(data => processor.GetTile(data, 3))
                .ToList();

            _colorPalettes = rom
                .ReadBytes(CARTROMAddresses.CharacterPalettesAddress, CARTROMAddresses.CharacterPalettesBytes)
                .ReadMany<byte[]>(0, 32*8, 16) //split into palettes
                .Select(paletteData => paletteData.ReadMany<uint>(0, 16, 16).Select(c => ColorProcessor.GetColor(c)).ToList())
                .ToList();

            _stickerPalette = rom
                .ReadBytes(CARTROMAddresses.BattleStatusStickersPalette, 16)
                .ReadMany<uint>(0, 16, 8).Select(c => ColorProcessor.GetColor(c))
                .ToList();

            _colorPalettes.ForEach(p => p[0] = Color.Transparent);
            _stickerPalette[0] = Color.Transparent;
        }

        public void Dispose()
        {
            foreach (var b in _frameCache.Values)
                b.Dispose();
        }

        private const int SpritesPerCharacter = 64;

        private byte[,] GetCharacterTile(CharacterType character, int spriteIndex)
                => _tiles[(int)character * SpritesPerCharacter + spriteIndex];

        public int GetFrameIndex(Pose pose, int frame)
        {
            if (PoseFrames[pose].Count == 1)
                return 0;

            frame %= Math.Max(PoseFrames[pose].Sum(f => f.FrameDelay), 1);
            var frames = 0;
            for (int frameNum = 0; frameNum < PoseFrames[pose].Count; frameNum++)
            {
                if (frames + PoseFrames[pose][frameNum].FrameDelay > frame)
                    return frameNum;

                frames += PoseFrames[pose][frameNum].FrameDelay;
            }

            return 0;
        }


        public Bitmap GetCharacterBitmap(byte id, CharacterType type, Pose pose, int frame)
        {            
            frame = GetFrameIndex(pose, frame);
            var key = new BitmapKey(id, type, 0, pose, frame);
            if (_frameCache.TryGetValue(key, out var bitmap))
                return bitmap;

            bitmap = new Bitmap(24, 24);

            if (key.ID != 0)
                PoseFrames[pose][frame].Render(
                    bitmap, 
                    tileIndex => GetCharacterTile(type, tileIndex), 
                    _colorPalettes[(int)type], 
                    stickerIndex => _stickers[stickerIndex], 
                    _stickerPalette);            

            _frameCache[key] = bitmap;
            return bitmap;
        }

        private class BitmapKey
        {
            public BitmapKey(byte iD, CharacterType characterType, byte paletteIndex, Pose pose, int frame)
            {
                ID = iD;
                CharacterType = characterType;
                PaletteIndex = paletteIndex;
                Pose = pose;
                Frame = frame;
            }

            public byte ID { get; }
            public CharacterType CharacterType { get; }
            public byte PaletteIndex { get; }
            public Pose Pose { get; }
            public int Frame { get; }

            public override int GetHashCode()
            {
                var hash = 17;
                hash = hash * 23 + ID.GetHashCode();
                hash = hash * 23 + CharacterType.GetHashCode();
                hash = hash * 23 + PaletteIndex.GetHashCode();
                hash = hash * 23 + Pose.GetHashCode();
                hash = hash * 23 + Frame.GetHashCode();
                return hash;
            }
        }
    }
}
