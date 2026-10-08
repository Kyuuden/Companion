using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System;

namespace FF.Rando.Companion.Games.WorldsCollide.Rendering;

public class TileSetSprites(byte[] tileData, byte[] paletteData) : OtherSprites<TileSet>(tileData, paletteData, 4)
{
    protected override SpriteInfo GetInfo(TileSet item)
        => item switch
        {
            TileSet.LargeChest => new SpriteInfo([6035, 6036, 6051, 6052, 6067, 6068], 75, 2, 3),
            TileSet.LargeOpenChest => new SpriteInfo([6033, 6034, 6049, 6050, 6065, 6066], 75, 2, 3),
            TileSet.PottedRoses => new SpriteInfo([4360, 4361, 4376, 4377, 4392, 4393], 374, 2, 3),
            TileSet.Throne => new SpriteInfo([2422, -2422, 2438, -2438, 2454, -2454, 2470, -2470], 50, 2, 4),
            TileSet.Engine => new SpriteInfo(
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
            TileSet.WeaponShopSign => new SpriteInfo([1557, 1558, 1605, 1606], 99, 2, 2),
            TileSet.GhostTrain => new SpriteInfo(
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
            TileSet.NarsheCobble => new SpriteInfo([4846, 4847, 4862, 4863], 98, 2, 2),
            TileSet.CafeChair => new SpriteInfo([null, 2735, 2750, 2751, 2766, 2767], 75, 2, 3),
            TileSet.CafeTable => new SpriteInfo([2727, null, 2741, 2734, 2800, 2801, 2768, 2769, 2784, -2784], 75, 2, 5),
            TileSet.Toilet => new SpriteInfo([11206, -11206, 11222, -11222, 11238, -11238, 11254, -11254,], 253, 2, 4),
            TileSet.Sink => new SpriteInfo([11224, 11225, 11240, 11241, null, 11257], 253, 2, 3),
            TileSet.Tree => new SpriteInfo(
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
}
