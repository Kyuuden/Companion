using BizHawk.Client.Common;
using BizHawk.Common;
using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.Database;
using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.FlagSet;
using BizHawk.FreeEnterprise.Companion.Sprites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public enum RunState
    {
        Loading,
        Menu,
        RunStarted,
        RunFinished,
        Unknown
    }

    public class Run : IDisposable
    {
        private ApiContainer _container;
        private MemoryMapping _memory;
        private Color backgroundColor;
        private Party party;
        private Timer _loadingTimer;
        private PersistentStorage _db;
        private readonly Settings settings;
        private TimeSpan _previousElapsed = TimeSpan.Zero;

        public Run(string hash, ApiContainer container, MemoryMapping memory, PersistentStorage persistentStorage, Settings settings)
        {
            _container = container;
            _memory = memory;
            _db = persistentStorage;
            this.settings = settings;
            Hash = hash;

            _previousElapsed = _db.ElapsedTime;

            var jsonLength = _memory.CartRom.Read<uint>(CARTROMAddresses.MetadataLengthAddress, CARTROMAddresses.MetadataLengthBytes * 8);
            if (jsonLength > 0)
            {
                var jsonBytes = _memory.CartRom.ReadBytes(CARTROMAddresses.MetadataAddress, (int)jsonLength);
                var json = Encoding.UTF8.GetString(jsonBytes);
                Metadata = JsonConvert.DeserializeObject<SeedMetadata>(json)!;
                FlagSet = FlagSetParser.Parse(Metadata.binary_flags);
                Objectives = new Objectives(Metadata.objectives!, _db.ObjectiveCompleteTimes);
            }
            else
            {
                //TODO Prompt user to input Objectives since cannot read from ROM
                Objectives = new Objectives(_db.ObjectiveCompleteTimes);
            }

            KeyItems = new KeyItems(_db);
            party = new Party();
            Locations = new Locations(_db, FlagSet);
            Locations.UpdateKeyItems(KeyItems);
            BackgroundColor = Color.FromArgb(0, 0, 99);

            Stopwatch = new Stopwatch();
            State = RunState.Loading;
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
            LoadComplete?.Invoke(this, null);
            _loadingTimer.Stop();
            _loadingTimer.Dispose();
            State = RunState.Menu;
            CreateCallbacks();
        }

        public void UpdateApis(ApiContainer container, MemoryMapping memory)
        {
            RemoveCallbacks();
            _container = container;
            _memory = memory;
            CreateCallbacks();
        }

        public event EventHandler? LoadComplete;
        public event EventHandler? RunStarted;
        public event EventHandler? RunEnded;
        public event EventHandler? KeyItemsUpdated;
        public event Action<KeyItemType?>? NewKeyItemFound;
        public event EventHandler? PartyUpdated;
        public event EventHandler? LocationsUpdated;
        public event EventHandler? ObjectivesUpdated;
        public event EventHandler? CustomSettingsUpdated;

        public string? Hash { get; }

        public SeedMetadata? Metadata { get; }

        public IFlagSet? FlagSet { get; }

        public RunState State { get; private set; }

        public KeyItems KeyItems { get; }

        public TimeSpan ElapsedTime
            => _previousElapsed + Stopwatch.Elapsed;

        public Party Party
        {
            get => party;
            private set
            {
                if (value.Equals(party))
                    return;

                party = value;
                PartyUpdated?.Invoke(this, null);
            }
        }

        public Color BackgroundColor
        {
            get => backgroundColor;
            private set
            {
                if (value == backgroundColor)
                    return;

                backgroundColor = value;
                CustomSettingsUpdated?.Invoke(this, null);
            }
        }

        public Locations Locations { get; }

        public Objectives Objectives { get; }

        public Stopwatch Stopwatch { get; private set; }

        private void ReadBackgroundColor()
            => BackgroundColor = ColorProcessor.GetColor(_memory.Main.ReadBytes(WRAMAddresses.BackgroundColorAddress, WRAMAddresses.BackgroundColorBytes).Read<ushort>(0));

        public void StartNewGame(uint address, uint value, uint flags)
        {
            RemoveCallbacks();
            _previousElapsed = TimeSpan.Zero;
            State = RunState.RunStarted;
            Stopwatch = Stopwatch.StartNew();
            RunStarted?.Invoke(this, null);
            CreateCallbacks();
        }

        public void LoadSaveGame(uint address, uint value, uint flags)
        {
            if (_container.Emulation.GetRegister("A") == 1)
                return;

            RemoveCallbacks();

            State = RunState.RunStarted;
            Stopwatch = Stopwatch.StartNew();
            RunStarted?.Invoke(this, null);
            CreateCallbacks();
        }

        public void Flash(uint address, uint value, uint flags)
        {
            RemoveCallbacks();
            State = RunState.RunFinished;
            RunEnded?.Invoke(this, null);
            Stopwatch.Stop();
        }

        public void NewFrame()
        {
            if (State == RunState.Loading)
                return;

            var frameMod = _container.Emulation.FrameCount() % settings.RefreshEveryNFrames;
            if (frameMod == 0)
            {
                var now = ElapsedTime;
                if (_db != null) _db.ElapsedTime = now;

                var partyData = _memory.Main.ReadBytes(WRAMAddresses.PartyAddress, WRAMAddresses.PartyBytes);
                var mainData = _memory.Main.ReadBytes(WRAMAddresses.FoundKeyItems, 64);
                var foundKeyItems = mainData.Read<KeyItemType>(WRAMAddresses.FoundKeyItems - WRAMAddresses.FoundKeyItems, WRAMAddresses.FoundKeyItemsBytes * 8);
                var usedKeyItems = mainData.Read<KeyItemType>((WRAMAddresses.UsedKeyItems - WRAMAddresses.FoundKeyItems) * 8, WRAMAddresses.UsedKeyItemsBytes * 8);
                var keyItemsLocations = _memory.CartSaveRam.ReadBytes(CARTRAMAddresses.KeyItemLocations, CARTRAMAddresses.KeyItemLocationsBytes);
                var checkedLocations  = mainData.Read<byte[]>((WRAMAddresses.CheckedLocations - WRAMAddresses.FoundKeyItems) * 8, WRAMAddresses.CheckedLocationsBytes * 8);
                var objectives = mainData.Read<byte[]>((WRAMAddresses.ObjectiveCompletionAddress - WRAMAddresses.FoundKeyItems) * 8, WRAMAddresses.ObjectiveCompletionBytes * 8);

                if (KeyItems.Update(now, foundKeyItems, usedKeyItems, keyItemsLocations))
                    KeyItemsUpdated?.Invoke(this, null);

                if (Locations.UpdateCheckedLocations(now, checkedLocations, out var newlyChecked) | Locations.UpdateKeyItems(KeyItems))
                {
                    LocationsUpdated?.Invoke(this, null);
                    if (State == RunState.RunStarted
                        && newlyChecked.Count == 1
                        && (FlagSet?.CanHaveKeyItem((KeyItemLocationType)newlyChecked[0]) ?? false)
                        && _db?.LocationCheckedTimes[(int)newlyChecked[0]] == now)
                    {
                        NewKeyItemFound?.Invoke(KeyItems.ItemFoundAt((KeyItemLocationType)newlyChecked[0]));
                    }
                }

                if (Objectives?.Update(now, objectives) == true)
                    ObjectivesUpdated?.Invoke(this, null);

                Party = new Party(partyData);
            }
            else if (frameMod == settings.RefreshEveryNFrames * 3 / 5)
            {
                ReadBackgroundColor();
            }
        }

        private void RemoveCallbacks()
        {
            try
            {
                switch (State)
                {
                    case RunState.RunStarted:
                        _container.MemoryEvents.RemoveMemoryCallback(Flash);
                        break;
                    case RunState.Menu:
                        _container.MemoryEvents.RemoveMemoryCallback(StartNewGame);
                        if (_previousElapsed > TimeSpan.Zero)
                            _container.MemoryEvents.RemoveMemoryCallback(LoadSaveGame);
                        break;
                }
            }
            catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
            {
            }
        }

        private void CreateCallbacks()
        {
            try
            {
                switch (State)
                {
                    case RunState.RunStarted:
                        if (FlagSet?.OWinCrystal ?? true)
                            _container.MemoryEvents.AddExecCallback(Flash, SystemBusAddresses.ZeromusDeathAnimation, "System Bus");
                        else
                            _container.MemoryEvents.AddExecCallback(Flash, SystemBusAddresses.Congradulations, "System Bus");
                        break;
                    case RunState.Menu:
                        _container.MemoryEvents.AddExecCallback(StartNewGame, SystemBusAddresses.MenuSaveNewGame, "System Bus");
                        if (_previousElapsed > TimeSpan.Zero)
                            _container.MemoryEvents.AddExecCallback(LoadSaveGame, SystemBusAddresses.MenuLoadSaveGame, "System Bus");
                        break;
                }
            }
            catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
            {
                if (State != RunState.Unknown)
                {
                    State = RunState.Unknown;
                    MessageBox.Show("Automatic timing of runs is not supported on the Snes9x core. Please use the BSNES or BSNESv115+ core to have this feature.\nThe Timer can be manually started/stopped by double clicking on it.", "Timing unavailable");
                }
            }
        }

        public void Dispose()
        {
            RemoveCallbacks();
        }
    }
}
