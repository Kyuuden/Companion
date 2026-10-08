using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Rendering;
using FF.Rando.Companion.Games.WorldsCollide.RomData;
using FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Rendering.Transforms;
using KGySoft.CoreLibraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

internal class SerializedSpriteSet : ISpriteSet, IDisposable
{
    private readonly Dictionary<Statistic, Func<ISprite?>> _statisticSpritesGetters = [];
    private readonly Dictionary<DragonType, Func<ISprite?>> _dragonSpritesGetters = [];
    private readonly Dictionary<EventType, Func<ISprite?>> _checkSpritesGetters = [];
    private readonly Dictionary<Statistic, ISprite?> _statisticSprites = [];
    private readonly Dictionary<DragonType, ISprite?> _dragonSprites = [];
    private readonly Dictionary<EventType, ISprite?> _checkSprites = [];

    private readonly Sprites _sprites;
    private readonly LocationMaps _maps;
    private readonly Font _font;
    private bool disposedValue;

    public SerializedSpriteSet(Sprites sprites, Font font, LocationMaps maps, SpriteSetDefinition spriteSetDefinition)
    {
        _sprites = sprites;
        _font = font;
        _maps = maps;

        _dragonSpritesGetters[DragonType.DIRT_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.DirtDragon);
        _dragonSpritesGetters[DragonType.GOLD_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.GoldDragon);
        _dragonSpritesGetters[DragonType.ICE_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.IceDragon);
        _dragonSpritesGetters[DragonType.RED_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.RedDragon);
        _dragonSpritesGetters[DragonType.WHITE_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.WhiteDragon);
        _dragonSpritesGetters[DragonType.BLUE_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.BlueDragon);
        _dragonSpritesGetters[DragonType.SKULL_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.SkullDragon);
        _dragonSpritesGetters[DragonType.STORM_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.StormDragon);

        _checkSpritesGetters[EventType.TERRA_IN_PARTY] = GetSprite(spriteSetDefinition.Terra);
        _checkSpritesGetters[EventType.LOCKE_IN_PARTY] = GetSprite(spriteSetDefinition.Locke);
        _checkSpritesGetters[EventType.EDGAR_IN_PARTY] = GetSprite(spriteSetDefinition.Edgar);
        _checkSpritesGetters[EventType.SABIN_IN_PARTY] = GetSprite(spriteSetDefinition.Sabin);
        _checkSpritesGetters[EventType.SHADOW_IN_PARTY] = GetSprite(spriteSetDefinition.Shadow);
        _checkSpritesGetters[EventType.CYAN_IN_PARTY] = GetSprite(spriteSetDefinition.Cyan);
        _checkSpritesGetters[EventType.GAU_IN_PARTY] = GetSprite(spriteSetDefinition.Gau);
        _checkSpritesGetters[EventType.CELES_IN_PARTY] = GetSprite(spriteSetDefinition.Celes);
        _checkSpritesGetters[EventType.SETZER_IN_PARTY] = GetSprite(spriteSetDefinition.Setzer);
        _checkSpritesGetters[EventType.MOG_IN_PARTY] = GetSprite(spriteSetDefinition.Mog);
        _checkSpritesGetters[EventType.STRAGO_IN_PARTY] = GetSprite(spriteSetDefinition.Strago);
        _checkSpritesGetters[EventType.RELM_IN_PARTY] = GetSprite(spriteSetDefinition.Relm);
        _checkSpritesGetters[EventType.GOGO_IN_PARTY] = GetSprite(spriteSetDefinition.Gogo);
        _checkSpritesGetters[EventType.UMARO_IN_PARTY] = GetSprite(spriteSetDefinition.Umaro);

        _checkSpritesGetters[EventType.DEFEATED_WHELK] = GetSprite(spriteSetDefinition.WhelkGate);
        _checkSpritesGetters[EventType.RODE_RAFT_LETE_RIVER] = GetSprite(spriteSetDefinition.LeteRiver);
        _checkSpritesGetters[EventType.BLOCK_SEALED_GATE] = GetSprite(spriteSetDefinition.SealedGate);
        _checkSpritesGetters[EventType.GOT_ZOZO_REWARD] = GetSprite(spriteSetDefinition.ZozoTower);
        _checkSpritesGetters[EventType.RECRUITED_TERRA_MOBLIZ] = GetSprite(spriteSetDefinition.MoblizAttack);
        _checkSpritesGetters[EventType.DEFEATED_TUNNEL_ARMOR] = GetSprite(spriteSetDefinition.SouthFigaroCave);
        _checkSpritesGetters[EventType.GOT_RAGNAROK] = GetSprite(spriteSetDefinition.NarsheWeaponShop);
        _checkSpritesGetters[EventType.GOT_BOTH_REWARDS_WEAPON_SHOP] = GetSprite(spriteSetDefinition.NarsheWeaponShopMines);
        _checkSpritesGetters[EventType.RECRUITED_LOCKE_PHOENIX_CAVE] = GetSprite(spriteSetDefinition.PhoenixCave);
        _checkSpritesGetters[EventType.NAMED_EDGAR] = GetSprite(spriteSetDefinition.FigaroCastleThrone);
        _checkSpritesGetters[EventType.DEFEATED_TENTACLES_FIGARO] = GetSprite(spriteSetDefinition.FigaroCastleEngine);
        _checkSpritesGetters[EventType.GOT_RAIDEN] = GetSprite(spriteSetDefinition.AncientCastle);
        _checkSpritesGetters[EventType.DEFEATED_VARGAS] = GetSprite(spriteSetDefinition.MtKolts);
        _checkSpritesGetters[EventType.FINISHED_COLLAPSING_HOUSE] = GetSprite(spriteSetDefinition.CollapsingHouse);
        _checkSpritesGetters[EventType.NAMED_GAU] = GetSprite(spriteSetDefinition.BarenFalls);
        _checkSpritesGetters[EventType.FINISHED_IMPERIAL_CAMP] = GetSprite(spriteSetDefinition.ImperialCamp);
        _checkSpritesGetters[EventType.GOT_PHANTOM_TRAIN_REWARD] = GetSprite(spriteSetDefinition.PhantomTrain);
        _checkSpritesGetters[EventType.RECRUITED_SHADOW_GAU_FATHER_HOUSE] = GetSprite(spriteSetDefinition.GauFatherHouse);
        _checkSpritesGetters[EventType.RECRUITED_SHADOW_FLOATING_CONTINENT] = GetSprite(spriteSetDefinition.FloatingContinentArrival);
        _checkSpritesGetters[EventType.DEFEATED_ATMAWEAPON] = GetSprite(spriteSetDefinition.FloatingContinentBeast);
        _checkSpritesGetters[EventType.FINISHED_FLOATING_CONTINENT] = GetSprite(spriteSetDefinition.FloatingContinentEscape);
        _checkSpritesGetters[EventType.DEFEATED_SR_BEHEMOTH] = GetSprite(spriteSetDefinition.VeldtCave);
        _checkSpritesGetters[EventType.FINISHED_DOMA_WOB] = GetSprite(spriteSetDefinition.DomaSiege);
        _checkSpritesGetters[EventType.DEFEATED_STOOGES] = GetSprite(spriteSetDefinition.DomaDreamDoor);
        _checkSpritesGetters[EventType.FINISHED_DOMA_WOR] = GetSprite(spriteSetDefinition.DomaDreamAwaken);
        _checkSpritesGetters[EventType.GOT_ALEXANDR] = GetSprite(spriteSetDefinition.DomaDreamThrone);
        _checkSpritesGetters[EventType.FINISHED_MT_ZOZO] = GetSprite(spriteSetDefinition.MtZozo);
        _checkSpritesGetters[EventType.VELDT_REWARD_OBTAINED] = GetSprite(spriteSetDefinition.Veldt);
        _checkSpritesGetters[EventType.GOT_SERPENT_TRENCH_REWARD] = GetSprite(spriteSetDefinition.SerpentTrench);
        _checkSpritesGetters[EventType.FREED_CELES] = GetSprite(spriteSetDefinition.SouthFigaroPrisoner);
        _checkSpritesGetters[EventType.GOT_IFRIT_SHIVA] = GetSprite(spriteSetDefinition.MagitekFactoryTrash);
        _checkSpritesGetters[EventType.DEFEATED_NUMBER_024] = GetSprite(spriteSetDefinition.MagitekFactoryGuard);
        _checkSpritesGetters[EventType.DEFEATED_CRANES] = GetSprite(spriteSetDefinition.MagitekFactoryFinish);
        _checkSpritesGetters[EventType.FINISHED_OPERA_DISRUPTION] = GetSprite(spriteSetDefinition.OperaHouseDisruption);
        _checkSpritesGetters[EventType.RECRUITED_SHADOW_KOHLINGEN] = GetSprite(spriteSetDefinition.KohlingenCafe);
        _checkSpritesGetters[EventType.DEFEATED_DULLAHAN] = GetSprite(spriteSetDefinition.DarylsTomb);
        _checkSpritesGetters[EventType.CHASING_LONE_WOLF7] = GetSprite(spriteSetDefinition.LoneWolfChase);
        _checkSpritesGetters[EventType.GOT_BOTH_REWARDS_LONE_WOLF] = GetSprite(spriteSetDefinition.LoneWolfMoogleRoom);
        _checkSpritesGetters[EventType.COMPLETED_MOOGLE_DEFENSE] = GetSprite(spriteSetDefinition.MoogleDefense);
        _checkSpritesGetters[EventType.DEFEATED_FLAME_EATER] = GetSprite(spriteSetDefinition.BurningHouse);
        _checkSpritesGetters[EventType.DEFEATED_HIDON] = GetSprite(spriteSetDefinition.EbotsRock);
        _checkSpritesGetters[EventType.DEFEATED_MAGIMASTER] = GetSprite(spriteSetDefinition.FanaticsTowerLeader);
        _checkSpritesGetters[EventType.RECRUITED_STRAGO_FANATICS_TOWER] = GetSprite(spriteSetDefinition.FanaticsTowerFollower);
        _checkSpritesGetters[EventType.DEFEATED_ULTROS_ESPER_MOUNTAIN] = GetSprite(spriteSetDefinition.EsperMountain);
        _checkSpritesGetters[EventType.DEFEATED_CHADARNOOK] = GetSprite(spriteSetDefinition.OwzersMansion);
        _checkSpritesGetters[EventType.RECRUITED_GOGO_WOR] = GetSprite(spriteSetDefinition.ZoneEater);
        _checkSpritesGetters[EventType.RECRUITED_UMARO_WOR] = GetSprite(spriteSetDefinition.UmarosCave);
        _checkSpritesGetters[EventType.FINISHED_NARSHE_BATTLE] = GetSprite(spriteSetDefinition.NarsheBattle);
        _checkSpritesGetters[EventType.BOUGHT_ESPER_TZEN] = GetSprite(spriteSetDefinition.TzenThief);
        _checkSpritesGetters[EventType.DEFEATED_DOOM_GAZE] = GetSprite(spriteSetDefinition.SearchTheSkies);
        _checkSpritesGetters[EventType.GOT_TRITOCH] = GetSprite(spriteSetDefinition.TritochCliff);
        _checkSpritesGetters[EventType.AUCTION_BOUGHT_ESPER1] = GetSprite(spriteSetDefinition.JidoorAuctionHouse1);
        _checkSpritesGetters[EventType.AUCTION_BOUGHT_ESPER2] = GetSprite(spriteSetDefinition.JidoorAuctionHouse2);
        _checkSpritesGetters[EventType.DEFEATED_ATMA] = GetSprite(spriteSetDefinition.KefkasTowerCellBeast);

        _checkSpritesGetters[EventType.DEFEATED_PHOENIX_CAVE_DRAGON] = GetSprite(spriteSetDefinition.PhoenixCaveDragon);
        _checkSpritesGetters[EventType.DEFEATED_ANCIENT_CASTLE_DRAGON] = GetSprite(spriteSetDefinition.AncientCasteDragon);
        _checkSpritesGetters[EventType.DEFEATED_MT_ZOZO_DRAGON] = GetSprite(spriteSetDefinition.MtZozoDragon);
        _checkSpritesGetters[EventType.DEFEATED_OPERA_HOUSE_DRAGON] = GetSprite(spriteSetDefinition.OperaHouseDragon);
        _checkSpritesGetters[EventType.DEFEATED_FANATICS_TOWER_DRAGON] = GetSprite(spriteSetDefinition.FanaticsTowerDragon);
        _checkSpritesGetters[EventType.DEFEATED_NARSHE_DRAGON] = GetSprite(spriteSetDefinition.NarsheDragon);
        _checkSpritesGetters[EventType.DEFEATED_KEFKA_TOWER_DRAGON_G] = GetSprite(spriteSetDefinition.KefkasTowerMiddlePathDragon);
        _checkSpritesGetters[EventType.DEFEATED_KEFKA_TOWER_DRAGON_S] = GetSprite(spriteSetDefinition.KefkasTowerRightPathDragon);
        _checkSpritesGetters[EventType.GODDESS_STATUE_KEFKA_TOWER] = GetSprite(spriteSetDefinition.GoddessStatue);
        _checkSpritesGetters[EventType.DOOM_STATUE_KEFKA_TOWER] = GetSprite(spriteSetDefinition.DoomStatue);
        _checkSpritesGetters[EventType.POLTRGEIST_STATUE_KEFKA_TOWER] = GetSprite(spriteSetDefinition.PoltrgeistStatue);
        _checkSpritesGetters[EventType.UNLOCKED_KT_SKIP] = GetSprite(spriteSetDefinition.KefkaTowerSkipUnlocked);
        _checkSpritesGetters[EventType.UNLOCKED_FINAL_KEFKA] = GetSprite(spriteSetDefinition.FinalKefkaUnlocked);

        _statisticSpritesGetters[Statistic.CharacterCount] = GetSprite(spriteSetDefinition.CharacterCount);
        _statisticSpritesGetters[Statistic.EsperCount] = GetSprite(spriteSetDefinition.EsperCount);
        _statisticSpritesGetters[Statistic.DragonCount] = GetSprite(spriteSetDefinition.DragonCount);
        _statisticSpritesGetters[Statistic.BossCount] = GetSprite(spriteSetDefinition.BossCount);
        _statisticSpritesGetters[Statistic.CheckCount] = GetSprite(spriteSetDefinition.CheckCount);
        _statisticSpritesGetters[Statistic.ChestCount] = GetSprite(spriteSetDefinition.ChestCount);
    }

