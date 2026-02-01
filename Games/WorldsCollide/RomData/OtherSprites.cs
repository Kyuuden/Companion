using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public class OtherSprites(byte[] spriteTileData, byte[] spritePaletteData, byte[] backgroundTileData, byte[] backgroundPaletteData) : IDisposable
{
    private readonly List<Palette> _spritePalettes = spritePaletteData.ReadMany<byte[]>(8 * 0x20).Select(p => p.DecodePalette(new Color32())).ToList();
    private readonly List<Palette> _backgroundPalettes = backgroundPaletteData.ReadMany<byte[]>(8 * 0x20).Select(p => p.DecodePalette(new Color32())).ToList();
    private readonly Dictionary<Item, OtherSprite> _spriteCache = [];

    public ISprite Get(Item item)
    {
        if (_spriteCache.TryGetValue(item, out var sprite))
            return sprite;

        var tileData = IsBackground(item) ? backgroundTileData : spriteTileData;
        var palettes = IsBackground(item) ? _backgroundPalettes : _spritePalettes;

        _spriteCache[item] = sprite = GenerateSprite(item, tileData, palettes);
        return sprite;
    }
    private OtherSprite GenerateSprite(Item item, ReadOnlySpan<byte> tileData, IList<Palette> palettes)
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

    private SpriteInfo GetInfo(Item item)
        => item switch
        {
            Item.UmaroSkull => new SpriteInfo([5847, 5848, 5849, 5850, 5851, 5852], 4, 2, 3),
            Item.Chest => new SpriteInfo([5925, 5926, 5927, 5928, 5929, 5930], 17, 2, 2),
            Item.WarringTriad => new SpriteInfo([5944, 5945, 5946, 5947], 3, 2, 2),
            Item.Boquet => new SpriteInfo([5949, 5950, 5951, 5952], 3, 2, 2),
            Item.Envelope => new SpriteInfo([5956, 5957], 3, 2, 1),
            Item.Plant => new SpriteInfo([5959, 5960, 5961, 5962], 0, 2, 2),
            Item.Magicite => new SpriteInfo([5964, 5965, 5966, 5967], 2, 2, 2),
            Item.Book => new SpriteInfo([5969, 5970, 5971, 5972], 0, 2, 2),
            Item.SwaddledBaby => new SpriteInfo([5974, 5975, 5976, 5977], 2, 2, 2),
            Item.QuestionMark => new SpriteInfo([5979, 5980, 5981, 5982], 6, 2, 2),
            Item.ExclamationPoint => new SpriteInfo([5984, 5985, 5986, 5987], 6, 2, 2),
            Item.Circlet => new SpriteInfo([5989, 5990], 4, 2, 1),
            Item.Weight => new SpriteInfo([5994, 5995, 5996, 5997], 4, 2, 2),
            Item.Stunner1 => new SpriteInfo([6073, 6074, 6075, 6076], 3, 2, 2),
            Item.Stunner2 => new SpriteInfo([6077, 6078, 6079, 6080], 3, 2, 2),
            Item.Turtle1 => new SpriteInfo([6082, 6083, 6084, 6085], 7, 2, 2),
            Item.Turtle2 => new SpriteInfo([6086, 6087, 6088, 6089], 7, 2, 2),
            Item.Fire1 => new SpriteInfo([6117, 6118, 6119, 6120], 6, 2, 2),
            Item.Fire2 => new SpriteInfo([6121, 6122, 6123, 6124], 6, 2, 2),
            Item.Fire3 => new SpriteInfo([6125, 6126, 6127, 6128], 6, 2, 2),
            Item.Fire4 => new SpriteInfo([6129, 6130, 6131, 6132], 6, 2, 2),
            Item.Explosion1 => new SpriteInfo([6134, 6135, 6136, 6137], 6, 2, 2),
            Item.Explosion2 => new SpriteInfo([6138, 6139, 6140, 6141], 6, 2, 2),
            Item.Explosion3 => new SpriteInfo([6142, 6143, 6144, 6145], 6, 2, 2),
            Item.Explosion4 => new SpriteInfo([6146, 6147, 6148, 6149], 6, 2, 2),
            Item.MagitekElevator => new SpriteInfo([6200, 6201, 6202, 6203, 6204, 6205, 6206, 6207, 6208, 6209, 6210, 6211, 6212, 6213, 6214, 6215], 12, 4, 4),
            Item.DiveHelm => new SpriteInfo([6228, 6229, 6230, 6231], 3, 2, 2),
            Item.Guardian => new SpriteInfo([6232, 6233, 6248, 6249, -6233, -6232, 6234, 6235, 6250, 6251, -6235, -6234, 6236, 6237, 6252, 6253, -6237, -6236, 6238, 6239, 6254, 6255, -6239, -6238, 6240, 6241, 6256, 6257, -6241, -6240, 6242, 6243, 6258, 6259, -6243, -6242,], 13, 6, 6),
            Item.MagiciteMech => new SpriteInfo([6264, 6265, 6266, 6267, 6268, 6269, 6270, 6271, 6272, 6273, 6274, 6275, 6276, 6277, 6278, 6279], 10, 4, 4),
            Item.SealedGate => new SpriteInfo([6280, 6281, 6282, -6282, -6281, -6280, 6284, 6285, 6286, -6286, -6285, -6284, 6288, 6289, 6290, -6290, -6289, -6288, 6292, 6293, 6294, -6294, -6293, -6292, 6296, 6297, 6300, -6300, -6297, -6296, 6298, 6299, 6302, -6302, -6299, -6298,], 14, 6, 6),
            Item.Spitfire => new SpriteInfo([6304, 6305, 6306, 6307], 4, 2, 2),
            Item.Sword => new SpriteInfo([6308, 6309, 6310, 6311], 4, 2, 2),
            Item.Crane => new SpriteInfo([6328, 6329, 6330, 6331, 6332, 6333, 6334, 6335, 6336, 6337, 6338, 6339, 6340, 6341, 6342, 6344, 6344, 6345, 6348, 6349, 6346, 6347, 6350, 6351], 15, 4, 6),
            Item.OwzersPainting => new SpriteInfo([6360, 6361, 6362, 6363, 6364, 6365, 6366, 6367, 6368, 6369, 6370, 6371, 6372, 6373, 6374, 6375, 6352, 6353, 6356, 6357, 6354, 6355, 6358, 6359], 18, 4, 6),
            Item.GoddessStatue => new SpriteInfo([6440, 6441, 6442, 6443, 6444, 6445, 6446, 6447, 6448, 6449, 6450, 6451, 6452, 6453, 6454, 6455, 6488, 6489, 6492, 6493, 6490, 6491, 6494, 6495], 16, 4, 6),
            Item.DoomStatue => new SpriteInfo([6456, 6457, 6458, 6459, 6460, 6461, 6462, 6463, 6464, 6465, 6466, 6467, 6468, 6469, 6470, 6471, -6501, -6500, -6497, -6496 - 6503, -6502, -6499, -6498], 16, 4, 6),
            Item.PoltrgeistStatue => new SpriteInfo([6472, 6473, 6474, 6475, 6476, 6477, 6478, 6479, 6480, 6481, 6482, 6483, 6484, 6485, 6486, 6487, 6496, 6497, 6500, 6501, 6498, 6499, 6502, 6503], 16, 4, 6),
            Item.Raft1 => new SpriteInfo([6536, 6537, 6540, 6541, 6552, 6553, 6556, 6557, 6538, 6539, 6542, 6543, 6554, 6555, 6558, 6559], 11, 4, 4),
            Item.Raft2 => new SpriteInfo([6528, 6529, 6532, 6533, 6544, 6545, 6548, 6549, 6530, 6531, 6534, 6535, 6546, 6547, 6550, 6551], 11, 4, 4),
            Item.Blackjack => new SpriteInfo([5907, 5908, 5909, 5910, 5911, 5912], 4, 2, 3),
            Item.Falcon => new SpriteInfo([6376, 6377, 6378, 6379, 6392, 6393, 6380, 6381, 6382, 6383, 6394, 6395, 6384, 6385, 6386, 6387, 6396, 6397, 6388, 6389, 6390, 6391, 6398, 6399], 20, 6, 4),
            Item.LargeChest => new SpriteInfo([6035, 6036, 6051, 6052, 6067, 6068], 75, 2, 3),
            Item.LargeOpenChest => new SpriteInfo([6033, 6034, 6049, 6050, 6065, 6066], 75, 2, 3),
            Item.PottedRoses => new SpriteInfo([4360, 4361, 4376, 4377, 4392, 4393], 374, 2, 3),
            Item.Throne => new SpriteInfo([2422, -2422, 2438, -2438, 2454, -2454, 2470, -2470], 50, 2, 4),
            Item.Engine => new SpriteInfo(
                [
                    null, null, null, 2604, 2605, 2606, 2607, null, null, null,
                    null, 2618, 2619, 2620, 2621, 2622, 2623, 2624, null, null,
                    null, 2634, 2635, 2636, 2637, 2638, 2639, 2640, 2641, 2642,
                    2646, 2647, 2648, 2649, 2650, 2651, 2652, 2653, 2654, 2655,
                    2662, 2663, 2664, 2665, 2666, 2667, 2668, 2669, 2670, 2671,
                    2678, 2679 ,2680, 2681, 2682, 2683, 2684, 2685, 2686, 2687,
                    2694, 2695, 2696, 2697, 2698, 2699, 2700, 2701, 2702, 2703,
                    2708, 2709, 2710, 2711, 2712, 2713, 2714, 2715, 2716, 2717
                ], 49, 10, 8),
            Item.WeaponShopSign => new SpriteInfo([1557, 1558, 1605, 1606], 99, 2, 2),
            Item.GhostTrain => new SpriteInfo(
                [
                     null,  null,  null,  null,  null,  null,  null, -6136,  null,  null,  null,  null,  null,
                     null,  null,  null,  null,  null,  null, -6134, -6135, -6307, -6308,  null,  null,  null,
                     null,  null,  null,  null,  null,  null, -6150, -6151, -6323, -6324, -6146,  null, -6227,
                     null,  null,  null, -6123, -6124, -6125, -6126, -6127, -6128, -6129, -6130, -6131, -6132,
                     null,  null,  null, -6139, -6140, -6141, -6142, -6143, -6144, -6145, -6145, -6147, -6148,
                     null,  null,  null, -6155, -6156, -6157, -6158, -6159, -6160, -6161, -6162, -6163, -6164,
                     null, -6137, -6138, -6171, -6172, -6173, -6174, -6175, -6176, -6177, -6178, -6178, -6180,
                    -6152, -6153, -6154, -6187, -6188, -6189, -6190, -6191, -6192, -6193, -6194, -6195, -6196,
                    -6168, -6169, -6170, -6203, -6204, -6205, -6206, -6207, -6208, -6209, -6210, -6211, -6212,
                    -6184, -6185, -6186, -6219, -6220, -6353, -6354, -6355, -6356, -6357, -6358, -6359, -6360,
                    -6200, -6201, -6202, -6235, -6236, -6361, -6362, -6363, -6364, -6365,  -6261,-6262, -6368,
                    -6216, -6217, -6218, -6251, -6252, -6369, -6370, -6371, -6372, -6373, -6374, -6488, -6489,
                     null, -6216, -6234, -6249, -6250,  null,  null, -6490, -6491, -6492, -6490, -6491, -6492,
                ], 113, 13, 13),
            Item.NarsheCobble => new SpriteInfo([4846, 4847, 4862, 4863], 98, 2, 2),
            Item.FanaticsTowerStairs => new SpriteInfo(
                [
                    null, null, null, null, null, null, null, null, null,-7139,
                    null, null, null, null, null, null, null, null, null,-7155,
                    null, null, null, null, null, null, null, null, 7171, 7172,
                    null, null, null, null, null, null, null, 7170, 7186, 7187,
                    null, null, null, null, null, null, 7170, 7185, 7201, 7202,
                    null, null, null, null, null, 7170, 7185, 7189, 7190, null,
                    7139, null, null, null, 7170, 7185, 7189, 7190, null, null,
                    7155, null, null, 7170, 7185, 7189, 7190, null, null, null,
                    7191, 7192, 7193, 7185, 7189, 7190, null, null, null, null,
                    7194, 7195, 7196, 7189, 7190, null, null, null, null, null,
                    7197, 7198, 7199, 7190, null, null, null, null, null, null,
                ], 338, 10, 11),
            Item.FanaticsTowerFloor => new SpriteInfo([6894, 6895, 6894, 6895, 6894, 6895, 6894, 6895, 6894, 6895], 337, 10, 1),
            Item.FanaticsTowerWall => new SpriteInfo(
                [
                    6910, 6911, 6910, 6911, 6910, 6911, 6910, 6911, 6910, 6911,
                    6926, 6927, 6926, 6927, 6926, 6927, 6926, 6927, 6926, 6927,
                    6942, 6943, 6942, 6943, 6942, 6943, 6942, 6943, 6942, 6943,
                    6958, 6959, 6958, 6959, 6958, 6959, 6958, 6959, 6958, 6959,
                    6974, 6975, 6974, 6975, 6974, 6975, 6974, 6975, 6974, 6975,
                ], 339, 10, 5),
            Item.DarylsTomb => new SpriteInfo(
                [
                    null,  null, 12469, 12470, null, null,
                    null,  null, 12448, 12449, null, null,
                    null,  null, 12464, 12465, null, null,
                    null,  null, 12480, 12481, 12392,12393,
                    null,  null, 12450, 12451,12473, 12474,
                    null,  null, 12466, 12467,12489, 12490,
                    null, 12468, 12482, 12483, 12471, 12472,
                    null, 12484, 12485, 12486, 12487, null
                ], 324, 6, 8),
            Item.CafeChair => new SpriteInfo([null, 2735, 2750, 2751, 2766, 2767], 75, 2, 3),
            Item.CafeTable => new SpriteInfo([2727, null, 2741, 2734, 2800, 2801, 2768, 2769, 2784, -2784], 75, 2, 5),
            //Item.CafeRug => new SpriteInfo(
            //    [
            //        4147, 4149, 4148, 4149, 4148, 4149, 4148, 4149, 4148, 4150,
            //        4114, 
            //        4151,
            //        4114,
            //        4151,
            //        4114,
            //        4151,
            //    ], 290, 10, 8),
            Item.Toilet => new SpriteInfo([11206, -11206, 11222, -11222, 11238, -11238, 11254, -11254, ],253, 2,4),
            Item.Sink => new SpriteInfo([11224, 11225, 11240, 11241, null, 11257], 253, 2, 3),
            Item.Tree => new SpriteInfo(
                [
                    1059, 1060,
                    1075, 1076,
                    1024, 1027,
                    1040, 1043,
                    1030, 1031,
                    1046, 1047,
                    1032, 1033
                ], 11, 2, 7),
            _ => throw new NotSupportedException()
        };

    private bool IsBackground(Item item)
        => item switch
        {
            Item.LargeChest => true,
            Item.LargeOpenChest => true,
            Item.PottedRoses => true,
            Item.Throne => true,
            Item.Engine => true,
            Item.WeaponShopSign => true,
            Item.GhostTrain => true,
            Item.NarsheCobble => true,
            Item.FanaticsTowerFloor => true,
            Item.FanaticsTowerWall => true,
            Item.FanaticsTowerStairs => true,
            Item.DarylsTomb => true,
            Item.CafeTable => true,
            Item.CafeChair => true,
            //Item.CafeRug => true,
            Item.Toilet => true,
            Item.Sink => true,
            Item.Tree => true,
            _ => false
        };

    public void Dispose()
    {
        foreach (var sprite in _spriteCache.Values) sprite.Dispose();
        _spriteCache.Clear();
    }

    private record SpriteInfo
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
}