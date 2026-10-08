using BizHawk.Common.CollectionExtensions;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;
internal class Events : INotifyPropertyChanged
{
    private byte[]? _state;
    private readonly List<uint> _knownFlags = [];
    private readonly List<uint> _unknownFlags = [];
    public event PropertyChangedEventHandler? PropertyChanged;
    private static readonly int ArraySize = (int)RomData.Addresses.WRAM.State.Length();

    public Events()
    {
        foreach (EventType flag in Enum.GetValues(typeof(EventType)))
        {
            _knownFlags.Add((uint)flag);
        }

        _unknownFlags.AddRange(Enumerable.Range(0, ArraySize * 8).Select(i => (uint)i).Except(_knownFlags));
        _state = new byte[ArraySize];
    }

    public bool this[EventType property] => _state?.Read<bool>((uint)property) ?? false;

    public bool Update(ReadOnlySpan<byte> newState)
    {
        if (_state == null || !newState.SequenceEqual(_state))
        {
            HashSet<EventType> changed = [];

            var anyKnownChanges = _state == null;
            if (_state != null)
            {
                foreach (var i in _knownFlags)
                {
                    if (newState.Read<bool>((int)i) != _state.Read<bool>(i))
                    {
                        changed.Add((EventType)i);
                        anyKnownChanges = true;
                        break;
                    }
                }

                List<uint> changedUnknown = [];

                for (uint i = 0; i < _state.Length * 8; i++)
                {
                    if (!_knownFlags.Contains(i) && !_state.Read<bool>(i) && newState.Read<bool>((int)i))
                        changedUnknown.Add(i);
                }
#if DEBUG
                if (changedUnknown.Count > 0)
                    Debug.WriteLine($"Unknown set: {string.Join(";", changedUnknown)}");
#endif
            }

            _state = newState.ToArray();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Join(";", changed.Select(p=>p.GetDescription()))));
            return anyKnownChanges;
        }

        return false;
    }

    public byte[] EventData => _state?.ToArray() ?? [];

}