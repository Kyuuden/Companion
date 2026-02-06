using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal class Checks
{
    private readonly IReadOnlyList<Check> _values;
    private readonly IDictionary<Events, Check> _checkLookup;
    private readonly Seed _seed;

    public Checks(Seed seed)
    {
        _seed = seed;
        _values =
            [
                new Check(seed, Events.DEFEATED_WHELK),
                new Check(seed, Events.RODE_RAFT_LETE_RIVER),
                new Check(seed, Events.BLOCK_SEALED_GATE),
                new Check(seed, Events.GOT_ZOZO_REWARD),
                new Check(seed, Events.RECRUITED_TERRA_MOBLIZ),
                new Check(seed, Events.DEFEATED_TUNNEL_ARMOR),
                new Check(seed, Events.GOT_RAGNAROK),
                new Check(seed, Events.GOT_BOTH_REWARDS_WEAPON_SHOP),
                new Check(seed, Events.RECRUITED_LOCKE_PHOENIX_CAVE),
                new Check(seed, Events.NAMED_EDGAR),
                new Check(seed, Events.DEFEATED_TENTACLES_FIGARO),
                new Check(seed, Events.GOT_RAIDEN),
                new Check(seed, Events.DEFEATED_VARGAS),
                new Check(seed, Events.FINISHED_COLLAPSING_HOUSE),
                new Check(seed, Events.NAMED_GAU),
                new Check(seed, Events.FINISHED_IMPERIAL_CAMP),
                new Check(seed, Events.GOT_PHANTOM_TRAIN_REWARD),
                new Check(seed, Events.RECRUITED_SHADOW_GAU_FATHER_HOUSE),
                new Check(seed, Events.RECRUITED_SHADOW_FLOATING_CONTINENT),
                new Check(seed, Events.DEFEATED_ATMAWEAPON),
                new Check(seed, Events.FINISHED_FLOATING_CONTINENT),
                new Check(seed, Events.DEFEATED_SR_BEHEMOTH),
                new Check(seed, Events.FINISHED_DOMA_WOB),
                new Check(seed, Events.DEFEATED_STOOGES),
                new Check(seed, Events.FINISHED_DOMA_WOR),
                new Check(seed, Events.GOT_ALEXANDR),
                new Check(seed, Events.FINISHED_MT_ZOZO),
                new Check(seed, Events.VELDT_REWARD_OBTAINED),
                new Check(seed, Events.GOT_SERPENT_TRENCH_REWARD),
                new Check(seed, Events.FREED_CELES),
                new Check(seed, Events.GOT_IFRIT_SHIVA),
                new Check(seed, Events.DEFEATED_NUMBER_024),
                new Check(seed, Events.DEFEATED_CRANES),
                new Check(seed, Events.FINISHED_OPERA_DISRUPTION),
                new Check(seed, Events.RECRUITED_SHADOW_KOHLINGEN),
                new Check(seed, Events.DEFEATED_DULLAHAN),
                new Check(seed, Events.CHASING_LONE_WOLF7),
                new Check(seed, Events.GOT_BOTH_REWARDS_LONE_WOLF),
                new Check(seed, Events.COMPLETED_MOOGLE_DEFENSE),
                new Check(seed, Events.DEFEATED_FLAME_EATER),
                new Check(seed, Events.DEFEATED_HIDON),
                new Check(seed, Events.DEFEATED_MAGIMASTER),
                new Check(seed, Events.RECRUITED_STRAGO_FANATICS_TOWER),
                new Check(seed, Events.DEFEATED_ULTROS_ESPER_MOUNTAIN),
                new Check(seed, Events.DEFEATED_CHADARNOOK),
                new Check(seed, Events.RECRUITED_GOGO_WOR),
                new Check(seed, Events.RECRUITED_UMARO_WOR),
                new Check(seed, Events.FINISHED_NARSHE_BATTLE),
                new Check(seed, Events.BOUGHT_ESPER_TZEN),
                new Check(seed, Events.DEFEATED_DOOM_GAZE),
                new Check(seed, Events.GOT_TRITOCH),
                new Check(seed, Events.AUCTION_BOUGHT_ESPER1),
                new Check(seed, Events.AUCTION_BOUGHT_ESPER2),
                new Check(seed, Events.DEFEATED_ATMA),
            ];

        _checkLookup = _values.ToDictionary(c => c.Event, c => c);
    }

    internal IReadOnlyList<Check> Values => _values;

    public void UpdateRelatedChecks()
    {
        foreach (var check in _values)
        {
            check.IsVisible = true;
            check.LinkedChecks = [];
        }
        
        foreach (var group in _seed.SpriteSet.RelatedEvents.Select(g=>g.ToList()))
        {
            if (group.Count == 0)
                continue;

            _checkLookup[group.First()].LinkedChecks = group.Skip(1).Select(c => _checkLookup[c]).ToList();
            foreach (var check in group.Skip(1))
            {
                if (!_checkLookup.TryGetValue(check, out var trackedCheck))
                    continue;

                trackedCheck.IsVisible = false;
            }
        }
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> events, ref Reward? currentReward)
    {
        var updated = false;
        foreach (var check in _values)
        {
            var isComplete = events.Read<bool>(check.Id);

            if (isComplete != check.IsCompleted)
            {
                updated = true;
                check.IsCompleted = isComplete;
                check.WhenCompleted = time;
                if (!check.Reward.HasValue)
                {
                    check.Reward = currentReward ?? Reward.Item;
                    currentReward = null;
                }
            }

            if (!check.IsCompleted)
            {
                bool isAvailable = check.Event.IsAvailable(events);
                if (isAvailable != check.IsAvailable)
                {
                    updated = true;
                    check.IsAvailable = isAvailable;
                }
            }
        }

        return updated;
    }
}