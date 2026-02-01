using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.Enums;

public static class EventExtensions
{
    public static bool IsCheck(this Events eventBitIndex)
        => eventBitIndex switch
        {
            Events.GOT_RAIDEN => true,
            Events.NAMED_GAU => true,
            Events.DEFEATED_FLAME_EATER => true,
            Events.FINISHED_COLLAPSING_HOUSE => true,
            Events.DEFEATED_DULLAHAN => true,
            Events.FINISHED_DOMA_WOB => true,
            Events.DEFEATED_STOOGES => true,
            Events.FINISHED_DOMA_WOR => true,
            Events.GOT_ALEXANDR => true,
            Events.DEFEATED_HIDON => true,
            Events.DEFEATED_ULTROS_ESPER_MOUNTAIN => true,
            Events.RECRUITED_STRAGO_FANATICS_TOWER => true,
            Events.DEFEATED_MAGIMASTER => true,
            Events.NAMED_EDGAR => true,
            Events.DEFEATED_TENTACLES_FIGARO => true,
            Events.RECRUITED_SHADOW_FLOATING_CONTINENT => true,
            Events.DEFEATED_ATMAWEAPON => true,
            Events.FINISHED_FLOATING_CONTINENT => true,
            Events.RECRUITED_SHADOW_GAU_FATHER_HOUSE => true,
            Events.FINISHED_IMPERIAL_CAMP => true,
            Events.DEFEATED_ATMA => true,
            Events.RECRUITED_SHADOW_KOHLINGEN => true,
            Events.RODE_RAFT_LETE_RIVER => true,
            Events.CHASING_LONE_WOLF7 => true,
            Events.GOT_BOTH_REWARDS_LONE_WOLF => true,
            Events.GOT_IFRIT_SHIVA => true,
            Events.DEFEATED_NUMBER_024 => true,
            Events.DEFEATED_CRANES => true,
            Events.RECRUITED_TERRA_MOBLIZ => true,
            Events.COMPLETED_MOOGLE_DEFENSE => true,
            Events.DEFEATED_VARGAS => true,
            Events.FINISHED_MT_ZOZO => true,
            Events.FINISHED_NARSHE_BATTLE => true,
            Events.GOT_RAGNAROK => true,
            Events.GOT_BOTH_REWARDS_WEAPON_SHOP => true,
            Events.FINISHED_OPERA_DISRUPTION => true,
            Events.DEFEATED_CHADARNOOK => true,
            Events.GOT_PHANTOM_TRAIN_REWARD => true,
            Events.RECRUITED_LOCKE_PHOENIX_CAVE => true,
            Events.BLOCK_SEALED_GATE => true,
            Events.DEFEATED_DOOM_GAZE => true,
            Events.GOT_SERPENT_TRENCH_REWARD => true,
            Events.FREED_CELES => true,
            Events.DEFEATED_TUNNEL_ARMOR => true,
            Events.GOT_TRITOCH => true,
            Events.BOUGHT_ESPER_TZEN => true,
            Events.RECRUITED_UMARO_WOR => true,
            Events.VELDT_REWARD_OBTAINED => true,
            Events.DEFEATED_SR_BEHEMOTH => true,
            Events.DEFEATED_WHELK => true,
            Events.RECRUITED_GOGO_WOR => true,
            Events.GOT_ZOZO_REWARD => true,
            Events.AUCTION_BOUGHT_ESPER1 => true,
            Events.AUCTION_BOUGHT_ESPER2 => true,
            _ => false
        };

    public static bool IsCharacter(this Events eventBitIndex)
        => eventBitIndex switch
        {
            Events.TERRA_IN_PARTY => true,
            Events.LOCKE_IN_PARTY => true,
            Events.CYAN_IN_PARTY => true,
            Events.SHADOW_IN_PARTY => true,
            Events.EDGAR_IN_PARTY => true,
            Events.SABIN_IN_PARTY => true,
            Events.CELES_IN_PARTY => true,
            Events.STRAGO_IN_PARTY => true,
            Events.RELM_IN_PARTY => true,
            Events.SETZER_IN_PARTY => true,
            Events.MOG_IN_PARTY => true,
            Events.GAU_IN_PARTY => true,
            Events.GOGO_IN_PARTY => true,
            Events.UMARO_IN_PARTY => true,
            _ => false
        };

    public static bool IsDragonLocation(this Events eventBitIndex)
        => eventBitIndex switch
        {
            Events.DEFEATED_ANCIENT_CASTLE_DRAGON => true,
            Events.DEFEATED_FANATICS_TOWER_DRAGON => true,
            Events.DEFEATED_KEFKA_TOWER_DRAGON_G => true,
            Events.DEFEATED_KEFKA_TOWER_DRAGON_S => true,
            Events.DEFEATED_MT_ZOZO_DRAGON => true,
            Events.DEFEATED_NARSHE_DRAGON => true,
            Events.DEFEATED_OPERA_HOUSE_DRAGON => true,
            Events.DEFEATED_PHOENIX_CAVE_DRAGON => true,
            _ => false,
        };

