using FF.Rando.Companion.Games.FreeEnterprise.Shared;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.FreeEnterprise;

public interface ILocation : INotifyPropertyChanged
{
    int ID { get; }
    string Description { get; }
    bool IsAvailable { get; }
    bool IsChecked { get; }
    bool IsCharacter { get; }
    bool IsKeyItem { get; }
    bool IsBoss { get; }
    bool IsShop { get; }
    World World { get; }
}
