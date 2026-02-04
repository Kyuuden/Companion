using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public class EffectSprites(byte[] tileData, byte[] paletteData) : OtherSprites<Effect>(tileData, paletteData, 3)
{
    protected override SpriteInfo GetInfo(Effect effect)
        => effect switch
        {
            Effect.AtmaWeapon => throw new NotImplementedException(),
            Effect.Illumina => new SpriteInfo(
            [
                null, null, null, 2579,
                null, null, 2588, 2581,
                null, 2588, 2581, null,
                2580, 2581, null, null,
                2589, 2590, null, null,
                2582, null, null, null,

            ], 32, 4, 6),
            Effect.Ragnarock => throw new NotImplementedException(),
            Effect.Condemmed => new SpriteInfo(
            [
                8,   9,  -9,  -8,
                24, 25, -25, -24,
                38, 39, -39, -38,
                50, 51, -51, -50,
                null, 36,  37, null,
                null, 49, -49, null
            ], 166, 4, 6),
            _ => throw new NotSupportedException()
        };
}