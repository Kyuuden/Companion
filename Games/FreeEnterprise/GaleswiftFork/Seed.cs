using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.FreeEnterprise.GaleswiftFork;
using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FF.Rando.Companion.Games.FreeEnterprise.GaleswiftFork;
internal class Seed : LegacySeed
{
    private readonly IFlags? _flags;
    private readonly Descriptors _descriptors;
    private readonly KeyItems _keyItems;
    private readonly Party _party;
    private readonly Objectives _objectives;
    private readonly Locations _locations;

    public override IEnumerable<ICharacter> Party => _party.Characters;

    public override IEnumerable<IKeyItem> KeyItems => _keyItems.Items;

    public override IEnumerable<IBoss> Bosses => [];

    public override IEnumerable<IObjectiveGroup> Objectives => _objectives.Yield();

    public override IEnumerable<ILocation> AvailableLocations => _locations.Items.Where(l => l.IsAvailable && !l.IsChecked);

    public Seed(string hash, Metadata metadata, Container container)
        : base(hash, metadata, container)
    {
        XpRate = 1;

        if (!string.IsNullOrEmpty(Flags.Text) && Flags.Text != "(hidden)")
            _flags = new TextFlags(Flags.Text!);

        _flags ??= new MysteryFlags();
        _descriptors = new Descriptors(_flags);
        _keyItems = new KeyItems(container.Settings.KeyItems, Font, _descriptors);
        _party = new Party(container.Settings.Party, Sprites, _flags?.VanillaAgility, (_flags?.CHero ?? false) || (_flags?.CSuperhero ?? false));
        _locations = new Locations(_descriptors, _flags!);
        _objectives = new Objectives(metadata.Objectives!, _flags!);
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
            var currentLocation = wramData.Slice(Addresses.WRAM.Location);
            var teasureCount = Game.Wram.ReadBytes(Shared.Addresses.WRAM.TreasureBits);
            TreasureCount = teasureCount.CountBits();

            var time = Elapsed;

            if (_keyItems.Update(time, keyItemsFound, keyItemUsed, keyItemLocations, inventory))
                NotifyPropertyChanged(nameof(KeyItems));

            var foundKIs = _keyItems.Items.Where(ki => ki.IsFound).Select(ki => (KeyItemType)ki.Id).ToImmutableHashSet();

            if (_party.Update(time, partyData))
                NotifyPropertyChanged(nameof(Party));

            if (_objectives.Update(time, objectiveCompletion))
                NotifyPropertyChanged(nameof(Objectives));

            if (_locations.Update(time, locationsChecked, foundKIs))
                NotifyPropertyChanged(nameof(AvailableLocations));


            if (_flags != null)
            {
                var xpRate = 100m;

                if (_flags.XObjectiveBonus != ObjectiveXpBonus.None)
                {
                    xpRate *= 1 + _objectives.Tasks.Count(t => t.IsCompleted) *
                    _flags.XObjectiveBonus switch
                    {
                        ObjectiveXpBonus._5Percent => 0.5m,
                        ObjectiveXpBonus._10Percent => 0.10m,
                        ObjectiveXpBonus._25Percent => 0.25m,
                        ObjectiveXpBonus.Split => 1.0m / _objectives.Tasks.Count(),
                        _ => 0m,
                    };
                }

                var kiCount = foundKIs.Count;
                var kIchecks = _locations.Items.Where(l => l.IsKeyItem && l.IsChecked).Count();
                if (keyItemLocations.Contains((byte)RewardSlot.StartingItem)) // starting check doesn't count
                    kIchecks--;

                xpRate *= 1 + kIchecks * _flags.XKeyItemCheckBonus switch
                {
                    KeyItemCheckXpBonus._2Percent => 0.2m,
                    KeyItemCheckXpBonus._5Percent => 0.5m,
                    KeyItemCheckXpBonus._10Percent => 0.10m,
                    KeyItemCheckXpBonus.Split => 1.0m / _locations.Items.Count(l => l.IsKeyItem),
                    _ => 0m
                };

                var zonks = kIchecks - kiCount;
                if (!keyItemLocations.Contains((byte)RewardSlot.StartingItem)) // starting zonk doesn't count
                    zonks--;

                xpRate *= 1 + zonks * _flags.XKeyItemZonkXpBonus switch
                {
                    KeyItemZonkXpBonus._2Percent => 0.2m,
                    KeyItemZonkXpBonus._5Percent => 0.5m,
                    KeyItemZonkXpBonus._10Percent => 0.10m,
                    _ => 0m
                };

                if (_flags.XMoonXpBonus != MoonXpBonus.None && (currentLocation[0] == 2 || currentLocation[0] == 3 && BinaryPrimitives.ReadUInt16BigEndian(currentLocation[1..]) >= 0x015a))
                {
                    xpRate *= 1 + _flags.XMoonXpBonus switch
                    {
                        MoonXpBonus._100Percent => 1,
                        MoonXpBonus._200Percent => 2,
                        _ => 0
                    };
                }

                if (_flags.XCrystalBonus && foundKIs.Contains(KeyItemType.Crystal))
                    xpRate *= 2;

                if (!_flags.XNoKeyBonus && KeyItems.Count(ki => ki.IsFound && ki.IsTrackable) >= 10)
                    xpRate *= 2;

                if (_flags.XSmallParty && _party.Characters.Count(c => c.Id != 0) < _flags.MaxPartySize)
                {
                    var bonuses = 0;
                    var currentPartySize = _party.Characters.Count(c => c.Id != 0);
                    for (var size = currentPartySize; size < _flags.MaxPartySize; size++)
                    {
                        bonuses += _flags.MaxPartySize - size;
                    }
                    xpRate *= 1 + bonuses * 0.1m;
                }

                XpRate = xpRate / 100.0m;
            }

            DefeatedEncounters = defeatedBosses;
            BackgroundColor = Game.Wram.ReadBytes(Shared.Addresses.WRAM.BackgroundColor).Read<ushort>(0).ToColor();
        }
    }

    protected override bool OWinGame => _flags?.OWinGame ?? false;
}