    public static IList<Events> GetRequirements(this Events eventBit)
    {
        if (eventBit.IsCharacter())
            return [];

        if (eventBit.IsDragonLocation())
            return [];

        if (!eventBit.IsCheck())
            return [];

        return eventBit switch
        {
            Events.GOT_RAIDEN => [Events.EDGAR_IN_PARTY],
            Events.NAMED_GAU => [Events.SABIN_IN_PARTY],
            Events.DEFEATED_FLAME_EATER => [Events.STRAGO_IN_PARTY],
            Events.FINISHED_COLLAPSING_HOUSE => [Events.SABIN_IN_PARTY],
            Events.DEFEATED_DULLAHAN => [Events.SETZER_IN_PARTY],
            Events.FINISHED_DOMA_WOB => [Events.CYAN_IN_PARTY],
            Events.DEFEATED_STOOGES => [Events.CYAN_IN_PARTY],
            Events.FINISHED_DOMA_WOR => [Events.CYAN_IN_PARTY, Events.DEFEATED_STOOGES],
            Events.GOT_ALEXANDR => [Events.CYAN_IN_PARTY, Events.FINISHED_DOMA_WOR, Events.DEFEATED_STOOGES],
            Events.DEFEATED_HIDON => [Events.STRAGO_IN_PARTY],
            Events.DEFEATED_ULTROS_ESPER_MOUNTAIN => [Events.RELM_IN_PARTY],
            Events.RECRUITED_STRAGO_FANATICS_TOWER => [Events.STRAGO_IN_PARTY],
            Events.DEFEATED_MAGIMASTER => [Events.STRAGO_IN_PARTY, Events.RECRUITED_STRAGO_FANATICS_TOWER],
            Events.NAMED_EDGAR => [Events.EDGAR_IN_PARTY],
            Events.DEFEATED_TENTACLES_FIGARO => [Events.EDGAR_IN_PARTY],
            Events.RECRUITED_SHADOW_FLOATING_CONTINENT => [Events.SHADOW_IN_PARTY],
            Events.DEFEATED_ATMAWEAPON => [Events.SHADOW_IN_PARTY, Events.RECRUITED_SHADOW_FLOATING_CONTINENT],
            Events.FINISHED_FLOATING_CONTINENT => [Events.SHADOW_IN_PARTY, Events.DEFEATED_ATMAWEAPON, Events.RECRUITED_SHADOW_FLOATING_CONTINENT],
            Events.RECRUITED_SHADOW_GAU_FATHER_HOUSE => [Events.SHADOW_IN_PARTY],
            Events.FINISHED_IMPERIAL_CAMP => [Events.SABIN_IN_PARTY],
            Events.DEFEATED_ATMA => [], //TODO KEFKAs tower access
            Events.RECRUITED_SHADOW_KOHLINGEN => [Events.SETZER_IN_PARTY],
            Events.RODE_RAFT_LETE_RIVER => [Events.TERRA_IN_PARTY],
            Events.CHASING_LONE_WOLF7 => [Events.MOG_IN_PARTY],
            Events.GOT_BOTH_REWARDS_LONE_WOLF => [Events.MOG_IN_PARTY, Events.CHASING_LONE_WOLF7],
            Events.GOT_IFRIT_SHIVA => [Events.CELES_IN_PARTY],
            Events.DEFEATED_NUMBER_024 => [Events.CELES_IN_PARTY, Events.GOT_IFRIT_SHIVA],
            Events.DEFEATED_CRANES => [Events.CELES_IN_PARTY, Events.DEFEATED_NUMBER_024, Events.GOT_IFRIT_SHIVA],
            Events.RECRUITED_TERRA_MOBLIZ => [Events.TERRA_IN_PARTY],
            Events.COMPLETED_MOOGLE_DEFENSE => [Events.MOG_IN_PARTY],
            Events.DEFEATED_VARGAS => [Events.SABIN_IN_PARTY],
            Events.FINISHED_MT_ZOZO => [Events.CYAN_IN_PARTY],
            Events.FINISHED_NARSHE_BATTLE => [],
            Events.GOT_RAGNAROK => [Events.LOCKE_IN_PARTY],
            Events.GOT_BOTH_REWARDS_WEAPON_SHOP => [Events.LOCKE_IN_PARTY, Events.GOT_RAGNAROK],
            Events.FINISHED_OPERA_DISRUPTION => [Events.CELES_IN_PARTY],
            Events.DEFEATED_CHADARNOOK => [Events.RELM_IN_PARTY],
            Events.GOT_PHANTOM_TRAIN_REWARD => [Events.SABIN_IN_PARTY],
            Events.RECRUITED_LOCKE_PHOENIX_CAVE => [Events.LOCKE_IN_PARTY],
            Events.BLOCK_SEALED_GATE => [Events.TERRA_IN_PARTY],
            Events.DEFEATED_DOOM_GAZE => [],
            Events.GOT_SERPENT_TRENCH_REWARD => [Events.GAU_IN_PARTY],
            Events.FREED_CELES => [Events.CELES_IN_PARTY],
            Events.DEFEATED_TUNNEL_ARMOR => [Events.LOCKE_IN_PARTY],
            Events.GOT_TRITOCH => [],
            Events.BOUGHT_ESPER_TZEN => [],
            Events.RECRUITED_UMARO_WOR => [Events.UMARO_IN_PARTY],
            Events.VELDT_REWARD_OBTAINED => [],
            Events.DEFEATED_SR_BEHEMOTH => [Events.SHADOW_IN_PARTY],
            Events.DEFEATED_WHELK => [Events.TERRA_IN_PARTY],
            Events.RECRUITED_GOGO_WOR => [Events.GOGO_IN_PARTY],
            Events.GOT_ZOZO_REWARD => [Events.TERRA_IN_PARTY],
            Events.AUCTION_BOUGHT_ESPER1 => [],
            Events.AUCTION_BOUGHT_ESPER2 => [Events.AUCTION_BOUGHT_ESPER1],
            _ => []
        };
    }
}