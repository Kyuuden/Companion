using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;
using HorizontalAlignment = FF.Rando.Companion.Rendering.HorizontalAlignment;
using VerticalAlignment = FF.Rando.Companion.Rendering.VerticalAlignment;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

internal static class DefaultSpriteSets
{
    private static readonly SpriteSetDefinition _vanillaBosses = new()
    {
        WhelkGate = new SpriteDefinition
        {
            Source = SpriteSource.Boss,
            Id = (int)Boss.Whelk,
            Transforms =
            [
                new Pad { Width = 96, Height = 64, HorizontalAlignment = HorizontalAlignment.Left },
                new Overlay
                {
                    X = 48,
                    Y = 24,
                    OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.WhelkHead }
                },
                new Crop { X = 24, Y = 8, Width = 56, Height = 56 }
            ]
        },
        LeteRiver = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.UltrosLeteRiver },
        SealedGate = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Maduin, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        ZozoTower = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Ramuh, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        MoblizAttack = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Phunbaba1 },
        SouthFigaroCave = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.TunnelArmr, Transforms = [new Crop { X = 32, Width = 64, Height = 64 }] },
        NarsheWeaponShop = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.WeaponShopSign, Transforms = [new Pad(24,24)] },
        NarsheWeaponShopMines = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.WeaponShopSign, Transforms = [new Pad(24, 24)] },
        PhoenixCave = new SpriteDefinition { Source = SpriteSource.Esper, Id = (int)Esper.Phoenix, Transforms = [new Crop { Y = 8, Width = 64, Height = 64 }] },
        FigaroCastleThrone = new SpriteDefinition
        {
            Source = SpriteSource.Background,
            Id = (int)TileSet.Throne,
            Transforms =
            [
                new Overlay
                {
                    OverlayedSprite = new SpriteDefinition
                    {
                        Source = SpriteSource.Character, Id = (int)CharacterEx.DomaGuard, SubId = (int)Pose.Stand,
                    },
                    Y = 7
                }
            ]
        },
        FigaroCastleEngine = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Tentacle, Transforms = [new Crop { Width = 32, Height = 32 }] },
        AncientCastle = new SpriteDefinition { Source = SpriteSource.Esper, Id = (int)Esper.Raiden, Transforms = [new Crop { X = 4, Y = 8, Width = 56, Height = 56 }] },
        MtKolts = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Vargas, Transforms = [new Crop { X = 16, Width = 40, Height = 40 }] },
        CollapsingHouse = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.YoungBoy1, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        BarenFalls = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Rizopas },
        ImperialCamp = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.ImperialSoldier, SubId = (int)Pose.Ready, Transforms = [new Resize { Width = 32, Height = 48 }] },
        PhantomTrain = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.GhostTrain },
        GauFatherHouse = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.GausFather, SubId = (int)Pose.Surprised, Transforms = [new Resize { Width = 32, Height = 48 }] },
        FloatingContinentArrival = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Shadow, (int)Pose.Celebrate2, [new Resize(32, 48)]),
        FloatingContinentBeast = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Shadow, (int)Pose.Celebrate2, [new Resize(32, 48)]),
        FloatingContinentEscape = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Shadow, (int)Pose.Celebrate2, [new Resize(32, 48)]),
        VeldtCave = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.SrBehemoth, Transforms = [new Crop { X = 24, Y = 8, Width = 40, Height = 40 }] },
        DomaSiege = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Leader },
        DomaDreamDoor = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Wrexsoul, (int)Pose.Stand, [new Resize(32, 48)]),
        DomaDreamAwaken = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Wrexsoul, (int)Pose.Stand, [new Resize(32, 48)]),
        DomaDreamThrone = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Wrexsoul, (int)Pose.Stand, [new Resize(32, 48)]),
        MtZozo = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.PottedRoses, Transforms = [new Pad { Height = 28, Width = 28 }, new Resize { Width = 56, Height = 56 }] },
        Veldt = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Gau, SubId = (int)Pose.Kneel, Transforms = [new Resize { Width = 32, Height = 48 }] },
        SerpentTrench = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.DiveHelm, Transforms = [new Pad { Height = 20, Width = 20 }, new Resize { Height = 40, Width = 40 }] },
        SouthFigaroPrisoner = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Celes, SubId = (int)Pose.Chained, Transforms = [new Resize { Width = 32, Height = 48 }] },
        MagitekFactoryTrash = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.LeftCrane, Transforms = [new Crop { X = 24, Y = 48, Width = 40, Height = 40 }] },
        MagitekFactoryGuard = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.LeftCrane, Transforms = [new Crop { X = 24, Y = 48, Width = 40, Height = 40 }] },
        MagitekFactoryFinish = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.LeftCrane, Transforms = [new Crop { X = 24, Y = 48, Width = 40, Height = 40 }] },
        OperaHouseDisruption = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Weight, Transforms = [new Pad { Height = 28, Width = 28 }, new Resize { Width = 56, Height = 56 }] },
        KohlingenCafe = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Interceptor, SubId = (int)Pose.Sit, Transforms = [new Resize { Width = 32, Height = 48 }] },
        DarylsTomb = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Dullahan, Transforms = [new Crop { Width = 64, Height = 64 }] },
        LoneWolfChase = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.LoneWolf, SubId = (int)Pose.Stand, Transforms = [new Resize(32,48)] },
        LoneWolfMoogleRoom = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.LoneWolf, SubId = (int)Pose.Stand, Transforms = [new Resize(32, 48)] },
        MoogleDefense = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Mog, SubId = (int)Pose.CombatStandLeft, Transforms = [new Resize { Width = 32, Height = 48 }] },
        BurningHouse = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.FlameEater },
        EbotsRock = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Hidon, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        FanaticsTowerLeader = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.MagiMaster, Transforms = [new Crop { X = 16, Width = 40, Height = 40 }] },
        FanaticsTowerFollower = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.MagiMaster, Transforms = [new Crop { X = 16, Width = 40, Height = 40 }] },
        EsperMountain = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.WarringTriad, Transforms = [new Pad { Height = 28, Width = 28 }, new Resize { Width = 56, Height = 56 }] },
        OwzersMansion = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.ChadarnookMonster, Transforms = [new Crop { Width = 64, Height = 64 }] },
        ZoneEater = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.ZoneEater, Transforms = [new Crop { X = 24, Width = 72, Height = 72 }] },
        UmarosCave = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.UmaroP1 },
        NarsheBattle = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Kefka, SubId = (int)Pose.Laugh2, Transforms = [new Resize { Width = 32, Height = 48 }] },
        TzenThief = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.TzenThief,
            SubId = (int)Pose.StandLeft,
            Transforms =
            [
                new Pad { Width = 24, Height = 24, HorizontalAlignment = HorizontalAlignment.Left },
                new Overlay(7, 0, new SpriteDefinition(TileSet.Tree, [new Crop(0, 10, 16, 24)])),
                new Resize { Width = 48, Height = 48 }
            ]
        },
        SearchTheSkies = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.DoomGaze, Transforms = [new Crop { X = 32, Width = 40, Height = 40 }] },
        TritochCliff = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Tritoch, SubId = (int)Pose.Stand },
        JidoorAuctionHouse1 = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Auctioneer, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        JidoorAuctionHouse2 = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Auctioneer, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        KefkasTowerCellBeast = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.AtmaWeapon, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },

        PhoenixCaveDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.RedDragon },
        AncientCasteDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.BlueDrgn, Transforms = [new Crop(0, 0, 64, 64)] },
        MtZozoDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.StormDrgn, Transforms = [new Crop(16, 0, 48, 48)] },
        OperaHouseDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.DirtDrgn, Transforms = [new Crop(0, 0, 56, 56)] },
        FanaticsTowerDragon = new SpriteDefinition { Source = SpriteSource.Monster, Id = (int)Monster.WhiteDrgn },
        NarsheDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.IceDragon, Transforms = [new Resize(64, 64)] },
        KefkasTowerMiddlePathDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.GoldDrgn, Transforms = [new Crop(0, 0, 64, 64)] },
        KefkasTowerRightPathDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.SkullDrgn },
        GoddessStatue = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.GoddessStatue },
        DoomStatue = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.DoomStatue },
        PoltrgeistStatue = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.PoltrgeistStatue },
        FinalKefkaUnlocked = new SpriteDefinition(Boss.FinalKefka, [new Crop(80, 0, 36, 36)]),

        KefkaTowerSkipUnlocked = new SpriteDefinition(Item.Switch)
        {
            Transforms =
            [
                new ManualPad(0, 0, 32, 32),
                new Overlay(16, 0, new SpriteDefinition(Item.Switch)),
                new Overlay(8, 16, new SpriteDefinition(Item.Switch)),
                new Crop(0, 1, 32, 31),
                new Pad(32, 32, HorizontalAlignment.Center, VerticalAlignment.Top),
            ]
        },

        BlueDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.BlueDrgn, Transforms = [new Crop(0, 0, 64, 64)] },
        DirtDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.DirtDrgn, Transforms = [new Crop(0, 0, 56, 56)] },
        GoldDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.GoldDrgn, Transforms = [new Crop(0, 0, 64, 64)] },
        IceDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.IceDragon, Transforms = [new Resize(64, 64)] },
        RedDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.RedDragon },
        SkullDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.SkullDrgn },
        StormDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.StormDrgn, Transforms = [new Crop(16, 0, 48, 48)] },
        WhiteDragon = new SpriteDefinition { Source = SpriteSource.Monster, Id = (int)Monster.WhiteDrgn },

        Terra = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Terra },
        Locke = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Locke },
        Edgar = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Edgar },
        Sabin = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Sabin },
        Shadow = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Shadow },
        Cyan = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Cyan },
        Gau = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Gau },
        Celes = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Celes },
        Setzer = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Setzer },
        Mog = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Mog },
        Strago = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Strago },
        Relm = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Relm },
        Gogo = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Gogo },
        Umaro = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Umaro },

        CharacterCount = new SpriteDefinition(CharacterEx.GeneralLeo, Pose.Salute, [new ManualPad(6, 8, 40, 40)]),
        EsperCount = new SpriteDefinition(CharacterEx.EsperTerra, Pose.Sad)
        {
            Transforms =
            [
                new ManualPad(6, 8, 40,40),
                new Overlay(0, 24, new SpriteDefinition(Item.Magicite, [new Crop(2,2,12,12)])),
                new Overlay(16, 24, new SpriteDefinition(Item.Magicite, [new Crop(2,2,12,12)]))
            ]
        },
        DragonCount = new SpriteDefinition(CharacterEx.SkullDragon)
        {
            Transforms =
            [
                new ManualPad(0, 2, 40, 40),
                new Overlay(14, 2, new SpriteDefinition(CharacterEx.BlueDragon) ),
                new Overlay(6, 10, new SpriteDefinition(CharacterEx.RedDragon) ),
            ]
        },
        //BossCount = new SpriteDefinition(CharacterEx.Ultros, Pose.Surprised, [new ManualPad(4, 9, 40, 40)]),
        BossCount = new SpriteDefinition(Boss.Ultros, [new Crop(4, 0, 60, 48), new ManualPad(0, 8, 60, 60)]),
        CheckCount = new SpriteDefinition(CharacterEx.Imp, Pose.HandsUp, [new ManualPad(6, 6, 40, 40)]),
        ChestCount = new SpriteDefinition(TileSet.LargeOpenChest, [new ManualPad(4, 7, 40, 40)]),
    };

    private static readonly SpriteSetDefinition _locationBased = new()
    {
        WhelkGate = new SpriteDefinition(Boss.Whelk)
        {
            Transforms =
            [
                new ManualPad(0,0, 96, 64),
                new Overlay(48,24, new SpriteDefinition(Boss.WhelkHead)),
                new Crop(24, 8, 56, 56)
            ]
        },
        LeteRiver = new SpriteDefinition(Item.Raft1, [new Resize { Width = 64, Height = 64 }]),
        SealedGate = new SpriteDefinition(Item.SealedGate, [new Crop { X = 4, Y = 2, Width = 40, Height = 45 }, new SetGreyscaleBrightness(.2f)]),
        ZozoTower = new SpriteDefinition(CharacterEx.Dadaluma, Pose.Stand, [new Resize { Width = 32, Height = 48 }]),
        MoblizAttack = new SpriteDefinition(Boss.Phunbaba1),
        SouthFigaroCave = new SpriteDefinition(MapLocation.CavetoSouthFigaro_Room1, [new Crop(740, 432, 40, 40), new Overlay(12,12, new SpriteDefinition(Item.Turtle1)), new Pad(48, 48)]),
        NarsheWeaponShop = new SpriteDefinition(TileSet.NarsheCobble)
        {
            Transforms =
            [
                new ManualPad(0, 0, 32, 32),
                new Overlay(16, 0, new SpriteDefinition(TileSet.NarsheCobble)),
                new Overlay(16, 16, new SpriteDefinition(TileSet.NarsheCobble)),
                new Overlay(0, 16, new SpriteDefinition(TileSet.NarsheCobble)),
                new Overlay(8, 8, new SpriteDefinition(TileSet.WeaponShopSign)),
                new Pad(40, 40),
            ]
        },
        NarsheWeaponShopMines = new SpriteDefinition(TileSet.NarsheCobble)
        {
            Transforms =
            [
                new ManualPad(0, 0, 32, 32),
                new Overlay(16, 0, new SpriteDefinition(TileSet.NarsheCobble)),
                new Overlay(16, 16, new SpriteDefinition(TileSet.NarsheCobble)),
                new Overlay(0, 16, new SpriteDefinition(TileSet.NarsheCobble)),
                new Overlay(8, 8, new SpriteDefinition(TileSet.WeaponShopSign)),
                new Pad(40, 40),
            ]
        },
        PhoenixCave = new SpriteDefinition(MapLocation.PheonixCave_BigLavaRoom, [new Crop(568, 800, 48, 48), new Pad(56,56)]),
        FigaroCastleThrone = new SpriteDefinition(MapLocation.FigaroCastle_ThroneRoom, [new Crop(1608, 646, 32, 36), new Pad(40,40), new Overlay(12, 10, new SpriteDefinition(CharacterEx.FigaroGuard))]),
        FigaroCastleEngine = new SpriteDefinition(TileSet.Engine),
        AncientCastle = new SpriteDefinition(CharacterEx.AncientQueen, Pose.Stand, [new Resize { Width = 32, Height = 48 }, new Greyscale()]),
        MtKolts = new SpriteDefinition(CharacterEx.Vargas, Pose.StandLeft, [new Resize { Height = 48, Width = 32 }]),
        CollapsingHouse = new SpriteDefinition(CharacterEx.YoungBoy1, Pose.Stand, [new Resize { Width = 32, Height = 48 }]),
        BarenFalls = new SpriteDefinition(MapLocation.WaterfallCliff, [new Crop(248, 96, 40, 40), new Pad(48,48)]),
        ImperialCamp = new SpriteDefinition(CharacterEx.ImperialSoldier, Pose.Ready, [new Resize { Width = 32, Height = 48 }]),
        PhantomTrain = new SpriteDefinition(Boss.GhostTrain),
        GauFatherHouse = new SpriteDefinition(CharacterEx.GausFather, Pose.Surprised, [new Resize { Width = 32, Height = 48 }]),
        FloatingContinentArrival = new SpriteDefinition(Item.Spitfire)
        {
            Transforms =
            [
                new ManualPad(0, 0, 32, 32),
                new Overlay(16, 0, new SpriteDefinition(Item.Spitfire)),
                new Overlay(8, 16, new SpriteDefinition(Item.Spitfire)),
                new Resize(64, 64)
            ]
        },
        FloatingContinentBeast = new SpriteDefinition(CharacterEx.AtmaWeapon, Pose.Stand, [new Resize { Width = 32, Height = 48 }]),
        FloatingContinentEscape = new SpriteDefinition(Item.Blackjack, [new Resize { Width = 32, Height = 48 }]),
        VeldtCave = new SpriteDefinition(CharacterEx.SrBehemoth, Pose.StandLeft)
        {
            Transforms =
            [
                new Pad { Width = 32, Height = 24, HorizontalAlignment = HorizontalAlignment.Right },
                new Overlay { Y = 8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Interceptor, SubId = (int)Pose.StandLeft, Transforms = [new FlipHorizontal() ]} },
                new Resize { Width = 64, Height = 48 }
            ]
        },
        DomaSiege = new SpriteDefinition(CharacterEx.ImperialSoldier)
        {
            Transforms =
            [
                new Pad(32, 32, HorizontalAlignment.Left, VerticalAlignment.Top),
                new Overlay(16, 0, new SpriteDefinition(CharacterEx.ImperialSoldier, Pose.Stand)),
                new Overlay(8, 8, new SpriteDefinition(CharacterEx.ImperialCommander, Pose.Stand)),
                new Resize(64, 64)
            ]
        },
        DomaDreamDoor = new SpriteDefinition(CharacterEx.YoungBoy3)
        {
            Transforms =
            [
                new Pad { Width = 32, Height = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                new Overlay(16, 0, new SpriteDefinition(CharacterEx.YoungBoy3)),
                new Overlay(8, 8, new SpriteDefinition(CharacterEx.YoungBoy3)),
                new Resize { Width = 64, Height = 64 }
            ]
        },
        DomaDreamAwaken = new SpriteDefinition(CharacterEx.Wrexsoul, Pose.Stand, [new Resize { Width = 32, Height = 48 }]),
        DomaDreamThrone = new SpriteDefinition(Item.Sword, [new Pad(20, 20), new Resize(40, 40)]),
        MtZozo = new SpriteDefinition(Item.Boquet, [new Pad(20, 20), new Resize(40, 40)]),
        Veldt = new SpriteDefinition(CharacterEx.Gau, Pose.Kneel, [new Resize(32, 48)]),
        SerpentTrench = new SpriteDefinition(Item.DiveHelm, [new Pad(20, 20), new Resize(40, 40)]),
        SouthFigaroPrisoner = new SpriteDefinition(CharacterEx.Celes, Pose.Chained, [new Resize { Width = 32, Height = 48 }]),
        MagitekFactoryTrash = new SpriteDefinition(CharacterEx.Ifrit)
        {
            Transforms =
            [
                new Pad(32, 32, HorizontalAlignment.Left),
                new Overlay(18, 0, new SpriteDefinition(CharacterEx.Shiva)),
                new Resize(64, 64)
            ]
        },
        MagitekFactoryGuard = new SpriteDefinition(CharacterEx.Number024, Pose.Stand, [new Resize { Width = 32, Height = 48 }]),
        MagitekFactoryFinish = new SpriteDefinition(Item.MagitekElevator)
        {
            Transforms =
            [
                new Pad(32, 40) { VerticalAlignment = VerticalAlignment.Bottom },
                new FlipHorizontal(),
                new Overlay(16, 0, new SpriteDefinition(CharacterEx.Cid, Pose.StandLeft))
            ]
        },
        OperaHouseDisruption = new SpriteDefinition(MapLocation.OperaHouse_Ceiling, [new Crop(240,96, 32, 32), new Overlay(16, 8, new SpriteDefinition(Item.Weight))]),
        KohlingenCafe = new SpriteDefinition(TileSet.CafeTable)
        {
            Transforms =
            [
                new Pad(48, 40),
                new Overlay(0, 8, new SpriteDefinition(TileSet.CafeChair, [new FlipHorizontal()])),
                new Overlay(32, 8, new SpriteDefinition(TileSet.CafeChair))
            ]
        },
        DarylsTomb = new SpriteDefinition(MapLocation.DarillsTomb_Basement3, [new Crop(1580, 164, 56, 56)]),
        LoneWolfChase = new SpriteDefinition(CharacterEx.LoneWolf, Pose.Stand, [new Resize(32, 48),]),
        LoneWolfMoogleRoom = new SpriteDefinition(CharacterEx.LoneWolf, Pose.Stand, [new Resize(32, 48),]),
        MoogleDefense = new SpriteDefinition(CharacterEx.Mog, Pose.SadLeft)
        {
            Transforms =
            [
                new FlipHorizontal(),
                new Pad(48, 24, HorizontalAlignment.Left),
                new Overlay(16,0, new SpriteDefinition(CharacterEx.Mog, Pose.Dead)),
                new Overlay(32,0, new SpriteDefinition(CharacterEx.Mog, Pose.SadLeft)),
            ]
        },
        BurningHouse = new SpriteDefinition(Item.Fire1)
        {
            Transforms =
            [
                new ManualPad(0, 0, 32, 32),
                new Overlay(16, 0, new SpriteDefinition(Item.Fire2)),
                new Overlay(8, 16, new SpriteDefinition(Item.Fire3)),
                new Resize(64, 64)
            ]
        },
        EbotsRock = new SpriteDefinition(CharacterEx.Hidon, Pose.Stand, [new Resize { Width = 32, Height = 48 }, new Pad(56,56)]),
        FanaticsTowerLeader = new SpriteDefinition(MapLocation.FanaticsTower_Level1, [new Crop(80, 40, 80, 80)]),
        FanaticsTowerFollower = new SpriteDefinition(CharacterEx.Cultist, Pose.WalkLeft1)
        {
            Transforms =
            [
                new Pad { Height = 24, Width = 32, HorizontalAlignment = HorizontalAlignment.Left },
                new Overlay { X = 16, OverlayedSprite = new SpriteDefinition(CharacterEx.Cultist, Pose.WalkLeft2) },
                new FlipHorizontal()
            ]
        },
        EsperMountain = new SpriteDefinition(Item.WarringTriad)
        {
            Transforms =
            [
                new ManualPad(4, 16, 48, 32),
                new Overlay(16, 0, new SpriteDefinition(Item.WarringTriad)),
                new Overlay(28, 16, new SpriteDefinition(Item.WarringTriad)),
            ]
        },
        OwzersMansion = new SpriteDefinition(Item.OwzersPainting, [new Crop(0, 2, 32, 32)]),
        ZoneEater = new SpriteDefinition(Boss.ZoneEater, [new Crop(24, 0, 72, 72)]),
        UmarosCave = new SpriteDefinition(Item.UmaroSkull, [new Crop(2,2,11,22), new Pad(11,26), new Resize(22,52)]),  //[new Resize(32, 48)]),
        NarsheBattle = new SpriteDefinition(CharacterEx.Kefka, Pose.Laugh2, [new Resize(32, 48)]),
        TzenThief = new SpriteDefinition(CharacterEx.TzenThief, Pose.StandLeft)
        {
            Transforms =
            [
                new Pad(24, 24, HorizontalAlignment.Left),
                new Overlay(7, 0, new SpriteDefinition(TileSet.Tree, [new Crop(0, 10, 11, 24)])),
                new Resize(48, 48)
            ]
        },
        SearchTheSkies = new SpriteDefinition(Item.Falcon),
        TritochCliff = new SpriteDefinition(CharacterEx.Tritoch),
        JidoorAuctionHouse1 = new SpriteDefinition(CharacterEx.Auctioneer, Pose.Stand, [new Resize(32, 48)]),
        JidoorAuctionHouse2 = new SpriteDefinition(CharacterEx.Auctioneer, Pose.Stand, [new Resize(32, 48)]),
        KefkasTowerCellBeast = new SpriteDefinition(TileSet.Toilet)
        {
            Transforms =
            [
                new ManualPad(0, 0, 32, 32),
                new Overlay(16, 8, new SpriteDefinition(CharacterEx.AtmaWeapon))
            ]
        },

        PhoenixCaveDragon = new SpriteDefinition(Boss.RedDragon),
        AncientCasteDragon = new SpriteDefinition(Boss.BlueDrgn, [new Crop(0, 0, 64, 64)]),
        MtZozoDragon = new SpriteDefinition(Boss.StormDrgn, [new Crop(16, 0, 48, 48)]),
        OperaHouseDragon = new SpriteDefinition(Boss.DirtDrgn, [new Crop(0, 0, 56, 56)]),
        FanaticsTowerDragon = new SpriteDefinition(Monster.WhiteDrgn),
        NarsheDragon = new SpriteDefinition(Boss.IceDragon, [new Resize(64, 64)]),
        KefkasTowerMiddlePathDragon = new SpriteDefinition(Boss.GoldDrgn, [new Crop(0, 0, 64, 64)]),
        KefkasTowerRightPathDragon = new SpriteDefinition(Boss.SkullDrgn),

        GoddessStatue = new SpriteDefinition(Item.GoddessStatue),
        DoomStatue = new SpriteDefinition(Item.DoomStatue),
        PoltrgeistStatue = new SpriteDefinition(Item.PoltrgeistStatue),

        FinalKefkaUnlocked = new SpriteDefinition(Boss.FinalKefka, [new Crop(80, 0, 36, 36)]),

        KefkaTowerSkipUnlocked = new SpriteDefinition(Item.Switch)
        {
            Transforms =
            [
                new ManualPad(0, 0, 32, 32),
                new Overlay(16, 0, new SpriteDefinition(Item.Switch)),
                new Overlay(8, 16, new SpriteDefinition(Item.Switch)),
                new Crop(0, 1, 32, 31),
                new Pad(36, 36),
            ]
        },

        BlueDragon = new SpriteDefinition(Boss.BlueDrgn, [new Crop(0, 0, 64, 64)]),
        DirtDragon = new SpriteDefinition(Boss.DirtDrgn, [new Crop(0, 0, 56, 56)]),
        GoldDragon = new SpriteDefinition(Boss.GoldDrgn, [new Crop(0, 0, 64, 64)]),
        IceDragon = new SpriteDefinition(Boss.IceDragon),
        RedDragon = new SpriteDefinition(Boss.RedDragon),
        SkullDragon = new SpriteDefinition(Boss.SkullDrgn),
        StormDragon = new SpriteDefinition(Boss.StormDrgn, [new Crop(16, 0, 48, 48)]),
        WhiteDragon = new SpriteDefinition(Monster.WhiteDrgn),

        Terra = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Terra },
        Locke = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Locke },
        Edgar = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Edgar },
        Sabin = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Sabin },
        Shadow = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Shadow },
        Cyan = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Cyan },
        Gau = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Gau },
        Celes = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Celes },
        Setzer = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Setzer },
        Mog = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Mog },
        Strago = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Strago },
        Relm = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Relm },
        Gogo = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Gogo },
        Umaro = new SpriteDefinition { Source = SpriteSource.Portrait, Id = (int)Character.Umaro },

        CharacterCount = new SpriteDefinition(CharacterEx.GeneralLeo, Pose.Salute, [new ManualPad(6, 8, 40, 40)]),
        EsperCount = new SpriteDefinition(CharacterEx.EsperTerra, Pose.Sad)
        {
            Transforms =
            [
                new ManualPad(6, 8, 40,40),
                new Overlay(0, 24, new SpriteDefinition(Item.Magicite, [new Crop(2,2,12,12)])),
                new Overlay(16, 24, new SpriteDefinition(Item.Magicite, [new Crop(2,2,12,12)]))
            ]
        },
        DragonCount = new SpriteDefinition(CharacterEx.SkullDragon)
        {
            Transforms =
            [
                new ManualPad(0, 2, 40, 40),
                new Overlay(14, 2, new SpriteDefinition(CharacterEx.BlueDragon) ),
                new Overlay(6, 10, new SpriteDefinition(CharacterEx.RedDragon) ),
            ]
        },
        //BossCount = new SpriteDefinition(CharacterEx.Ultros, Pose.Surprised, [new ManualPad(4, 9, 40, 40)]),
        BossCount = new SpriteDefinition(Boss.Ultros, [new Crop(4,0,60,48), new ManualPad(0,8,60,60)]),
        CheckCount = new SpriteDefinition(CharacterEx.Imp, Pose.HandsUp, [new ManualPad(6, 6, 40, 40)]),
        ChestCount = new SpriteDefinition(TileSet.LargeOpenChest, [new ManualPad(4, 7, 40, 40)]),
    };

    public static SpriteSetDefinition VanillaBosses => _vanillaBosses;
    public static SpriteSetDefinition LocationBased => _locationBased;
}