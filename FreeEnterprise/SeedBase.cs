using FF.Rando.Companion.FreeEnterprise.RomData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Immutable;
using FF.Rando.Companion.FreeEnterprise.Settings;

namespace FF.Rando.Companion.FreeEnterprise;

internal abstract class SeedBase : ISeed
{
    private Color _backgroundColor = Color.FromArgb(0, 0, 99);
    private bool _started = false;
    private bool _victory = false;
    private decimal? _xpRate = null;
    private int _treasureCount = 0;

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
            }
        }
    }

    public abstract IEnumerable<ICharacter> Party { get; }

    public abstract IEnumerable<IKeyItem> KeyItems { get; }

    public abstract IEnumerable<IBoss> Bosses { get; }

    public abstract int? MaxPartySize { get; protected set; }

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
        get => _treasureCount - (_treasureOffset ?? 0);
        protected set
        {
            if (_treasureCount != value)
            {
                if (!_treasureOffset.HasValue && _treasureCount == 0 && value > 10)
                    _treasureOffset = value;

                _treasureCount = value;
                NotifyPropertyChanged();
            }
        }
    }

    public abstract IEnumerable<IObjectiveGroup> Objectives { get; protected set; }

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

    public abstract TimeSpan Elapsed { get; protected set; }
    public abstract IEnumerable<ILocation> CheckedLocations { get; }
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

    public SeedBase(string hash, Metadata metadata, Container container)
    {
        if (container == null) throw new ArgumentNullException(nameof(container));

        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        Game = container;

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

        base64 = base64.Substring(1);
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

    public Bitmap Icon { get; }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public abstract Control CreateControls();

    public abstract void Pause();
    public abstract void Unpause();

    public void Dispose()
    {
        Font.Dispose();
        Sprites.Dispose();
    }
}
