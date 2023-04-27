using BizHawk.FreeEnterprise.Companion.Extensions;
using System;

namespace BizHawk.FreeEnterprise.Companion.FlagSet._4._5._0
{
    public enum Objective
    {
        None,
        GetCecil = 1,
        GetKain,
        GetRydia,
        GetTellah,
        GetEdward,
        GetRosa,
        GetYang,
        GetPalom,
        GetPorom,
        GetCid,
        GetEdge,
        GetFuSoYa,
        DefeatDMist,
        DefeatOfficer,
        DefeatOctomamm,
        DefeatAntlion,
        DefeatWaterHag,
        DefeatMomBomb,
        DefeatGauntlet,
        DefeatMilon,
        DefeatMilonZ,
        DefeatMirrorCecil,
        DefeatGuards,
        DefeatKarate,
        DefeatBaigan,
        DefeatKainazzo,
        DefeatDarkElf,
        DefeatMagus,
        DefeatValvalis,
        DefeatCalbrena,
        DefeatGolbez,
        DefeatLugae,
        DefeatDarkImps,
        DefeatKingQueen,
        DefeatRubicant,
        DefeatEvilWall,
        DefeatAsura,
        DefeatLeviatan,
        DefeatOdin,
        DefeatBahamut,
        DefeatElements,
        DefeatCpu,
        DefeatPaledim,
        DefeatWyvern,
        DefeatPlague,
        DefeatDlunar,
        DefeatOgopogo,
        QuestMistcave,
        QuestWaterfall,
        QuestAntlionNest,
        QuestHobs,
        QuestFabul,
        QuestOrdeals,
        QuestBaronInn,
        QuestBaronCastle,
        QuestMagnes,
        QuestZot,
        QuestDwarfCastle,
        QuestLowerBabil,
        QuestFalcon,
        QuestSealedCave,
        QuestMonsterQueen,
        QuestMonsterKing,
        QuestBaronBasement,
        QuestGiant,
        QuestCaveBahamut,
        QuestMurasameAltar,
        QuestCrystalAltar,
        QuestWhiteAltar,
        QuestRibbonAltar,
        QuestMasamuneAltar,
        QuestBurnMist,
        QuestCureFever,
        QuestUnlockSewer,
        QuestMusic,
        QuestToroiaTreasury,
        QuestMagma,
        QuestSupercannon,
        QuestUnlockSealedCave,
        QuestBigWHale,
        QuestTradeRat,
        QuestForge,
        QuestWakeYang,
        QuestTradePan,
        QuestTradePink,
        QuestPass
    }

    public class FlagSet : BizHawk.FreeEnterprise.Companion.FlagSet.FlagSet
    {
        public FlagSet(byte[] data) : base(data)
        {
            Objectives = new Objectives(this);
            KeyItems = new KeyItems(this);
            Pass = new Pass(this);
            Characters = new Characters(this);
            Treasures = new Treasures(this);
            Shops = new Shops(this);
            Bosses = new Bosses(this);
            Encounters = new Encounters(this);
            Glitches = new Glitches(this);
            Other = new Other(this);
        }

        protected override int MinDataLength => 33;

        public Objectives Objectives { get; }
        public KeyItems KeyItems { get; }
        public Pass Pass { get; }
        public Characters Characters { get; }
        public Treasures Treasures { get; }
        public Shops Shops { get; }
        public Bosses Bosses { get; }
        public Encounters Encounters { get; }
        public Glitches Glitches { get; }
        public Other Other { get; }
        public override int MaxParty => Characters.PartySize;
        public override int RequriedObjectiveCount => Objectives.NumRequiredObjectives;
        public override bool OWinGame => Objectives.WinGame;
        public override bool OWinCrystal => Objectives.WinCrystal;
        public override bool No10KeyItemBonus => Other.No10KeyItemBonus;

        public override bool CHero => Characters.Hero;
        public override bool VanillaAgility => Other.VanillaAgility;

