using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Bosses
{
    private readonly Dictionary<BossType, Boss> _bosses;
    private readonly Descriptors _descriptors;

    public Bosses(Descriptors descriptors)
    {
        _bosses = Enum.GetValues(typeof(BossType))
            .OfType<BossType>()
            .Where(t => t != BossType.Altgauntlet)
            .ToDictionary(t => t, t => new Boss(descriptors, t));

        _bosses[BossType.Altgauntlet] = _bosses[BossType.FabulGauntlet];
        _descriptors = descriptors;
    }

    public IReadOnlyList<Boss> Items => _bosses.Where(v => v.Key != BossType.Altgauntlet).Select(v=> v.Value).ToList();

    public bool Update(TimeSpan time, ReadOnlySpan<byte> locations, ReadOnlySpan<byte> defeated, ReadOnlySpan<byte> bossLocationsDefeated)
    {
        var updated = false;

        for (int i = 0; i < locations.Length; i++)
        {
            var bossId = locations[i];

            if (bossId == 0xFF)
                continue;

            var location = _descriptors.GetBossLocationName((BossLocationType)i);

            if (_bosses.TryGetValue((BossType)bossId, out var boss))
            {
                updated |= boss.AddEncounter((BossLocationType)i, time);
                if (bossLocationsDefeated.Read<bool>(i))
                    updated |= boss.DefeatEncounter((BossLocationType)i, time);
            }
        }

        return updated;
    }
}
