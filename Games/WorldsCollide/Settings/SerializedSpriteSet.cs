using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.RomData;
using FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Rendering.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Settings;

internal class SerializedSpriteSet : ISpriteSet, IDisposable
{
    private readonly Dictionary<Statistic, Func<ISprite?>> _statisticSpritesGetters = [];
    private readonly Dictionary<Dragons, Func<ISprite?>> _dragonSpritesGetters = [];
    private readonly Dictionary<Events, Func<ISprite?>> _checkSpritesGetters = [];
    private readonly Dictionary<Statistic, ISprite?> _statisticSprites = [];
    private readonly Dictionary<Dragons, ISprite?> _dragonSprites = [];
    private readonly Dictionary<Events, ISprite?> _checkSprites = [];
    private readonly List<List<Events>> _relatedEvents = [];

    private readonly Sprites _sprites;
    private readonly Font _font;
    private bool disposedValue;

    public IEnumerable<IEnumerable<Events>> RelatedEvents => _relatedEvents;

    public SerializedSpriteSet(Sprites sprites, Font font, SpriteSetDefinition spriteSetDefinition)
    {
        _sprites = sprites;
        _font = font;

        _dragonSpritesGetters[Dragons.DIRT_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.DirtDragon);
        _dragonSpritesGetters[Dragons.GOLD_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.GoldDragon);
        _dragonSpritesGetters[Dragons.ICE_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.IceDragon);
        _dragonSpritesGetters[Dragons.RED_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.RedDragon);
        _dragonSpritesGetters[Dragons.WHITE_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.WhiteDragon);
        _dragonSpritesGetters[Dragons.BLUE_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.BlueDragon);
        _dragonSpritesGetters[Dragons.SKULL_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.SkullDragon);
        _dragonSpritesGetters[Dragons.STORM_DRAGON_DEFEATED] = GetSprite(spriteSetDefinition.StormDragon);

        _checkSpritesGetters[Events.TERRA_IN_PARTY] = GetSprite(spriteSetDefinition.Terra);
        _checkSpritesGetters[Events.LOCKE_IN_PARTY] = GetSprite(spriteSetDefinition.Locke);
        _checkSpritesGetters[Events.EDGAR_IN_PARTY] = GetSprite(spriteSetDefinition.Edgar);
        _checkSpritesGetters[Events.SABIN_IN_PARTY] = GetSprite(spriteSetDefinition.Sabin);
        _checkSpritesGetters[Events.SHADOW_IN_PARTY] = GetSprite(spriteSetDefinition.Shadow);
        _checkSpritesGetters[Events.CYAN_IN_PARTY] = GetSprite(spriteSetDefinition.Cyan);
        _checkSpritesGetters[Events.GAU_IN_PARTY] = GetSprite(spriteSetDefinition.Gau);
        _checkSpritesGetters[Events.CELES_IN_PARTY] = GetSprite(spriteSetDefinition.Celes);
        _checkSpritesGetters[Events.SETZER_IN_PARTY] = GetSprite(spriteSetDefinition.Setzer);
        _checkSpritesGetters[Events.MOG_IN_PARTY] = GetSprite(spriteSetDefinition.Mog);
        _checkSpritesGetters[Events.STRAGO_IN_PARTY] = GetSprite(spriteSetDefinition.Strago);
        _checkSpritesGetters[Events.RELM_IN_PARTY] = GetSprite(spriteSetDefinition.Relm);
        _checkSpritesGetters[Events.GOGO_IN_PARTY] = GetSprite(spriteSetDefinition.Gogo);
        _checkSpritesGetters[Events.UMARO_IN_PARTY] = GetSprite(spriteSetDefinition.Umaro);
        _checkSpritesGetters[Events.DEFEATED_WHELK] = GetSprite(spriteSetDefinition.WhelkGate);
        _checkSpritesGetters[Events.RODE_RAFT_LETE_RIVER] = GetSprite(spriteSetDefinition.LeteRiver);
        _checkSpritesGetters[Events.BLOCK_SEALED_GATE] = GetSprite(spriteSetDefinition.SealedGate);
        _checkSpritesGetters[Events.GOT_ZOZO_REWARD] = GetSprite(spriteSetDefinition.ZozoTower);
        _checkSpritesGetters[Events.RECRUITED_TERRA_MOBLIZ] = GetSprite(spriteSetDefinition.MoblizAttack);
        _checkSpritesGetters[Events.DEFEATED_TUNNEL_ARMOR] = GetSprite(spriteSetDefinition.SouthFigaroCave);
        _checkSpritesGetters[Events.GOT_RAGNAROK] = GetSprite(spriteSetDefinition.NarsheWeaponShop);
        _checkSpritesGetters[Events.GOT_BOTH_REWARDS_WEAPON_SHOP] = GetSprite(spriteSetDefinition.NarsheWeaponShopMines);
        _checkSpritesGetters[Events.RECRUITED_LOCKE_PHOENIX_CAVE] = GetSprite(spriteSetDefinition.PhoenixCave);
        _checkSpritesGetters[Events.NAMED_EDGAR] = GetSprite(spriteSetDefinition.FigaroCastleThrone);
        _checkSpritesGetters[Events.DEFEATED_TENTACLES_FIGARO] = GetSprite(spriteSetDefinition.FigaroCastleEngine);
        _checkSpritesGetters[Events.GOT_RAIDEN] = GetSprite(spriteSetDefinition.AncientCastle);
        _checkSpritesGetters[Events.DEFEATED_VARGAS] = GetSprite(spriteSetDefinition.MtKolts);
        _checkSpritesGetters[Events.FINISHED_COLLAPSING_HOUSE] = GetSprite(spriteSetDefinition.CollapsingHouse);
        _checkSpritesGetters[Events.NAMED_GAU] = GetSprite(spriteSetDefinition.BarenFalls);
        _checkSpritesGetters[Events.FINISHED_IMPERIAL_CAMP] = GetSprite(spriteSetDefinition.ImperialCamp);
        _checkSpritesGetters[Events.GOT_PHANTOM_TRAIN_REWARD] = GetSprite(spriteSetDefinition.PhantomTrain);
        _checkSpritesGetters[Events.RECRUITED_SHADOW_GAU_FATHER_HOUSE] = GetSprite(spriteSetDefinition.GauFatherHouse);
        _checkSpritesGetters[Events.RECRUITED_SHADOW_FLOATING_CONTINENT] = GetSprite(spriteSetDefinition.FloatingContinentArrival);
        _checkSpritesGetters[Events.DEFEATED_ATMAWEAPON] = GetSprite(spriteSetDefinition.FloatingContinentBeast);
        _checkSpritesGetters[Events.FINISHED_FLOATING_CONTINENT] = GetSprite(spriteSetDefinition.FloatingContinentEscape);
        _checkSpritesGetters[Events.DEFEATED_SR_BEHEMOTH] = GetSprite(spriteSetDefinition.VeldtCave);
        _checkSpritesGetters[Events.FINISHED_DOMA_WOB] = GetSprite(spriteSetDefinition.DomaSiege);
        _checkSpritesGetters[Events.DEFEATED_STOOGES] = GetSprite(spriteSetDefinition.DomaDreamDoor);
        _checkSpritesGetters[Events.FINISHED_DOMA_WOR] = GetSprite(spriteSetDefinition.DomaDreamAwaken);
        _checkSpritesGetters[Events.GOT_ALEXANDR] = GetSprite(spriteSetDefinition.DomaDreamThrone);
        _checkSpritesGetters[Events.FINISHED_MT_ZOZO] = GetSprite(spriteSetDefinition.MtZozo);
        _checkSpritesGetters[Events.VELDT_REWARD_OBTAINED] = GetSprite(spriteSetDefinition.Veldt);
        _checkSpritesGetters[Events.GOT_SERPENT_TRENCH_REWARD] = GetSprite(spriteSetDefinition.SerpentTrench);
        _checkSpritesGetters[Events.FREED_CELES] = GetSprite(spriteSetDefinition.SouthFigaroPrisoner);
        _checkSpritesGetters[Events.GOT_IFRIT_SHIVA] = GetSprite(spriteSetDefinition.MagitekFactoryTrash);
        _checkSpritesGetters[Events.DEFEATED_NUMBER_024] = GetSprite(spriteSetDefinition.MagitekFactoryGuard);
        _checkSpritesGetters[Events.DEFEATED_CRANES] = GetSprite(spriteSetDefinition.MagitekFactoryFinish);
        _checkSpritesGetters[Events.FINISHED_OPERA_DISRUPTION] = GetSprite(spriteSetDefinition.OperaHouseDisruption);
        _checkSpritesGetters[Events.RECRUITED_SHADOW_KOHLINGEN] = GetSprite(spriteSetDefinition.KohlingenCafe);
        _checkSpritesGetters[Events.DEFEATED_DULLAHAN] = GetSprite(spriteSetDefinition.DarylsTomb);
        _checkSpritesGetters[Events.CHASING_LONE_WOLF7] = GetSprite(spriteSetDefinition.LoneWolfChase);
        _checkSpritesGetters[Events.GOT_BOTH_REWARDS_LONE_WOLF] = GetSprite(spriteSetDefinition.LoneWolfMoogleRoom);
        _checkSpritesGetters[Events.COMPLETED_MOOGLE_DEFENSE] = GetSprite(spriteSetDefinition.MoogleDefense);
        _checkSpritesGetters[Events.DEFEATED_FLAME_EATER] = GetSprite(spriteSetDefinition.BurningHouse);
        _checkSpritesGetters[Events.DEFEATED_HIDON] = GetSprite(spriteSetDefinition.EbotsRock);
        _checkSpritesGetters[Events.DEFEATED_MAGIMASTER] = GetSprite(spriteSetDefinition.FanaticsTowerLeader);
        _checkSpritesGetters[Events.RECRUITED_STRAGO_FANATICS_TOWER] = GetSprite(spriteSetDefinition.FanaticsTowerFollower);
        _checkSpritesGetters[Events.DEFEATED_ULTROS_ESPER_MOUNTAIN] = GetSprite(spriteSetDefinition.EsperMountain);
        _checkSpritesGetters[Events.DEFEATED_CHADARNOOK] = GetSprite(spriteSetDefinition.OwzersMansion);
        _checkSpritesGetters[Events.RECRUITED_GOGO_WOR] = GetSprite(spriteSetDefinition.ZoneEater);
        _checkSpritesGetters[Events.RECRUITED_UMARO_WOR] = GetSprite(spriteSetDefinition.UmarosCave);
        _checkSpritesGetters[Events.FINISHED_NARSHE_BATTLE] = GetSprite(spriteSetDefinition.NarsheBattle);
        _checkSpritesGetters[Events.BOUGHT_ESPER_TZEN] = GetSprite(spriteSetDefinition.TzenThief);
        _checkSpritesGetters[Events.DEFEATED_DOOM_GAZE] = GetSprite(spriteSetDefinition.SearchTheSkies);
        _checkSpritesGetters[Events.GOT_TRITOCH] = GetSprite(spriteSetDefinition.TritochCliff);
        _checkSpritesGetters[Events.AUCTION_BOUGHT_ESPER1] = GetSprite(spriteSetDefinition.JidoorAuctionHouse1);
        _checkSpritesGetters[Events.AUCTION_BOUGHT_ESPER2] = GetSprite(spriteSetDefinition.JidoorAuctionHouse2);
        _checkSpritesGetters[Events.DEFEATED_ATMA] = GetSprite(spriteSetDefinition.KefkasTowerCellBeast);
        _checkSpritesGetters[Events.DEFEATED_PHOENIX_CAVE_DRAGON] = GetSprite(spriteSetDefinition.PhoenixCaveDragon);
        _checkSpritesGetters[Events.DEFEATED_ANCIENT_CASTLE_DRAGON] = GetSprite(spriteSetDefinition.AncientCasteDragon);
        _checkSpritesGetters[Events.DEFEATED_MT_ZOZO_DRAGON] = GetSprite(spriteSetDefinition.MtZozoDragon);
        _checkSpritesGetters[Events.DEFEATED_OPERA_HOUSE_DRAGON] = GetSprite(spriteSetDefinition.OperaHouseDragon);
        _checkSpritesGetters[Events.DEFEATED_FANATICS_TOWER_DRAGON] = GetSprite(spriteSetDefinition.FanaticsTowerDragon);
        _checkSpritesGetters[Events.DEFEATED_NARSHE_DRAGON] = GetSprite(spriteSetDefinition.NarsheDragon);
        _checkSpritesGetters[Events.DEFEATED_KEFKA_TOWER_DRAGON_G] = GetSprite(spriteSetDefinition.KefkasTowerMiddlePathDragon);
        _checkSpritesGetters[Events.DEFEATED_KEFKA_TOWER_DRAGON_S] = GetSprite(spriteSetDefinition.KefkasTowerRightPathDragon);

        _statisticSpritesGetters[Statistic.Character] = GetSprite(spriteSetDefinition.CharacterCount);
        _statisticSpritesGetters[Statistic.Esper] = GetSprite(spriteSetDefinition.EsperCount);
        _statisticSpritesGetters[Statistic.Dragon] = GetSprite(spriteSetDefinition.DragonCount);
        _statisticSpritesGetters[Statistic.Boss] = GetSprite(spriteSetDefinition.BossCount);
        _statisticSpritesGetters[Statistic.Check] = GetSprite(spriteSetDefinition.CheckCount);
        _statisticSpritesGetters[Statistic.Chest] = GetSprite(spriteSetDefinition.ChestCount);

        _relatedEvents = spriteSetDefinition.RelatedEvents;
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
            SpriteSource.Character => () => _sprites.Character.Get((CharacterEx)spriteDefinition.Id, (Pose)spriteDefinition.SubId),
            SpriteSource.Portrait => () => _sprites.Portrait.Get((Character)spriteDefinition.Id),
            SpriteSource.Other => () => _sprites.Other.Get((Item)spriteDefinition.Id),
            SpriteSource.Monster => () => _sprites.Combat.Get((Monster)spriteDefinition.Id),
            SpriteSource.Boss => () => spriteDefinition.Id == (int)Boss.GhostTrain
                    ? _sprites.Other.Get(Item.GhostTrain)
                    : _sprites.Combat.Get((Boss)spriteDefinition.Id),
            SpriteSource.Esper => () => _sprites.Combat.Get((Esper)spriteDefinition.Id),
            _ => () => null
        };