        public override bool? CanHaveKeyItem(KeyItemLocationType location)
        {
            switch (location)
            {
                case KeyItemLocationType.StartingItem: return KeyItems.MainLocations;
                case KeyItemLocationType.AntlionNest: return KeyItems.MainLocations;
                case KeyItemLocationType.DefendingFabul: return KeyItems.MainLocations;
                case KeyItemLocationType.MtOrdeals: return KeyItems.MainLocations;
                case KeyItemLocationType.BaronInn: return KeyItems.MainLocations;
                case KeyItemLocationType.BaronCastle: return KeyItems.MainLocations;
                case KeyItemLocationType.EdwardInToroia: return !KeyItems.NoFreeItem;
                case KeyItemLocationType.CaveMagnes: return KeyItems.MainLocations;
                case KeyItemLocationType.TowerOfZot: return KeyItems.MainLocations;
                case KeyItemLocationType.LowerBabIlBoss: return KeyItems.MainLocations;
                case KeyItemLocationType.SuperCannon: return KeyItems.MainLocations;
                case KeyItemLocationType.Luca: return KeyItems.MainLocations;
                case KeyItemLocationType.SealedCave: return KeyItems.MainLocations;
                case KeyItemLocationType.FeymarchChest: return KeyItems.MainLocations;
                case KeyItemLocationType.RatTailTrade: return KeyItems.MainLocations;
                case KeyItemLocationType.Shelia1: return KeyItems.MainLocations;
                case KeyItemLocationType.Shelia2: return KeyItems.MainLocations;
                case KeyItemLocationType.FeymarchQueen: return KeyItems.SummonLocations;
                case KeyItemLocationType.FeymarchKing: return KeyItems.SummonLocations;
                case KeyItemLocationType.OdinThrone: return KeyItems.SummonLocations;
                case KeyItemLocationType.FromTheSylphs: return KeyItems.SummonLocations;
                case KeyItemLocationType.CaveBahamut: return KeyItems.SummonLocations;
                case KeyItemLocationType.MurasameAltar: return KeyItems.MoonLocations;
                case KeyItemLocationType.CrystalSwordAltar: return KeyItems.MoonLocations;
                case KeyItemLocationType.WhiteSpearAltar: return KeyItems.MoonLocations;
                case KeyItemLocationType.RibbonChest1: return KeyItems.MoonLocations;
                case KeyItemLocationType.RibbonChest2: return KeyItems.MoonLocations;
                case KeyItemLocationType.MasamuneAltar: return KeyItems.MoonLocations;
                case KeyItemLocationType.TowerOfZotTrappedChest: return KeyItems.TrapChests;
                case KeyItemLocationType.EblanTrappedChest1: return KeyItems.TrapChests;
                case KeyItemLocationType.EblanTrappedChest2: return KeyItems.TrapChests;
                case KeyItemLocationType.EblanTrappedChest3: return KeyItems.TrapChests;
                case KeyItemLocationType.LowerBabIlTrappedChest1: return KeyItems.TrapChests;
                case KeyItemLocationType.LowerBabIlTrappedChest2: return KeyItems.TrapChests;
                case KeyItemLocationType.LowerBabIlTrappedChest3: return KeyItems.TrapChests;
                case KeyItemLocationType.LowerBabIlTrappedChest4: return KeyItems.TrapChests;
                case KeyItemLocationType.CaveEblanTrappedChest: return KeyItems.TrapChests;
                case KeyItemLocationType.UpperBabIlTrappedChest: return KeyItems.TrapChests;
                case KeyItemLocationType.CaveOfSummonsTrappedChest: return KeyItems.TrapChests;
                case KeyItemLocationType.SylphCaveTrappedChest1: return KeyItems.TrapChests;
                case KeyItemLocationType.SylphCaveTrappedChest2: return KeyItems.TrapChests;
                case KeyItemLocationType.SylphCaveTrappedChest3: return KeyItems.TrapChests;
                case KeyItemLocationType.SylphCaveTrappedChest4: return KeyItems.TrapChests;
                case KeyItemLocationType.SylphCaveTrappedChest5: return KeyItems.TrapChests;
                case KeyItemLocationType.SylphCaveTrappedChest6: return KeyItems.TrapChests;
                case KeyItemLocationType.SylphCaveTrappedChest7: return KeyItems.TrapChests;
                case KeyItemLocationType.GiantOfBabIlTrappedChest: return KeyItems.TrapChests;
                case KeyItemLocationType.LunarPathTrappedChest: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest1: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest2: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest3: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest4: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest5: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest6: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest7: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest8: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.LunarCoreTrappedChest9: return KeyItems.TrapChests && (KeyItems.MoonLocations || KeyItems.Unsafe);
                case KeyItemLocationType.RydiasMom: return KeyItems.NoFreeItem;
                case KeyItemLocationType.FallenGolbez:
                case KeyItemLocationType.ObjectiveCompletion:
                default:
                    return false;
            }
        }

