using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;
internal class SpriteSetDefinition
{
    // Terra
    public SpriteDefinition? WhelkGate { get; set; }
    public SpriteDefinition? LeteRiver { get; set; }
    public SpriteDefinition? SealedGate { get; set; }
    public SpriteDefinition? ZozoTower { get; set; }
    public SpriteDefinition? MoblizAttack { get; set; }

    // Locke
    public SpriteDefinition? SouthFigaroCave { get; set; }
    public SpriteDefinition? NarsheWeaponShop { get; set; }
    public SpriteDefinition? NarsheWeaponShopMines { get; set; }
    public SpriteDefinition? PhoenixCave { get; set; }

    // Edgar
    public SpriteDefinition? FigaroCastleThrone { get; set; }
    public SpriteDefinition? FigaroCastleEngine { get; set; }
    public SpriteDefinition? AncientCastle { get; set; }

    // Sabin
    public SpriteDefinition? MtKolts { get; set; }
    public SpriteDefinition? CollapsingHouse { get; set; }
    public SpriteDefinition? BarenFalls { get; set; }
    public SpriteDefinition? ImperialCamp { get; set; }
    public SpriteDefinition? PhantomTrain { get; set; }

    // Shadow
    public SpriteDefinition? GauFatherHouse { get; set; }
    public SpriteDefinition? FloatingContinentArrival { get; set; }
    public SpriteDefinition? FloatingContinentBeast { get; set; }
    public SpriteDefinition? FloatingContinentEscape { get; set; }
    public SpriteDefinition? VeldtCave { get; set; }

    // Cyan
    public SpriteDefinition? DomaSiege { get; set; }
    public SpriteDefinition? DomaDreamDoor { get; set; }
    public SpriteDefinition? DomaDreamAwaken { get; set; }
    public SpriteDefinition? DomaDreamThrone { get; set; }
    public SpriteDefinition? MtZozo { get; set; }

    // Gau
    public SpriteDefinition? Veldt { get; set; }
    public SpriteDefinition? SerpentTrench { get; set; }

    // Celes
    public SpriteDefinition? SouthFigaroPrisoner { get; set; }
    public SpriteDefinition? MagitekFactoryTrash { get; set; }
    public SpriteDefinition? MagitekFactoryGuard { get; set; }
    public SpriteDefinition? MagitekFactoryFinish { get; set; }
    public SpriteDefinition? OperaHouseDisruption { get; set; }

    // Setzer
    public SpriteDefinition? KohlingenCafe { get; set; }
    public SpriteDefinition? DarylsTomb { get; set; }

    // Mog
    public SpriteDefinition? LoneWolfChase { get; set; }
    public SpriteDefinition? LoneWolfMoogleRoom { get; set; }
    public SpriteDefinition? MoogleDefense { get; set; }

    // Strago
    public SpriteDefinition? BurningHouse { get; set; }
    public SpriteDefinition? EbotsRock { get; set; }
    public SpriteDefinition? FanaticsTowerLeader { get; set; }
    public SpriteDefinition? FanaticsTowerFollower { get; set; }

    // Relm
    public SpriteDefinition? EsperMountain { get; set; }
    public SpriteDefinition? OwzersMansion { get; set; }

    // Gogo
    public SpriteDefinition? ZoneEater { get; set; }

    // Umaro
    public SpriteDefinition? UmarosCave { get; set; }

    // Other
    public SpriteDefinition? NarsheBattle { get; set; }
    public SpriteDefinition? TzenThief { get; set; }
    public SpriteDefinition? SearchTheSkies { get; set; }
    public SpriteDefinition? TritochCliff { get; set; }
    public SpriteDefinition? JidoorAuctionHouse1 { get; set; }
    public SpriteDefinition? JidoorAuctionHouse2 { get; set; }
    public SpriteDefinition? KefkasTowerCellBeast { get; set; }

    // Dragon Locations
    public SpriteDefinition? PhoenixCaveDragon { get; set; }
    public SpriteDefinition? AncientCasteDragon { get; set; }
    public SpriteDefinition? MtZozoDragon { get; set; }
    public SpriteDefinition? OperaHouseDragon { get; set; }
    public SpriteDefinition? FanaticsTowerDragon { get; set; }
    public SpriteDefinition? NarsheDragon { get; set; }
    public SpriteDefinition? KefkasTowerMiddlePathDragon { get; set; }
    public SpriteDefinition? KefkasTowerRightPathDragon { get; set; }

    // Dragons
    public SpriteDefinition? BlueDragon { get; set; }
    public SpriteDefinition? DirtDragon { get; set; }
    public SpriteDefinition? GoldDragon { get; set; }
    public SpriteDefinition? IceDragon { get; set; }
    public SpriteDefinition? RedDragon { get; set; }
    public SpriteDefinition? SkullDragon { get; set; }
    public SpriteDefinition? StormDragon { get; set; }
    public SpriteDefinition? WhiteDragon { get; set; }

    // Characters
    public SpriteDefinition? Terra { get; set; }
    public SpriteDefinition? Locke { get; set; }
    public SpriteDefinition? Edgar { get; set; }
    public SpriteDefinition? Sabin { get; set; }
    public SpriteDefinition? Shadow { get; set; }
    public SpriteDefinition? Cyan { get; set; }
    public SpriteDefinition? Gau { get; set; }
    public SpriteDefinition? Celes { get; set; }
    public SpriteDefinition? Setzer { get; set; }
    public SpriteDefinition? Mog { get; set; }
    public SpriteDefinition? Strago { get; set; }
    public SpriteDefinition? Relm { get; set; }
    public SpriteDefinition? Gogo { get; set; }
    public SpriteDefinition? Umaro { get; set; }

    // Statistics
    public SpriteDefinition? CharacterCount { get; set; }
    public SpriteDefinition? EsperCount { get; set; }
    public SpriteDefinition? DragonCount { get; set; }
    public SpriteDefinition? BossCount { get; set; }
    public SpriteDefinition? CheckCount { get; set; }
    public SpriteDefinition? ChestCount { get; set; }

    // Related Events
    public List<List<Events>> RelatedEvents { get; set; } = [];
}