        foreach (var transform in spriteDefinition.Transforms)
        {
            switch (transform)
            {
                case SpriteSet.Crop crop:
                    transformFuncs.Add(source => source.Crop(crop.GetRectangle()));
                    break;
                case SpriteSet.Overlay overlay:
                    transformFuncs.Add(source =>
                    {
                        var o = GetSprite(overlay.OverlayedSprite)();
                        if (o == null)
                            return source;
                        return source.Overlay(o, overlay.GetDestination());
                    });
                    break;
                case SpriteSet.Resize resize:
                    transformFuncs.Add(source => source.Resize(resize.GetSize()));
                    break;
                case Pad pad:
                    transformFuncs.Add(source => source.Pad(pad.GetSize(), pad.HorizontalAlignment, pad.VerticalAlignment));
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
                case TextOverlay textOverlay:
                    transformFuncs.Add(source =>
                    {
                        var text = _font.RenderText(textOverlay.Text);
                        return source.Overlay(new BasicSprite(text), textOverlay.GetDestination());
                    });
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

    public ISprite? Get(Events @event)
    {
        if (_checkSprites.ContainsKey(@event))
            return _checkSprites[@event];

        _checkSprites[@event] = _checkSpritesGetters[@event]();

        return _checkSprites[@event];
    }

    public ISprite? Get(Statistic statistic)
    {
        if (_statisticSprites.ContainsKey(statistic))
            return _statisticSprites[statistic];

        _statisticSprites[statistic] = _statisticSpritesGetters[statistic]();

        return _statisticSprites[statistic];
    }

    public ISprite? Get(Dragons dragon)
    {
        if (_dragonSprites.ContainsKey(dragon))
            return _dragonSprites[dragon];

        _dragonSprites[dragon] = _dragonSpritesGetters[dragon]();

        return _dragonSprites[dragon];
    }

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
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
