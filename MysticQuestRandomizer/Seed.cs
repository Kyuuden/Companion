using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MysticQuestRandomizer.RomData;
using FF.Rando.Companion.MysticQuestRandomizer.View;
using FF.Rando.Companion.Settings;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class Seed : IGame
{
    private int _collectedSkyFragments;
    private bool _started = false;
    private bool _victory = false;
    private readonly Stopwatch _stopwatch = new();
    private readonly Weapons _weapons;
    private readonly Armors _armors;
    private readonly Spells _spells;
    private readonly KeyItems _keyitems;
    private readonly GameInfo _gameinfo;

    internal readonly RomData.Font Font;
    internal readonly Sprites Sprites;

    internal Seed(string hash, Container container)
    {
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        MQRContainer = container ?? throw new ArgumentNullException(nameof(container));

        Settings = container.Settings;
        Sprites = new Sprites(MQRContainer.Rom);
        Font = new RomData.Font(MQRContainer.Rom);

        _gameinfo = GameInfo.Parse(MQRContainer.Rom, Settings, Font);
        _weapons = new Weapons(Sprites);
        _armors = new Armors(Sprites);
        _spells = new Spells(Sprites);
        _keyitems = new KeyItems(Sprites, _gameinfo.RequiredSkyFragmentCount.HasValue);
    }

    public string Hash { get; }

    public Bitmap Icon => MysticQuest.crystal_light;

    public Color BackgroundColor => Color.Black;

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

    public IEmulationContainer Container => MQRContainer;

    internal Container MQRContainer { get; }

    internal Settings.MysticQuestRandomizerSettings Settings { get; }

    internal ISettings RootSettings => MQRContainer.RootSettings;

    GameSettings IGame.Settings => Settings;

    public event Action<string>? ButtonPressed;

    private ImmutableHashSet<string> _lastPressedButtons = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    public IEnumerable<Weapon> Weapons => _weapons.Items;

    public IEnumerable<Armor> Armors => _armors.Items;

    public IEnumerable<Spell> Spells => _spells.Items;

    public IEnumerable<KeyItem> KeyItems => _keyitems.Items;

    public IEnumerable<Element> Elements => _gameinfo.Elements;

    public IEnumerable<Companion> Companions => _gameinfo.Companions;

    public int? RequiredSkyFragmentCount => _gameinfo.RequiredSkyFragmentCount;

    public int CollectedSkyFragments
    {
        get => _collectedSkyFragments;
        set
        {
            if (value == _collectedSkyFragments)
                return;

            _collectedSkyFragments = value;
            NotifyPropertyChanged();
        }
    }

    public Control CreateControls()
    {
        var control =  new MysticQuestRandomizerControl();
        control.InitializeDataSources(this);
        return control;
    }

    public void Dispose()
    {
        Sprites.Dispose();
    }

    public void OnNewFrame()
    {
        var pressed = ImmutableHashSet.CreateRange(MQRContainer.Input.GetPressedButtons());
        foreach (var b in pressed.Except(_lastPressedButtons))
        {
            ButtonPressed?.Invoke(b);
        }
        _lastPressedButtons = pressed;

        if (!Started)
            Started = MQRContainer.Wram.ReadByte(Addresses.WRAM.GameStateIndicator) == 1;

        if (!Victory)
            Victory = (MQRContainer.Wram.ReadByte(Addresses.WRAM.GameVictoryIndicator) & 0x80) == 0x80 &&
                (MQRContainer.Wram.ReadByte(Addresses.WRAM.GameVictoryIndicator2) & 0x18) == 0x18;

        if (Started && MQRContainer.Emulation.FrameCount() % MQRContainer.RootSettings.TrackingInterval == 0)
        {
            ReadOnlySpan<byte> wramData = Container.Wram.ReadBytes(Addresses.WRAM.WramRegion).AsSpan();

            //var checkedBattlefields = wramData.Slice(Addresses.WRAM.CheckedBattlefieldBits);
            //var checkedLocations = wramData.Slice(Addresses.WRAM.GameStateFlags);

            if (RequiredSkyFragmentCount.HasValue)
                CollectedSkyFragments = wramData.Slice(Addresses.WRAM.FoundShards).Read<byte>(0);

            var keyItemsFound = wramData.Slice(Addresses.WRAM.FoundKeyItemBits);
            var weapons = wramData[Addresses.WRAM.FoundWeaponBits];
            var armors = wramData[Addresses.WRAM.FoundArmorBits];
            var spells = wramData[Addresses.WRAM.FoundSpellBits];

            if (_weapons.Update(Elapsed, weapons))
                NotifyPropertyChanged(nameof(Weapons));

            if (_armors.Update(Elapsed, armors))
                NotifyPropertyChanged(nameof(Armors));

            if (_spells.Update(Elapsed, spells))
                NotifyPropertyChanged(nameof(Spells));

            if (_keyitems.Update(Elapsed, keyItemsFound))
                NotifyPropertyChanged(nameof(KeyItems));
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
}
