namespace FF.Rando.Companion.FreeEnterprise.GaleswiftFork;

public enum ObjectiveXpBonus : byte
{
    None = 0,
    _5Percent = 3,
    _10Percent = 2,
    _25Percent = 1,
    Split = 4,
}

public enum KeyItemCheckXpBonus : byte
{
    None = 0,
    _2Percent = 3,
    _5Percent = 2,
    _10Percent = 1,
    Split = 4,
}

public enum KeyItemZonkXpBonus : byte
{
    None = 0,
    _2Percent = 3,
    _5Percent = 2,
    _10Percent = 1,
}

public enum MiabXpBonus : byte
{
    None = 0,
    _100Percent = 1,
    _50Percent = 2,
}

public enum MoonXpBonus : byte
{
    None = 0,
    _200Percent = 1,
    _100Percent = 2,
}

public enum KNoFreeMode : byte
{
    Disabled = 0,
    Standard = 1,
    DwarfCastle = 2,
    Package = 3
}