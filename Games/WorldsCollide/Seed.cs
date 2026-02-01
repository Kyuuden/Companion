using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Games.WorldsCollide.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Immutable;

namespace FF.Rando.Companion.Games.WorldsCollide;
public class Seed : IGame
{
    internal readonly RomData.Font Font;
    internal readonly RomData.Backgrounds Backgrounds;
    internal readonly RomData.Sprites Sprites;
    private readonly Stopwatch _stopwatch = new();

    private bool _started = false;
    private bool _victory = false;
    private Color32 _primaryFontColor;
    private List<Palette> _backgroundPalettes = [];
    private int _selectedBackground;
    private ISpriteSet _spriteSet;

    private int _characterCount;
    private int _esperCount;
    private int _dragonCount;
    private int _bossCount;
    private int _checkCount;
    private int _chestCount;

    private readonly Tracking.Checks _characters;
    private readonly Tracking.Checks _checks;
    private readonly Tracking.Dragons _dragons;
    private readonly Tracking.Checks _dragonLocations;

    internal Seed(string hash, Container container)
    {
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        WorldsCollideContainer = container ?? throw new ArgumentNullException(nameof(container));
        Font = new RomData.Font(container.Rom);
        Backgrounds = new RomData.Backgrounds(container.Rom);
        Sprites = new RomData.Sprites(container.Rom);

        Icon = Sprites.Items.Get(Item.Magicite).Render();

        _spriteSet = GetSpriteSet(Settings.Icons);
        _characters = new Tracking.Checks(this, e => e.IsCharacter());
        _checks = new Tracking.Checks(this, e => e.IsCheck());
        _dragons = new Tracking.Dragons(this);
        _dragonLocations = new Tracking.Checks(this, e => e.IsDragonLocation());

        _checks.UpdateRelatedChecks();

        Settings.PropertyChanged += Settings_PropertyChanged;
    }

