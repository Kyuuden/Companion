using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.MysticQuestRandomizer.RomData;
using FF.Rando.Companion.Games.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;
using FF.Rando.Companion.Games.MysticQuestRandomizer.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer;

public class Seed : GameBase<MysticQuestRandomizerSettings> 
{
    private int _collectedSkyFragments;
    private readonly Weapons _weapons;
    private readonly Armors _armors;
    private readonly Spells _spells;
    private readonly KeyItems _keyitems;
    private readonly GameInfo _gameinfo;
    internal readonly RomData.Font Font;
    internal readonly Sprites Sprites;

    internal Seed(string hash, EmulationContainer<MysticQuestRandomizerSettings> container)
        :base(hash, container)
    {
        Sprites = new Sprites(Rom);
        Font = new RomData.Font(Rom);
        _gameinfo = GameInfo.Parse(this, Rom);
        _weapons = new Weapons(this);
        _armors = new Armors(this);
        _spells = new Spells(this);
        _keyitems = new KeyItems(this, _gameinfo.RequiredSkyFragmentCount.HasValue);
    }

    public override Bitmap Icon => MysticQuest.crystal_light;

    public IEnumerable<Weapon> Weapons => _weapons.Items;

    public IEnumerable<Armor> Armors => _armors.Items;

    public IEnumerable<Spell> Spells => _spells.Items;

    public IEnumerable<KeyItem> KeyItems => _keyitems.Items;

    public IEnumerable<Element> Elements => _gameinfo.Elements;

    public IEnumerable<Tracking.Companion> Companions => _gameinfo.Companions;

    public GameState StateFlags { get; } = new GameState();

    public Battlefields Battlefields { get; } = new Battlefields();

    public int? RequiredSkyFragmentCount => _gameinfo.RequiredSkyFragmentCount;

    public SkyCoinMode SkyCoinMode
    {
        get
        {
            if (_gameinfo.RequiredSkyFragmentCount.HasValue)
                return SkyCoinMode.Shattered;

            if (_gameinfo.SaveTheCrystals)
                return SkyCoinMode.SaveTheCrystals;

            return SkyCoinMode.Standard;
        }
    }

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

    public override Control CreateTrackingControl()
    {
        var control = new MysticQuestRandomizerControl();
        control.InitializeDataSources(this);
        return control;
    }

    public override void Dispose()
    {
        base.Dispose();
        Font.Dispose();
        Sprites.Dispose();
    }

    private byte[]? lastLocations;

    protected override bool CheckIfStarted()
    {
        return Wram.ReadByte(Addresses.WRAM.GameStateIndicator) == 1;
    }

    protected override bool CheckIfVictory()
    {
        return (Wram.ReadByte(Addresses.WRAM.GameVictoryIndicator) & 0x80) == 0x80 &&
                Wram.ReadBytes(Addresses.WRAM.Mob1Health).Read<ushort>(0, 16) is ushort.MinValue or ushort.MaxValue &&
                Wram.ReadBytes(Addresses.WRAM.Mob2Health).Read<ushort>(0, 16) is ushort.MinValue or ushort.MaxValue &&
                Wram.ReadBytes(Addresses.WRAM.Mob3Health).Read<ushort>(0, 16) is ushort.MinValue or ushort.MaxValue;
    }

    protected override void ReadTrackingData()
    {
        if (Started)
        {
            var wramData = Wram.ReadBytes(Addresses.WRAM.WramRegion).AsReadOnlySpan();
            var checkedBattlefields = wramData.Slice(Addresses.WRAM.Battlefields);
            var checkedLocations = wramData[Addresses.WRAM.Chests];

            if (RequiredSkyFragmentCount.HasValue)
                CollectedSkyFragments = wramData.Slice(Addresses.WRAM.FoundShards).Read<byte>(0);

            bool? skycoinComplete = RequiredSkyFragmentCount.HasValue ? CollectedSkyFragments >= RequiredSkyFragmentCount : null;

            var keyItemsFound = wramData[Addresses.WRAM.FoundKeyItemBits];
            var weapons = wramData[Addresses.WRAM.FoundWeaponBits];
            var armors = wramData[Addresses.WRAM.FoundArmorBits];
            var spells = wramData[Addresses.WRAM.FoundSpellBits];
            var stateFlags = wramData[Addresses.WRAM.StateFlags];

            if (lastLocations == null || checkedLocations.SequenceCompareTo(lastLocations) != 0)
                lastLocations = checkedLocations.ToArray();

            if (Battlefields.Update(checkedBattlefields))
                NotifyPropertyChanged(nameof(Battlefields));

            var flagsUpdated = StateFlags.Update(stateFlags);

            if (flagsUpdated)
                NotifyPropertyChanged(nameof(StateFlags));

            if (_gameinfo.UpdateQuests(stateFlags))
                NotifyPropertyChanged(nameof(Companions));

            if (_weapons.Update(weapons))
                NotifyPropertyChanged(nameof(Weapons));

            if (_armors.Update(armors))
                NotifyPropertyChanged(nameof(Armors));

            if (_spells.Update(spells))
                NotifyPropertyChanged(nameof(Spells));

            if (_keyitems.Update(keyItemsFound, StateFlags, skycoinComplete))
                NotifyPropertyChanged(nameof(KeyItems));
        }
    }
}
