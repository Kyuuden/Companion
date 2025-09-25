using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;
internal class Seed : SeedBase
{
    private readonly Descriptors _descriptors;
    private readonly Objectives _objectives;
    private readonly Party _party;
    private readonly KeyItems _keyItems;
    private readonly Bosses _bosses;
    private readonly Locations _locations;
    private readonly IFlags? _flags;

    public Seed(string hash, Metadata metadata, Container container)
        : base(hash, metadata, container)
    {
        _descriptors = new Descriptors(Game.Rom);

        if (Flags.Binary != null)
        {
            _flags = metadata.Version switch
            {
                "v5.0.0-a.1" => new FlagsAlpha1(Flags.Binary),
                _ => new FlagsAlpha2(Flags.Binary),
            };
            XpRate = 1;
        }

        _objectives = new Objectives(_descriptors, metadata.Objectives.OfType<GroupObjectives>());
        _party = new Party(
            container.Settings.Party,
            Sprites, 
            _flags?.VanillaAgility, 
            _flags?.CHero);

        _keyItems = new KeyItems(container.Settings.KeyItems, Font, _descriptors);
        _bosses = new Bosses(_descriptors);
        _locations = new Locations(_descriptors, _flags!);
    }

    public override IEnumerable<ICharacter> Party => _party.Characters;

    public override IEnumerable<IKeyItem> KeyItems => _keyItems.Items;

    public override IEnumerable<IBoss> Bosses => _bosses.Items;

    public override IEnumerable<IObjectiveGroup> Objectives => _objectives.Groups;

    public override IEnumerable<ILocation> AvailableLocations => _locations.All.Where(l => l.IsAvailable && !l.IsChecked);

    public override bool CanTackBosses => true;

    public override bool RequiresMemoryEvents => false;