        public override bool? CanHaveCharacter(CharacterLocationType location)
        {
            switch (location)
            {
                case CharacterLocationType.StartingPartner: return true;
                case CharacterLocationType.KaipoInn: return !Characters.NoEarned;
                case CharacterLocationType.WateryPass: return !Characters.NoFree;
                case CharacterLocationType.Damcyan: return !Characters.NoFree;
                case CharacterLocationType.KaipoInfirmary: return !Characters.NoEarned;
                case CharacterLocationType.MtHobbs: return !Characters.NoEarned;
                case CharacterLocationType.Mysidia1: return !Characters.NoFree;
                case CharacterLocationType.Mysidia2: return !Characters.NoFree;
                case CharacterLocationType.MtOrdeals: return !Characters.NoFree;
                case CharacterLocationType.BaronInn: return !Characters.NoEarned;
                case CharacterLocationType.BaronCastle: return !Characters.NoEarned;
                case CharacterLocationType.Zot1: return !Characters.NoEarned;
                case CharacterLocationType.Zot2: return !Characters.NoEarned;
                case CharacterLocationType.DwarfCastle: return !Characters.NoEarned;
                case CharacterLocationType.EblanCave: return !Characters.NoEarned;
                case CharacterLocationType.Moon: return !Characters.NoEarned;
                case CharacterLocationType.Giant: return !Characters.NoEarned && !Objectives.ClassicGiant;
                default:
                   return false;
            }
        }
    }

    public class Objectives
    {
        private readonly FlagSet parent;

        public Objectives(FlagSet parent)
        {
            this.parent = parent;
        }

        public bool ClassicForge => parent.Data.Read<bool>(0, 1);
        public bool ClassicGiant => parent.Data.Read<bool>(1, 1);
        public bool Fiends => parent.Data.Read<bool>(2, 1);
        public bool DKMatter => parent.Data.Read<bool>(3, 1);

        public Objective Objective1 => parent.Data.Read<Objective>(4, 7);
        public Objective Objective2 => parent.Data.Read<Objective>(11, 7);
        public Objective Objective3 => parent.Data.Read<Objective>(18, 7);
        public Objective Objective4 => parent.Data.Read<Objective>(25, 7);
        public Objective Objective5 => parent.Data.Read<Objective>(32, 7);
        public Objective Objective6 => parent.Data.Read<Objective>(39, 7);
        public Objective Objective7 => parent.Data.Read<Objective>(46, 7);
        public Objective Objective8 => parent.Data.Read<Objective>(53, 7);

        public byte NumRandomObjectives => parent.Data.Read<byte>(60, 4);

        public bool AllowRandomQuests => parent.Data.Read<byte>(64, 2) == 1;
        public bool AllowRandomToughQuests => parent.Data.Read<byte>(64, 2) == 2;
        public bool AllowRandomBossHunts => parent.Data.Read<bool>(66);
        public bool AllowRandomCharacterHunts => parent.Data.Read<bool>(67);
        public bool AllRequired => parent.Data.Read<byte>(68, 4) == 1;
        public byte NumRequiredObjectives => (byte)Math.Max(0, parent.Data.Read<byte>(68, 4) - 1);
        public bool WinGame => parent.Data.Read<bool>(72);
        public bool WinCrystal => parent.Data.Read<bool>(73);
    }

    public class KeyItems
    {
        private readonly FlagSet parent;

        public KeyItems(FlagSet parent)
        {
            this.parent = parent;
        }

        public bool MainLocations => parent.Data.Read<bool>(74);
        public bool SummonLocations => parent.Data.Read<bool>(75);
        public bool MoonLocations => parent.Data.Read<bool>(76);
        public bool TrapChests => parent.Data.Read<bool>(77);
        public bool NoFreeItem => parent.Data.Read<bool>(78);
        public bool Unsafe => parent.Data.Read<bool>(79);
        public bool ForcedMagma => parent.Data.Read<bool>(80);
        public bool ForcedHook => parent.Data.Read<bool>(81);
    }

