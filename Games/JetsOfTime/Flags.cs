using BizHawk.Common.ReflectionExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime;
public class Flags : IFlags
{
    private const string Mystery = "mystery";

    public event PropertyChangedEventHandler? PropertyChanged;

    public Flags()
    {

    }

    public bool? this[Flag flag] => flag switch
    {
        Flag.AntiLife => AntiLife,
        Flag.BossRando => BossRando,
        Flag.BossScaling => BossScaling,
        Flag.BossSightscope => BossSightscope,
        Flag.BossSpotHp => BossSpotHp,
        Flag.BucketFragments => BucketFragments,
        Flag.Chronosanity => Chronosanity,
        Flag.DuplicateChararacters => DuplicateChararacters,
        Flag.DuplicateTechs => DuplicateTechs,
        Flag.EpochFail => EpochFail,
        Flag.FastPendant => FastPendant,
        Flag.FastTabs => FastTabs,
        Flag.FixGlitches => FixGlitches,
        Flag.FreeMenuGlitch => FreeMenuGlitch,
        Flag.GearRando => GearRando,
        Flag.HealingItemRando => HealingItemRando,
        Flag.LockedCharacters => LockedCharacters,
        Flag.StartersSufficient => StartersSufficient,
        Flag.TabTreasures => TabTreasures,
        Flag.TackleEffects => TackleEffects,
        Flag.UnlockedMagic => UnlockedMagic,
        Flag.VisibleHealth => VisibleHealth,
        Flag.ZealEnd => ZealEnd,
        _ => null
    };

    public void Parse(string flagString)
    {
        FlagString = flagString;

        if (flagString.Equals(Mystery, StringComparison.InvariantCultureIgnoreCase))
            return;

        var parts = flagString.Split('.');

        if (parts.Length != 3)
            return;

        ParseMode(parts[0]);
        ParseDifficulty(parts[1]);
        ParseGameFlags(parts[2]);
    }

    private void ParseMode(string mode)
    {
        Mode = mode.ToLower() switch
        {
            "st" => GameMode.Standard,
            "lw" => GameMode.LostWorlds,
            "ia" => GameMode.IceAge,
            "loc" => GameMode.LegacyOfCyrus,
            "van" => GameMode.VanillaRando,
            _ => null
        };
    }

    private void ParseDifficulty(string mode)
    {
        static Difficulty? ToDifficulty(char c) => char.ToLower(c) switch
        {
            'e' => Difficulty.Easy,
            'n' => Difficulty.Normal,
            'h' => Difficulty.Hard,
            _ => Difficulty.Normal
        };

        if (mode.Length == 1)
        {
            ItemDifficulty = EnemyDifficulty = ToDifficulty(mode[0]);
        }
        else if (mode.Length == 2)
        {
            EnemyDifficulty = ToDifficulty(mode[0]);
            ItemDifficulty = ToDifficulty(mode[1]);
        }
    }

