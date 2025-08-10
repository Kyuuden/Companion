using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Shared;
using FF.Rando.Companion.FreeEnterprise.View;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;
internal class Seed : SeedBase
{
    private readonly Descriptors _descriptors;
    private readonly Objectives _objectives;
    private readonly Party _party;
    private readonly KeyItems _keyItems;
    private readonly Bosses _bosses;
    private readonly Locations _locations;
    private readonly Flags? _flags;

    public Seed(string hash, Metadata metadata, Container container)
        : base(hash, metadata, container)
    {
        _descriptors = new Descriptors(Game.Rom);

        if (Flags.Binary != null)
        {
            _flags = new Flags(Flags.Binary);
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

            var teasureCount = Game.Wram.ReadBytes(Shared.Addresses.WRAM.TreasureBits);
            TreasureCount = teasureCount.CountBits();

            var time = Elapsed;

            if(_keyItems.Update(time, keyItemsFound, keyItemUsed, keyItemLocations))
                NotifyPropertyChanged(nameof(KeyItems));
            
            if (_party.Update(time, partyData, axtorData))
                NotifyPropertyChanged(nameof(Party));

            if (_objectives.Update(time, objectiveTaskProgress, objectiveGroupProress))
                NotifyPropertyChanged(nameof(Objectives));

            if (_bosses.Update(time, bossLocations, bossDefeated, bossLocationsDefeated))
            {
                DefeatedEncounters = _bosses.Items.SelectMany(b => b.Encounters).Count(e => e.IsDefeated);
                NotifyPropertyChanged(nameof(Bosses));
            }

            var foundKIs = _keyItems.Items.Where(ki => ki.IsFound).Select(ki => (KeyItemType)ki.Id).ToImmutableHashSet();
            var defeatedBosses = _bosses.Items.Where(b => b.Encounters.Any(e => e.IsDefeated)).Select(b => (BossType)b.Id).ToImmutableHashSet();

            if (_locations.Update(time, rewardSlotCheckedBits, shopCheckedBits, teasureCount, bossLocationsDefeated, foundKIs, defeatedBosses))
                NotifyPropertyChanged(nameof(AvailableLocations));

            if (_flags != null)
            {
                var xpRate = 100m;

                if (_flags.XObjBonus != ObjectiveXpBonus.None)
                {
                    xpRate += _objectives.NumCompleted *
                        _flags.XObjBonus switch
                        {
                            ObjectiveXpBonus._2Percent => 2m,
                            ObjectiveXpBonus._3Percent => 3m,
                            ObjectiveXpBonus._5Percent => 5m,
                            ObjectiveXpBonus._8Percent => 8m,
                            ObjectiveXpBonus._10Percent => 10m,
                            ObjectiveXpBonus._12Percent => 12m,
                            ObjectiveXpBonus._14Percent => 14m,
                            ObjectiveXpBonus._16Percent => 16m,
                            ObjectiveXpBonus._20Percent => 20m,
                            ObjectiveXpBonus._25Percent => 25m,
                            _ => 0m,
                        };
                }

                if (!_flags.XNoKeyBonus && _keyItems.NumFound >= 10)
                    xpRate *= 2;

                XpRate = xpRate / 100.0m;
            }

            BackgroundColor = Game.Wram.ReadBytes(Shared.Addresses.WRAM.BackgroundColor).Read<ushort>(0).ToColor();
        }
    }
}
