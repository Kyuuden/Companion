using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.JetsOfTime.Data;
using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.Games.JetsOfTime.Tracking;
using FF.Rando.Companion.Games.JetsOfTime.View;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime;
internal class Seed : GameBase<JetsOfTimeSettings>
{
    internal readonly Data.Font Font;
    internal readonly Backgrounds Backgrounds;
    private int _selectedBackground = 0;
    internal readonly Sprites Sprites;
    internal readonly Locations Locations;
    internal readonly WorldMaps WorldMaps;

    public Seed(Flags flags, string hash, EmulationContainer<JetsOfTimeSettings> container)
        :base(hash, container)
    {
        Flags = flags;
        Font = new Data.Font(Rom);
        Backgrounds = new Backgrounds(Rom);
        Sprites = new Sprites(this);
        Locations = new Locations(this);
        WorldMaps = new WorldMaps(this);

        State = new State
        {
            Flags = Flags,
            Events = new Events(),
            KeyItems = new KeyItems(Timer, Flags, Sprites),
            Characters = new Characters(Timer, Sprites),
            Bosses = new Bosses(Timer, Sprites, Locations),
            TimePeriods = new TimePeriods(WorldMaps, Locations)
        };

        State.TimePeriods.Update(State);

        Icon = Sprites.GetPortrait(PortraitType.Epoch)?.Render()!;
    }

    public State State { get; }

    public override Bitmap Icon { get; }

    public override bool RequiresMemoryEventsForTiming => false;

    public override Control CreateTrackingControl()
    {
        var control = new JetsOfTimeControl();
        control.InitializeDataSources(this);
        return control;
    }

    public override void Dispose()
    {
        base.Dispose();
        Font.Dispose();
        Backgrounds.Dispose();
        Sprites.Dispose();
        Locations.Dispose();
        WorldMaps.Dispose();
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

    public Flags Flags { get; }

    protected override void ReadTrackingData()
    {
        SelectedBackground = Wram.ReadByte(Addresses.WRAM.Background) & 0x07;

        if (!Started)
            return;

        State.Update(Wram);
    }

    protected override bool CheckIfStarted()
    {
        return Wram.ReadByte(Addresses.WRAM.RunStartedMarker) != 0;
    }

    protected override bool CheckIfVictory()
    {
        switch (State.CurrentLocation)
        {
            case LocationType.BlackOmenCelestialGate when State.Flags.ZealEnd == true:
                var zeal = Wram.ReadBytes(Addresses.WRAM.BlackOmenZeal2);
                return zeal[0] == (byte)MonsterType.Zeal && BinaryPrimitives.ReadUInt16LittleEndian(zeal.AsSpan()[3..]) == 0;
            case LocationType.Tesseract:
                var core = Wram.ReadBytes(Addresses.WRAM.TesseractCore);
                return core[0] == (byte)MonsterType.Lavos_Bit2 && BinaryPrimitives.ReadUInt16LittleEndian(core.AsSpan()[3..]) == 0;
        }

        return State.CurrentLocation switch
        {
            LocationType.BlackOmenCelestialGate or LocationType.Tesseract => Wram.ReadByte(Addresses.WRAM.Storyline) == 0xD6,
            _ => base.CheckIfVictory(),
        };
    }
}