    private void ParseGameFlags(string gameFlags)
    {
        FixGlitches = false;
        BossScaling = false;
        ZealEnd = false;
        FastPendant = false;
        LockedCharacters = false;
        UnlockedMagic = false;
        Chronosanity = false;
        TabTreasures = false;
        BossRando = false;
        DuplicateChararacters = false;
        DuplicateTechs = false;
        VisibleHealth = false;
        FastTabs = false;
        BucketFragments = false;
        BossSightscope = false;
        AntiLife = false;
        TackleEffects = false;
        HealingItemRando = false;
        FreeMenuGlitch = false;
        GearRando = false;
        StartersSufficient = false;
        EpochFail = false;
        BossSpotHp = false;

        var flags = (List<string>)["g", "b", "ro", "z", "p", "c", "m", "cr", "tb", "dc", "h", "q", "ef", "k"];

        while (gameFlags.Length > 0)
        {
            bool flagFound = false;
            foreach (var flag in flags)
            {
                if (gameFlags.StartsWith(flag, StringComparison.OrdinalIgnoreCase))
                {
                    flagFound = true;
                    gameFlags = gameFlags[flag.Length..];

                    switch (flag)
                    {
                        case "g": FixGlitches = true; break;
                        case "b": BossScaling = true; break;
                        case "ro": BossRando = true; break;
                        case "z": ZealEnd = true; break;
                        case "p": FastPendant = true; break;
                        case "c": LockedCharacters = true; break;
                        case "m": UnlockedMagic = true; break;
                        case "cr": Chronosanity = true; break;
                        case "tb": TabTreasures = true; break;
                        case "dc": DuplicateChararacters = true; break;
                        case "h": HealingItemRando = true; break;
                        case "q": GearRando = true; break;
                        case "ef": EpochFail = true; break;
                        case "k": BucketFragments = true; break;
                    }

                    break;
                }
            }

            if (!flagFound)
                break;
        }

        ShopPrices = JetsOfTime.ShopPrices.Normal;
        TechOrder = JetsOfTime.TechOrder.Normal;

        while (gameFlags.Length > 0)
        {
            switch (gameFlags)
            {
                case string s when s is ['s', 'p', 'f', ..]:
                    gameFlags = gameFlags[3..];
                    ShopPrices = JetsOfTime.ShopPrices.Free;
                    break;
                case string s when s is ['s', 'p', 'm', ..]:
                    gameFlags = gameFlags[3..];
                    ShopPrices = JetsOfTime.ShopPrices.MostlyRandom;
                    break;
                case string s when s is ['s', 'p', 'r', ..]:
                    gameFlags = gameFlags[3..];
                    ShopPrices = JetsOfTime.ShopPrices.FullyRandom;
                    break;
                case string s when s is ['t', 'e', 'x', ..]:
                    gameFlags = gameFlags[3..];
                    TechOrder = JetsOfTime.TechOrder.BalancedRandom;
                    break;
                case string s when s is ['t', 'e', ..]:
                    gameFlags = gameFlags[2..];
                    TechOrder = JetsOfTime.TechOrder.FullRandom;
                    break;
                default:
                    return;
            }
        }

        PropertyChanged?.Invoke(this, new(""));
    }

    public string? FlagString { get; private set; }

    public GameMode? Mode { get; private set; }
    public Difficulty? ItemDifficulty { get; private set; }
    public Difficulty? EnemyDifficulty { get; private set; }
    public TechOrder? TechOrder { get; private set; }
    public ShopPrices? ShopPrices { get; private set; }

    public bool? FixGlitches { get; private set; }
    public bool? BossScaling { get; private set; }
    public bool? ZealEnd { get; private set; }
    public bool? FastPendant { get; private set; }
    public bool? LockedCharacters { get; private set; }
    public bool? UnlockedMagic { get; private set; }
    public bool? Chronosanity { get; private set; }
    public bool? TabTreasures { get; private set; }
    public bool? BossRando { get; private set; }
    public bool? DuplicateChararacters { get; private set; }
    public bool? DuplicateTechs { get; private set; }
    public bool? VisibleHealth { get; private set; }
    public bool? FastTabs { get; private set; }
    public bool? BucketFragments { get; private set; }
    public bool? BossSightscope { get; private set; }
    public bool? AntiLife { get; private set; }
    public bool? TackleEffects { get; private set; }
    public bool? HealingItemRando { get; private set; }
    public bool? FreeMenuGlitch { get; private set; }
    public bool? GearRando { get; private set; }
    public bool? StartersSufficient { get; private set; }
    public bool? EpochFail { get; private set; }
    public bool? BossSpotHp { get; private set; }

    public override string ToString()
    {
        return FlagString ?? Mystery;
    }
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public enum TechOrder
{
    Normal,
    FullRandom,
    BalancedRandom
}

public enum ShopPrices
{
    Normal,
    MostlyRandom,
    FullyRandom,
    Free
}

public enum GameMode
{
    Standard,
    LostWorlds,
    IceAge,
    LegacyOfCyrus,
    VanillaRando
}