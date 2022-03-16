using BizHawk.Client.Common;
using BizHawk.Common;
using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.FlagSet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public enum Runstate
    {
        Loading,
        Menu,
        RunStarted,
        ZeromusWatch,
        RunFinished
    }

    public class Run : IDisposable
    {
        private enum EventType
        {
            RunStarted,
            RunEnded,
            PartyUpdated,
            KeyItemsUpdated,
            LocationsUpdated,
            ObjectivesUpdated,
            CustomSettingsUpdated,
        }

        private readonly ApiContainer _container;
        private readonly MemoryMapping _memory;
        private readonly HashSet<EventType> pendingEvents = new HashSet<EventType>();
        private Color backgroundColor;
        private Party party;
        private KeyItems keyItems;

        public Run(ApiContainer container, MemoryMapping memory)
        {
            _container = container;
            _memory = memory;

            Hash = _container.GameInfo.GetGameInfo()?.Hash;
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

            keyItems = new KeyItems();
            party = new Party();
            Locations = new Locations(FlagSet);
            ReadBackgroundColor();

            Stopwatch = new Stopwatch();
            Runstate = Runstate.Loading;
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

        public Runstate Runstate { get; private set; }

        public KeyItems KeyItems
        {
            get => keyItems;
            private set
            {
                if (value.Equals(keyItems))
                    return;

                keyItems = value;
                KeyItemsUpdated?.Invoke(this, null);

                if (Locations?.UpdateKeyItems(KeyItems) ?? false)
                    LocationsUpdated?.Invoke(this, null);
            }
        }

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

        public Locations Locations { get; private set; }

        public Objectives Objectives { get; private set; }

        public Stopwatch Stopwatch { get; private set; }

        private void ReadBackgroundColor()
        {
            var color = _memory.Main.ReadBytes(
                WRAMAddresses.BackgroundColorAddress,
                WRAMAddresses.BackgroundColorBytes).Read<ushort>(0);

            var red = color & 0x1F;
            var green = color >> 5 & 0x1F;
            var blue = color >> 10 & 0x1F;
            BackgroundColor = Color.FromArgb((int)(red / 31.0 * 255), (int)(green / 31.0 * 255), (int)(blue / 31.0 * 255));
        }

        public void Flash (uint address, uint value, uint flags)
        {
            Runstate = Runstate.RunFinished;
            RunEnded?.Invoke(this, null);
            Stopwatch.Stop();
        }

        private void CheckStarted()
        {
            try
            {
                if (Runstate == Runstate.RunStarted)
                    return;

                var PC = _container.Emulation.GetRegister("PC");
                var X = _container.Emulation.GetRegister("X");
                var Y = _container.Emulation.GetRegister("Y");

                switch (Runstate)
                {
                    case Runstate.Loading:
                        if (PC >= 0x181AE && PC <= 0x181B5)
                        {
                            Runstate = Runstate.Menu;
                            LoadComplete?.Invoke(this, null);
                            ObjectivesUpdated?.Invoke(this, null);
                        }
                        break;
                    case Runstate.Menu:
                        if (X == 0x2e9 && Y == 0x8189)
                        {
                            Runstate = Runstate.RunStarted;
                            Log.Note("FE", "Runstate = Runstate.RunStarted");
                            Stopwatch = Stopwatch.StartNew();
                            _container.MemoryEvents.AddExecCallback(Flash, 0x03F591, "System Bus");
                            RunStarted?.Invoke(this, null);
                        }
                        break;
                }
            }
            catch (NullReferenceException)
            {
                //early in loading
            }
        }

        public void NewFrame()
        {
            CheckStarted();

            var frameMod = _container.Emulation.FrameCount() % Properties.Settings.Default.RefreshEveryNFrames;
            if (frameMod == 0)
            { 
                Party = new Party(_memory.Main.ReadBytes(WRAMAddresses.PartyAddress, WRAMAddresses.PartyBytes)); 
            }
            else if (frameMod == Properties.Settings.Default.RefreshEveryNFrames / 5)
            {
                KeyItems = new KeyItems(
                   _memory.Main.Read<KeyItemType>(WRAMAddresses.FoundKeyItems, WRAMAddresses.FoundKeyItemsBytes),
                   _memory.Main.Read<KeyItemType>(WRAMAddresses.UsedKeyItems, WRAMAddresses.UsedKeyItemsBytes),
                   _memory.CartSaveRam.ReadBytes(CARTRAMAddresses.KeyItemLocations, CARTRAMAddresses.KeyItemLocationsBytes));
            }
            else if (frameMod == Properties.Settings.Default.RefreshEveryNFrames * 2 / 5)
            {
                ReadBackgroundColor();
            }
            else if (frameMod == Properties.Settings.Default.RefreshEveryNFrames * 3 / 5)
            {
                if (Locations.UpdateCheckedLocations(_memory.Main.ReadBytes(WRAMAddresses.CheckedLocations, WRAMAddresses.CheckedLocationsBytes)))
                    LocationsUpdated?.Invoke(this, null);
            }
            else if (frameMod == Properties.Settings.Default.RefreshEveryNFrames * 4 / 5)
            {
                if (Objectives?.UpdateCompletions(_memory.Main.ReadBytes(WRAMAddresses.ObjectiveCompletionAddress, WRAMAddresses.ObjectiveCompletionBytes)) == true)
                    ObjectivesUpdated?.Invoke(this, null);
            }
        }

        public void Dispose()
        {
        }
    }
}
