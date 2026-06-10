using FF.Rando.Companion.Extensions;
using System;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;

internal class KeyItems(Seed seed, bool shatteredSkyCoin)
{
    private readonly IReadOnlyList<KeyItem> _items =
        [
            new KeyItem(seed, KeyItemType.Elixer, s => s.KaeliCured),
            new KeyItem(seed, KeyItemType.TreeWither, s=> s.MinotaurDefeated),
            new KeyItem(seed, KeyItemType.WakeWater, s => s.WakeWaterUsed),
            new KeyItem(seed, KeyItemType.VenusKey, s => !s.VenusChestUnopened ),
            new KeyItem(seed, KeyItemType.MultiKey, s=> s.TalkToGrenadeGuy),
            new KeyItem(seed, KeyItemType.GasMask, s=> false),
            new KeyItem(seed, KeyItemType.MagicMirror, s => false),
            new KeyItem(seed, KeyItemType.ThunderRock, s => s.RainbowRoad),
            new KeyItem(seed, KeyItemType.CapitansCap, s=> s.SpencerItemGiven),
            new KeyItem(seed, KeyItemType.LibraCrest, s=> false),
            new KeyItem(seed, KeyItemType.GeminiCrest, s=> false),
            new KeyItem(seed, KeyItemType.MobiusCrest, s=> false),
            new KeyItem(seed, KeyItemType.SandCoin, s=> s.SandCoinUsed),
            new KeyItem(seed, KeyItemType.RiverCoin, s=> s.UseRiverCoin),
            new KeyItem(seed, KeyItemType.SunCoin, s=> s.SunCoinUsed),
            new KeyItem(seed, KeyItemType.SkyCoin, s=> false, shatteredSkyCoin, KeyItemType.CompleteSkyCoin),
        ];

    public IReadOnlyList<KeyItem> Items => _items;

    public bool Update(ReadOnlySpan<byte> found, bool? skyCoinComplete)
    {
        var updated = false;

        foreach (var keyitem in _items)
        {
            var isfound = found.Read<bool>(keyitem.Id);
            if (keyitem.Type == KeyItemType.SkyCoin && skyCoinComplete.HasValue)
            {
                if (skyCoinComplete.Value != keyitem.IsFound)
                {
                    updated = true;
                    keyitem.IsFound = isfound;
                }
            }
            else if (isfound != keyitem.IsFound)
            {
                updated = true;
                keyitem.IsFound = isfound;
            }
        }

        return updated;
    }

    public bool UpdateUsed(GameState flags)
    {
        var updated = false;

        foreach (var keyitem in _items)
        {
            var isUsed = keyitem.CheckUsed(flags);
            if (isUsed != keyitem.IsUsed)
            {
                updated = true;
                keyitem.IsUsed = isUsed;
            }
        }

        return updated;
    }
}
