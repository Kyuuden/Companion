using FF.Rando.Companion.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;

public class CharacterQuest : INotifyPropertyChanged
{
    private readonly Seed _seed;
    private bool _completed = false;
    private bool _hasBeenCompleted = false;

    public CharacterQuest(Seed seed, byte flag, string description)
    {
        _seed = seed;
        var index = flag / 8;
        var offset = flag % 8;
        Flag = (byte)(index * 8 + 7 - offset);

        Description = Regex.Replace(description, "^[0-9]\\.", "").Replace("  ", " ");
    }

    public byte Flag { get; }
    public string Description { get; }

    public bool IsCompleted
    {
        get => _completed;
        private set
        {
            if (value == _completed)
                return;

            _completed = value;
            NotifyPropertyChanged();
        }
    }

    public bool Update(ReadOnlySpan<byte> data)
    {
        var isCompleted = data.Read<bool>(Flag);
        if (isCompleted != IsCompleted)
        {
            IsCompleted = isCompleted;
            if (IsCompleted && !_hasBeenCompleted)
            {
                _hasBeenCompleted = true;
                _seed.Timer.Info(Description);
            }

            return true;
        }

        return false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}