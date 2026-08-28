using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;
internal class Events : INotifyPropertyChanged
{
    private byte[]? _state;
    private readonly Dictionary<uint, List<EventType>> _knownFlags = [];
    private readonly List<uint> _unknownFlags = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly Dictionary<EventType, PropertyInfo> _eventProperties = [];


    public Events()
    {
        var props = GetType()
            .GetProperties()
            .Where(p => p.CanRead && p.PropertyType == typeof(bool) && p.GetMethod.GetParameters().Length == 0)
            .ToDictionary(p => p.Name);

        foreach (var p in props)
        {
            if (Enum.TryParse<EventType>(p.Key, out var eventType))
                _eventProperties[eventType] = p.Value;
        }

        _state = new byte[0x200];
        for (uint i = 0; i < _state.Length * 8; i++)
        {
            if (i > 0)
                _state.Write(false, i - 1);

            _state.Write(true, i);

            var setProperties = _eventProperties.Where(p => (bool)p.Value.GetValue(this)).ToList();

            if (setProperties.Any())
            {
                _knownFlags[i] = setProperties.Select(p => p.Key).ToList();
            }
            else
            {
                _unknownFlags.Add(i);
            }
        }
        _state = new byte[0x200];
    }

    public bool this[EventType property] => _eventProperties.TryGetValue(property, out var result) && (bool)result.GetValue(this);

