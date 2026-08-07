using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.JetsOfTime.Data;
using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.Games.JetsOfTime.Tracking;
using FF.Rando.Companion.Games.JetsOfTime.View;
using System.Collections.Generic;
using System.Drawing;
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
            TimePeriods = new TimePeriods(WorldMaps)
        };

        State.TimePeriods.Update(State);

        Icon = Sprites.GetPortrait(PortraitType.Epoch)?.Render()!;
        CreateCallbacks();
    }

    public State State { get; }

    public override Bitmap Icon { get; }

    public override bool RequiresMemoryEventsForTiming => true;

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

    private void CreateCallbacks()
    {
        try
        {
            if (MemoryEvents == null)
                throw new KeyNotFoundException();

            MemoryEvents?.AddWriteCallback (StartNewGame, 0x7E2990, "System Bus");
        }
        catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
        {
        }
    }

    private uint? StartNewGame(uint address, uint value, uint flags)
    {
        var menuPtr = Wram.ReadBytes(Addresses.WRAM.MenuPointer).Read<uint>(0, 24);

        if (menuPtr == 0x00c2e1e3)
        {
            MemoryEvents?.RemoveMemoryCallback(StartNewGame);
            Started = true;
        }
        return null;
    }

    protected override void ReadTrackingData()
    {
        SelectedBackground = Wram.ReadByte(Addresses.WRAM.Background) & 0x07;

        if (!Started)
            return;

        State.Update(Wram);
    }
}
