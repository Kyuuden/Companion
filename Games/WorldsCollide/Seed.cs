using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Games.WorldsCollide.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide;
public class Seed : GameBase<WorldsCollideSettings>
{
    internal readonly RomData.Font Font;
    internal readonly RomData.Backgrounds Backgrounds;
    internal readonly RomData.Sprites Sprites;

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

    private readonly Tracking.Characters _characters;
    private readonly Tracking.Checks _checks;
    private readonly Tracking.Dragons _dragons;
    private readonly Tracking.DragonLocations _dragonLocations;

    private readonly HashSet<Esper> _foundEspers = [];

    internal Seed(string hash, EmulationContainer<WorldsCollideSettings> container)
        :base(hash, container)
    {
        Font = new RomData.Font(container.Rom);
        Backgrounds = new RomData.Backgrounds(container.Rom);
        Sprites = new RomData.Sprites(container.Rom);
        Icon = Sprites.Items.Get(Item.Magicite).Render();
        _spriteSet = GetSpriteSet(Settings.Icons);
        _characters = new Tracking.Characters(this);
        _checks = new Tracking.Checks(this);
        _dragons = new Tracking.Dragons(this);
        _dragonLocations = new Tracking.DragonLocations(this);
        _checks.UpdateRelatedChecks();
        Reward? reward = null;
        _checks.Update(new byte[(int)RomData.Addresses.WRAM.State.Length()], ref reward);
        _dragonLocations.Update(new byte[(int)RomData.Addresses.WRAM.State.Length()]);
        Settings.PropertyChanged += GameSettings_PropertyChanged;
    }

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

    public bool KefkaTowerUnlocked
    {
        get => _kefkaTowerUnlocked;
        protected set
        {
            if (_kefkaTowerSkipUnlocked == value)
                return;

            _kefkaTowerUnlocked = value;
            NotifyPropertyChanged();
        }
    }

    public bool KefkaTowerSkipUnlocked
    {
        get => _kefkaTowerSkipUnlocked; 
        protected set
        {
            if (_kefkaTowerSkipUnlocked = value)
                return;

            _kefkaTowerSkipUnlocked = value;
            NotifyPropertyChanged();
        }
    }

    public bool KefkaTowerStatueOneDefeated
    {
        get => _kefkaTowerStatueOneDefeated;
        protected set
        {
            if (value == _kefkaTowerStatueOneDefeated)
                return;

            _kefkaTowerStatueOneDefeated = value;
            NotifyPropertyChanged();
        }
    }

    public bool KefkaTowerStatueTwoDefeated
    {
        get => _kefkaTowerStatueTwoDefeated;
        protected set
        {
            if (value == _kefkaTowerStatueTwoDefeated)
                return;

            _kefkaTowerStatueTwoDefeated = value;
            NotifyPropertyChanged();
        }
    }

    public bool KefkaTowerStatueThreeDefeated
    {
        get => _kefkaTowerStatueThreeDefeated;
        protected set
        {
            if (value == _kefkaTowerStatueThreeDefeated)
                return;

            _kefkaTowerStatueThreeDefeated = value;
            NotifyPropertyChanged();
        }
    }

    public override Control CreateTrackingControl()
    {
        var control = new WorldsCollideControl();
        control.InitializeDataSources(this);
        return control;
    }

    public override void Dispose()
    {
        Settings.PropertyChanged -= GameSettings_PropertyChanged;
        Font.Dispose();
        Backgrounds.Dispose();
        Sprites.Dispose();
    }

    private Reward? _currentReward;
    private bool _kefkaTowerUnlocked;
    private bool _kefkaTowerSkipUnlocked;
    private bool _kefkaTowerStatueOneDefeated;
    private bool _kefkaTowerStatueTwoDefeated;
    private bool _kefkaTowerStatueThreeDefeated;

    protected override bool CheckIfStarted()
    {
        var mapId = BinaryPrimitives.ReadUInt16LittleEndian(Wram.ReadBytes(RomData.Addresses.WRAM.MapIndex)) & 0x1FF;
        var menuType = Wram.ReadByte(RomData.Addresses.WRAM.MenuType);
        var saveGameSlot = Wram.ReadByte(RomData.Addresses.WRAM.CurrentSaveGameSlot);
        if (mapId == 3 && saveGameSlot == 1 && menuType == 9)
            return true;

        return false;
    }

    protected override bool CheckIfVictory()
    {
        var mapId = BinaryPrimitives.ReadUInt16LittleEndian(Wram.ReadBytes(RomData.Addresses.WRAM.MapIndex)) & 0x1FF;
        if (mapId == 0x164)
        {
            var inKefkaFight = (BinaryPrimitives.ReadUInt16LittleEndian(Wram.ReadBytes(RomData.Addresses.WRAM.BattleIndex)) & 0x3FF) == 0x0202;
            var thunderclap = Wram.ReadByte(0xE9E9) == 0xE3;
            var isKefkaDead = Wram.ReadByte(RomData.Addresses.WRAM.KefkaCrumbleAnimation) == 0x01;
            return inKefkaFight && (thunderclap || isKefkaDead);
        }

        return false;
    }

