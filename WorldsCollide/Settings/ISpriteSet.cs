using FF.Rando.Companion.WorldsCollide.RomData;
using FF.Rando.Companion.WorldsCollide.Enums;
using FF.Rando.Companion.Rendering;
using System;

namespace FF.Rando.Companion.WorldsCollide.Settings;

public enum Statistic
{
    Character,
    Esper,
    Dragon,
    Boss,
    Check,
    Chest
}


public interface ISpriteSet
{
    ISprite Get(Sprites sprites, Events @event);

    ISprite Get(Sprites sprites, Statistic statistic);
}

internal class VanillaBossSpriteSet : ISpriteSet
{
    public ISprite Get(Sprites sprites, Statistic statistic)
    {
        return statistic switch
        {
            Statistic.Character => sprites.Character.Get(CharacterEx.Arvis, Pose.Salute),
            Statistic.Esper => sprites.Misc.Get(Item.Magicite),
            Statistic.Dragon => sprites.Character.Get(CharacterEx.RedDragon, Pose.Stand),
            Statistic.Boss => sprites.Character.Get(CharacterEx.Ultros, Pose.Surprised),
            Statistic.Check => sprites.Character.Get(CharacterEx.Imp, Pose.HandsUp),
            Statistic.Chest => sprites.Misc.Get(Item.LargeOpenChest),
            _ => null
        } ?? throw new NotSupportedException();
    }

