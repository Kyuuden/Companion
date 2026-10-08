using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.RomData;
using FF.Rando.Companion.Games.WorldsCollide.Tracking;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Timing;
using KGySoft.CoreLibraries;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.WorldsCollide;

internal class State : INotifyPropertyChanged
{
    private readonly List<EventType> _allEvents;
    private readonly List<EventType> _characterEvents;
    private readonly List<EventType> _checkEvents;
    private readonly List<EventType> _dragonLocationEvents;
    private readonly Dictionary<EventType, CharacterCheck> _characters;

    private int _characterCount;
    private int _esperCount;
    private int _dragonCount;
    private int _bossCount;
    private int _checkCount;
    private int _chestCount;
    private readonly HashSet<Esper> _foundEspers = [];
    private ushort _currentLocation;
    private Point _worldPosition;
    //private Reward? _currentReward;

    public State()
    {
        _allEvents = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
        _characterEvents = _allEvents.Where(e => e.IsCharacter()).ToList();
        _checkEvents = _allEvents.Where(e => e.IsCheck()).ToList();
        _dragonLocationEvents = _allEvents.Where(e => e.IsDragonLocation()).ToList();
        _characters = _characterEvents.ToDictionary(c => c, c => new CharacterCheck(c));
    }

    public required Events Events { get; init; }
    public required Worlds Worlds { get; init; }

    public IEnumerable<ICheck> Characters => _characters.Values.OfType<ICheck>();

    public ICheck GetCharacter(EventType characterEvent) => _characters[characterEvent];

    public int GetStatistic(Statistic stat) =>
        stat switch
        {
            Statistic.CharacterCount => CharacterCount,
            Statistic.EsperCount => EsperCount,
            Statistic.DragonCount => DragonCount,
            Statistic.BossCount => BossCount,
            Statistic.CheckCount => CheckCount,
            Statistic.ChestCount => ChestCount,
            _ => throw new InvalidOperationException()
        };

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

    public ushort CurrentLocation
    {
        get => _currentLocation;
        set
        {
            if (_currentLocation == value) return;
            _currentLocation = value;
            NotifyPropertyChanged();
        }
    }

    public Point WorldPosition
    {
        get => _worldPosition;
        set
        {
            if (_worldPosition == value) return;
            _worldPosition = value;
            NotifyPropertyChanged();
        }
    }

    private float _effectiveZoomRate;
    public float EffectiveZoomRate
    {
        get => _effectiveZoomRate;
        set
        {
            if (_effectiveZoomRate == value) return;
            _effectiveZoomRate = value;
            NotifyPropertyChanged();
        }
    }

    public TimeSpan LastUpdateTime { get; private set; }

    public void Update(IMemorySpace wram, ITimer timer)
    {
        LastUpdateTime = timer.Elapsed ?? TimeSpan.Zero;
        var eventState = wram.ReadBytes(Addresses.WRAM.State).AsReadOnlySpan();
        var dragonState = wram.ReadBytes(Addresses.WRAM.Dragons).AsReadOnlySpan();
        var chests = wram.ReadBytes(Addresses.WRAM.Chests);
        var espers = wram.ReadBytes(Addresses.WRAM.KnownEspers);
        var newEspers = new HashSet<Esper>();

        for (int i = 0; i < espers.Length * 8; i++)
        {
            if (((espers[i / 8] >> (i % 8)) & 0x01) == 1)
                newEspers.Add(Esper.Ramuh + i);
        }

        var latestEspers = newEspers.Except(_foundEspers).ToList();

        _foundEspers.Clear();
        _foundEspers.UnionWith(newEspers);

        var previouslyFoundCharacters = _characterEvents.Where(e => Events[e]).ToList();
        var previouslyCompletedChecks = _checkEvents.Where(e=>Events[e]).ToList();
        var previouslyDefeatedDragons = _dragonLocationEvents.Where(e => Events[e]).ToList();

        //if (CurrentLocation == 0x0 || CurrentLocation == 0x1)
        //{
        //    var location = wram.ReadBytes(0xC6L.WithLength(4));

        //    if (BinaryPrimitives.ReadUInt32LittleEndian(location) != 0)
        //    {
        //        var x = BinaryPrimitives.ReadUInt16LittleEndian(location);
        //        var y = BinaryPrimitives.ReadUInt16LittleEndian(location.AsSpan().Slice(2));

        //        WorldPosition = new Point(x / 4 + 8, y / 4 + 8);
        //    }
        //}

        if (Events.Update(eventState))
        {
            Worlds.Update(this);
            Characters.ForEach(c => c.Update(this));
        }

        //var previousFoundCharacters = Characters.Where(c => c.IsFound).Select(c => c.Event).ToHashSet();

        //if (_characters.Update(eventState))
        //    NotifyPropertyChanged(nameof(Characters));

        //var newcharacters = Characters.Where(c => c.IsFound).Select(c => c.Event).ToHashSet();
        //newcharacters.ExceptWith(previousFoundCharacters);

        //if ((latestEspers.Count + newcharacters.Count) == 1)
        //{
        //    _currentReward = latestEspers.Any() ? latestEspers.First().ToReward() : newcharacters.First().ToReward();
        //}

        //if (_checks.Update(eventState, ref _currentReward))
        //    NotifyPropertyChanged(nameof(Checks));

        //if (_dragonLocations.Update(eventState))
        //    NotifyPropertyChanged(nameof(DragonLocations));

        //if (_dragons.Update(dragonState, ref _currentReward))
        //    NotifyPropertyChanged(nameof(Dragons));

        var characterCountData = wram.ReadBytes(Addresses.WRAM.CHARACTER_COUNT);
        characterCountData[1] &= 0x3F;

        CharacterCount = characterCountData.CountBits();
        EsperCount = wram.ReadByte(Addresses.WRAM.ESPER_COUNT);
        BossCount = wram.ReadByte(Addresses.WRAM.BOSS_COUNT);
        DragonCount = wram.ReadByte(Addresses.WRAM.DRAGON_COUNT);
        CheckCount = wram.ReadByte(Addresses.WRAM.CHECK_COUNT);
        ChestCount = chests.CountBits();
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
