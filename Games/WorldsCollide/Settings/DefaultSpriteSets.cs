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
        LeteRiver = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.UltrosLetRiver },
        SealedGate = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Maduin, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        ZozoTower = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Ramuh, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        MoblizAttack = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Phunbaba1 },
        SouthFigaroCave = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.TunnelArmr, Transforms = [new Crop { X = 32, Width = 64, Height = 64 }] },
        NarsheWeaponShop = new SpriteDefinition
        {
            Source = SpriteSource.Background,
            Id = (int)TileSet.WeaponShopSign,
            Transforms =
            [
                new Pad(24,24),
                new TextOverlay(16, 16, "1"),
                new AlternateDisabledSprite(new SpriteDefinition(SpriteSource.Background, (int)TileSet.WeaponShopSign, [new Pad(24,24)]))
            ]
        },
        NarsheWeaponShopMines = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.WeaponShopSign, Transforms = [new Pad(24, 24), new TextOverlay(16, 16, "1")] },
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
        FloatingContinentArrival = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Shadow, (int)Pose.Celebrate2, [new Resize(32, 48), new TextOverlay(0, 40, "1"), new AlternateDisabledSprite(new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Shadow, (int)Pose.Celebrate2, [new Resize(32, 48)]))]),
        FloatingContinentBeast = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Shadow, (int)Pose.Celebrate2, [new Resize(32, 48), new TextOverlay(0, 40, "2")]),
        FloatingContinentEscape = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Shadow, (int)Pose.Celebrate2, [new Resize(32, 48), new TextOverlay(0, 40, "3")]),
        VeldtCave = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.SrBehemoth, Transforms = [new Crop { X = 24, Y = 8, Width = 40, Height = 40 }] },
        DomaSiege = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Leader },
        DomaDreamDoor = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Wrexsoul, (int)Pose.Stand, [new Resize(32, 48), new TextOverlay(24, 32, "1"), new AlternateDisabledSprite(new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Wrexsoul, (int)Pose.Stand, [new Resize(32, 48)]))]),
        DomaDreamAwaken = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Wrexsoul, (int)Pose.Stand, [new Resize(32, 48), new TextOverlay(24, 32, "2")]),
        DomaDreamThrone = new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Wrexsoul, (int)Pose.Stand, [new Resize(32, 48), new TextOverlay(24, 32, "3")]),
        MtZozo = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.PottedRoses, Transforms = [new Pad { Height = 28, Width = 28 }, new Resize { Width = 56, Height = 56 }] },
        Veldt = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Gau, SubId = (int)Pose.Kneel, Transforms = [new Resize { Width = 32, Height = 48 }] },
        SerpentTrench = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.DiveHelm, Transforms = [new Pad { Height = 20, Width = 20 }, new Resize { Height = 40, Width = 40 }] },
        SouthFigaroPrisoner = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Celes, SubId = (int)Pose.Chained, Transforms = [new Resize { Width = 32, Height = 48 }] },
        MagitekFactoryTrash = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.LeftCrane, Transforms = [new Crop { X = 24, Y = 48, Width = 40, Height = 40 }, new TextOverlay(32, 32, "1"), new AlternateDisabledSprite(new SpriteDefinition(SpriteSource.Boss, (int)Boss.LeftCrane, [new Crop { X = 24, Y = 48, Width = 40, Height = 40 }]))] },
        MagitekFactoryGuard = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.LeftCrane, Transforms = [new Crop { X = 24, Y = 48, Width = 40, Height = 40 }, new TextOverlay(32, 32, "2")] },
        MagitekFactoryFinish = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.LeftCrane, Transforms = [new Crop { X = 24, Y = 48, Width = 40, Height = 40 }, new TextOverlay(32, 32, "3")] },
        OperaHouseDisruption = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Weight, Transforms = [new Pad { Height = 28, Width = 28 }, new Resize { Width = 56, Height = 56 }] },
        KohlingenCafe = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Interceptor, SubId = (int)Pose.Sit, Transforms = [new Resize { Width = 32, Height = 48 }] },
        DarylsTomb = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Dullahan, Transforms = [new Crop { Width = 64, Height = 64 }] },
        LoneWolfChase = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.LoneWolf,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Resize(32,48),
                new TextOverlay(24,40, "1"),
                new AlternateDisabledSprite(new SpriteDefinition{
                    Source = SpriteSource.Character,
                    Id = (int)CharacterEx.LoneWolf,
                    SubId = (int)Pose.Stand,
                    Transforms = [new Resize(32, 48)]
                })
            ]
        },
        LoneWolfMoogleRoom = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.LoneWolf,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Resize { Width = 32, Height = 48 },
                new TextOverlay(24,40, "2"),
            ]
        },
        MoogleDefense = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Mog, SubId = (int)Pose.CombatStandLeft, Transforms = [new Resize { Width = 32, Height = 48 }] },
        BurningHouse = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.FlameEater },
        EbotsRock = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Hidon, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        FanaticsTowerLeader = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.MagiMaster, Transforms = [new Crop { X = 16, Width = 40, Height = 40 }, new TextOverlay(32, 32, "1"), new AlternateDisabledSprite(new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.MagiMaster, Transforms = [new Crop { X = 16, Width = 40, Height = 40 }] })] },
        FanaticsTowerFollower = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.MagiMaster, Transforms = [new Crop { X = 16, Width = 40, Height = 40 }, new TextOverlay(32, 32, "2")] },
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
                new Overlay { X=7, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.Tree, Transforms = [new Crop { Y= 10, Height = 24, Width = 16}] }},
                new Resize { Width = 48, Height = 48 }
            ]
        },
        SearchTheSkies = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.DoomGaze, Transforms = [new Crop { X = 32, Width = 40, Height = 40 }] },
        TritochCliff = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Tritoch, SubId = (int)Pose.Stand },
        JidoorAuctionHouse1 = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Auctioneer, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }, new TextOverlay(24, 40, "1"), new AlternateDisabledSprite(new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Auctioneer, (int)Pose.Stand, [new Resize { Width = 32, Height = 48 }]))] },
        JidoorAuctionHouse2 = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Auctioneer, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }, new TextOverlay(24, 40, "1")] },
        KefkasTowerCellBeast = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.AtmaWeapon, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },

        PhoenixCaveDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.RedDragon },
        AncientCasteDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.BlueDrgn },
        MtZozoDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.StormDrgn },
        OperaHouseDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.DirtDrgn },
        FanaticsTowerDragon = new SpriteDefinition { Source = SpriteSource.Monster, Id = (int)Monster.WhiteDrgn },
        NarsheDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.IceDragon },
        KefkasTowerMiddlePathDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.GoldDrgn },
        KefkasTowerRightPathDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.SkullDrgn },

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

        CharacterCount = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.GeneralLeo, SubId = (int)Pose.Salute },
        EsperCount = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Magicite },
        DragonCount = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.RedDragon, SubId = (int)Pose.Stand },
        BossCount = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Ultros, SubId = (int)Pose.Surprised },
        CheckCount = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Imp, SubId = (int)Pose.HandsUp },
        ChestCount = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.LargeOpenChest },

        RelatedEvents =
        [
            [Events.GOT_RAGNAROK, Events.GOT_BOTH_REWARDS_WEAPON_SHOP],
            [Events.RECRUITED_SHADOW_FLOATING_CONTINENT, Events.DEFEATED_ATMAWEAPON, Events.FINISHED_FLOATING_CONTINENT],
            [Events.DEFEATED_STOOGES, Events.FINISHED_DOMA_WOR, Events.GOT_ALEXANDR],
            [Events.GOT_IFRIT_SHIVA, Events.DEFEATED_NUMBER_024, Events.DEFEATED_CRANES],
            [Events.DEFEATED_MAGIMASTER, Events.RECRUITED_STRAGO_FANATICS_TOWER],
            [Events.CHASING_LONE_WOLF7, Events.GOT_BOTH_REWARDS_LONE_WOLF],
            [Events.AUCTION_BOUGHT_ESPER1, Events.AUCTION_BOUGHT_ESPER2]
        ]
    };

    private static readonly SpriteSetDefinition _locationBased = new()
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
        LeteRiver = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Raft1, Transforms = [new Resize { Width = 64, Height = 64 }] },
        SealedGate = new SpriteDefinition
        {
            Source = SpriteSource.Item,
            Id = (int)Item.SealedGate,
            Transforms =
            [
                new Crop { X = 4, Y = 2, Width = 40, Height = 45 },
            ]
        },
        ZozoTower = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Dadaluma, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        MoblizAttack = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.Phunbaba1 },
        SouthFigaroCave = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.TunnelArmr, Transforms = [new Crop { X = 32, Width = 64, Height = 64 }] },
        NarsheWeaponShop = new SpriteDefinition
        {
            Source = SpriteSource.Background,
            Id = (int)TileSet.NarsheCobble,
            Transforms =
            [
                new Pad { Height = 32, Width = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                new Overlay { X = 16, Y = 0, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                new Overlay { X = 16, Y = 16,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                new Overlay { X = 0, Y = 16,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                new Overlay { X = 8, Y = 8,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.WeaponShopSign } },
                new TextOverlay(24, 24, "1"),
                new Pad(40,40),
                new AlternateDisabledSprite(new SpriteDefinition
                {
                    Source = SpriteSource.Background,
                    Id = (int)TileSet.NarsheCobble,
                    Transforms =
                    [
                        new Pad { Height = 32, Width = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                        new Overlay { X = 16, Y = 0, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                        new Overlay { X = 16, Y = 16,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                        new Overlay { X = 0, Y = 16,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                        new Overlay { X = 8, Y = 8,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.WeaponShopSign } },
                        new Pad(40,40)
                    ]
                })
            ]
        },
        NarsheWeaponShopMines = new SpriteDefinition
        {
            Source = SpriteSource.Background,
            Id = (int)TileSet.NarsheCobble,
            Transforms =
            [
                new Pad { Height = 32, Width = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                new Overlay { X = 16, Y = 0, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                new Overlay { X = 16, Y = 16,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                new Overlay { X = 0, Y = 16,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.NarsheCobble } },
                new Overlay { X = 8, Y = 8,  OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.WeaponShopSign } },
                new TextOverlay(24, 24, "2"),
                new Pad(40,40)
            ]
        },
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
                            Source = SpriteSource.Character, Id = (int)CharacterEx.FigaroChancellor, SubId = (int)Pose.Stand,
                        },
                        Y = 7
                    }
            ]
        },
        FigaroCastleEngine = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.Engine },
        AncientCastle = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.AncientQueen, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }, new Greyscale()] },
        MtKolts = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Vargas, SubId = (int)Pose.StandLeft, Transforms = [new Resize { Height = 48, Width = 32 }] },
        CollapsingHouse = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.YoungBoy1, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        BarenFalls = new SpriteDefinition
        {
            Source = SpriteSource.Background,
            Id = (int)TileSet.WaterfallIsland,
            Transforms =
            [
                new Pad(40, 40, HorizontalAlignment.Left, VerticalAlignment.Top),
                new Overlay(0, 8, new SpriteDefinition(SpriteSource.Background, (int)TileSet.Waterfall)),
                new Pad(48,48)
            ]
        },
        ImperialCamp = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.ImperialSoldier, SubId = (int)Pose.Ready, Transforms = [new Resize { Width = 32, Height = 48 }] },
        PhantomTrain = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.GhostTrain },
        GauFatherHouse = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.GausFather, SubId = (int)Pose.Surprised, Transforms = [new Resize { Width = 32, Height = 48 }] },
        FloatingContinentArrival = new SpriteDefinition
        {
            Source = SpriteSource.Item,
            Id = (int)Item.Spitfire,
            Transforms =
            [
                new Pad { Height = 32, Width = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                new Overlay { X = 16, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Spitfire } },
                new Overlay { X = 8, Y = 16, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Spitfire } },
                new Resize { Height = 64, Width = 64 }
            ]
        },
        FloatingContinentBeast = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.AtmaWeapon, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        FloatingContinentEscape = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Blackjack, Transforms = [new Resize { Width = 32, Height = 48 }] },
        VeldtCave = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.SrBehemoth,
            SubId = (int)Pose.StandLeft,
            Transforms =
            [
                new Pad { Width = 32, Height = 24, HorizontalAlignment = HorizontalAlignment.Right },
                new Overlay { Y = 8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Interceptor, SubId = (int)Pose.StandLeft, Transforms = [new FlipHorizontal() ]} },
                new Resize { Width = 64, Height = 48 }
            ]
        },
        DomaSiege = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.ImperialSoldier,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Pad(32, 32, HorizontalAlignment.Left, VerticalAlignment.Top),
                new Overlay(16, 0, new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.ImperialSoldier, (int)Pose.Stand)),
                new Overlay(8, 8, new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.ImperialCommander, (int)Pose.Stand)),
                new Resize(64, 64)
            ]
        },
        DomaDreamDoor = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.YoungBoy3,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Pad { Width = 32, Height = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                new Overlay { X = 16, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.YoungBoy3, SubId = (int)Pose.Stand } },
                new Overlay { X = 8, Y = 8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.YoungBoy3, SubId = (int)Pose.Stand } },
                new Resize { Width = 64, Height = 64 }
            ]
        },
        DomaDreamAwaken = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Wrexsoul, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        DomaDreamThrone = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Sword, Transforms = [new Pad { Height = 20, Width = 20 }, new Resize { Height = 40, Width = 40 }] },
        MtZozo = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Boquet, Transforms = [new Pad { Height = 20, Width = 20 }, new Resize { Width = 40, Height = 40 }] },
        Veldt = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Gau, SubId = (int)Pose.Kneel, Transforms = [new Resize { Width = 32, Height = 48 }] },
        SerpentTrench = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.DiveHelm, Transforms = [new Pad { Height = 20, Width = 20 }, new Resize { Height = 40, Width = 40 }] },
        SouthFigaroPrisoner = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Celes, SubId = (int)Pose.Chained, Transforms = [new Resize { Width = 32, Height = 48 }] },
        MagitekFactoryTrash = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.Ifrit,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Pad { Width = 32, Height = 32, HorizontalAlignment = HorizontalAlignment.Left },
                new Overlay {
                    X = 18,
                    OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Shiva, SubId = (int)Pose.Stand }
                },
                new Resize { Width = 64, Height = 64 }
            ]
        },
        MagitekFactoryGuard = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Number024, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        MagitekFactoryFinish = new SpriteDefinition
        {
            Source = SpriteSource.Item,
            Id = (int)Item.MagitekElevator,
            Transforms =
            [
                new Pad { Width = 32, Height = 40, VerticalAlignment = VerticalAlignment.Bottom },
                new FlipHorizontal(),
                new Overlay { X = 16, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Cid, SubId = (int)Pose.StandLeft }}
            ]
        },
        OperaHouseDisruption = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Weight, Transforms = [new Pad { Height = 28, Width = 28 }, new Resize { Width = 56, Height = 56 }] },
        KohlingenCafe = new SpriteDefinition
        {
            Source = SpriteSource.Background,
            Id = (int)TileSet.CafeTable,
            Transforms =
            [
                new Pad { Width = 48, Height = 40 },
                new Overlay { Y=8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.CafeChair, Transforms = [new FlipHorizontal()]}},
                new Overlay { X=32, Y=8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.CafeChair}},
            ]
        },
        DarylsTomb = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.DarylsTomb, Transforms = [new Crop { X = 8, Width = 40, Height = 40 }] },
        LoneWolfChase = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.LoneWolf,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Resize(32,48),
                new TextOverlay(24,40, "1"),
                new AlternateDisabledSprite(new SpriteDefinition{
                    Source = SpriteSource.Character,
                    Id = (int)CharacterEx.LoneWolf,
                    SubId = (int)Pose.Stand,
                    Transforms = [new Resize(32, 48)]
                })
            ]
        },
        LoneWolfMoogleRoom = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.LoneWolf,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Resize(32,48),
                new TextOverlay(24,40, "2")
            ]
        },
        MoogleDefense = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.Mog,
            SubId = (int)Pose.SadLeft,
            Transforms =
            [
                new FlipHorizontal(),
                new Pad(48, 24, HorizontalAlignment.Left),
                new Overlay(16,0, new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Mog, (int)Pose.Dead)),
                new Overlay(32,0, new SpriteDefinition(SpriteSource.Character, (int)CharacterEx.Mog, (int)Pose.SadLeft)),
            ]
        },
        BurningHouse = new SpriteDefinition
        {
            Source = SpriteSource.Item,
            Id = (int)Item.Fire1,
            Transforms =
            [
                new Pad { Height = 32, Width = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top},
                new Overlay { X = 16, OverlayedSprite = new SpriteDefinition {Source = SpriteSource.Item, Id = (int)Item.Fire2 } },
                new Overlay { X = 8, Y = 16, OverlayedSprite = new SpriteDefinition {Source = SpriteSource.Item, Id = (int)Item.Fire3 }},
                new Resize { Height = 64, Width = 64 }
            ]
        },
        EbotsRock = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Hidon, SubId = (int)Pose.Stand, Transforms = [new Resize { Width = 32, Height = 48 }] },
        FanaticsTowerLeader = new SpriteDefinition
        {
            Source = SpriteSource.Background,
            Id = (int)TileSet.FanaticsTowerWall,
            Transforms =
            [
                new Pad { Height = 136, Width = 80, VerticalAlignment = VerticalAlignment.Top },
                new Overlay { Y = 40, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.FanaticsTowerFloor } },
                new Overlay { Y = 48, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.FanaticsTowerWall } },
                new Overlay { Y = 88, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.FanaticsTowerFloor } },
                new Overlay { Y = 96, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.FanaticsTowerWall } },
                new Crop { Y = 24, Width = 80, Height = 88  },
                new Overlay { OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.FanaticsTowerStairs, Transforms = [new Crop { Width = 80, Height = 40, Y = 48 }] } },
                new Overlay { OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.FanaticsTowerStairs } },
                new Overlay { Y = 48, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.FanaticsTowerStairs } }
            ]
        },
        FanaticsTowerFollower = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.Cultist,
            SubId = (int)Pose.WalkLeft1,
            Transforms =
            [
                new Pad { Height = 24, Width = 32, HorizontalAlignment = HorizontalAlignment.Left },
                new Overlay { X = 16, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Cultist, SubId = (int)Pose.WalkLeft2} },
                new FlipHorizontal()
            ]
        },
        EsperMountain = new SpriteDefinition
        {
            Source = SpriteSource.Item,
            Id = (int)Item.WarringTriad,
            Transforms =
            [
                new Pad { Height = 32, Width = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top},
                new Overlay { X = 16, OverlayedSprite = new SpriteDefinition {Source = SpriteSource.Item, Id = (int)Item.WarringTriad } },
                new Overlay { X = 8, Y = 16, OverlayedSprite = new SpriteDefinition {Source = SpriteSource.Item, Id = (int)Item.WarringTriad }},
                new Resize { Height = 64, Width = 64 }
            ]
        },
        OwzersMansion = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.OwzersPainting, Transforms = [new Crop { Width = 32, Height = 32 }] },
        ZoneEater = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.ZoneEater, Transforms = [new Crop { X = 24, Width = 72, Height = 72 }] },
        UmarosCave = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.UmaroSkull, Transforms = [new Resize { Width = 32, Height = 48 }] },
        NarsheBattle = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Kefka, SubId = (int)Pose.Laugh2, Transforms = [new Resize { Width = 32, Height = 48 }] },
        TzenThief = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.TzenThief,
            SubId = (int)Pose.StandLeft,
            Transforms =
            [
                new Pad { Width = 24, Height = 24, HorizontalAlignment = HorizontalAlignment.Left },
                new Overlay { X=7, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.Tree, Transforms = [new Crop { Y= 10, Height = 24, Width = 16}] }},
                new Resize { Width = 48, Height = 48 }
            ]
        },
        SearchTheSkies = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Falcon },
        TritochCliff = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Tritoch, SubId = (int)Pose.Stand },
        JidoorAuctionHouse1 = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.Auctioneer,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Pad { Width = 24, Height = 24 },
                new Overlay { X= 12, Y= 8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Magicite}},
                new Resize { Width = 48, Height = 48 },
                new AlternateDisabledSprite(new SpriteDefinition
                {
                    Source = SpriteSource.Character,
                    Id = (int)CharacterEx.Auctioneer,
                    SubId = (int)Pose.Stand,
                    Transforms =
                    [
                        new Pad { Width = 24, Height = 24 },
                        new Resize { Width = 48, Height = 48 }
                    ]
                })
            ]
        },
        JidoorAuctionHouse2 = new SpriteDefinition
        {
            Source = SpriteSource.Character,
            Id = (int)CharacterEx.Auctioneer,
            SubId = (int)Pose.Stand,
            Transforms =
            [
                new Pad { Width = 24, Height = 24 },
                new Overlay { X= 12, Y= 8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Magicite}},
                new Overlay { X= -4, Y= 8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Magicite}},
                new Resize { Width = 48, Height = 48 }
            ]
        },
        KefkasTowerCellBeast = new SpriteDefinition
        {
            Source = SpriteSource.Background,
            Id = (int)TileSet.Toilet,
            Transforms =
            [
                new Pad { Width = 32, Height = 32, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                new Overlay { X = 16, Y = 8, OverlayedSprite = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.AtmaWeapon, SubId = (int)Pose.Stand}}
            ]

        },

        PhoenixCaveDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.RedDragon },
        AncientCasteDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.BlueDrgn },
        MtZozoDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.StormDrgn },
        OperaHouseDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.DirtDrgn },
        FanaticsTowerDragon = new SpriteDefinition { Source = SpriteSource.Monster, Id = (int)Monster.WhiteDrgn },
        NarsheDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.IceDragon },
        KefkasTowerMiddlePathDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.GoldDrgn },
        KefkasTowerRightPathDragon = new SpriteDefinition { Source = SpriteSource.Boss, Id = (int)Boss.SkullDrgn },

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

        CharacterCount = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.GeneralLeo, SubId = (int)Pose.Salute },
        EsperCount = new SpriteDefinition { Source = SpriteSource.Item, Id = (int)Item.Magicite },
        DragonCount = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.RedDragon, SubId = (int)Pose.Stand },
        BossCount = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Ultros, SubId = (int)Pose.Surprised },
        CheckCount = new SpriteDefinition { Source = SpriteSource.Character, Id = (int)CharacterEx.Imp, SubId = (int)Pose.HandsUp },
        ChestCount = new SpriteDefinition { Source = SpriteSource.Background, Id = (int)TileSet.LargeOpenChest },

        RelatedEvents =
        [
            [Events.GOT_RAGNAROK, Events.GOT_BOTH_REWARDS_WEAPON_SHOP],
            [Events.CHASING_LONE_WOLF7, Events.GOT_BOTH_REWARDS_LONE_WOLF],
            [Events.AUCTION_BOUGHT_ESPER1, Events.AUCTION_BOUGHT_ESPER2]
        ]
    };

    public static SpriteSetDefinition VanillaBosses => _vanillaBosses;
    public static SpriteSetDefinition LocationBased => _locationBased;
}