    public bool Update(ReadOnlySpan<byte> newState)
    {
        if (BinaryPrimitives.ReadUInt16LittleEndian(newState) == 0x4140 &&
            BinaryPrimitives.ReadUInt16LittleEndian(newState[2..]) == 0x4342)
            return false;

        if (_state == null || !newState.SequenceEqual(_state))
        {
            HashSet<EventType> changedProperties = [];

            var anyKnownChanges = _state == null;
            if (_state != null)
            {
                foreach (var i in _knownFlags)
                {
                    if (newState.Read<bool>((int)i.Key) != _state.Read<bool>(i.Key))
                    {
                        changedProperties.AddRange(i.Value);
                        anyKnownChanges = true;
                        break;
                    }
                }

                List<uint> changedUnknown = [];

                for (uint i = 0; i < _state.Length * 8; i++)
                {
                    if (!_knownFlags.ContainsKey(i) && !_state.Read<bool>(i) && newState.Read<bool>((int)i))
                        changedUnknown.Add(i);
                }

                if (changedUnknown.Count > 0)
                    Debug.WriteLine($"Unknown set: {string.Join(";", changedUnknown)}");
            }

            _state = newState.ToArray();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Join(";", changedProperties)));
            return anyKnownChanges;
        }

        return false;
    }

    public byte[] EventData => _state?.ToArray() ?? [];

    #region Boss Checks

    public bool ZomborSpotBossDefeated => _state?.Read<bool>(0x101 * 8 + 1) ?? false;
    public bool NizbelSpotBossDefeated => _state?.Read<bool>(0x105 * 8 + 5) ?? false;
    public bool BlackTyranoSpotBossDefeated => _state?.Read<bool>(0xEC * 8 + 7) ?? false;
    public bool GigaGaiaSpotBossDefeated => _state?.Read<bool>(0x100 * 8 + 5) ?? false;
    public bool GolemSpotBossDefeated => _state?.Read<bool>(0x105 * 8 + 7) ?? false;
    public bool YakaraSpotBossDefeated => _state?.Read<bool>(0xD * 8 + 0) ?? false;
    public bool MasamumeSpotBossDefeated => _state?.Read<bool>(0xF3 * 8 + 5) ?? false;
    public bool RetiniteSpotBossDefeated => _state?.Read<bool>(0x1A3 * 8 + 0) ?? false;
    public bool RustTryanoSpotBossDefeated => _state?.Read<bool>(0x1D2 * 8 + 6) ?? false;
    public bool MagusSpotBossDefeated => _state?.Read<bool>(0x1FF * 8 + 2) ?? false;
    public bool HeckranSpotBossDefeated => _state?.Read<bool>(0x1A3 * 8 + 3) ?? false;
    public bool DragonTankSpotBossDefeated => _state?.Read<bool>(0x198 * 8 + 3) ?? false;
    public bool YakaraIIISpotBossDefeated => _state?.Read<bool>(0x50 * 8 + 6) ?? false;
    public bool GuardianSpotBossDefeated => _state?.Read<bool>(0xEC * 8 + 0) ?? false;
    public bool RSeriesSpotBossDefeated => _state?.Read<bool>(0x103 * 8 + 6) ?? false;
    public bool SonOfSunSpotBossDefeated => _state?.Read<bool>(0x13A * 8 + 1) ?? false;
    public bool MotherBrainSpotBossDefeated => _state?.Read<bool>(0x13B * 8 + 4) ?? false;
    public bool ZealSpotBossDefeated => false;
    public bool OzzieSpotBossDefeated => _state?.Read<bool>(0x1A1 * 8 + 7) ?? false;
    public bool CyrusGraveSpotBossDefeated => _state?.Read<bool>(0x1A3 * 8 + 6) ?? false;
    #endregion

    #region Character Checks
    public bool FriendToTheDactyls => _state?.Read<bool>(0x160 * 8 + 4) ?? false;
    public bool SavedByFrog => _state?.Read<bool>(0x100 * 8) ?? false;
    public bool RescueMarle => _state?.Read<bool>(0xA1 * 8 + 2) ?? false;
    public bool FixRobo => _state?.Read<bool>(0xF3 * 8 + 1) ?? false;
    public bool ReturnTheMasamune => _state?.Read<bool>(0xFF * 8 + 5) ?? false;
    #endregion


    #region Final Events
    public bool CooksRations => (_state?.Read<bool>(0xA9 * 8 + 4) ?? false) || ZomborSpotBossDefeated;
    public bool MelchiorsRefinements => YakaraIIISpotBossDefeated && !(_state?.Read<bool>(0x6D * 8 + 4) ?? false);
    public bool WokeRoboUp => _state?.Read<bool>(0x7C * 8 + 7) ?? false;
    public bool BurrowHeroMedalChest => _state?.Read<bool>(0x106 * 8 + 2) ?? false;
    public bool RainbowShell => _state?.Read<bool>(0xA9 * 8 + 7) ?? false;
    public bool SnailStopPurchase => _state?.Read<bool>(0x1D0 * 8 + 4) ?? false;
    public bool BorrowCarpentersTools => _state?.Read<bool>(0x19E * 8 + 7) ?? false;
    public bool TabansGift => _state?.Read<bool>(0x7A * 8) ?? false;
    public bool ReforgeTheMasamune => _state?.Read<bool>(0x103 * 8 + 1) ?? false;
    public bool KingsGuardiasTrial => _state?.Read<bool>(0xA2 * 8 +7) ?? false;
    public bool CloneGame => _state?.Read<bool>(0x7C * 8) ?? false;
    public bool CollectedArrisDomeReward => _state?.Read<bool>(0xA4 * 8) ?? false;
    public bool LearnMagic => _state?.Read<bool>(0xE1 * 8 + 1) ?? false;
    public bool AttachEpochWings => _state?.Read<bool>(0xBA * 8 + 7) ?? false;
    public bool SeedValidated => _state?.Read<bool>(0x1A6 * 8 + 3) ?? false;

    #endregion

    #region Intermediate Events
    public bool ReplantedTheForest => _state?.Read<bool>(0x1F0 * 8 + 1) ?? false;// ((_state?[0x19E] ?? 0x00) & 0x81) == 0x81;
    public bool TalkedToToma => _state?.Read<bool>(0x1F6 * 8 + 7) ?? false;
    public bool TalkToCarpenter => _state?.Read<bool>(0x19E * 8 + 5) ?? false;
    public bool MoonstoneDroppedOff => _state?.Read<bool>(0x13A * 8 + 2) ?? false;
    public bool UnlockedDeathPeak => _state?.Read<bool>(0x70 * 8 + 2) ?? false;
    public bool BlackOmenRaised => _state?.Read<byte>(0x67 * 8, 3) != 0;
    public bool TalkedToKnightCaptain => _state?.Read<bool>(0xA9 * 8 + 2) ?? false;
    public bool TalkedToCook => _state?.Read<bool>(0xA9 * 8 + 3) ?? false;
    public bool UnlockedMagusCastle => _state?.Read<bool>(0x57 * 8 + 1) ?? false;
    public bool ActivateComputer => _state?.Read<bool>(0x105 * 8 + 2) ?? false;
    public bool SecureRainbowShell => _state?.Read<bool>(1294) ?? false;
    public bool GiveJerkyToPorreMayorAncestor => _state?.Read<bool>(3730) ?? false;
    public bool PorreMayorItem => _state?.Read<bool>(2515) ?? false;

    public bool KinoCellButton => _state?.Read<bool>(0x5C * 8 + 4) ?? false;

    public bool ZealTeleportersEnabled => (!_state?.Read<bool>(0x57 * 8 + 7)) ?? false;
    #endregion


}