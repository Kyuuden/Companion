using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Locations
{
    private IReadOnlyList<RewardSlotLocation> _rewardSlots;
    private IReadOnlyList<BossLocation> _bossLocations;
    private IReadOnlyList<ShopLocation> _shopLocations;

    public Locations(ReadOnlySpan<byte> rewardSlots, ReadOnlySpan<byte> bosses, ReadOnlySpan<byte> shops)
    {
        
    }

    public Locations(Descriptors descriptors, Flags? flags)
    {
        Enum.GetValues(typeof(RewardSlot)).OfType<RewardSlot>().Select(s => new RewardSlotLocation(s, descriptors, flags)).ToList();
    }
}

internal class RewardSlotLocation : ILocation
{
    public RewardSlotLocation(RewardSlot slot, Descriptors descriptors, Flags? flags)
    {
        Description = descriptors.GetRewardSlotDescription((int)slot);
        switch (slot)
        {
            case RewardSlot.None:
                break;
            case RewardSlot.StartingCharacter:
                IsCharacter = true;
                break;
            case RewardSlot.StartingPartnerCharacter:
                IsCharacter = true;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.MistCharacter:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.WateryPassCharacter:
                IsCharacter = flags?.CNoFree == false;
                break;
            case RewardSlot.DamcyanCharacter:
                IsCharacter = flags?.CNoFree == false;
                break;
            case RewardSlot.KaipoCharacter:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.HobsCharacter:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.MysidiaCharacter1:
                IsCharacter = flags?.CNoFree == false;
                break;
            case RewardSlot.MysidiaCharacter2:
                IsCharacter = flags?.CNoFree == false;
                break;
            case RewardSlot.OrdealsCharacter:
                IsCharacter = flags?.CNoFree == false;
                break;
            case RewardSlot.BaronInnCharacter:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.BaronCastleCharacter:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.ZotCharacter1:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.ZotCharacter2:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.DwarfCastleCharacter:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.CaveEblanCharacter:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.LunarPalaceCharacter:
                IsCharacter = flags?.CNoEarned == false;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.GiantCharacter:
                IsCharacter = (flags?.CNoEarned == false) && (flags?.CNoGiant == false);
                break;
            case RewardSlot.StartingItem:
                IsKeyItem = true;
                IsKeyItem = flags?.KChar;
                break;
            case RewardSlot.AntlionItem:
                break;
            case RewardSlot.FabulItem:
                break;
            case RewardSlot.OrdealsItem:
                break;
            case RewardSlot.BaronInnItem:
                break;
            case RewardSlot.BaronCastleItem:
                break;
            case RewardSlot.ToroiaHospitalItem:
                break;
            case RewardSlot.MagnesItem:
                break;
            case RewardSlot.ZotItem:
                break;
            case RewardSlot.BabilBossItem:
                break;
            case RewardSlot.CannonItem:
                break;
            case RewardSlot.LucaItem:
                break;
            case RewardSlot.SealedCaveItem:
                break;
            case RewardSlot.FeymarchItem:
                break;
            case RewardSlot.RatTradeItem:
                break;
            case RewardSlot.FoundYangItem:
                break;
            case RewardSlot.PanTradeItem:
                break;
            case RewardSlot.FeymarchQueenItem:
                break;
            case RewardSlot.FeymarchKingItem:
                break;
            case RewardSlot.BaronThroneItem:
                break;
            case RewardSlot.SylphItem:
                break;
            case RewardSlot.BahamutItem:
                break;
            case RewardSlot.LunarBoss1Item:
                break;
            case RewardSlot.LunarBoss2Item:
                break;
            case RewardSlot.LunarBoss3Item:
                break;
            case RewardSlot.LunarBoss4Item1:
                break;
            case RewardSlot.LunarBoss4Item2:
                break;
            case RewardSlot.LunarBoss5Item:
                break;
            case RewardSlot.RydiasMomItem:
                break;
            case RewardSlot.FallenGolbezItem:
                break;
            case RewardSlot.ForgeItem:
                break;
            case RewardSlot.PinkTradeItem:
                break;
        }

    }

    public string Description { get; }

    public bool? IsCharacter { get; }

    public bool? IsKeyItem { get; }

    public bool IsBoss => false;

    public bool IsShop => false;
}

internal class BossLocation : ILocation
{ 
    public string Description => throw new NotImplementedException();

    public bool? IsCharacter => false;

    public bool? IsKeyItem => false;

    public bool IsBoss => true;

    public bool IsShop => false;
}

internal class ShopLocation : ILocation
{
    public string Description => throw new NotImplementedException();

    public bool? IsCharacter => false;

    public bool? IsKeyItem => false;

    public bool IsBoss => false;

    public bool IsShop => true;
}