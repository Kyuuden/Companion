using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._4._6._0;

internal class Seed : LegacySeed
{
    private readonly IFlags _flags;
    private readonly Descriptors _descriptors;
    private readonly KeyItems _keyItems;
    private readonly Party _party;
    private readonly Objectives _objectives;
    private readonly Locations _locations;

    public override IEnumerable<ICharacter> Party => _party.Characters;

    public override IEnumerable<IKeyItem> KeyItems => _keyItems.Items;

    public override IEnumerable<IBoss> Bosses => [];

    public override IEnumerable<IObjectiveGroup> Objectives  => _objectives.Yield();

    public override IEnumerable<ILocation> AvailableLocations => _locations.Items.Where(i=>i.IsAvailable && !i.IsChecked);

    public Seed(string hash, Metadata metadata, Container container)
        : base(hash, metadata, container)
    {
        _flags = Flags.Binary != null
            ? new Flags(Flags.Binary)
            : new MysteryFlags();

        if (Flags.Binary != null)
            XpRate = 1;

        _descriptors = new Descriptors();
        _keyItems = new KeyItems(container.Settings.KeyItems, Font, _descriptors);
        _party = new Party(container.Settings.Party, Sprites, _flags.VanillaAgility, _flags.CHero);
        _objectives = new Objectives(metadata.Objectives!, _flags.NumRequiredObjectives, _flags.OWinGame == true, _flags.OWinCrystal == true);
        _locations = new Locations(_descriptors, _flags);
    }

    public override void OnNewFrame()
    {
        base.OnNewFrame();

        if (IsLoading)
            return;

        if (Game.Emulation.FrameCount() % Game.RootSettings.TrackingInterval == 0)
        {
            var partyData = Game.Wram.ReadBytes(Addresses.WRAM.PartyRegion);
            ReadOnlySpan<byte> wramData = Game.Wram.ReadBytes(Addresses.WRAM.WramRegion).AsSpan();
            var keyItemLocations = Game.Sram.ReadBytes(Addresses.SRAM.KeyItemLocations);
            var keyItemsFound = wramData.Slice(Addresses.WRAM.KeyItemFoundBits);
            var keyItemUsed = wramData.Slice(Addresses.WRAM.KeyItemUsedBits);
            var locationsChecked = wramData.Slice(Addresses.WRAM.CheckedLocations);
            var objectiveCompletion = wramData.Slice(Addresses.WRAM.ObjectiveCompletion);
            var inventory = wramData.Slice(Addresses.WRAM.Inventory);
            var defeatedBosses = Game.Wram.ReadByte(Addresses.WRAM.BossesDefeated);
            var teasureCount = Game.Wram.ReadBytes(Shared.Addresses.WRAM.TreasureBits);
            TreasureCount = teasureCount.CountBits();

            var time = Elapsed;

            if (_keyItems.Update(time, keyItemsFound, keyItemUsed, keyItemLocations, inventory))
            {
                NotifyPropertyChanged(nameof(KeyItems));

                XpRate = (!_flags.XNoKeyBonus && KeyItems.Count(ki => ki.IsFound && ki.IsTrackable) >= 10)
                    ? 2m : 1m;
            }

            if (_party.Update(time, partyData))
                NotifyPropertyChanged(nameof(Party));

            if (_objectives.Update(time, objectiveCompletion))
                NotifyPropertyChanged(nameof(Objectives));

            if (_locations.Update(time, locationsChecked, _keyItems.Items.Where(ki => ki.IsFound).Select(ki => (KeyItemType)ki.Id).ToImmutableHashSet()))
                NotifyPropertyChanged(nameof(AvailableLocations));

            DefeatedEncounters = defeatedBosses;
            BackgroundColor = Game.Wram.ReadBytes(Shared.Addresses.WRAM.BackgroundColor).Read<ushort>(0).ToColor();
        }
    }

    protected override bool OWinGame => _flags.OWinGame;
}
