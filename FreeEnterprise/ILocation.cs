using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise;

public interface ILocation : INotifyPropertyChanged
{
    string Description { get; }
    bool IsAvailable { get; }
    bool IsChecked { get; }
    bool IsCharacter { get; }
    bool IsKeyItem { get; }
    bool IsBoss { get; }
    bool IsShop { get; }
}