    private Func<ISprite?> GetSprite(SpriteDefinition? spriteDefinition)
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(ISpriteSet));

        if (spriteDefinition == null)
            return () => null;

        Func<ISprite?> spriteGetter;
        List<Func<ISprite, ISprite>> transformFuncs = [];
        spriteGetter = spriteDefinition.Source switch
        {
            SpriteSource.Character => () => _sprites.Characters.Get((CharacterEx)spriteDefinition.Id, (Pose)spriteDefinition.SubId),
            SpriteSource.Portrait => () => _sprites.Portraits.Get((Character)spriteDefinition.Id),
            SpriteSource.Background => () => _sprites.Backgrounds.Get((TileSet)spriteDefinition.Id),
            SpriteSource.Item => () => _sprites.Items.Get((Item)spriteDefinition.Id),
            SpriteSource.Monster => () => _sprites.Combat.Get((Monster)spriteDefinition.Id),
            SpriteSource.Boss => () => spriteDefinition.Id == (int)Boss.GhostTrain
                    ? _sprites.Backgrounds.Get(TileSet.GhostTrain)
                    : _sprites.Combat.Get((Boss)spriteDefinition.Id),
            SpriteSource.Esper => () => _sprites.Combat.Get((Esper)spriteDefinition.Id),
            SpriteSource.Map => () => _maps.Get((MapLocation)spriteDefinition.Id),
            _ => () => null
        };

        foreach (var transform in spriteDefinition.Transforms)
        {
            switch (transform)
            {
                case Crop crop:
                    transformFuncs.Add(source => source.Crop(crop.GetRectangle()));
                    break;
                case Overlay overlay:
                    transformFuncs.Add(source =>
                    {
                        var o = GetSprite(overlay.OverlayedSprite)();
                        if (o == null)
                            return source;
                        return source.Overlay(o, overlay.GetDestination());
                    });
                    break;
                case Resize resize:
                    transformFuncs.Add(source => source.Resize(resize.GetSize()));
                    break;
                case Pad pad:
                    transformFuncs.Add(source => source.Pad(pad.GetSize(), pad.HorizontalAlignment, pad.VerticalAlignment));
                    break;
                case ManualPad mp:
                    transformFuncs.Add(source => source.Pad(mp.Left, mp.Top, mp.Width, mp.Height));
                    break;
                case Greyscale:
                    transformFuncs.Add(source => source.Greyscale());
                    break;
                case FlipHorizontal:
                    transformFuncs.Add(source => source.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX));
                    break;
                case FlipVertical:
                    transformFuncs.Add(source => source.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY));
                    break;
                case AdjustBrightness adjust:
                    transformFuncs.Add(source => source.AdjustBrightness(adjust.Adjustment));
                    break;
                case AlternateDisabledSprite alternateDisabledSprite:
                    transformFuncs.Add(source =>
                    {
                        var o = GetSprite(alternateDisabledSprite.DisabledSprite)();
                        if (o == null)
                            return source;

                        return new CustomGreyscaleSrpite(source, o);
                    });
                    break;
                case SetGreyscaleBrightness setGreyscaleBrightness:
                    transformFuncs.Add(source =>
                    {
                        source.GreyscaleBrightnessAdjustment = setGreyscaleBrightness.Brightness;
                        return source;
                    });
                    break;
            }
        }

        return () =>
        {
            var sprite = spriteGetter();
            if (sprite == null)
                return null;

            foreach (var transform in transformFuncs)
                sprite = transform(sprite);
            return sprite;
        };
    }

    private static ISprite? GetOrCreate<T>(T value, Dictionary<T, ISprite?> cache, Dictionary<T, Func<ISprite?>> creators)
    {
        var sprite = cache.GetValueOrDefault(value);

        if (sprite != null && !sprite.IsDisposed)
            return sprite;

        if (!creators.TryGetValue(value, out var creator))
            return null;

        cache[value] = creator();
        return cache[value];
    }

    public ISprite? Get(EventType @event) => GetOrCreate(@event, _checkSprites, _checkSpritesGetters);

    public ISprite? Get(Statistic statistic) => GetOrCreate(statistic, _statisticSprites, _statisticSpritesGetters);

    public ISprite? Get(DragonType dragon) => GetOrCreate(dragon, _dragonSprites, _dragonSpritesGetters);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var value in _statisticSprites.Values.OfType<ITemporarySprite>()) value?.Dispose();
                _statisticSprites.Clear();
                foreach (var value in _dragonSprites.Values.OfType<ITemporarySprite>()) value?.Dispose();
                _dragonSprites.Clear();
                foreach (var value in _checkSprites.Values.OfType<ITemporarySprite>()) value?.Dispose();
                _checkSprites.Clear();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
