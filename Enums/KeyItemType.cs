using System;

namespace BizHawk.FreeEnterprise.Companion
{
    [Flags]
    public enum KeyItemType : uint
    {
        Package = 1,
        SandRuby = 2,
        LegendSword = 4,
        BaronKey = 8,
        TwinHarp = 16,
        EarthCrystal = 32,
        MagmaKey = 64,
        TowerKey = 128,
        Hook = 256,
        LucaKey = 512,
        DarknessCrystal = 1024,
        RatTail = 2048,
        Adamant = 4096,
        Pan = 8192,
        Spoon = 16384,
        PinkTail = 32768,
        Crystal = 65536,
        Pass = 131072, 
    }
}
