using BizHawk.Client.Common;
using BizHawk.Common;
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

        public Run(ApiContainer container, MemoryMapping memory)
        {
            _container = container;
            _memory = memory;

#if BIZHAWK_28
            Hash = _container.GameInfo.GetGameInfo()?.Hash;
#elif BIZHAWK_261
            Hash = _container.GameInfo.GetRomHash();
#endif
            var jsonLength = _memory.CartRom.Read<uint>(CARTROMAddresses.MetadataLengthAddress, CARTROMAddresses.MetadataLengthBytes * 8);
            if (jsonLength > 0)
            {
                var jsonBytes = _memory.CartRom.ReadBytes(CARTROMAddresses.MetadataAddress, (int)jsonLength);
                var json = Encoding.UTF8.GetString(jsonBytes);
                Metadata = JsonConvert.DeserializeObject<SeedMetadata>(json)!;
                FlagSet = FlagSetParser.Parse(Metadata.binary_flags);
                Objectives = new Objectives(Metadata.objectives!);
            }
            else
            {
                //TODO Prompt user to input Objectives since cannot read from ROM
                Objectives = new Objectives();
            }

            KeyItems = new KeyItems();
            party = new Party();
            Locations = new Locations(FlagSet);
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
        public event EventHandler? PartyUpdated;
        public event EventHandler? LocationsUpdated;
        public event EventHandler? ObjectivesUpdated;
        public event EventHandler? CustomSettingsUpdated;

        public string? Hash { get; }

        public SeedMetadata? Metadata { get; }

        public IFlagSet? FlagSet { get; }

        public RunState State { get; private set; }

        public KeyItems KeyItems { get; } 

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

            var frameMod = _container.Emulation.FrameCount() % Properties.Settings.Default.RefreshEveryNFrames;
            if (frameMod == 0)
            {
                Party = new Party(_memory.Main.ReadBytes(WRAMAddresses.PartyAddress, WRAMAddresses.PartyBytes));
            }
            else if (frameMod == Properties.Settings.Default.RefreshEveryNFrames / 5)
            {
                if (KeyItems.Update(
                    Stopwatch.Elapsed,
                   _memory.Main.Read<KeyItemType>(WRAMAddresses.FoundKeyItems, WRAMAddresses.FoundKeyItemsBytes),
                   _memory.Main.Read<KeyItemType>(WRAMAddresses.UsedKeyItems, WRAMAddresses.UsedKeyItemsBytes),
                   _memory.CartSaveRam.ReadBytes(CARTRAMAddresses.KeyItemLocations, CARTRAMAddresses.KeyItemLocationsBytes)))
                {
                    KeyItemsUpdated?.Invoke(this, null);
                    if (Locations.UpdateKeyItems(KeyItems))
                        LocationsUpdated?.Invoke(this, null);
                }
            }
            else if (frameMod == Properties.Settings.Default.RefreshEveryNFrames * 2 / 5)
            {
                ReadBackgroundColor();
            }
            else if (frameMod == Properties.Settings.Default.RefreshEveryNFrames * 3 / 5)
            {
                if (Locations.UpdateCheckedLocations(Stopwatch.Elapsed, _memory.Main.ReadBytes(WRAMAddresses.CheckedLocations, WRAMAddresses.CheckedLocationsBytes)))
                    LocationsUpdated?.Invoke(this, null);
            }
            else if (frameMod == Properties.Settings.Default.RefreshEveryNFrames * 4 / 5)
            {
                if (Objectives?.UpdateCompletions(Stopwatch.Elapsed, _memory.Main.ReadBytes(WRAMAddresses.ObjectiveCompletionAddress, WRAMAddresses.ObjectiveCompletionBytes)) == true)
                    ObjectivesUpdated?.Invoke(this, null);
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
                        _container.MemoryEvents.AddExecCallback(Flash, SystemBusAddresses.ZeromusDeathAnimation, "System Bus");
                        break;
                    case RunState.Menu:
                        _container.MemoryEvents.AddExecCallback(StartNewGame, SystemBusAddresses.MenuSaveNewGame, "System Bus");
                        break;
                }
            }
            catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
            {
                if (State != RunState.Unknown)
                {
                    State = RunState.Unknown;
                    MessageBox.Show("Automatic timing of runs is not supported on the Snes9x core. Please use the BSNES or BSNESv115+ core to have this feature.", "Timing unavailable");
                }
            }
        }


        public void Dispose()
        {
            RemoveCallbacks();
        }
    }
}