    public class Pass
    {
        private readonly FlagSet parent;

        public Pass(FlagSet parent)
        {
            this.parent = parent;
        }

        public bool InShops => parent.Data.Read<bool>(82);
        public bool InKeyItemLocations => parent.Data.Read<bool>(83);
        public bool InChests => parent.Data.Read<bool>(84);
    }


    public class Characters
    {
        private readonly FlagSet parent;

        public Characters(FlagSet parent)
        {
            this.parent = parent;
        }

        public bool Standard => parent.Data.Read<bool>(85);
        public bool Relaxed => parent.Data.Read<bool>(83);
        public bool NoFree => parent.Data.Read<bool>(87);
        public bool NoEarned => parent.Data.Read<bool>(88);
        public bool Maybe => parent.Data.Read<bool>(89);
        public byte DistinctCharacters => (byte)(parent.Data.Read<byte>(90, 4) - 1);
        public bool IncludeCecilStart => parent.Data.Read<bool>(94);
        public bool IncludeKainStart => parent.Data.Read<bool>(95);
        public bool IncludeRydiaStart => parent.Data.Read<bool>(96);
        public bool IncludeTellahStart => parent.Data.Read<bool>(97);
        public bool IncludeEdwardStart => parent.Data.Read<bool>(98);
        public bool IncludeRoseStart => parent.Data.Read<bool>(99);
        public bool IncludeYangStart => parent.Data.Read<bool>(100);
        public bool IncludePalomStart => parent.Data.Read<bool>(101);
        public bool IncludePoromStart => parent.Data.Read<bool>(102);
        public bool IncludeCidStart => parent.Data.Read<bool>(103);
        public bool IncludeEdgeStart => parent.Data.Read<bool>(104);
        public bool IncludeFuSoYaStart => parent.Data.Read<bool>(105);
        public bool IncludeAnyStart => parent.Data.Read<bool>(106);
        public bool ExcludeCecilStart => parent.Data.Read<bool>(107);
        public bool ExcludeKainStart => parent.Data.Read<bool>(108);
        public bool ExcludeRydiaStart => parent.Data.Read<bool>(109);
        public bool ExcludeTellahStart => parent.Data.Read<bool>(110);
        public bool ExcludeEdwardStart => parent.Data.Read<bool>(111);
        public bool ExcludeRoseStart => parent.Data.Read<bool>(112);
        public bool ExcludeYangStart => parent.Data.Read<bool>(113);
        public bool ExcludePalomStart => parent.Data.Read<bool>(114);
        public bool ExcludePoromStart => parent.Data.Read<bool>(115);
        public bool ExcludeCidStart => parent.Data.Read<bool>(116);
        public bool ExcludeEdgeStart => parent.Data.Read<bool>(117);
        public bool ExcludeFuSoYaStart => parent.Data.Read<bool>(118);
        public bool OnlyCecil => parent.Data.Read<bool>(119);
        public bool OnlyKain => parent.Data.Read<bool>(120);
        public bool OnlyRydia => parent.Data.Read<bool>(121);
        public bool OnlyTellah => parent.Data.Read<bool>(122);
        public bool OnlyEdward => parent.Data.Read<bool>(123);
        public bool OnlyRose => parent.Data.Read<bool>(124);
        public bool OnlyYang => parent.Data.Read<bool>(125);
        public bool OnlyPalom => parent.Data.Read<bool>(126);
        public bool OnlyPorom => parent.Data.Read<bool>(127);
        public bool OnlyCid => parent.Data.Read<bool>(128);
        public bool OnlyEdge => parent.Data.Read<bool>(129);
        public bool OnlyFuSoYa => parent.Data.Read<bool>(130);
        public bool NoCecil => parent.Data.Read<bool>(131);
        public bool NoKain => parent.Data.Read<bool>(132);
        public bool NoRydia => parent.Data.Read<bool>(133);
        public bool NoTellah => parent.Data.Read<bool>(134);
        public bool NoEdward => parent.Data.Read<bool>(135);
        public bool NoRose => parent.Data.Read<bool>(136);
        public bool NoYang => parent.Data.Read<bool>(137);
        public bool NoPalom => parent.Data.Read<bool>(138);
        public bool NoPorom => parent.Data.Read<bool>(139);
        public bool NoCid => parent.Data.Read<bool>(140);
        public bool NoEdge => parent.Data.Read<bool>(141);
        public bool NoFuSoYa => parent.Data.Read<bool>(142);
        public bool RestrictedCecil => parent.Data.Read<bool>(143);
        public bool RestrictedKain => parent.Data.Read<bool>(144);
        public bool RestrictedRydia => parent.Data.Read<bool>(145);
        public bool RestrictedTellah => parent.Data.Read<bool>(146);
        public bool RestrictedEdward => parent.Data.Read<bool>(147);
        public bool RestrictedRose => parent.Data.Read<bool>(148);
        public bool RestrictedYang => parent.Data.Read<bool>(149);
        public bool RestrictedPalom => parent.Data.Read<bool>(150);
        public bool RestrictedPorom => parent.Data.Read<bool>(151);
        public bool RestrictedCid => parent.Data.Read<bool>(152);
        public bool RestrictedEdge => parent.Data.Read<bool>(153);
        public bool RestrictedFuSoYa => parent.Data.Read<bool>(154);
        public bool JSpells => parent.Data.Read<bool>(155);
        public bool JAbilities => parent.Data.Read<bool>(156);
        public bool Nekkie => parent.Data.Read<bool>(157);
        public bool NoDupes => parent.Data.Read<bool>(158);
        public byte PartySize
        {
            get
            {
                var size = parent.Data.Read<byte>(159, 3);
                return size == 0 ? (byte)5 : size;
            }
        }
        public bool Bye => parent.Data.Read<bool>(162);
        public bool Permajoin => parent.Data.Read<bool>(163);
        public bool PermaDeath => parent.Data.Read<bool>(164);
        public bool PermaDeader => parent.Data.Read<bool>(165);
        public bool Hero => parent.Data.Read<bool>(166);
    }

