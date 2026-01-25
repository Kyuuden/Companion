using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.WorldsCollide.Enums;
using FF.Rando.Companion.WorldsCollide.Settings;
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

namespace FF.Rando.Companion.WorldsCollide;
public class Seed : IGame
{
    private bool _started = false;
    private bool _victory = false;
    internal readonly RomData.Font Font;
    internal readonly RomData.Backgrounds Backgrounds;
    internal readonly RomData.Sprites Sprites;
    private Color32 _primaryFontColor;
    private List<Palette> _backgroundPalettes;
    private int _selectedBackground;
    private readonly Stopwatch _stopwatch = new();
    private readonly Descriptors _descriptors = new ();

    private int _characterCount;
    private int _esperCount;
    private int _dragonCount;
    private int _bossCount;
    private int _checkCount;
    private int _chestCount;

    private readonly Tracking.Checks _characters;
    private readonly Tracking.Checks _checks;
    private readonly Tracking.Checks _dragons;
    private readonly Tracking.Checks _dragonLocations;

    internal Seed(string hash, Container container)
    {
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        WorldsCollideContainer = container ?? throw new ArgumentNullException(nameof(container));
        Font = new RomData.Font(container.Rom);

        var configData = container.Wram.ReadBytes(RomData.Addresses.WRAM.ConfigData).AsSpan();
        _primaryFontColor =  BinaryPrimitives.ReadUInt16LittleEndian(configData.Slice(8, 2)).ToColor();
        Font.UpdateFontColor(_primaryFontColor);
        _backgroundPalettes = GetBackgroundPalettes(configData);
        Backgrounds = new RomData.Backgrounds(container.Rom, _backgroundPalettes);
        Sprites = new RomData.Sprites(container.Rom);
        Icon = Sprites.Misc.Get(Item.Magicite).Render();

        _characters = new Tracking.Checks(this, _descriptors, Settings, e => e.IsCharacter());
        _checks = new Tracking.Checks(this, _descriptors, Settings, e => e.IsCheck());
        _dragons = new Tracking.Checks(this, _descriptors, Settings, e => e.IsDragon());
        _dragonLocations = new Tracking.Checks(this, _descriptors, Settings, e => e.IsDragonLocation());
    }

    private List<Palette> GetBackgroundPalettes(ReadOnlySpan<byte> configData)
    {
        var palettes = new List<Palette>();
        var colors = new List<Color32>();
        foreach (var c in MemoryMarshal.Cast<byte, ushort>(configData.Slice(9, 112)))
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

    public Color BackgroundColor => BackgroundPalettes[SelectedBackground].GetColor(7);

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
            NotifyPropertyChanged(nameof(BackgroundColor));
        }
    }

    public List<Palette> BackgroundPalettes
    {
        get => _backgroundPalettes;
        protected set
        {
            if (_backgroundPalettes.SequenceEqual(value)) return;
            _backgroundPalettes = value;
            NotifyPropertyChanged();
            NotifyPropertyChanged(nameof(BackgroundColor));
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
        return new Control();
    }

    public void Dispose()
    {
        Font.Dispose();
        Backgrounds.Dispose();
        Sprites.Dispose();
    }

    public void OnNewFrame()
    {
        
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
    internal IEnumerable<Tracking.Check> Dragons => _dragons.Values;
    internal IEnumerable<Tracking.Check> DragonLocations => _dragonLocations.Values;
}
