using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Rendering;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal class Worlds
{
    public IReadOnlyList<World> Items { get; }
    public bool Update(State state)
    {
        var ret = false;
        foreach (var check in Items)
        {
            ret |= check.Update(state);
        }
        return ret;
    }

    public Worlds(Rendering.WorldMaps worldMaps)
    {
        Items =
        [
            new World(WorldMapType.WorldOfBalance)
            {
                Map = worldMaps.Get(WorldMapType.WorldOfBalance).Resize(new(1024, 1024)),
                Locations =
                [
                    new CheckLocation("Lete River")
                    {
                        Location = new(419,257),
                        Checks = [new GatedCheck(EventType.RODE_RAFT_LETE_RIVER,EventType.TERRA_IN_PARTY, RewardType.Any) ]
                    },
                    new CheckLocation("Sealed Cave")
                    {
                        Location = new(676,777),
                        Checks = [new GatedCheck(EventType.BLOCK_SEALED_GATE, EventType.TERRA_IN_PARTY, RewardType.Any) ]
                    },
                    new CheckLocation("Narshe")
                    {
                        Location = new(337,133),
                        Checks =
                        [
                            new GatedCheck(EventType.DEFEATED_WHELK, EventType.TERRA_IN_PARTY, RewardType.Any),
                            new BasicCheck(EventType.FINISHED_NARSHE_BATTLE, RewardType.Any),
                            new GatedProgressiveCheck(EventType.MOG_IN_PARTY, [EventType.CHASING_LONE_WOLF7, EventType.GOT_BOTH_REWARDS_LONE_WOLF], [RewardType.Any, RewardType.Item]),
                            new GatedCheck(EventType.COMPLETED_MOOGLE_DEFENSE, EventType.MOG_IN_PARTY, RewardType.Any),
                            //new Check(EventType.GOT_BOTH_REWARDS_WEAPON_SHOP) {AvailabilityRule = new Rule {Events = [EventType.LOCKE_IN_PARTY, EventType.DEFEATED_WHELK, EventType.GOT_RAGNAROK]}}
                        ]
                    },
                    new CheckLocation("Zozo")
                    {
                        Location = new(89,369),
                        Checks = [new GatedCheck(EventType.GOT_ZOZO_REWARD, EventType.TERRA_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Barren Falls")
                    {
                        Location = new(739,372),
                        Checks = [new GatedCheck(EventType.NAMED_GAU, EventType.SABIN_IN_PARTY, RewardType.Any)],
                    },
                    new CheckLocation("Mount Kolts")
                    { 
                        Location = new(410,403),
                        Checks = [new GatedCheck(EventType.DEFEATED_VARGAS, EventType.SABIN_IN_PARTY, RewardType.Any) ]
                    },
                    new CheckLocation("Haunted Forest")
                    {
                        Location= new(714,328),
                        Checks = [new GatedCheck(EventType.GOT_PHANTOM_TRAIN_REWARD, EventType.SABIN_IN_PARTY, RewardType.Any) ]
                    },
                    new CheckLocation("Imperial Camp")
                    {
                        Location = new(720,286),
                        Checks = [new GatedCheck(EventType.FINISHED_IMPERIAL_CAMP, EventType.SABIN_IN_PARTY, RewardType.Any) ]
                    },
                    new CheckLocation("Opera House")
                    {
                        Location = new(180,618),
                        Checks = [new GatedCheck(EventType.FINISHED_OPERA_DISRUPTION, EventType.CELES_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("South Figaro")
                    { 
                        Location = new(343,450),
                        Checks = [new GatedCheck(EventType.FREED_CELES, EventType.CELES_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Jidoor")
                    {
                        Location = new(108,524),
                        Checks = [new ProgressiveCheck([EventType.AUCTION_BOUGHT_ESPER1, EventType.AUCTION_BOUGHT_ESPER2], [RewardType.EsperOrItem, RewardType.EsperOrItem])]
                    },
                    new CheckLocation("Kohlingen")
                    {
                        Location= new(45,156),
                        Checks = [new GatedCheck(EventType.RECRUITED_SHADOW_KOHLINGEN, EventType.SETZER_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Serpent's Trench")
                    {
                        Location = new(859,593),
                        Checks = [new GatedCheck(EventType.GOT_SERPENT_TRENCH_REWARD, EventType.GAU_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Veldt")
                    {
                        Location = new(820,520),
                        Checks = [new GatedCheck(EventType.VELDT_REWARD_OBTAINED, EventType.GAU_IN_PARTY, RewardType.CharacterOEsper)] //TODO dried meat?
                    },
                    new CheckLocation("Figaro Castle")
                    {
                        Location = new(263,312),
                        Checks = [new GatedCheck(EventType.NAMED_EDGAR, EventType.EDGAR_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Figaro Cave")
                    {
                        Location = new(302,410),
                        Checks = [new GatedCheck(EventType.DEFEATED_TUNNEL_ARMOR, EventType.LOCKE_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Doma")
                    {
                        Location = new(626,336),
                        Checks = [new GatedCheck(EventType.FINISHED_DOMA_WOB, EventType.CYAN_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Esper Mountain")
                    {
                        Location = new(918,521),
                        Checks = [new GatedCheck(EventType.DEFEATED_ULTROS_ESPER_MOUNTAIN, EventType.RELM_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Thamasa")
                    {
                        Location = new(1005,507),
                        Checks = [new GatedCheck(EventType.DEFEATED_FLAME_EATER, EventType.STRAGO_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Tzen")
                    {
                        Location = new(480,600),
                        Checks = [new BasicCheck(EventType.BOUGHT_ESPER_TZEN, RewardType.EsperOrItem)]
                    },
                    new CheckLocation("Vector")
                    {
                        Location = new(484,748),
                        Checks = [new GatedProgressiveCheck(EventType.CELES_IN_PARTY, [EventType.GOT_IFRIT_SHIVA, EventType.DEFEATED_NUMBER_024, EventType.DEFEATED_CRANES], [RewardType.EsperOrItem, RewardType.EsperOrItem, RewardType.CharacterOEsper])]
                    },
                    new CheckLocation("Gau Manor")
                    {
                        Location = new(660,140),
                        Checks = [new GatedCheck(EventType.RECRUITED_SHADOW_GAU_FATHER_HOUSE, EventType.SHADOW_IN_PARTY, RewardType.Any)]
                        
                    },
                    new CheckLocation("Floating Continent")
                    {
                        Location = new(547,477),
                        Checks = [new GatedProgressiveCheck(EventType.SHADOW_IN_PARTY, [EventType.RECRUITED_SHADOW_FLOATING_CONTINENT, EventType.DEFEATED_ATMAWEAPON, EventType.FINISHED_FLOATING_CONTINENT], [RewardType.CharacterOEsper, RewardType.EsperOrItem, RewardType.CharacterOEsper])]
                    }
                ]
            },
            new World(WorldMapType.WorldOfRuin)
            {
                Map = worldMaps.Get(WorldMapType.WorldOfRuin).Resize(new(1024, 1024)),
                Locations =
                [
                    new CheckLocation("Mobliz")
                    {
                        Location = new(945,545),
                        Checks = [new GatedCheck(EventType.RECRUITED_TERRA_MOBLIZ, EventType.TERRA_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Tzen")
                    {
                        Location = new(515,719),
                        Checks = 
                        [
                            new GatedCheck(EventType.FINISHED_COLLAPSING_HOUSE, EventType.SABIN_IN_PARTY, RewardType.Any),
                            new BasicCheck(EventType.BOUGHT_ESPER_TZEN, RewardType.EsperOrItem)
                        ]
                    },
                    new CheckLocation("Daryl's Tomb")
                    {
                        Location = new(102,209),
                        Checks = [new GatedCheck(EventType.DEFEATED_DULLAHAN, EventType.SETZER_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Phoenix Cave")
                    {
                        Location = new(472,628),
                        Checks =
                        [
                            new GatedCheck(EventType.RECRUITED_LOCKE_PHOENIX_CAVE, EventType.LOCKE_IN_PARTY, RewardType.Any),
                            new GatedCheck(EventType.DEFEATED_PHOENIX_CAVE_DRAGON, EventType.LOCKE_IN_PARTY, RewardType.Item),
                        ]
                    },
                    new CheckLocation("Triangle Island")
                    {
                        Location = new(950,208),
                        Checks = [new GatedCheck(EventType.RECRUITED_GOGO_WOR, EventType.GOGO_IN_PARTY, RewardType.Any) ]
                    },
                    new CheckLocation("Narshe")
                    {
                        Location = new(461,134),
                        Checks =
                        [
                            new GatedCheck(EventType.GOT_RAGNAROK, EventType.LOCKE_IN_PARTY, RewardType.EsperOrItem),
                            new BasicCheck(EventType.DEFEATED_NARSHE_DRAGON, RewardType.Item),
                            new BasicCheck(EventType.GOT_TRITOCH, RewardType.EsperOrItem),
                            new GatedCheck(EventType.RECRUITED_UMARO_WOR, EventType.UMARO_IN_PARTY, RewardType.Any),
                            
                        ]
                    },
                    new CheckLocation("Doma")
                    {
                        Location = new(688,300),
                        Checks = [new GatedProgressiveCheck(EventType.CYAN_IN_PARTY, [EventType.DEFEATED_STOOGES, EventType.FINISHED_DOMA_WOR, EventType.GOT_ALEXANDR], [RewardType.EsperOrItem, RewardType.CharacterOEsper, RewardType.EsperOrItem])]
                    },
                    new CheckLocation("Fanatics' Tower")
                    {
                        Location = new(688,300),
                        Checks =
                        [
                            new GatedProgressiveCheck( EventType.STRAGO_IN_PARTY, [EventType.DEFEATED_MAGIMASTER, EventType.RECRUITED_STRAGO_FANATICS_TOWER], [RewardType.Item, RewardType.CharacterOEsper]),
                            new BasicCheck(EventType.DEFEATED_FANATICS_TOWER_DRAGON, RewardType.Item),
                        ]
                    },
                    new CheckLocation("Figaro Cave")
                    {
                        Location = new(426,394),
                        Checks = [new GatedCheck(EventType.DEFEATED_TENTACLES_FIGARO, EventType.EDGAR_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Figaro Castle")
                    {
                        Location = new(334,343),
                        Checks =
                        [
                            new GatedCheck(EventType.DEFEATED_ANCIENT_CASTLE_DRAGON, EventType.EDGAR_IN_PARTY, RewardType.Item),
                            new GatedCheck(EventType.GOT_RAIDEN, EventType.EDGAR_IN_PARTY, RewardType.Any),
                            new GatedCheck(EventType.NAMED_EDGAR, EventType.EDGAR_IN_PARTY, RewardType.Any),
                        ]
                    },
                    new CheckLocation("Jidoor")
                    {
                        Location = new(140,627),
                        Checks =
                        [
                            new GatedCheck(EventType.DEFEATED_CHADARNOOK, EventType.RELM_IN_PARTY, RewardType.Any),
                            new ProgressiveCheck([EventType.AUCTION_BOUGHT_ESPER1, EventType.AUCTION_BOUGHT_ESPER2], [RewardType.EsperOrItem, RewardType.EsperOrItem])
                        ]
                    },
                    new CheckLocation("Zozo")
                    {
                        Location = new(174,524),
                        Checks =
                        [
                            new GatedCheck(EventType.FINISHED_MT_ZOZO, EventType.CYAN_IN_PARTY, RewardType.Any),
                            new GatedCheck(EventType.DEFEATED_MT_ZOZO_DRAGON, EventType.CYAN_IN_PARTY, RewardType.Item)
                        ]
                    },
                    new CheckLocation("Ebots Rock")
                    {
                        Location = new(999,894),
                        Checks = [new GatedCheck(EventType.DEFEATED_HIDON, EventType.STRAGO_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Veldt Cave")
                    { 
                        Location = new(830,356),
                        Checks = [new GatedCheck(EventType.DEFEATED_SR_BEHEMOTH, EventType.SHADOW_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Search The Skies")
                    {
                        Location = new(522,522),
                        Checks = [new BasicCheck(EventType.DEFEATED_DOOM_GAZE, RewardType.EsperOrItem)]
                    },
                    new CheckLocation("Opera House")
                    { 
                        Location = new(125,733),
                        Checks =[new BasicCheck(EventType.DEFEATED_OPERA_HOUSE_DRAGON, RewardType.Item)]
                    },
                    new CheckLocation("Veldt")
                    {
                        Location = new(860,329),
                        Checks = [new GatedCheck(EventType.VELDT_REWARD_OBTAINED, EventType.GAU_IN_PARTY, RewardType.CharacterOEsper)] //TODO dried meat?
                    },
                    new CheckLocation("Kefka's Tower")
                    {
                        Location= new(549,792),
                        Checks =
                        [
                            new BasicCheck(EventType.DEFEATED_ATMA, RewardType.Item),
                            new BasicCheck(EventType.DEFEATED_KEFKA_TOWER_DRAGON_G, RewardType.Item),
                            new BasicCheck(EventType.DEFEATED_KEFKA_TOWER_DRAGON_S, RewardType.Item),
                            new CustomAvailabilityCheck(EventType.POLTRGEIST_STATUE_KEFKA_TOWER){ Rules = [EventType.UNLOCKED_KT_SKIP, EventType.UNLOCKED_FINAL_KEFKA]},
                            new CustomAvailabilityCheck(EventType.DOOM_STATUE_KEFKA_TOWER) { Rules = [EventType.UNLOCKED_KT_SKIP, EventType.UNLOCKED_FINAL_KEFKA]},
                            new CustomAvailabilityCheck(EventType.GODDESS_STATUE_KEFKA_TOWER){ Rules = [EventType.UNLOCKED_KT_SKIP, EventType.UNLOCKED_FINAL_KEFKA]}
                        ]
                    },
                    new CheckLocation("Kohlingen")
                    {
                        Location= new(156, 182),
                        Checks = [new GatedCheck(EventType.RECRUITED_SHADOW_KOHLINGEN, EventType.SETZER_IN_PARTY, RewardType.Any)]
                    },
                    new CheckLocation("Thamasa")
                    {
                        Location = new(1005,924),
                        Checks = [new GatedCheck(EventType.DEFEATED_FLAME_EATER, EventType.STRAGO_IN_PARTY, RewardType.Any)]
                    }
                ]
            },
        ];
    }
}
