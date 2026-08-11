using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using FF.Rando.Companion.Rendering;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;
internal class TimePeriods
{
    public IReadOnlyList<TimePeriod> Periods { get; }

    public TimePeriods(WorldMaps maps, Locations locations)
    {
        Periods =
        [
            new TimePeriod(TimePeriodType.Prehistory)
            {
                Map = maps.Get(MapType.Prehistoric).Render(true, true, false)!.Crop(new System.Drawing.Rectangle(0x30, 0, 0x570, 1024)),
                AccessRules =
                [
                    KeyItemType.GateKey,
                    GameMode.LostWorlds
                ],
                Locations =
                [
                    new(CheckLocationType.DactylNest)
                    {
                        Location = new(0x20,0x10),
                        Checks =
                        [
                            new Check("Friend to the Dactyls")
                            {
                                AccessRules =
                                [
                                    new() { KeyItems = [KeyItemType.Dreamstone], Flags = [Flag.LockedCharacters]},
                                    new() { Negate = true, Flags = [Flag.LockedCharacters]}
                                ],
                                CompleteRules = [EventType.FriendToTheDactyls]
                            },
                            new ChestsCheck("Chests") { ChestIds = [175, 176, 177] }
                        ]
                    },
                    new(CheckLocationType.ReptiteLair)
                    {
                        Location = new(0x21,0x34),
                        Checks =
                        [
                            new Check("Defeat Nizbel") { IsKeyItem = true, CompleteRules = [EventType.NizbelSpotBossDefeated]},
                            new ChestsCheck("Chests") { ChestIds = [173,174] }
                        ]
                    },
                    new(CheckLocationType.ForestMaze)
                    {
                        Location = new(0x21,0x2D),
                        Checks =
                        [
                            new ChestsCheck("Chests") { ChestIds = [158,159,160,161,162,163,164,165,166] }
                        ]
                    },
                    new(CheckLocationType.MysticMountains)
                    {
                        Location = new(0x18,0x2C),
                        Checks =
                        [
                            new ChestsCheck("Chests") { ChestIds = [157] }
                        ]
                    },
                    new(CheckLocationType.SunKeep)
                    {
                        ExistanceRules = [GameMode.Standard, GameMode.VanillaRando],
                        Location = new(0x51,0x9),
                        AccessRules =
                        [
                            new AccessRule { CanFly = true, KeyItems = [KeyItemType.GateKey, KeyItemType.MoonStone], Events = [EventType.DragonTankSpotBossDefeated] },
                        ],
                        Checks = [new Check("Charge the Moonstone") { CompleteRules = [EventType.MoonstoneDroppedOff] }]
                    },
                    new(CheckLocationType.TyranoLair)
                    {
                        ExistanceRules = [new() { Negate = true, GameMode = GameMode.LegacyOfCyrus}],
                        Location = new(0x35,0x23),
                        AccessRules = [ KeyItemType.Dreamstone ]
                    }
                ]
            },
            new TimePeriod(TimePeriodType.DarkAges)
            {
                Map = maps.Get(MapType.DarkAges).Render(true, true, false)!.Crop(new System.Drawing.Rectangle(0x30, 0, 0x570, 1024)),
                AccessRules =
                [
                    KeyItemType.GateKey,
                    EventType.MagusSpotBossDefeated,
                    EventType.RSeriesSpotBossDefeated,
                    GameMode.LostWorlds
                ],
                Locations =
                [
                    new(CheckLocationType.MtWoe)
                    {
                        Location = new(0x1a,0x17),
                        Checks =
                        [
                        ]
                    },
                    new(CheckLocationType.OceanPalace)
                    {
                        Location = new(),
                        Checks =
                        [
                        ]
                    },
                ]
            },
            new TimePeriod(TimePeriodType.KingdomOfZeal)
            {
                Map = maps.Get(MapType.KingdomOfZeal).Render(true, true, false)!.Crop(new System.Drawing.Rectangle(0x200, 0x148, 512, 376)),
                AccessRules =
                [
                    KeyItemType.GateKey,
                    EventType.MagusSpotBossDefeated,
                    EventType.RSeriesSpotBossDefeated,
                    GameMode.LostWorlds
                ],
            },
            new TimePeriod(TimePeriodType.MiddleAges)
            {
                Map = maps.Get(MapType.MiddleAges).Render(true, true, false)!.Crop(new System.Drawing.Rectangle(0x30, 0, 0x570, 1024)),
                ExistanceRules = [new() { GameMode = GameMode.LostWorlds, Negate = true }],
                Locations =
                [
                    new(CheckLocationType.ManoriaCathedral)
                    {
                        Location = new(0xc, 0x11),
                        Checks =
                        [
                            new Check("Defeat Yakra") { CompleteRules = [EventType.YakaraSpotBossDefeated]},
                            new Check("Saved by Frog") { CompleteRules = [EventType.SavedByFrog]},
                            new ChestsCheck("Front Half Chests") { ChestIds = [33,34,35] },
                            new ChestsCheck("Bromide Room Chests") { ChestIds = [99,100,101] },
                            new ChestsCheck("Disguised Royalty Chests") { ChestIds = [97,98] },
                            new ChestsCheck("Shrine Chests") { ChestIds = [102,103] },
                            new ChestsCheck("Back Half Chests") { ChestIds = [36,37,38,39] },
                            new ChestsCheck("Final Chest") { ChestIds = [96] },
                        ]
                    },
                    new(CheckLocationType.TruceCanyon)
                    {
                        Location = new(0x1b,0xd),
                        Checks =
                        [
                            new ChestsCheck { ChestIds = [27,28] },
                        ]
                    },
                    new(CheckLocationType.CursedWoods)
                    {
                        Location = new(0xf, 0x2f),
                        Checks =
                        [
                            new Check("Return the Masamune") { CompleteRules = [EventType.ReturnTheMasamune], AccessRules = [KeyItemType.Masamune] },
                            new Check("Hero's Medal Chest") { IsKeyItem = true, CompleteRules = [EventType.BurrowHeroMedalChest], AccessRules = [KeyItemType.HerosMedal] },
                            new ChestsCheck("Burrow Right Chest") { ChestIds = [43] },
                            new ChestsCheck("Forest Chests") { ChestIds = [40,41] },
                        ]
                    },
                    new(CheckLocationType.ZenanBridge)
                    {
                        ExistanceRules = [new() { Negate = true, GameMode = GameMode.LostWorlds} ],
                        Location = new(0xF, 0x1C),
                        Checks =
                        [
                            new Check("Talk to Knight-Capitan") { CompleteRules = [EventType.TalkedToKnightCaptain] },
                            new Check("Defeat Zombor") { AccessRules = [EventType.CooksRations],  CompleteRules = [EventType.ZomborSpotBossDefeated]},
                        ]
                    },
                   new(CheckLocationType.SunkenDesert)
                    {
                        Location = new(0x16,0x2c),
                        Checks =
                        [
                            new Check("Defeat Retinite") { CompleteRules = [EventType.RetiniteSpotBossDefeated]},
                        ]
                    },
                    new(CheckLocationType.FionasVilla)
                    {
                        Location = new(0x18,0x29),
                        Checks =
                        [
                            new Check("Replant the Forest") { CompleteRules = [EventType.ReplantTheForest], AccessRules = [new(){Characters=[CharacterType.Robo], Events = [EventType.RetiniteSpotBossDefeated] }]},
                            new ChestsCheck { ChestIds = [62,63] },
                        ]
                    },
                    new(CheckLocationType.DenadoroMts)
                    {
                        Location = new(0x1a,0x24),
                        Checks =
                        [
                            new Check("Defeat Masa & Mune") { IsKeyItem = true, CompleteRules = [EventType.MasamumeSpotBossDefeated]},
                            new ChestsCheck("Entrance Chests") { ChestIds = [43,44,45,54,55] },
                            new ChestsCheck("Right Side Climb Chests") { ChestIds = [48,56,57,58,59,60] },
                            new ChestsCheck("Waterfall Top Chests") { ChestIds = [51,52,53] },
                            new ChestsCheck("Waterfall Bottom Chests") { ChestIds = [49,50] },
                            new ChestsCheck("Left Side Chests") { ChestIds = [46,47,61] },
                        ]
                    },
                    new(CheckLocationType.GuardiaCastlePast)
                    {
                        Location = new(0x11,0xc),
                        Checks =
                        [
                            new Check("Talk to Cook") { ExistanceRules = [new() { Negate = true, GameMode = GameMode.LostWorlds} ],CompleteRules = [EventType.TalkedToCook, EventType.CooksRations], AccessRules = [EventType.TalkedToKnightCaptain] },
                            new Check("Cooks Rations") { ExistanceRules = [new() { Negate = true, GameMode = GameMode.LostWorlds} ], CompleteRules = [EventType.CooksRations], AccessRules = [EventType.TalkedToCook] },
                            new Check("Rescue Marle") { CompleteRules = [EventType.RescueMarle], AccessRules = [EventType.YakaraSpotBossDefeated]},
                            new ChestsCheck("King's Tower Chests") { ChestIds = [242, 29] },
                            new ChestsCheck("Queen's Tower Chest") { ChestIds = [235] },
                            new ChestsCheck("Queen's Room Chest") { ChestIds = [30] },
                            new ChestsCheck("Kitchen Chest") { ChestIds = [31] },
                            new SealedChestsCheck("Sealed Chest") { ChestIds = [1737] }
                        ]
                    },
                    new(CheckLocationType.GiantsClaw)
                    {
                        Location = new(0x3b,0x25),
                        AccessRules = [new() { CanFly = true, Events = [EventType.TalkedToToma] }],
                        Checks =
                        [
                            new Check("Defeat Rust Tyrano") { CompleteRules = [EventType.RustTryanoSpotBossDefeated]},
                            new Check("Rainbow Shell") { IsKeyItem = true, CompleteRules = [EventType.RainbowShell]},
                            new ChestsCheck("Entrance Chests") { ChestIds = [90,26] },
                            new ChestsCheck("Caverns Chests") { ChestIds = [95,93,92,91] },
                            new ChestsCheck("Kino's Cell Chest") { ChestIds = [25] },
                        ]
                    },
                    new(CheckLocationType.MagicCave)
                    {
                        Location = new(0x21,0x2a),
                        AccessRules = [new() { Characters = [CharacterType.Frog], KeyItems = [KeyItemType.Masamune] }],
                        Checks =
                        [
                            new SealedChestsCheck("Sealed Chest") { ChestIds = [968] }
                        ]
                    },
                    new(CheckLocationType.MagusCastle)
                    {
                        Location = new(0x2a,0x1d),
                        AccessRules = 
                        [
                            new() { GameMode = GameMode.Standard, Characters = [CharacterType.Frog], KeyItems = [KeyItemType.Masamune] },
                            new() { GameMode = GameMode.LegacyOfCyrus, Characters = [CharacterType.Frog, CharacterType.Magus], KeyItems = [KeyItemType.Masamune] }
                        ],
                        Checks =
                        [
                            new Check("Defeat Magus") { CompleteRules = [EventType.MagusSpotBossDefeated]},
                        ]
                    },
                    new(CheckLocationType.OzziesFort)
                    {
                        Location = new(0x3d,0x19),
                        AccessRules =
                        [
                            new() { GameMode = GameMode.LegacyOfCyrus, KeyItems =  [KeyItemType.Masamune], Characters = [CharacterType.Frog, CharacterType.Magus] },
                            new() { GameMode = GameMode.Standard, CanFly = true, KeyItems = [KeyItemType.GateKey] },
                            new() { GameMode = GameMode.Standard, CanFly = true, KeyItems = [KeyItemType.Pendant] },
                        ],
                        Checks =
                        [
                            new Check("Defeat Ozzie") { ExistanceRules = [GameMode.LegacyOfCyrus], CompleteRules = [EventType.OzzieSpotBossDefeated]},
                            new ChestsCheck("Front Half") { ChestIds = [84,85,86,87] },
                            new ChestsCheck("Back Half") { ChestIds = [88,89] },
                        ]
                    },
                    new(CheckLocationType.PorreEldersHouse)
                    {
                        Location = new(0x15,0x35),
                        Checks =
                        [
                            new SealedChestsCheck("Sealed Chests") { ChestIds = [3740, 3741] }
                        ]
                    },
                    new(CheckLocationType.TruceInnPast)
                    {
                        Location = new(0x19,0x13),
                        Checks =
                        [
                            new SealedChestsCheck("Sealed Chest") { ChestIds = [2647] }
                        ]
                    },
                    new(CheckLocationType.NorthernRuinsPast)
                    {
                        Location = new(0x4d,0x2b),
                        AccessRules = [new() { CanFly = true, KeyItems = [KeyItemType.GrandLeon] }],
                        Checks =
                        [
                            new ChestsCheck("Chests", 0) { ChestIds = [3425, 3427] },
                            new SealedChestsCheck("Sealed Chests") { ChestIds = [3376, 3377, 3378] }
                        ]
                    },
                    new(CheckLocationType.GuardiaForestPast)
                    {
                        Location = new(0x11,0x11),
                        Checks =
                        [
                            new SealedChestsCheck("Sealed Chest") { ChestIds = [3735] }
                        ]
                    },

                ]
            },
            new TimePeriod(TimePeriodType.Present)
            {
                Map = maps.Get(MapType.Present).Render(true, true, false)!.Crop(new System.Drawing.Rectangle(0x30, 0, 0x570, 1024)),
                ExistanceRules = [new() { GameMode = GameMode.LostWorlds, Negate = true }],
                Locations =
                [
                    new(CheckLocationType.GuardiaCastlePresent)
                    {
                        Location = new(0x11, 0xc),
                        Checks =
                        [
                            new Check("Defeat Dragon Tank")
                            {
                                ExistanceRules = [new() {  GameMode = GameMode.LegacyOfCyrus, Negate = true }],
                                AccessRules = [KeyItemType.Pendant],
                                CompleteRules = [EventType.DragonTankSpotBossDefeated]
                            },
                            new Check("Defeat Yakra III")
                            {
                                AccessRules = [new AccessRule { Characters = [CharacterType.Marle], KeyItems = [KeyItemType.PrismShard] }],
                                CompleteRules = [EventType.YakaraIIISpotBossDefeated]
                            },
                            new Check("King Guardia's Trial")
                            {
                                IsKeyItem = true,
                                AccessRules =
                                [
                                    new AccessRule { Characters = [CharacterType.Marle], KeyItems = [KeyItemType.PrismShard, KeyItemType.Pendant] },
                                    new AccessRule { Characters = [CharacterType.Marle], KeyItems = [KeyItemType.PrismShard, KeyItemType.GateKey] }
                                ],
                                CompleteRules = [EventType.KingsGuardiasTrial]
                            },
                            new Check("Melchior's Refinements")
                            {
                                IsKeyItem = true,
                                ExistanceRules = [new() {  GameMode = GameMode.LegacyOfCyrus, Negate = true }],
                                AccessRules = [ new AccessRule {CanFly = true, Characters = [CharacterType.Marle], KeyItems = [KeyItemType.PrismShard, KeyItemType.SunStone] }],
                                CompleteRules = [EventType.MelchiorsRefinements]
                            },
                            new ChestsCheck("King's Tower Chests") { ChestIds = [4,243] },
                            new ChestsCheck("Queen's Tower Chests") { ChestIds = [5,244] },
                            new ChestsCheck("Courtroom Tower Chests") { ChestIds = [245] },
                            new ChestsCheck("Prison Tower Chests")
                            {
                                ExistanceRules = [new() {  GameMode = GameMode.LegacyOfCyrus, Negate = true }],
                                AccessRules = [EventType.DragonTankSpotBossDefeated],
                                ChestIds = [246]
                            },
                            new ChestsCheck("Guardia Treasury")
                            {
                                AccessRules = [new AccessRule { Characters = [CharacterType.Marle], KeyItems = [KeyItemType.PrismShard] }],
                                ChestIds = [6,7,8,232,233,234]
                            },
                            new SealedChestsCheck("Guardia Castle Sealed Chest") { ChestIds = [1738] }
                        ]
                    },
                    new(CheckLocationType.SnailStop)
                    {
                        Location = new(0x1b, 0x36),
                        Checks =
                        [
                            new Check("Purchased Key Item")
                            {
                                IsKeyItem = true,
                                AccessRules = [new AccessRule {  Gold = 9900 }],
                                CompleteRules = [EventType.SnailStopPurchase]
                            },
                            new Check("Attach Epoch Wings")
                            {
                                ExistanceRules = [Flag.EpochFail],
                                AccessRules =[KeyItemType.JetsOfTime],
                                CompleteRules = [EventType.AttachEpochWings]
                            }
                        ]
                    },
                    new(CheckLocationType.HeckranCave)
                    {
                        Location = new(0x2a, 0x1c),
                        Checks =
                        [
                            new Check("Defeat Heckran") { CompleteRules = [EventType.HeckranSpotBossDefeated] },
                            new ChestsCheck("Heckran Cave Chests") { ChestIds = [11,12,13,14] },
                            new SealedChestsCheck("Heckran Cave Sealed Chest") { ChestIds = [3330] }
                        ]
                    },
                    new(CheckLocationType.ChorasInn)
                    {
                        Location = new(0x49, 0x34),
                        Checks =
                        [
                            new Check("Talk to Carpenter") { AccessRules = [AccessRule.Flight], CompleteRules = [EventType.TalkToCarpenter] }
                        ]
                    },
                    new(CheckLocationType.ChorasCarpenter)
                    {
                        Location = new(0x47, 0x35),
                        Checks =
                        [
                            new Check("Borrow Carpenter's Tools") { IsKeyItem = true, AccessRules = [EventType.TalkToCarpenter], CompleteRules = [EventType.BorrowCarpentersTools] }
                        ]
                    },
                    new(CheckLocationType.MelchiorsHut)
                    {
                        Location = new(0x2b, 0x1f),
                        AccessRules = {new AccessRule { KeyItems = [KeyItemType.BentHilt, KeyItemType.BentSword] } },
                        Checks =
                        [
                            new Check("Reforge the Masamune") { CompleteRules = [KeyItemType.Masamune], }
                        ]
                    },
                    new(CheckLocationType.LuccasHouse)
                    {
                        Location = new(0x15, 0x1b),
                        AccessRules = [EventType.HeckranSpotBossDefeated],
                        Checks =
                        [
                            new Check("Taban's Gift") {IsKeyItem = true,  CompleteRules = [EventType.TabansGift]}
                        ]
                    },
                    new(CheckLocationType.ForestRuins)
                    {
                        Location = new(0x3e, 0x10),
                        Checks =
                        [
                            new ChestsCheck("Chests") { ChestIds = [10]},
                            new SealedChestsCheck("Blue Pyramid") { ChestIds = [3328]}
                        ]
                    },
                    new(CheckLocationType.TruceMayorsHouse)
                    {
                        Location = new(0x17, 0x14),
                        Checks =
                        [
                            new ChestsCheck("Chests") { ChestIds = [2,3]},
                        ]
                    },
                    new(CheckLocationType.TruceInnPresent)
                    {
                        Location = new(0x19, 0x13),
                        Checks =
                        [
                            new SealedChestsCheck("Sealed Chest") { ChestIds = [2645]}
                        ]
                    },
                    new(CheckLocationType.PorreMayorsHouse)
                    {
                        Location = new(0x15, 0x35),
                        Checks =
                        [
                            new ChestsCheck("Chests") { ChestIds = [15]},
                            new SealedChestsCheck("Sealed Chests") { ChestIds = [3726,3727]}
                        ]
                    },
                    new(CheckLocationType.NorthernRuinsPresent)
                    {
                        AccessRules = 
                        [
                            new() {GameMode = GameMode.Standard, CanFly = true, KeyItems = [KeyItemType.GrandLeon] },
                            new() {GameMode = GameMode.LegacyOfCyrus, CanFly = true, KeyItems = [KeyItemType.GrandLeon], Characters = [CharacterType.Magus, CharacterType.Frog] }
                        ],
                        Location = new(0x4d, 0x2b),
                        Checks =
                        [
                            new ChestsCheck("Upstairs Chest", 0) { ChestIds = [3426]},
                            new ChestsCheck("Basement Chest", 0) { AccessRules = [CharacterType.Frog], ChestIds = [3424] },
                            new SealedChestsCheck("Sealed Chests") { ChestIds = [3429, 3430, 3431] }
                        ]
                    },
                    new(CheckLocationType.GuardiaForestPresent)
                    {
                        Location = new(0x11, 0xf),
                        Checks =
                        [
                            new SealedChestsCheck("Sealed Chest") { ChestIds = [3725] }
                        ]
                    },
                    new(CheckLocationType.FionasShrine)
                    {
                        Location = new(0x16, 0x2a),
                        Checks =
                        [
                            new Check("Retrieved Robo") { IsKeyItem = true, AccessRules = [EventType.ReplantTheForest], CompleteRules = [EventType.WokeRoboUp]}
                        ]
                    },
                    new(CheckLocationType.WestCape)
                    {
                        Location = new(0x40, 0x23),
                        Checks =
                        [
                            new Check("Toma's Grave") { AccessRules = [KeyItemType.TomasPop], CompleteRules = [EventType.TalkedToToma] }
                        ]
                    }
                ]
            },
            new TimePeriod(TimePeriodType.Future)
            {
                Map = maps.Get(MapType.Future).Render(true, true, false)!.Crop(new System.Drawing.Rectangle(0x30, 0, 0x570, 1024)),
                ExistanceRules = [new() { GameMode = GameMode.LegacyOfCyrus, Negate = true}],
                AccessRules =
                [
                    new() { GameMode = GameMode.LostWorlds },
                    new() { GameMode = GameMode.Standard, Events = [EventType.DragonTankSpotBossDefeated] },
                    new() { GameMode = GameMode.VanillaRando, Events = [EventType.DragonTankSpotBossDefeated] },
                ],
                Locations =
                [
                    new(CheckLocationType.ArrisDome)
                    {
                        Location = new(0x1b, 0x16),
                        Checks =
                        [
                            new Check("Defeat Guardian, Activate the Computer") { IsKeyItem = true, CompleteRules = [EventType.GuardianSpotBossDefeated, EventType.ActivateComputer] },
                            new ChestsCheck("Chests") { ChestIds = [113, 208] },
                            new SealedChestsCheck("Sealed Door") { ChestIds = [114, 115, 116, 117]},
                        ]
                    },
                    new(CheckLocationType.ProtoDome)
                    {
                        AccessRules =
                        [
                            new() { Events = [EventType.RSeriesSpotBossDefeated], Flags = [Flag.LockedCharacters]},
                            new() { Negate = true, Flags = [Flag.LockedCharacters]}
                        ],
                        Checks = [new Check("Fix Robo") {  CompleteRules = [EventType.FixRobo] }],
                        Location = new(0x38, 0x19),
                    },
                    new(CheckLocationType.Factory)
                    {
                        Location = new(0x39, 0x12),
                        Checks =
                        [
                            new Check("Defeat R-Series") { CompleteRules = [EventType.RSeriesSpotBossDefeated]},
                            new ChestsCheck("Left Side Chests") { ChestIds = [121, 122, 123, 135]},
                            new ChestsCheck("Right Side Chests") { ChestIds = [124, 125, 126, 127, 128, 129, 130, 147, 148]}
                        ]
                    },
                    new(CheckLocationType.GenoDome)
                    {
                        Location = new(0x48, 0x2e),
                        AccessRules = [AccessRule.Flight],
                        Checks =
                        [
                            new Check("Defeat Mother Brain") { IsKeyItem = true, CompleteRules = [EventType.MotherBrainSpotBossDefeated]},
                            new ChestsCheck("First Floor Chests") { ChestIds = [139, 140, 141, 142, 143, 144, 145, 146]},
                            new ChestsCheck("Second Floor Chests") { ChestIds = [153, 154, 155, 156]},
                        ]
                    },
                    new(CheckLocationType.SunKeepFuture)
                    {
                        Location = new(0x4e, 0x8),
                        AccessRules = [AccessRule.Flight],
                        Checks = [new Check("Retrieve Sun Stone") { AccessRules = [ EventType.MoonstoneDroppedOff ], CompleteRules = [KeyItemType.SunStone] } ]
                    },
                    new(CheckLocationType.SunPalace)
                    {
                        AccessRules = [AccessRule.Flight],
                        Location = new(0x1E, 0x37),
                        Checks = [new Check("Defeat Son of Sun") { CompleteRules = [EventType.SonOfSunSpotBossDefeated] }],
                    },
                    new(CheckLocationType.DeathPeak)
                    {
                        AccessRules = [new() { KeyItems = [KeyItemType.ChronoTrigger, KeyItemType.Clone] }],
                        Checks = [new Check("Black Omen Path") { IsGoMode = true, CompleteRules = [EventType.ZealSpotBossDefeated] }],
                        Location = new(0x2a, 0x23),
                    },
                    new(CheckLocationType.TrannDome)
                    {
                        Location = new(0xB,  0x22),
                        Checks = [new SealedChestsCheck("Sewers Chests") { ChestIds = [107, 108] }]
                    },
                    new(CheckLocationType.BangorDome)
                    {
                        Location = new(0xD,  0x1B),
                        Checks = [new SealedChestsCheck("Sewers Chests") { ChestIds = [104, 105, 106] }]
                    },
                    new(CheckLocationType.Sewers)
                    {
                        Location = new(0x22, 0x17),
                        Checks = [new ChestsCheck("Sewers Chests") { ChestIds = [132, 133, 134] }]
                    },
                    new(CheckLocationType.Lab16)
                    {
                        Location = new(0x10, 0x17),
                        Checks = [new ChestsCheck("Lab 16 Chests") { ChestIds = [109, 110, 110, 112] }]
                    },
                    new(CheckLocationType.Lab32)
                    {
                        Location = new(0x1f, 0x11),
                        Checks = [new ChestsCheck("Lab 32 Chests") { ChestIds = [119] }]
                    },
                    //new(CheckLocationType.KeepersDome)
                    //{
                    //    Location = new(0x26, 0x26),
                    //    Checks =
                    //    [
                    //        new Check("Chests")
                    //        {
                    //            AccessRules = [ new() {  }]
                    //        }
                    //    ]
                    //},
                ]
            },
            new TimePeriod(TimePeriodType.EndOfTime)
            {
                ExistanceRules = [new() { GameMode = GameMode.LostWorlds, Negate = true }],
                Map = locations.Get(LocationType.EndofTime).Render(true, true, false)!.Crop(new System.Drawing.Rectangle(0,0,0x300, 0x1f0)),
                AccessRules = [KeyItemType.GateKey, EventType.MagusSpotBossDefeated, EventType.RSeriesSpotBossDefeated]
            }
        ];
    }

    public bool Update(State state)
    {
        var ret = false;
        foreach (var period in Periods)
            ret |= period.Update(state);

        return ret;
    }
}
