using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.FreeEnterprise.View;
using FF.Rando.Companion.Settings;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise;

internal abstract class SeedBase : ISeed
{
    private Color _backgroundColor = Color.FromArgb(0, 0, 99);
    private readonly Stopwatch _stopwatch = new();
    private bool _started = false;
    private bool _victory = false;
    private decimal? _xpRate = null;
    private int _defeatedEncounters = 0;
    private int? _treasureCount;

    public RomData.Font Font { get; }
    public Sprites Sprites { get; }
    public string Hash { get; }
    public Metadata Metadata { get; }
    public Flags Flags { get; }
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

    public abstract IEnumerable<ICharacter> Party { get; }

    public abstract IEnumerable<IKeyItem> KeyItems { get; }

    public abstract IEnumerable<IBoss> Bosses { get; }

    public int DefeatedEncounters
    {
        get => _defeatedEncounters;
        protected set
        {
            if (_defeatedEncounters != value)
            {
                _defeatedEncounters = value;
                NotifyPropertyChanged();
            }
        }
    }

    public decimal? XpRate
    {
        get => _xpRate;
        protected set
        {
            if (_xpRate != value)
            {
                _xpRate = value;
                NotifyPropertyChanged();
            }
        }
    }

    private int? _treasureOffset = null;

    public int TreasureCount
    {
        get => _treasureCount ?? 0 - _treasureOffset ?? 0;
        protected set
        {
            if (_treasureCount != value)
            {
                if (_treasureCount.HasValue)
                {
                    _treasureCount = value;
                    NotifyPropertyChanged();
                }
                else if (value != 0)
                {
                    if (value > 10)
                        _treasureOffset = value;
                    else
                        _treasureOffset = 0;

                    _treasureCount = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    public abstract IEnumerable<IObjectiveGroup> Objectives { get; }

    public Color BackgroundColor
    {
        get => _backgroundColor;
        protected set
        {
            if (value != _backgroundColor)
            {
                _backgroundColor = value;
                NotifyPropertyChanged();
            }
        }
    }

    public event Action<string>? ButtonPressed;

    private ImmutableHashSet<string> _lastPressedButtons = [];

    public TimeSpan Elapsed { get => _stopwatch.Elapsed; }
    public abstract IEnumerable<ILocation> AvailableLocations { get; }
    public virtual void OnNewFrame()
    {
        var pressed = ImmutableHashSet.CreateRange(Game.Input.GetPressedButtons());
        foreach (var b in pressed.Except(_lastPressedButtons))
        {
            ButtonPressed?.Invoke(b);
        }
        _lastPressedButtons = pressed;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected Container Game { get; private set; }

    public ISettings RootSettings => Game.RootSettings;

    public IEmulationContainer Container => Game;

    public SeedBase(string hash, Metadata metadata, Container container)
    {
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        Game = container ?? throw new ArgumentNullException(nameof(container));

        Font = new RomData.Font(Game.Rom);
        Sprites = new Sprites(Game.Rom);

        Flags = new Flags
        {
            Text = Metadata.Flags == "(hidden)" ? null : Metadata.Flags,
            Binary = Metadata.BinaryFlags == "(hidden)" || Metadata.BinaryFlags == null ? null : ParseBinaryFlags(Metadata.BinaryFlags),
        };

        Settings = container.Settings;
        Icon = FreeEnterprise.FFIVFE_Icons_1THECrystal_Color;
    }

    private byte[] ParseBinaryFlags(string base64)
    {
        var code = base64[0];

        base64 = base64[1..];
        while (base64.Length % 4 != 0)
            base64 += '=';
        base64 = base64.Replace('-', '+').Replace('_', '/');

        var bytes = Convert.FromBase64String(base64);

        if (code == 'c')
            bytes = Decompress(bytes);

        return bytes.Skip(3).ToArray();
    }

    private byte[] Decompress(byte[] data)
    {
        var result = new List<byte>();

        for (var i = 0; i < data.Length; i++)
        {
            if (data [i] == 0) 
            {
                result.AddRange(Enumerable.Repeat((byte)0, data[i + 1]));
                i++; 
            }
            else
            {
                result.Add(data [i]);
            }
        }

        return [.. result];
    }

    public FreeEnterpriseSettings Settings { get; private set; }

    GameSettings IGame.Settings => Settings;

    public Bitmap Icon { get; }
    public virtual bool CanTackBosses => false;

    public abstract bool RequiresMemoryEvents { get; }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Control CreateControls()
    {
        var control = new FreeEnterpriseControl();
        control.InitializeDataSources(this);
        return control;
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

    public void Dispose()
    {
        Font.Dispose();
        Sprites.Dispose();
    }
}
