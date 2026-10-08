namespace FF.Rando.Companion.Games.WorldsCollide.Enums;

public static class EnumExtensions
{
    public static Reward? ToReward(this EventType @event)
        => @event switch
        {
            EventType.TERRA_IN_PARTY => Reward.Terra,
            EventType.LOCKE_IN_PARTY => Reward.Locke,
            EventType.CYAN_IN_PARTY => Reward.Cyan,
            EventType.SHADOW_IN_PARTY => Reward.Shadow,
            EventType.EDGAR_IN_PARTY => Reward.Edgar,
            EventType.SABIN_IN_PARTY => Reward.Sabin,
            EventType.CELES_IN_PARTY => Reward.Celes,
            EventType.STRAGO_IN_PARTY => Reward.Strago,
            EventType.RELM_IN_PARTY => Reward.Relm,
            EventType.SETZER_IN_PARTY => Reward.Setzer,
            EventType.MOG_IN_PARTY => Reward.Mog,
            EventType.GAU_IN_PARTY => Reward.Gau,
            EventType.GOGO_IN_PARTY => Reward.Gogo,
            EventType.UMARO_IN_PARTY => Reward.Umaro,
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

    public static bool IsCheck(this EventType eventBitIndex)
        => eventBitIndex switch
        {
            EventType.GOT_RAIDEN => true,
            EventType.NAMED_GAU => true,
            EventType.DEFEATED_FLAME_EATER => true,
            EventType.FINISHED_COLLAPSING_HOUSE => true,
            EventType.DEFEATED_DULLAHAN => true,
            EventType.FINISHED_DOMA_WOB => true,
            EventType.DEFEATED_STOOGES => true,
            EventType.FINISHED_DOMA_WOR => true,
            EventType.GOT_ALEXANDR => true,
            EventType.DEFEATED_HIDON => true,
            EventType.DEFEATED_ULTROS_ESPER_MOUNTAIN => true,
            EventType.RECRUITED_STRAGO_FANATICS_TOWER => true,
            EventType.DEFEATED_MAGIMASTER => true,
            EventType.NAMED_EDGAR => true,
            EventType.DEFEATED_TENTACLES_FIGARO => true,
            EventType.RECRUITED_SHADOW_FLOATING_CONTINENT => true,
            EventType.DEFEATED_ATMAWEAPON => true,
            EventType.FINISHED_FLOATING_CONTINENT => true,
            EventType.RECRUITED_SHADOW_GAU_FATHER_HOUSE => true,
            EventType.FINISHED_IMPERIAL_CAMP => true,
            EventType.DEFEATED_ATMA => true,
            EventType.RECRUITED_SHADOW_KOHLINGEN => true,
            EventType.RODE_RAFT_LETE_RIVER => true,
            EventType.CHASING_LONE_WOLF7 => true,
            EventType.GOT_BOTH_REWARDS_LONE_WOLF => true,
            EventType.GOT_IFRIT_SHIVA => true,
            EventType.DEFEATED_NUMBER_024 => true,
            EventType.DEFEATED_CRANES => true,
            EventType.RECRUITED_TERRA_MOBLIZ => true,
            EventType.COMPLETED_MOOGLE_DEFENSE => true,
            EventType.DEFEATED_VARGAS => true,
            EventType.FINISHED_MT_ZOZO => true,
            EventType.FINISHED_NARSHE_BATTLE => true,
            EventType.GOT_RAGNAROK => true,
            EventType.GOT_BOTH_REWARDS_WEAPON_SHOP => true,
            EventType.FINISHED_OPERA_DISRUPTION => true,
            EventType.DEFEATED_CHADARNOOK => true,
            EventType.GOT_PHANTOM_TRAIN_REWARD => true,
            EventType.RECRUITED_LOCKE_PHOENIX_CAVE => true,
            EventType.BLOCK_SEALED_GATE => true,
            EventType.DEFEATED_DOOM_GAZE => true,
            EventType.GOT_SERPENT_TRENCH_REWARD => true,
            EventType.FREED_CELES => true,
            EventType.DEFEATED_TUNNEL_ARMOR => true,
            EventType.GOT_TRITOCH => true,
            EventType.BOUGHT_ESPER_TZEN => true,
            EventType.RECRUITED_UMARO_WOR => true,
            EventType.VELDT_REWARD_OBTAINED => true,
            EventType.DEFEATED_SR_BEHEMOTH => true,
            EventType.DEFEATED_WHELK => true,
            EventType.RECRUITED_GOGO_WOR => true,
            EventType.GOT_ZOZO_REWARD => true,
            EventType.AUCTION_BOUGHT_ESPER1 => true,
            EventType.AUCTION_BOUGHT_ESPER2 => true,
            _ => false
        };

    public static bool IsCharacter(this EventType eventBitIndex)
        => eventBitIndex switch
        {
            EventType.TERRA_IN_PARTY => true,
            EventType.LOCKE_IN_PARTY => true,
            EventType.CYAN_IN_PARTY => true,
            EventType.SHADOW_IN_PARTY => true,
            EventType.EDGAR_IN_PARTY => true,
            EventType.SABIN_IN_PARTY => true,
            EventType.CELES_IN_PARTY => true,
            EventType.STRAGO_IN_PARTY => true,
            EventType.RELM_IN_PARTY => true,
            EventType.SETZER_IN_PARTY => true,
            EventType.MOG_IN_PARTY => true,
            EventType.GAU_IN_PARTY => true,
            EventType.GOGO_IN_PARTY => true,
            EventType.UMARO_IN_PARTY => true,
            _ => false
        };

    public static bool IsDragonLocation(this EventType eventBitIndex)
        => eventBitIndex switch
        {
            EventType.DEFEATED_ANCIENT_CASTLE_DRAGON => true,
            EventType.DEFEATED_FANATICS_TOWER_DRAGON => true,
            EventType.DEFEATED_KEFKA_TOWER_DRAGON_G => true, 
            EventType.DEFEATED_KEFKA_TOWER_DRAGON_S => true,
            EventType.DEFEATED_MT_ZOZO_DRAGON => true,
            EventType.DEFEATED_NARSHE_DRAGON => true,
            EventType.DEFEATED_OPERA_HOUSE_DRAGON => true,
            EventType.DEFEATED_PHOENIX_CAVE_DRAGON => true,
            _ => false,
        };

    public static bool IsStatue(this EventType eventBitIndex)
        => eventBitIndex switch
        {
            EventType.GODDESS_STATUE_KEFKA_TOWER => true,
            EventType.DOOM_STATUE_KEFKA_TOWER => true,
            EventType.POLTRGEIST_STATUE_KEFKA_TOWER => true,
            _ => false
        };
}