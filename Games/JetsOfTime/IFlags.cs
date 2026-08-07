using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime;

public interface IFlags : INotifyPropertyChanged
{
    bool? AntiLife { get; }
    bool? BossRando { get; }
    bool? BossScaling { get; }
    bool? BossSightscope { get; }
    bool? BossSpotHp { get; }
    bool? BucketFragments { get; }
    bool? Chronosanity { get; }
    bool? DuplicateChararacters { get; }
    bool? DuplicateTechs { get; }
    Difficulty? EnemyDifficulty { get; }
    bool? EpochFail { get; }
    bool? FastPendant { get; }
    bool? FastTabs { get; }
    bool? FixGlitches { get; }
    string? FlagString { get; }
    bool? FreeMenuGlitch { get; }
    bool? GearRando { get; }
    bool? HealingItemRando { get; }
    Difficulty? ItemDifficulty { get; }
    bool? LockedCharacters { get; }
    GameMode? Mode { get; }
    ShopPrices? ShopPrices { get; }
    bool? StartersSufficient { get; }
    bool? TabTreasures { get; }
    bool? TackleEffects { get; }
    TechOrder? TechOrder { get; }
    bool? UnlockedMagic { get; }
    bool? VisibleHealth { get; }
    bool? ZealEnd { get; }

    bool? this[Flag flag] { get; }
}

public enum Flag
{
    AntiLife,
    BossRando,
    BossScaling,
    BossSightscope,
    BossSpotHp,
    BucketFragments,
    Chronosanity,
    DuplicateChararacters,
    DuplicateTechs,
    EpochFail,
    FastPendant,
    FastTabs,
    FixGlitches,
    FreeMenuGlitch,
    GearRando,
    HealingItemRando,
    LockedCharacters,
    StartersSufficient,
    TabTreasures,
    TackleEffects,
    UnlockedMagic,
    VisibleHealth,
    ZealEnd,
}