    protected override void ReadTrackingData()
    {
        var configData = Wram.ReadBytes(RomData.Addresses.WRAM.ConfigData).AsSpan();
        if ((configData[2] & 0xF0) == 0)
        {
            PrimaryFontColor = BinaryPrimitives.ReadUInt16LittleEndian(configData.Slice(8, 2)).ToColor();
            BackgroundPalettes = GetBackgroundPalettes(configData);
            SelectedBackground = configData[1] & 0x7;
        }

        if (Started)
        {
            var eventState = Wram.ReadBytes(RomData.Addresses.WRAM.State).AsReadOnlySpan();
            var dragonState = Wram.ReadBytes(RomData.Addresses.WRAM.Dragons).AsReadOnlySpan();
            var chests = Wram.ReadBytes(RomData.Addresses.WRAM.Chests);
            var espers = Wram.ReadBytes(RomData.Addresses.WRAM.KnownEspers);
            var newEspers = new HashSet<Esper>();

            for (int i = 0; i < espers.Length * 8; i++)
            {
                if (((espers[i / 8] >> (i % 8)) & 0x01) == 1)
                    newEspers.Add(Esper.Ramuh + i);
            }

            var latestEspers = newEspers.Except(_foundEspers).ToList();

            _foundEspers.Clear();
            _foundEspers.UnionWith(newEspers);

            var previousFoundCharacters = Characters.Where(c => c.IsFound).Select(c => c.Event).ToHashSet();

            if (_characters.Update(eventState))
                NotifyPropertyChanged(nameof(Characters));

            var newcharacters = Characters.Where(c => c.IsFound).Select(c => c.Event).ToHashSet();
            newcharacters.ExceptWith(previousFoundCharacters);

            if ((latestEspers.Count + newcharacters.Count) == 1)
            {
                _currentReward = latestEspers.Any() ? latestEspers.First().ToReward() : newcharacters.First().ToReward();
            }

            if (_checks.Update(eventState, ref _currentReward))
                NotifyPropertyChanged(nameof(Checks));

            if (_dragonLocations.Update(eventState))
                NotifyPropertyChanged(nameof(DragonLocations));

            if (_dragons.Update(dragonState, ref _currentReward))
                NotifyPropertyChanged(nameof(Dragons));

            var characterCountData = Wram.ReadBytes(RomData.Addresses.WRAM.CHARACTER_COUNT);
            characterCountData[1] &= 0x3F;

            CharacterCount = characterCountData.CountBits();
            EsperCount = Wram.ReadByte(RomData.Addresses.WRAM.ESPER_COUNT);
            BossCount = Wram.ReadByte(RomData.Addresses.WRAM.BOSS_COUNT);
            DragonCount = Wram.ReadByte(RomData.Addresses.WRAM.DRAGON_COUNT);
            CheckCount = Wram.ReadByte(RomData.Addresses.WRAM.CHECK_COUNT);
            ChestCount = chests.CountBits();

            KefkaTowerUnlocked = eventState.Read<bool>((int)Events.UNLOCKED_FINAL_KEFKA);
            KefkaTowerSkipUnlocked = eventState.Read<bool>((int)Events.UNLOCKED_KT_SKIP);
            KefkaTowerStatueOneDefeated = eventState.Read<bool>((int)Events.DOOM_STATUE_KEFKA_TOWER);
            KefkaTowerStatueTwoDefeated = eventState.Read<bool>((int)Events.GODDESS_STATUE_KEFKA_TOWER);
            KefkaTowerStatueThreeDefeated = eventState.Read<bool>((int)Events.POLTRGEIST_STATUE_KEFKA_TOWER);
        }
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

    public HashSet<Esper> Espers
    {
        get => _foundEspers;
        set
        {
            if (_foundEspers.Equals(value))
                return;

            _foundEspers.IntersectWith(value);
            NotifyPropertyChanged();
        }
    }

    internal IEnumerable<Tracking.Character> Characters => _characters.Values;
    internal IEnumerable<Tracking.Check> Checks => _checks.Values;
    internal IEnumerable<Tracking.Dragon> Dragons => _dragons.Values;
    internal IEnumerable<Tracking.DragonLocation> DragonLocations => _dragonLocations.Values;

    public ISpriteSet SpriteSet
    {
        get => _spriteSet;
        protected set
        {
            if (_spriteSet == value)
                return;

            _spriteSet?.Dispose();
            _spriteSet = value;
            _checks.UpdateRelatedChecks();

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