    public enum ChestContents
    {
        Vanilla,
        Shuffle,
        Standard,
        Pro,
        Wild,
        Wildish,
        Empty
    }

    public class Treasures
    {
        private readonly FlagSet parent;

        public Treasures(FlagSet parent)
        {
            this.parent = parent;
        }

        public ChestContents ChestContents => parent.Data.Read<ChestContents>(167, 3);
        public int Sparse => parent.Data.Read<byte>(170, 4) * 10;
        public bool NoJ => parent.Data.Read<bool>(174);
        public int MaxTier => parent.Data.Read<byte>(175, 3) == 0 ? 8 : parent.Data.Read<byte>(176, 3) + 2;
        public bool MoneyOnly => parent.Data.Read<bool>(178);
        public bool Junk => parent.Data.Read<bool>(179);
    }

    public enum ShopContents
    {
        Vanilla,
        Shuffle,
        Standard,
        Pro,
        Wild,
        Cabins,
        Empty,
    }

    public class Shops
    {
        private readonly FlagSet parent;

        public Shops(FlagSet parent)
        {
            this.parent = parent;
        }

        public ShopContents ShopContents => parent.Data.Read<ShopContents>(180, 3);
        public bool Free => parent.Data.Read<bool>(183);
        public bool SellQuarter => parent.Data.Read<bool>(184);
        public bool SellZero => parent.Data.Read<bool>(185);
        public bool NoJ => parent.Data.Read<bool>(186);
        public bool NoApples => parent.Data.Read<bool>(187);
        public bool NoSirens => parent.Data.Read<bool>(188);
        public bool NoLife => parent.Data.Read<bool>(189);
        public bool Unsafe => parent.Data.Read<bool>(190);
    }

    public class Bosses
    {
        private readonly FlagSet parent;

        public Bosses(FlagSet parent)
        {
            this.parent = parent;
        }

        public bool Standard => parent.Data.Read<bool>(191);
        public bool NoFree => parent.Data.Read<bool>(192);
        public bool Unsafe => parent.Data.Read<bool>(193);
        public bool AltGuantlet => parent.Data.Read<bool>(194);
        public bool WhyBurn => parent.Data.Read<bool>(195);
        public bool WhichBurn => parent.Data.Read<bool>(196);
    }

    public enum EncouterRate
    {
        Vanilla,
        Toggle,
        Reduced,
        None
    }

