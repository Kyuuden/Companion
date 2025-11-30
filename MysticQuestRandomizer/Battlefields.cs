using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class Battlefields : INotifyPropertyChanged
{
    private bool _isInitialized = false;
    private int _remaining = 0;
    private int _encounters = 0;
    private int _total = 0;

    public event PropertyChangedEventHandler? PropertyChanged;

    public int Remaining
    {
        get => _remaining;
        set
        {
            if (_remaining == value)
                return;

            _remaining = value;
            NotifyPropertyChanged();
        }
    }

    public int Encounters
    {
        get => _encounters;
        set
        {
            if (_encounters == value)
                return;

            _encounters = value;
            NotifyPropertyChanged();
        }
    }

    public int Total
    {
        get => _total;
        set
        {
            if (_total == value)
                return;

            _total = value;
            NotifyPropertyChanged();
        }
    }

    public bool Update(ReadOnlySpan<byte> data)
    {
        bool updated = false;

        if (!_isInitialized)
        {
            var total = 0;
            foreach (var b in data) if (b > 0) total++;
            Total = total;
            _isInitialized = true;
            updated = true;
        }

        var encounters = 0;
        var remaining = Total - 20;
        foreach (var b in data)
        {
            encounters += b;
            if (b > 0) remaining++;
        }

        if (Encounters != encounters || Remaining != remaining)
            updated = true;

        Encounters = encounters;
        Remaining = remaining;

        return updated;
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
