using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise._4._6._0;
internal class Seed : SeedBase
{
    private enum RunState
    {
        Loading,
        Menu,
        RunStarted,
        RunFinished,
        Unknown
    }

    private RunState _runState;
    private readonly Flags? _flags;
    private readonly Descriptors _descriptors;
    private readonly KeyItems _keyItems;
    private readonly Party _party;
    private readonly Objectives _objectives;
    private readonly Locations _locations;
    private Timer _loadingTimer;

    public override IEnumerable<ICharacter> Party => _party.Characters;

    public override IEnumerable<IKeyItem> KeyItems => _keyItems.Items;

    public override IEnumerable<IBoss> Bosses => [];

    public override IEnumerable<IObjectiveGroup> Objectives  => _objectives.Yield();

    public override IEnumerable<ILocation> AvailableLocations => _locations.Items;

    public override bool RequiresMemoryEvents => true;

    public Seed(string hash, Metadata metadata, Container container)
        : base(hash, metadata, container)
    {
        _descriptors = new Descriptors();
        _keyItems = new KeyItems(container.Settings.KeyItems, Font, _descriptors);

        if (Flags.Binary != null)
        {
            _flags = new Flags(Flags.Binary);
            XpRate = 1;
        }

        _party = new Party(container.Settings.Party, Sprites, _flags?.VanillaAgility, _flags?.CHero);
        _objectives = new Objectives(metadata.Objectives!, _flags?.NumRequiredObjectives, _flags?.OWinGame, _flags?.OWinCrystal);
        _locations = new Locations(_descriptors, _flags);
        _runState = RunState.Loading;
        _loadingTimer = new Timer
        {
            Enabled = true,
            Interval = 2000,
        };
        _loadingTimer.Tick += _loadingTimer_Tick;
        CreateCallbacks();
    }

    private void _loadingTimer_Tick(object sender, EventArgs e)
    {
        _loadingTimer.Stop();
        _loadingTimer.Dispose();
        _runState = RunState.Menu;
        CreateCallbacks();
    }

    private void StartNewGame(uint address, uint value, uint flags)
    {
        RemoveCallbacks();
        _runState = RunState.RunStarted;
        Started = true;
        CreateCallbacks();
    }

    private void Flash(uint address, uint value, uint flags)
    {
        RemoveCallbacks();
        Victory = true;
        _runState = RunState.RunFinished;
    }

    private void CreateCallbacks()
    {
        try
        {
            switch (_runState)
            {
                case RunState.RunStarted:
                    if (Game.MemoryEvents == null)
                        throw new KeyNotFoundException();

                    if (_flags?.OWinCrystal ?? true)
                        Game.MemoryEvents?.AddExecCallback(Flash, Addresses.SystemBus.ZeromusDeathAnimation, "System Bus");
                    break;
                case RunState.Menu:
                    if (Game.MemoryEvents == null)
                        throw new KeyNotFoundException();

                    Game.MemoryEvents?.AddExecCallback(StartNewGame, Addresses.SystemBus.MenuSaveNewGame, "System Bus");
                    break;
            }
        }
        catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
        {
        }
    }

    private void RemoveCallbacks()
    {
        try
        {
            switch (_runState)
            {
                case RunState.RunStarted:
                    Game.MemoryEvents?.RemoveMemoryCallback(Flash);
                    break;
                case RunState.Menu:
                    Game.MemoryEvents?.RemoveMemoryCallback(StartNewGame);
                    break;
            }
        }
        catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
        {
        }
    }

    public override void OnNewFrame()
    {
        base.OnNewFrame();

        if (_runState == RunState.Loading)
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

                XpRate = (_flags != null && !_flags.XNoKeyBonus && KeyItems.Count(ki => ki.IsFound && ki.IsTrackable) >= 10)
                    ? 2m : 1m;
            }

            if (_party.Update(time, partyData))
                NotifyPropertyChanged(nameof(Party));

            if (_objectives.Update(time, objectiveCompletion))
                NotifyPropertyChanged(nameof(Objectives));

            if (_locations.Update(time, locationsChecked, _keyItems.Items.Where(ki => ki.IsFound).Select(ki => (KeyItemType)ki.Id).ToImmutableHashSet()))
                NotifyPropertyChanged(nameof(Locations));

            DefeatedEncounters = defeatedBosses;
        }
    }
}
