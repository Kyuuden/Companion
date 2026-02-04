using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.Enums;

public static class EnumExtensions
{
    public static Reward? ToReward(this Events @event)
        => @event switch
        {
            Events.TERRA_IN_PARTY => Reward.Terra,
            Events.LOCKE_IN_PARTY => Reward.Locke,
            Events.CYAN_IN_PARTY => Reward.Cyan,
            Events.SHADOW_IN_PARTY => Reward.Shadow,
            Events.EDGAR_IN_PARTY => Reward.Edgar,
            Events.SABIN_IN_PARTY => Reward.Sabin,
            Events.CELES_IN_PARTY => Reward.Celes,
            Events.STRAGO_IN_PARTY => Reward.Strago,
            Events.RELM_IN_PARTY => Reward.Relm,
            Events.SETZER_IN_PARTY => Reward.Setzer,
            Events.MOG_IN_PARTY => Reward.Mog,
            Events.GAU_IN_PARTY => Reward.Gau,
            Events.GOGO_IN_PARTY => Reward.Gogo,
            Events.UMARO_IN_PARTY => Reward.Umaro,
            _ => null
        };

    public static CharacterEx? ToCharacter(this Reward? reward)
        => reward switch
        {
            Reward.Terra => CharacterEx.Terra,
            Reward.Locke => CharacterEx.Locke,
            Reward.Cyan => CharacterEx.Cyan,
            Reward.Shadow => CharacterEx.Shadow,
            Reward.Edgar => CharacterEx.Edgar,
            Reward.Sabin => CharacterEx.Sabin,
            Reward.Celes => CharacterEx.Celes,
            Reward.Strago => CharacterEx.Strago,
            Reward.Relm => CharacterEx.Relm,
            Reward.Setzer => CharacterEx.Setzer,
            Reward.Mog => CharacterEx.Mog,
            Reward.Gau => CharacterEx.Gau,
            Reward.Gogo => CharacterEx.Gogo,
            Reward.Umaro => CharacterEx.Umaro,
            _ => null
        };

    public static Reward? ToReward(this Esper esper)
        => esper switch
        {
            Esper.Ramuh => Reward.Ramuh,
            Esper.Ifrit => Reward.Ifrit,
            Esper.Shiva => Reward.Shiva,
            Esper.Siren => Reward.Siren,
            Esper.Terrato => Reward.Terrato,
            Esper.Shoat => Reward.Shoat,
            Esper.Maduin => Reward.Maduin,
            Esper.Bismark => Reward.Bismark,
            Esper.Stray => Reward.Stray,
            Esper.Palidor => Reward.Palidor,
            Esper.Tritoch => Reward.Tritoch,
            Esper.Odin => Reward.Odin,
            Esper.Raiden => Reward.Raiden,
            Esper.Bahamut => Reward.Bahamut,
            Esper.Alexandr => Reward.Alexandr,
            Esper.Crusader => Reward.Crusader,
            Esper.Ragnarok => Reward.Ragnarok,
            Esper.Kirin => Reward.Kirin,
            Esper.ZoneSeek => Reward.ZoneSeek,
            Esper.Carbunkl => Reward.Carbunkl,
            Esper.Phantom => Reward.Phantom,
            Esper.Sraphim => Reward.Sraphim,
            Esper.Golem => Reward.Golem,
            Esper.Unicorn => Reward.Unicorn,
            Esper.Fenrir => Reward.Fenrir,
            Esper.Starlet => Reward.Starlet,
            Esper.Phoenix => Reward.Phoenix,
            _ => null
        };

    public static Esper? ToEsper(this Reward? reward)
        => reward switch
        {
            Reward.Ramuh => Esper.Ramuh,
            Reward.Ifrit => Esper.Ifrit,
            Reward.Shiva => Esper.Shiva,
            Reward.Siren => Esper.Siren,
            Reward.Terrato => Esper.Terrato,
            Reward.Shoat => Esper.Shoat,
            Reward.Maduin => Esper.Maduin,
            Reward.Bismark => Esper.Bismark,
            Reward.Stray => Esper.Stray,
            Reward.Palidor => Esper.Palidor,
            Reward.Tritoch => Esper.Tritoch,
            Reward.Odin => Esper.Odin,
            Reward.Raiden => Esper.Raiden,
            Reward.Bahamut => Esper.Bahamut,
            Reward.Alexandr => Esper.Alexandr,
            Reward.Crusader => Esper.Crusader,
            Reward.Ragnarok => Esper.Ragnarok,
            Reward.Kirin => Esper.Kirin,
            Reward.ZoneSeek => Esper.ZoneSeek,
            Reward.Carbunkl => Esper.Carbunkl,
            Reward.Phantom => Esper.Phantom,
            Reward.Sraphim => Esper.Sraphim,
            Reward.Golem => Esper.Golem,
            Reward.Unicorn => Esper.Unicorn,
            Reward.Fenrir => Esper.Fenrir,
            Reward.Starlet => Esper.Starlet,
            Reward.Phoenix => Esper.Phoenix,
            _ => null
        };

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