    public override void OnNewFrame()
    {
        base.OnNewFrame();

        if (!Started)
            Started = Game.Sram.ReadByte(Addresses.SRAM.StartedIndicatorAddress) == 1;
        
        if (!Victory)
            Victory = Game.Wram.ReadByte(Addresses.WRAM.VictoryIndicatorAddress) == 1;

        if (Game.Emulation.FrameCount() % Game.RootSettings.TrackingInterval == 0)
        {
            var partyData = Game.Wram.ReadBytes(Addresses.WRAM.PartyRegion);
            ReadOnlySpan<byte> sramData = Game.Sram.ReadBytes(Addresses.SRAM.SramRegion).AsSpan();
            ReadOnlySpan<byte> wramData = Game.Wram.ReadBytes(Addresses.WRAM.WramRegion).AsSpan();

            var axtorData = sramData.Slice(Addresses.SRAM.AxtorBits);
            var keyItemLocations = sramData.Slice(Addresses.SRAM.KeyItemLocationBits);
            var bossLocations = sramData.Slice(Addresses.SRAM.BossLocationBits);

            var keyItemsFound = wramData.Slice(Addresses.WRAM.KeyItemFoundBits);
            var keyItemUsed = wramData.Slice(Addresses.WRAM.KeyItemUsedBits);
            var bossDefeated = wramData.Slice(Addresses.WRAM.BossDefeatedBits);
            var bossLocationsDefeated = wramData.Slice(Addresses.WRAM.BossLocationBits);
            var axtorFoundBits = wramData.Slice(Addresses.WRAM.AxtorFoundBits);
            var shopCheckedBits = wramData.Slice(Addresses.WRAM.ShopCheckedBits);
            var victoryIndicator = wramData.Slice(Addresses.WRAM.VictoryIndicator);
            var rewardSlotCheckedBits = wramData.Slice(Addresses.WRAM.RewardSlotCheckedBits);
            var objectiveTaskProgress = wramData.Slice(Addresses.WRAM.ObjectiveTaskProgress);
            var objectiveGroupProress = wramData.Slice(Addresses.WRAM.ObjectiveGroupProress);
            var keyItemCheckCount = wramData[Addresses.WRAM.KeyItemCheckCount];
            var keyItemZonkCount = wramData[Addresses.WRAM.KeyItemZonkCount];

            var teasureCount = Game.Wram.ReadBytes(Shared.Addresses.WRAM.TreasureBits);
            TreasureCount = teasureCount.CountBits();

            var time = Elapsed;

            if(_keyItems.Update(time, keyItemsFound, keyItemUsed, keyItemLocations))
                NotifyPropertyChanged(nameof(KeyItems));
            
            if (_party.Update(time, partyData, axtorData))
                NotifyPropertyChanged(nameof(Party));

            if (_objectives.Update(time, objectiveTaskProgress, objectiveGroupProress))
                NotifyPropertyChanged(nameof(Objectives));

            if (_bosses.Update(time, bossLocations, bossLocationsDefeated))
            {
                DefeatedEncounters = _bosses.Items.SelectMany(b => b.Encounters).Count(e => e.IsDefeated);
                NotifyPropertyChanged(nameof(Bosses));
            }

            var foundKIs = _keyItems.Items.Where(ki => ki.IsFound).Select(ki => (KeyItemType)ki.Id).ToImmutableHashSet();
            var defeatedBosses = _bosses.Items.Where(b => b.Encounters.Any(e => e.IsDefeated)).Select(b => (BossType)b.Id).ToImmutableHashSet();

            if (_locations.Update(time, rewardSlotCheckedBits, shopCheckedBits, teasureCount, bossLocationsDefeated, axtorData, keyItemLocations, foundKIs, defeatedBosses))
                NotifyPropertyChanged(nameof(AvailableLocations));

            if (_flags != null)
            {
                List<RewardSlot> slots = [RewardSlot.StartingItem, RewardSlot.StartingPartnerCharacter];
                foreach (RewardSlot slot in slots)
                {
                    if (_locations.CanHaveKeyItem(slot) && rewardSlotCheckedBits.Read<bool>((int)slot))
                    {
                        keyItemCheckCount--;

                        var keyItemZonk = MemoryMarshal.Cast<byte, ushort>(keyItemLocations).IndexOf((ushort)slot) == -1;
                        var charZonk = !_flags.KChar;

                        if (_flags.KChar)
                        {
                            foreach (var axtor in MemoryMarshal.Cast<byte, uint>(axtorData))
                            {
                                if ((axtor >> 16) != (uint)slot) continue;
                                charZonk = false;
                                break;
                            }
                        }

                        if (keyItemZonk && charZonk) keyItemZonkCount--;
                    }
                }

                var xpBonuses = new List<decimal>();

                if (_flags.XObjBonus != ObjectiveXpBonus.None)
                    xpBonuses.Add(_objectives.NumCompleted * _flags.XObjBonus switch
                    {
                        ObjectiveXpBonus._2Percent => 0.02m,
                        ObjectiveXpBonus._3Percent => 0.0303030303030303m,
                        ObjectiveXpBonus._5Percent => 0.05m,
                        ObjectiveXpBonus._8Percent => 0.0833333333333333m,
                        ObjectiveXpBonus._10Percent => 0.10m,
                        ObjectiveXpBonus._12Percent => 0.125m,
                        ObjectiveXpBonus._14Percent => 0.1428571428571429m,
                        ObjectiveXpBonus._16Percent => 0.1666666666666667m,
                        ObjectiveXpBonus._20Percent => 0.20m,
                        ObjectiveXpBonus._25Percent => 0.25m,
                        ObjectiveXpBonus._33Percent => 0.333m,
                        _ => 0m,
                    });

                if (_flags.XKeyItemCheckBonus != KeyItemCheckXpBonus.None)
                    xpBonuses.Add(keyItemCheckCount *  _flags.XKeyItemCheckBonus switch
                    {
                        KeyItemCheckXpBonus._1Percent => 0.01m,
                        KeyItemCheckXpBonus._2Percent => 0.02m,
                        KeyItemCheckXpBonus._3Percent => 0.0303030303030303m,
                        KeyItemCheckXpBonus._4Percent => 0.04m,
                        KeyItemCheckXpBonus._5Percent => 0.05m,
                        KeyItemCheckXpBonus._8Percent => 0.0833333333333333m,
                        KeyItemCheckXpBonus._10Percent => 0.10m,
                        _ => 0m,
                    });

                if (_flags.XKeyItemZonkXpBonus != KeyItemZonkXpBonus.None)
                    xpBonuses.Add(keyItemZonkCount * _flags.XKeyItemZonkXpBonus switch
                    {
                        KeyItemZonkXpBonus._1Percent => 0.01m,
                        KeyItemZonkXpBonus._2Percent => 0.02m,
                        KeyItemZonkXpBonus._3Percent => 0.0303030303030303m,
                        KeyItemZonkXpBonus._4Percent => 0.04m,
                        KeyItemZonkXpBonus._5Percent => 0.05m,
                        KeyItemZonkXpBonus._8Percent => 0.0833333333333333m,
                        KeyItemZonkXpBonus._10Percent => 0.10m,
                        _ => 0m,
                    });

                if (!_flags.XNoKeyBonus && _keyItems.NumFound >= 10)
                    xpBonuses.Add(1m);

                var xpRate = 1m;

                foreach (var bonus in xpBonuses)
                {
                    if (bonus == 0m)
                        continue;

                    switch (_flags.XPBonusMode)
                    {
                        case XPBonusMode.Multiplicative:
                            xpRate *= (1 + bonus);
                            break;
                        case XPBonusMode.Default:
                        case XPBonusMode.Additive:
                        default:
                            xpRate += bonus;
                            break;
                    }
                }

                if (_flags.XMaxXpRate != MaxXpRate.Unlimited)
                    xpRate = Math.Min(xpRate, _flags.XMaxXpRate switch
                    {
                        MaxXpRate.Unlimited => decimal.MaxValue,
                        MaxXpRate._50Percent => .5m,
                        MaxXpRate._75Percent => .75m,
                        MaxXpRate._100Percent => 1m,
                        MaxXpRate._150Percent => 1.5m,
                        MaxXpRate._200Percent => 2m,
                        MaxXpRate._250Percent => 2.5m,
                        MaxXpRate._300Percent => 3m,
                        MaxXpRate._400Percent => 4m,
                        MaxXpRate._500Percent => 5m,
                        MaxXpRate._600Percent => 6m,
                        MaxXpRate._700Percent => 7m,
                        MaxXpRate._800Percent => 8m,
                        MaxXpRate._1000Percent => 10m,
                        _ => decimal.MaxValue
                    });

                XpRate = xpRate;
            }

            BackgroundColor = Game.Wram.ReadBytes(Shared.Addresses.WRAM.BackgroundColor).Read<ushort>(0).ToColor();
        }
    }
}
