using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Rendering;
using FF.Rando.Companion.Games.WorldsCollide.RomData;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Games.WorldsCollide.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide;
internal class Seed : GameBase<WorldsCollideSettings>
{
    internal readonly RomData.Font Font;
    internal readonly Backgrounds Backgrounds;
    internal readonly Sprites Sprites;
    internal readonly WorldMaps WorldMaps;
    internal readonly LocationMaps LocationMaps;

    private Color32 _primaryFontColor;
    private List<Palette> _backgroundPalettes = [];
    private int _selectedBackground;
    private ISpriteSet _spriteSet;

    internal Seed(string hash, EmulationContainer<WorldsCollideSettings> container)
        :base(hash, container)
    {
        Font = new RomData.Font(container.Rom);
        Backgrounds = new Backgrounds(container.Rom);
        Sprites = new Sprites(container.Rom);
        WorldMaps = new WorldMaps(this);
        LocationMaps = new LocationMaps(this);
        Icon = Sprites.Items.Get(Item.Magicite).Render();

        State = new State()
        {
            Events = new(),
            Worlds = new(WorldMaps)
        };

        _spriteSet = GetSpriteSet(Settings.Icons);

        Settings.PropertyChanged += GameSettings_PropertyChanged;
    }

    public State State { get; }

    public override Bitmap Icon { get; }

    private void GameSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

    public ISpriteSet SpriteSet
    {
        get => _spriteSet;
        protected set
        {
            if (_spriteSet == value)
                return;

            _spriteSet?.Dispose();
            _spriteSet = value;

            NotifyPropertyChanged();
        }
    }

    private ISpriteSet GetSpriteSet(SpriteSetType type)
    {
        return type switch
        {
            SpriteSetType.VanillaBosses => new SerializedSpriteSet(Sprites, Font, LocationMaps, DefaultSpriteSets.VanillaBosses),
            SpriteSetType.Locations => new SerializedSpriteSet(Sprites, Font, LocationMaps, DefaultSpriteSets.LocationBased),
            _ => new SerializedSpriteSet(Sprites, Font, LocationMaps, DefaultSpriteSets.LocationBased),
        };
    }

    public override Control CreateTrackingControl() => new WorldsCollideControl(this);

    public override void Dispose()
    {
        Settings.PropertyChanged -= GameSettings_PropertyChanged;
        Font.Dispose();
        Backgrounds.Dispose();
        Sprites.Dispose();
    }

    protected override bool CheckIfStarted()
    {
        var mapId = State.CurrentLocation = (ushort)(BinaryPrimitives.ReadUInt16LittleEndian(Wram.ReadBytes(Addresses.WRAM.MapIndex)) & 0x1FF);
        var menuType = Wram.ReadByte(Addresses.WRAM.MenuType);
        var saveGameSlot = Wram.ReadByte(Addresses.WRAM.CurrentSaveGameSlot);
        if (mapId == 3 && saveGameSlot == 1 && menuType == 9)
            return true;

        return false;
    }

    protected override bool CheckIfVictory()
    {
        var mapId = State.CurrentLocation = (ushort)(BinaryPrimitives.ReadUInt16LittleEndian(Wram.ReadBytes(Addresses.WRAM.MapIndex)) & 0x1FF);
        if (mapId == 0x164)
        {
            var inKefkaFight = (BinaryPrimitives.ReadUInt16LittleEndian(Wram.ReadBytes(Addresses.WRAM.BattleIndex)) & 0x3FF) == 0x0202;
            var thunderclap = Wram.ReadByte(0xE9E9) == 0xE3;
            var isKefkaDead = Wram.ReadByte(Addresses.WRAM.KefkaCrumbleAnimation) == 0x01;
            return inKefkaFight && (thunderclap || isKefkaDead);
        }

        return false;
    }

    protected override void ReadTrackingData()
    {
        var configData = Wram.ReadBytes(Addresses.WRAM.ConfigData).AsSpan();
        if ((configData[2] & 0xF0) == 0)
        {
            PrimaryFontColor = BinaryPrimitives.ReadUInt16LittleEndian(configData.Slice(8, 2)).ToColor();
            BackgroundPalettes = GetBackgroundPalettes(configData);
            SelectedBackground = configData[1] & 0x7;
        }

        if (!Started)
            return;

        State.Update(Wram, Timer);
    }
}