    public class Encounters
    {
        private readonly FlagSet parent;

        public Encounters(FlagSet parent)
        {
            this.parent = parent;
        }

        public EncouterRate EncouterRate => parent.Data.Read<EncouterRate>(197, 2);
        public bool KeepDoors => parent.Data.Read<bool>(199);
        public bool KeepBehemoths => parent.Data.Read<bool>(200);
        public bool Danger => parent.Data.Read<bool>(201);
        public bool CantRun => parent.Data.Read<bool>(202);
        public bool NoExperience => parent.Data.Read<bool>(203);
        public bool NoJDrops => parent.Data.Read<bool>(204);
        public bool NoSirens => parent.Data.Read<bool>(205);
    }

    public class Glitches
    {
        private readonly FlagSet parent;

        public Glitches(FlagSet parent)
        {
            this.parent = parent;
        }

        public bool Dupe => parent.Data.Read<bool>(206);
        public bool Mp => parent.Data.Read<bool>(207);
        public bool Warp => parent.Data.Read<bool>(208);
        public bool Life => parent.Data.Read<bool>(209);
        public bool Sylph => parent.Data.Read<bool>(210);
        public bool BackRow => parent.Data.Read<bool>(211);
        public bool _64Floor => parent.Data.Read<bool>(212);
    }

    public enum KitType
    {
        None,
        Basic,
        Better,
        Loaded,
        Cata,
        Freedom,
        Cid,
        Yang,
        Money,
        GrabBag,
        Trap,
        Archer,
        Fabul,
        Castlevania,
        Summon,
        NotDeme,
        Meme,
        Defense,
        Mist,
        Mysidia,
        Baron,
        Dwarf,
        Eblan,
        _99,
        Green,
        Random
    }

    public enum Wacky
    {
        None,
        Random,
        Musical,
        Bodyguard,
        FistFight,
        Omnidextrous,
        BiggerMagnet,
        SixLeggedRace,
        FloorIsLava,
        NeatFreak,
        TimeIsMoney,
        NightMode,
        MysteryJuice,
        Misspelled,
        EnemyUnknown,
        Kleptomania,
        Darts,
        Unstackable,
        MenArePigs,
        SkyWarriors,
        Zombies,
        Afflicted,
        Batman,
        BattleScars,
        ImaginaryNumbers,
        TellahManeuver,
        _3Point,
        FriendlyFire,
        PayableGolbez,
        GottaGoFast,
        WorthFighting,
        SaveUsBigChocobo,
        IsThisRandomized,
        ForwardIsBack
    }

    public class Other
    {
        private readonly FlagSet parent;

        public Other(FlagSet parent)
        {
            this.parent = parent;
        }

        public KitType Kit1 => parent.Data.Read<KitType>(213, 5);
        public KitType Kit2 => parent.Data.Read<KitType>(218, 5);
        public KitType Kit3 => parent.Data.Read<KitType>(223, 5);

        public bool NoAdamantArmor => parent.Data.Read<bool>(228);
        public bool NoCursedRing => parent.Data.Read<bool>(229);
        public bool Spoon => parent.Data.Read<bool>(230);
        public bool SuperSmith => parent.Data.Read<bool>(231);
        public bool AltSmit => parent.Data.Read<bool>(232);
        public bool SplitExperience => parent.Data.Read<bool>(233);
        public bool NoSlingshot => parent.Data.Read<bool>(234);
        public bool No10KeyItemBonus => parent.Data.Read<bool>(235);
        public bool VanillaFuSoYa => parent.Data.Read<bool>(236);
        public bool VanillaAgility => parent.Data.Read<bool>(237);
        public bool VanillaHobs => parent.Data.Read<bool>(238);
        public bool VanillaGrowUp => parent.Data.Read<bool>(239);
        public bool VanillaFashion => parent.Data.Read<bool>(240);
        public bool VanillaTraps => parent.Data.Read<bool>(241);
        public bool VanillaGiant => parent.Data.Read<bool>(242);
        public bool VanillaZ => parent.Data.Read<bool>(243);
        public bool Vintage => parent.Data.Read<bool>(244);
        public bool PushBToJump => parent.Data.Read<bool>(245);
        public Wacky Wacky => parent.Data.Read<Wacky>(246, 6);

        //TODO Spoil Flags
    }
}
