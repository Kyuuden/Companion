using FF.Rando.Companion.Extensions;
using System;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;

internal class KeyItems
{
    private readonly IReadOnlyList<KeyItem> _items;

    private readonly Seed _seed;

    public KeyItems(Seed seed, bool shatteredSkyCoin)
    {
        List<KeyItem> keyItems =
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

        if (seed.SkyCoinMode == SkyCoinMode.SaveTheCrystals)
        {
            keyItems.Add(new KeyItem(seed, KeyItemType.EarthCrystal, s => false));
            keyItems.Add(new KeyItem(seed, KeyItemType.WaterCrystal, s => false));
            keyItems.Add(new KeyItem(seed, KeyItemType.FireCrystal, s => false));
            keyItems.Add(new KeyItem(seed, KeyItemType.WindCrystal, s => false));
        }

        _items = keyItems;
        _seed = seed;
    }

    public IReadOnlyList<KeyItem> Items => _items;

    public bool Update(ReadOnlySpan<byte> found, GameState flags, bool? skyCoinComplete)
    {
        var updated = false;

        foreach (var keyitem in _items)
        {
            bool isfound = keyitem.Type switch
            {
                KeyItemType.EarthCrystal => flags.FlamerusRexDefeated,
                KeyItemType.WaterCrystal => flags.IceGolemDefeated,
                KeyItemType.FireCrystal => flags.DualHeadHydraDefeated,
                KeyItemType.WindCrystal => flags.PazuzuDefeated,
                _ => found.Read<bool>(keyitem.Id)
            };

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

        if (_seed.Settings.Equipment.ShowUsedKeyItems)
            updated |= UpdateUsed(flags);

        return updated;
    }

    private bool UpdateUsed(GameState flags)
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
