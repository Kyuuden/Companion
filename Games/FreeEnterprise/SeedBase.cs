using FF.Rando.Companion.Games.FreeEnterprise.View;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.FreeEnterprise;

public abstract class SeedBase : GameBase<FreeEnterpriseSettings>, ISeed
{
    private decimal? _xpRate = null;
    private int _defeatedEncounters = 0;
    private int? _treasureCount;

    public abstract IKeyItemDescriptor KeyItemDescriptor { get; }

    public abstract IBossDescriptor BossDescriptor { get; }

    public RomData.Font Font { get; }

    public Sprites Sprites { get; }

    public Metadata Metadata { get; }

    public Flags Flags { get; }

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
        get => Math.Max(0, (_treasureCount ?? 0) - (_treasureOffset ?? 0));
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

    public abstract IEnumerable<ILocation> AvailableLocations { get; }

    public SeedBase(string hash, Metadata metadata, EmulationContainer<FreeEnterpriseSettings> container)
        :base(hash, container)
    {
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));

        Font = new RomData.Font(Rom);
        Sprites = new Sprites(Rom);

        Flags = new Flags
        {
            Text = Metadata.Flags == "(hidden)" ? null : Metadata.Flags,
            Binary = Metadata.BinaryFlags == "(hidden)" || Metadata.BinaryFlags == null ? null : ParseBinaryFlags(Metadata.BinaryFlags),
        };

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
            if (data[i] == 0)
            {
                result.AddRange(Enumerable.Repeat((byte)0, data[i + 1]));
                i++;
            }
            else
            {
                result.Add(data[i]);
            }
        }

        return [.. result];
    }

    public override Bitmap Icon { get; }

    public virtual bool CanTackBosses => false;

    public override Control CreateTrackingControl()
    {
        var control = new FreeEnterpriseControl();
        control.InitializeDataSources(this);
        return control;
    }

    public override void Dispose()
    {
        base.Dispose();
        Font.Dispose();
        Sprites.Dispose();
    }
}
