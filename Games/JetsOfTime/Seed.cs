using FF.Rando.Companion.Games.JetsOfTime.Data;
using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.Games.JetsOfTime.Tracking;
using FF.Rando.Companion.Games.JetsOfTime.View;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Settings;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime;
internal class Seed : IGame
{
    private bool _started = false;
    private bool _victory = false;
    internal readonly Data.Font Font;
    internal readonly Backgrounds Backgrounds;
    private int _selectedBackground = 0;

    internal readonly SpriteDB SpriteDB;
    internal readonly LocationDB LocationDB;

    public Seed(string hash, Container container)
    {
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        JetsOfTimeContainer = container ?? throw new ArgumentNullException(nameof(container));
        Font = new Data.Font(container.Rom);
        Backgrounds = new Backgrounds(container.Rom);
        SpriteDB = new SpriteDB(container);
        LocationDB = new LocationDB(container);
        KeyItems = new KeyItems(container, SpriteDB);
        Characters = new Characters(this);
        Bosses = new Bosses(container, SpriteDB, LocationDB);
        Icon = SpriteDB.GetPortrait(PortraitType.Epoch)?.Render()!;
        CreateCallbacks();
    }

    public KeyItems KeyItems { get; }

    public Characters Characters { get; }

    public Bosses Bosses { get; }

    public string Hash { get; }

    public Bitmap Icon { get; }

    public Color BackgroundColor => Color.Black;

    public bool RequiresMemoryEvents => true;

    public IEmulationContainer Container => JetsOfTimeContainer;

    internal Container JetsOfTimeContainer { get; }

    internal JetsOfTimeSettings Settings => JetsOfTimeContainer.Settings;

    internal ISettings RootSettings => JetsOfTimeContainer.RootSettings;

    GameSettings IGame.Settings => Settings;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Control CreateControls()
    {
        var control = new JetsOfTimeControl();
        control.InitializeDataSources(this);
        return control;
    }

    public void Dispose()
    {
        Font.Dispose();
        Backgrounds.Dispose();
        SpriteDB.Dispose();
        LocationDB.Dispose();
    }

    public int SelectedBackground
    {
        get => _selectedBackground;
        protected set
        {
            if (_selectedBackground == value) return;
            _selectedBackground = value;
            NotifyPropertyChanged();
        }
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private byte[]? lastEvents;

    private void CreateCallbacks()
    {
        try
        {
            if (Container.MemoryEvents == null)
                throw new KeyNotFoundException();

            Container.MemoryEvents?.AddWriteCallback (StartNewGame, 0x7E2990, "System Bus");
        }
        catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
        {
        }
    }

    private uint? StartNewGame(uint address, uint value, uint flags)
    {
        var programCounter = Container.Emulation.GetRegister("PC");

        if (programCounter == 0xc2e25b || programCounter == 0xc2e256)
        {
            Container.MemoryEvents?.RemoveMemoryCallback(StartNewGame);
            Started = true;
        }
        return null;
    }

    public void OnNewFrame()
    {
        if (JetsOfTimeContainer.Emulation.FrameCount() % JetsOfTimeContainer.RootSettings.TrackingInterval == 0)
        {
            SelectedBackground = Container.Wram.ReadByte(Addresses.WRAM.Background) & 0x07;

            if (!Started)
                return;

            var events = Container.Wram.ReadBytes(Addresses.WRAM.EventData).AsSpan();
            var party = Container.Wram.ReadBytes(Addresses.WRAM.PartyData).AsSpan();
            var inventory = Container.Wram.ReadBytes(Addresses.WRAM.InventoryData);
            var equipment = Container.Wram.ReadBytes(Addresses.WRAM.EquipmentData);

            if (BinaryPrimitives.ReadUInt16LittleEndian(party) == 0)
                return;

            if (BinaryPrimitives.ReadUInt16LittleEndian(events) == 0x4140 &&
                BinaryPrimitives.ReadUInt16LittleEndian(events[2..]) == 0x4342)
                return;

            if (lastEvents == null || events.SequenceCompareTo(lastEvents) != 0)
            {
                lastEvents = events.ToArray();
            }

            Characters.Update(party);
            KeyItems.Update(inventory, events, equipment);
            Bosses.Update(events);
        }
    }

    public bool Started
    {
        get => _started;
        protected set
        {
            if (!_started && value)
            {
                _started = true;
                NotifyPropertyChanged();
                if (_started)
                {
                    Container.Timer.Start();
                }
            }
        }
    }

    public bool Victory
    {
        get => _victory;
        protected set
        {
            if (!_victory && value)
            {
                _victory = true;
                NotifyPropertyChanged();
                if (_victory)
                {
                    Container.Timer.Stop();
                }
            }
        }
    }
}
