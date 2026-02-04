using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public class ItemSprites(byte[] tileData, byte[] paletteData) : OtherSprites<Item>(tileData, paletteData, 4)
{
    protected override SpriteInfo GetInfo(Item item)
        => item switch
        {
            Item.UmaroSkull => new SpriteInfo([5847, 5848, 5849, 5850, 5851, 5852], 4, 2, 3),
            Item.Chest => new SpriteInfo([5925, 5926, 5927, 5928], 17, 2, 2),
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
            _ => throw new NotSupportedException()
        };
}
