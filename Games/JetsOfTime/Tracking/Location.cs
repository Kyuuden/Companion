using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;
public class Location
{

}

public class Chest(int id, bool isSealed, bool exists) : INotifyPropertyChanged
{
    private bool _isOpened = false;

    public int Id { get; } = id;

    public bool IsSealed { get; } = isSealed;

    public bool Exists { get; } = exists;

    public bool IsOpened
    {
        get => _isOpened;
        set
        {
            if (_isOpened == value)
                return;

            _isOpened = value;
            NotifyPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}