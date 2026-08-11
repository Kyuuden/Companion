using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.JetsOfTime.Data;
using FF.Rando.Companion.Games.JetsOfTime.Tracking;
using FF.Rando.Companion.MemoryManagement;
using System;
using System.Buffers.Binary;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.JetsOfTime;

internal class State : INotifyPropertyChanged
{
    private bool _canOpenSealedChests = false;
    private bool _canFly = false;
    private bool _validated = false;
    private uint _gold;
    private int _openedChests;
    private int _openedSealedChests;
    private int _completedChecks;
    private TimePeriodType _currentTimePeriod;

    public required Characters Characters { get; init; }
    public required KeyItems KeyItems { get; init; }
    public required Bosses Bosses { get; init; }
    public required Events Events { get; init; }
    public required TimePeriods TimePeriods { get; init; }
    public required Flags Flags { get; init; }

    public uint Gold
    {
        get => _gold;
        protected set
        {
            if (_gold == value) return;
            _gold = value;
            NotifyPropertyChanged();
        }
    }

    public bool CanOpenSealedChests
    {
        get => _canOpenSealedChests;
        protected set
        {
            if (_canOpenSealedChests == value) return;
            _canOpenSealedChests = value;
            NotifyPropertyChanged();
        }
    }

    public bool CanFly
    {
        get => _canFly;
        protected set
        {
            if (_canFly == value) return;
            _canFly = value;
            NotifyPropertyChanged();
        }
    }

    public bool Validated
    {
        get => _validated;
        protected set
        {
            if (_validated == value) return;
            _validated = value;
            NotifyPropertyChanged();
        }
    }

    public int OpenedChests
    {
        get => _openedChests;
        protected set
        {
            if (_openedChests == value) return;
            _openedChests = value;
            NotifyPropertyChanged();
        }
    }

    public int OpenedSealedChests
    {
        get => _openedSealedChests;
        protected set
        {
            if (_openedSealedChests == value) return;
            _openedSealedChests = value;
            NotifyPropertyChanged();
        }
    }

    public int CompletedChecks
    {
        get => _completedChecks;
        protected set
        {
            if (_completedChecks == value) return;
            _completedChecks = value;
            NotifyPropertyChanged();
        }
    }

    public TimePeriodType CurrentTimePeriod
    {
        get => _currentTimePeriod;
        protected set
        {
            if (_currentTimePeriod == value) return;
            _currentTimePeriod = value;
            NotifyPropertyChanged();
        }
    }

    public bool Update(IMemorySpace wram)
    {
        var eventData = wram.ReadBytes(Addresses.WRAM.EventData).AsSpan();
        var partyData = wram.ReadBytes(Addresses.WRAM.PartyData).AsSpan();
        var inventoryData = wram.ReadBytes(Addresses.WRAM.InventoryData);
        var equipmentData = wram.ReadBytes(Addresses.WRAM.EquipmentData);
        var gold = wram.ReadBytes(Addresses.WRAM.Gold).Read<uint>(0, 24);
        var loc = (LocationType)BinaryPrimitives.ReadUInt16LittleEndian(wram.ReadBytes(Addresses.WRAM.CurrentLocation));

        if (BinaryPrimitives.ReadUInt16LittleEndian(partyData) == 0)
            return false;

        if (BinaryPrimitives.ReadUInt16LittleEndian(eventData) == 0x4140 &&
            BinaryPrimitives.ReadUInt16LittleEndian(eventData[2..]) == 0x4342)
            return false;

        var ret = false;

        ret |= Events.Update(eventData);
        Validated = Events.SeedValidated;

        ret |= Characters.Update(partyData);
        ret |= KeyItems.Update(inventoryData, eventData, equipmentData);
        ret |= Bosses.Update(eventData);
        ret |= TimePeriods.Update(this);

        ret |= gold != Gold;
        Gold = gold;

        var canfly = Flags.EpochFail == false || Events.AttachEpochWings;
        ret |= canfly != CanFly;
        CanFly = canfly;

        var isCyrus = Flags.Mode == GameMode.LegacyOfCyrus;
        var isLost = Flags.Mode == GameMode.LostWorlds;
        var pendant = KeyItems.IsFound(KeyItemType.Pendant);
        var earlyPendant = Flags.FastPendant == true;

        var canOpenSealed = ((Events.DragonTankSpotBossDefeated || (isCyrus && pendant)) && earlyPendant) || (pendant && (Events.MagusSpotBossDefeated || Events.BlackTyranoSpotBossDefeated || isLost));
        ret |= canOpenSealed != CanOpenSealedChests;
        CanOpenSealedChests = canOpenSealed;

        OpenedChests = TimePeriods.Periods.SelectMany(p => p.Locations.SelectMany(l => l.Checks)).OfType<ChestsCheck>().Where(c=> !c.AccessRules.Any(a=>a.CanOpenSealed)).Sum(c=>c.OpenedChests);
        OpenedSealedChests = TimePeriods.Periods.SelectMany(p => p.Locations.SelectMany(l => l.Checks)).OfType<SealedChestsCheck>().Sum(c => c.OpenedChests);
        CompletedChecks = TimePeriods.Periods.SelectMany(p => p.Locations.SelectMany(l => l.Checks)).Count(c => c.IsKeyItem && c.Exists && c.IsComplete);

        switch (loc)
        {
            case LocationType.Present: CurrentTimePeriod = TimePeriodType.Present; break;
            case LocationType.MiddleAges: CurrentTimePeriod = TimePeriodType.MiddleAges; break;
            case LocationType.Future: CurrentTimePeriod = TimePeriodType.Future; break;
            case LocationType.Prehistoric: CurrentTimePeriod = TimePeriodType.Prehistory; break;
            case LocationType.DarkAges: CurrentTimePeriod = TimePeriodType.DarkAges; break;
            case LocationType.KingdomofZeal: CurrentTimePeriod = TimePeriodType.KingdomOfZeal; break;
            case LocationType.EndofTime: CurrentTimePeriod = TimePeriodType.EndOfTime; break;
        }

        return ret;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