    private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(WorldsCollideSettings.Icons))
            SpriteSet = GetSpriteSet(Settings.Icons);
    }

    private List<Palette> GetBackgroundPalettes(ReadOnlySpan<byte> configData)
    {
        var palettes = new List<Palette>();
        var colors = new List<Color32>();
        foreach (var c in MemoryMarshal.Cast<byte, ushort>(configData.Slice(10, 112)))
        {
            if (colors.Count == 0)
                colors.Add(new Color32());

            colors.Add(c.ToColor());

            if (colors.Count != 8)
                continue;

            palettes.Add(new Palette(colors));
            colors.Clear();
        }

        return palettes;
    }

    public string Hash { get; }

    public Bitmap Icon { get; }

    public Color BackgroundColor => /*_selectedBackground < _backgroundPalettes.Count ? _backgroundPalettes[SelectedBackground][0] :*/ Color.Black;

    public Color32 PrimaryFontColor
    {
        get => _primaryFontColor;
        protected set
        {
            if (_primaryFontColor == value) return;
            _primaryFontColor = value;
            Font.UpdateFontColor(value);
            NotifyPropertyChanged();
        }
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

    public List<Palette> BackgroundPalettes
    {
        get => _backgroundPalettes;
        protected set
        {
            var equals = true;
            if (value.Count != _backgroundPalettes.Count)
                equals = false;

            for (int i = 0; equals && i < _backgroundPalettes.Count; i++)
            {
                if (_backgroundPalettes[i].Count != value[i].Count)
                    equals = false;

                for (var c = 0; equals && c < _backgroundPalettes[i].Count; c++)
                {
                    if (!_backgroundPalettes[i][c].Equals(value[i][c]))
                        equals = false;
                }
            }

            if (equals) return;
            _backgroundPalettes = value;
            Backgrounds.UpdatePalettes(value);
            NotifyPropertyChanged();
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
                    _stopwatch.Start();
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
                    _stopwatch.Stop();
            }
        }
    }

    public TimeSpan Elapsed { get => _stopwatch.Elapsed; }

    public bool RequiresMemoryEvents => false;

    public IEmulationContainer Container => WorldsCollideContainer;

    internal Container WorldsCollideContainer { get; }

    internal WorldsCollideSettings Settings => WorldsCollideContainer.Settings;

    internal ISettings RootSettings => WorldsCollideContainer.RootSettings;

    GameSettings IGame.Settings => Settings;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Control CreateControls()
    {
        var control = new WorldsCollideControl();
        control.InitializeDataSources(this);
        return control;
    }

    public void Dispose()
    {
        Settings.PropertyChanged -= Settings_PropertyChanged;
        Font.Dispose();
        Backgrounds.Dispose();
        Sprites.Dispose();
    }

    public void OnNewFrame()
    {
        if (!Started)
        {
            var mapId = BinaryPrimitives.ReadUInt16LittleEndian(Container.Wram.ReadBytes(RomData.Addresses.WRAM.MapIndex)) & 0x1FF;
            var menuType = Container.Wram.ReadByte(RomData.Addresses.WRAM.MenuType);
            var saveGameSlot = Container.Wram.ReadByte(RomData.Addresses.WRAM.CurrentSaveGameSlot);
            if (mapId == 3 && saveGameSlot == 1 && menuType == 9)
                Started = true;
        }

        if (!Victory)
        {
            var mapId = BinaryPrimitives.ReadUInt16LittleEndian(Container.Wram.ReadBytes(RomData.Addresses.WRAM.MapIndex)) & 0x1FF;
            if (mapId == 0x164)
            {

                var inKefkaFight = BinaryPrimitives.ReadUInt16LittleEndian(Container.Wram.ReadBytes(RomData.Addresses.WRAM.MapIndex)) & 0x3FF;
                var thunderclap = Container.Wram.ReadByte(0xE9E9) == 0xE3;
                //var isKefkaDead = Container.Wram.ReadByte(RomData.Addresses.WRAM.KefkaCrumbleAnimation) == 0x01;
                Victory = inKefkaFight == 0x0202 && thunderclap;// isKefkaDead;
            }
        }

        if (WorldsCollideContainer.Emulation.FrameCount() % WorldsCollideContainer.RootSettings.TrackingInterval == 0)
        {
            var configData = Container.Wram.ReadBytes(RomData.Addresses.WRAM.ConfigData).AsSpan();
            PrimaryFontColor = BinaryPrimitives.ReadUInt16LittleEndian(configData.Slice(8, 2)).ToColor();
            BackgroundPalettes = GetBackgroundPalettes(configData);
            SelectedBackground = configData[1] & 0x7;

            if (Started)
            {
                var eventState = Container.Wram.ReadBytes(RomData.Addresses.WRAM.State).AsSpan();
                var dragonState = Container.Wram.ReadBytes(RomData.Addresses.WRAM.Dragons).AsSpan();
                var chests = Container.Wram.ReadBytes(RomData.Addresses.WRAM.Chests);

                if (_checks.Update(Elapsed, eventState))
                    NotifyPropertyChanged(nameof(Checks));

                if (_characters.Update(Elapsed, eventState))
                    NotifyPropertyChanged(nameof(Characters));

                if (_dragonLocations.Update(Elapsed, eventState))
                    NotifyPropertyChanged(nameof(DragonLocations));

                if (_dragons.Update(Elapsed, dragonState))
                    NotifyPropertyChanged(nameof(Dragons));

                var characterCountData = Container.Wram.ReadBytes(RomData.Addresses.WRAM.CHARACTER_COUNT);
                characterCountData[1] &= 0x3F;

                CharacterCount = characterCountData.CountBits();
                EsperCount = Container.Wram.ReadByte(RomData.Addresses.WRAM.ESPER_COUNT);
                BossCount = Container.Wram.ReadByte(RomData.Addresses.WRAM.BOSS_COUNT);
                DragonCount = Container.Wram.ReadByte(RomData.Addresses.WRAM.DRAGON_COUNT);
                CheckCount = Container.Wram.ReadByte(RomData.Addresses.WRAM.CHECK_COUNT);
                ChestCount = chests.CountBits();
            }
        }
    }

    public void Pause()
    {
        if (Started && _stopwatch.IsRunning)
            _stopwatch.Stop();
    }

    public void Unpause()
    {
        if (Started && !Victory && !_stopwatch.IsRunning)
            _stopwatch.Start();
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public int ChestCount
    {
        get => _chestCount;
        protected set
        {
            if (_chestCount != value)
            {
                _chestCount = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int DragonCount
    {
        get => _dragonCount;
        protected set
        {
            if (_dragonCount != value)
            {
                _dragonCount = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int CheckCount
    {
        get => _checkCount;
        protected set
        {
            if (_checkCount != value)
            {
                _checkCount = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int CharacterCount
    {
        get => _characterCount;
        protected set
        {
            if (_characterCount != value)
            {
                _characterCount = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int EsperCount
    {
        get => _esperCount;
        protected set
        {
            if (_esperCount != value)
            {
                _esperCount = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int BossCount
    {
        get => _bossCount;
        protected set
        {
            if (_bossCount != value)
            {
                _bossCount = value;
                NotifyPropertyChanged();
            }
        }
    }

    internal IEnumerable<Tracking.Check> Characters => _characters.Values;
    internal IEnumerable<Tracking.Check> Checks => _checks.Values;
    internal IEnumerable<Tracking.Dragon> Dragons => _dragons.Values;
    internal IEnumerable<Tracking.Check> DragonLocations => _dragonLocations.Values;

    public ISpriteSet SpriteSet
    {
        get => _spriteSet;
        protected set
        {
            if (_spriteSet == value)
                return;

            _spriteSet?.Dispose();
            _spriteSet = value;

            if (_checks.UpdateRelatedChecks(true))
                NotifyPropertyChanged(nameof(Checks));

            NotifyPropertyChanged();
        }
    }

    internal Descriptors Descriptors { get; } = new Descriptors();

    private ISpriteSet GetSpriteSet(SpriteSetType type)
    {
        return type switch
        {
            SpriteSetType.VanillaBosses => new SerializedSpriteSet(Sprites, Font, DefaultSpriteSets.VanillaBosses),
            SpriteSetType.Locations => new SerializedSpriteSet(Sprites, Font, DefaultSpriteSets.LocationBased),
            _ => new SerializedSpriteSet(Sprites, Font, DefaultSpriteSets.LocationBased),
        };
    }
}