    public ISprite Get(Sprites sprites, Events @event)
    {
        return @event switch
        {
            Events.GOT_RAIDEN => sprites.Combat.Get(Esper.Raiden),
            Events.NAMED_GAU => sprites.Combat.Get(Boss.Rizopas),
            Events.DEFEATED_FLAME_EATER => sprites.Combat.Get(Boss.FlameEater),
            Events.FINISHED_COLLAPSING_HOUSE => sprites.Character.Get(CharacterEx.YoungBoy1, Pose.Stand),
            Events.DEFEATED_DULLAHAN => sprites.Combat.Get(Boss.Dullahan),
            Events.FINISHED_DOMA_WOB => sprites.Combat.Get(Monster.Commander),
            Events.DEFEATED_STOOGES => sprites.Character.Get(CharacterEx.Wrexsoul, Pose.Stand),
            Events.FINISHED_DOMA_WOR => sprites.Character.Get(CharacterEx.Wrexsoul, Pose.Stand),
            Events.GOT_ALEXANDR => sprites.Character.Get(CharacterEx.Wrexsoul, Pose.Stand),
            Events.DEFEATED_HIDON => sprites.Character.Get(CharacterEx.Hidon, Pose.Stand),
            Events.DEFEATED_ULTROS_ESPER_MOUNTAIN => sprites.Misc.Get(Item.WarringTriad),
            Events.RECRUITED_STRAGO_FANATICS_TOWER => sprites.Combat.Get(Boss.MagiMaster),
            Events.DEFEATED_MAGIMASTER => sprites.Combat.Get(Boss.MagiMaster),
            Events.NAMED_EDGAR => sprites.Misc.Get(Item.Throne),
            Events.DEFEATED_TENTACLES_FIGARO => sprites.Combat.Get(Boss.Tentacle),
            Events.RECRUITED_SHADOW_FLOATING_CONTINENT => sprites.Character.Get(CharacterEx.Shadow, Pose.JumpRide1),
            Events.DEFEATED_ATMAWEAPON => sprites.Character.Get(CharacterEx.Shadow, Pose.JumpRide1),
            Events.FINISHED_FLOATING_CONTINENT => sprites.Character.Get(CharacterEx.Shadow, Pose.JumpRide1),
            Events.RECRUITED_SHADOW_GAU_FATHER_HOUSE => sprites.Character.Get(CharacterEx.GausFather, Pose.Surprised),
            Events.FINISHED_IMPERIAL_CAMP => sprites.Character.Get(CharacterEx.ImperialSoldier, Pose.Threaten),
            Events.DEFEATED_ATMA => sprites.Character.Get(CharacterEx.AtmaWeapon, Pose.Stand),
            Events.RECRUITED_SHADOW_KOHLINGEN => sprites.Character.Get(CharacterEx.Interceptor, Pose.Sit),
            Events.RODE_RAFT_LETE_RIVER => sprites.Combat.Get(Boss.Ultros),
            Events.CHASING_LONE_WOLF7 => sprites.Character.Get(CharacterEx.LoneWolf, Pose.Stand),
            Events.GOT_BOTH_REWARDS_LONE_WOLF => sprites.Character.Get(CharacterEx.LoneWolf, Pose.Stand),
            Events.GOT_IFRIT_SHIVA => sprites.Combat.Get(Boss.LeftCrane),
            Events.DEFEATED_NUMBER_024 => sprites.Combat.Get(Boss.LeftCrane),
            Events.DEFEATED_CRANES => sprites.Combat.Get(Boss.LeftCrane),
            Events.RECRUITED_TERRA_MOBLIZ => sprites.Combat.Get(Boss.Phunbaba1),
            Events.COMPLETED_MOOGLE_DEFENSE => sprites.Character.Get(CharacterEx.Mog, Pose.CombatStandLeft),
            Events.DEFEATED_VARGAS => sprites.Combat.Get(Boss.Vargas),
            Events.FINISHED_MT_ZOZO => sprites.Misc.Get(Item.PottedRoses),
            Events.FINISHED_NARSHE_BATTLE => sprites.Character.Get(CharacterEx.Kefka, Pose.Laugh2),
            Events.GOT_RAGNAROK => sprites.Misc.Get(Item.WeaponShopSign),
            Events.GOT_BOTH_REWARDS_WEAPON_SHOP => sprites.Misc.Get(Item.WeaponShopSign),
            Events.FINISHED_OPERA_DISRUPTION => sprites.Misc.Get(Item.Weight),
            Events.DEFEATED_CHADARNOOK => sprites.Combat.Get(Boss.ChadarnookMonster),
            Events.GOT_PHANTOM_TRAIN_REWARD => sprites.Combat.Get(Boss.GhostTrain),
            Events.RECRUITED_LOCKE_PHOENIX_CAVE => sprites.Combat.Get(Esper.Phoenix),
            Events.BLOCK_SEALED_GATE => sprites.Character.Get(CharacterEx.Maduin, Pose.Stand),
            Events.DEFEATED_DOOM_GAZE => sprites.Combat.Get(Boss.DoomGaze),
            Events.GOT_SERPENT_TRENCH_REWARD => sprites.Misc.Get(Item.DiveHelm),
            Events.FREED_CELES => sprites.Character.Get(CharacterEx.Celes, Pose.Chained),
            Events.DEFEATED_TUNNEL_ARMOR => sprites.Combat.Get(Boss.TunnelArmr),
            Events.GOT_TRITOCH => sprites.Character.Get(CharacterEx.Tritoch, Pose.Stand),
            Events.BOUGHT_ESPER_TZEN => sprites.Character.Get(CharacterEx.TzenThief, Pose.StandLeft),
            Events.RECRUITED_UMARO_WOR => sprites.Combat.Get(Boss.UmaroP1),
            Events.VELDT_REWARD_OBTAINED => sprites.Character.Get(CharacterEx.Gau, Pose.CombatStandLeft),
            Events.DEFEATED_SR_BEHEMOTH => sprites.Combat.Get(Boss.SrBehemoth),
            Events.DEFEATED_WHELK => sprites.Combat.Get(Boss.Whelk),
            Events.RECRUITED_GOGO_WOR => sprites.Combat.Get(Boss.ZoneEater),
            Events.GOT_ZOZO_REWARD => sprites.Character.Get(CharacterEx.Ramuh, Pose.Stand),
            Events.AUCTION_BOUGHT_ESPER1 => sprites.Character.Get(CharacterEx.Auctioneer, Pose.Stand),
            Events.AUCTION_BOUGHT_ESPER2 => sprites.Character.Get(CharacterEx.Auctioneer, Pose.Stand),

            Events.TERRA_IN_PARTY => sprites.Portrait.Get(Character.Terra),
            Events.LOCKE_IN_PARTY => sprites.Portrait.Get(Character.Locke),
            Events.CYAN_IN_PARTY => sprites.Portrait.Get(Character.Cyan),
            Events.SHADOW_IN_PARTY => sprites.Portrait.Get(Character.Shadow),
            Events.EDGAR_IN_PARTY => sprites.Portrait.Get(Character.Edgar),
            Events.SABIN_IN_PARTY => sprites.Portrait.Get(Character.Sabin),
            Events.CELES_IN_PARTY => sprites.Portrait.Get(Character.Celes),
            Events.STRAGO_IN_PARTY => sprites.Portrait.Get(Character.Strago),
            Events.RELM_IN_PARTY => sprites.Portrait.Get(Character.Relm),
            Events.SETZER_IN_PARTY => sprites.Portrait.Get(Character.Setzer),
            Events.MOG_IN_PARTY => sprites.Portrait.Get(Character.Mog),
            Events.GAU_IN_PARTY => sprites.Portrait.Get(Character.Gau),
            Events.GOGO_IN_PARTY => sprites.Portrait.Get(Character.Gogo),
            Events.UMARO_IN_PARTY => sprites.Portrait.Get(Character.Umaro),

            Events.DEFEATED_ANCIENT_CASTLE_DRAGON => sprites.Combat.Get(Boss.BlueDrgn),
            Events.DEFEATED_FANATICS_TOWER_DRAGON => sprites.Combat.Get(Boss.CzarDragon),
            Events.DEFEATED_KEFKA_TOWER_DRAGON_G => sprites.Combat.Get(Boss.GoldDrgn),
            Events.DEFEATED_KEFKA_TOWER_DRAGON_S => sprites.Combat.Get(Boss.SkullDrgn),
            Events.DEFEATED_MT_ZOZO_DRAGON => sprites.Combat.Get(Boss.StormDrgn),
            Events.DEFEATED_NARSHE_DRAGON => sprites.Combat.Get(Boss.IceDragon),
            Events.DEFEATED_OPERA_HOUSE_DRAGON => sprites.Combat.Get(Boss.DirtDrgn),
            Events.DEFEATED_PHOENIX_CAVE_DRAGON => sprites.Combat.Get(Boss.RedDragon),
            _ => null
        } ?? throw new System.NotSupportedException();
    }
}