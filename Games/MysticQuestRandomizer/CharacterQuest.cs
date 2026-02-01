using FF.Rando.Companion.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer;

public class CharacterQuest : INotifyPropertyChanged
{
    private bool _completed = false;
    private TimeSpan? _completedAt;

    public CharacterQuest(byte flag, string description)
    {
        var index = flag / 8;
        var offset = flag % 8;
        Flag = (byte)(index * 8 + 7 - offset);

        Description = Regex.Replace(description, "^[0-9]\\.", "").Replace("  ", " ");
    }

    public byte Flag { get; }
    public string Description { get; }

    public TimeSpan? CompletedAt
    {
        get => _completedAt;
        private set
        {
            if (value == _completedAt || _completedAt.HasValue)
                return;

            _completedAt = value;
            NotifyPropertyChanged();
        }
    }

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

    public bool Update(TimeSpan time, ReadOnlySpan<byte> data)
    {
        var isCompleted = data.Read<bool>(Flag);
        if (isCompleted != IsCompleted)
        {
            IsCompleted = isCompleted;
            if (IsCompleted)
                CompletedAt = time;

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