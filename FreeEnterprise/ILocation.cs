namespace FF.Rando.Companion.FreeEnterprise;

public interface ILocation
{
    string Description { get; }
    bool? IsCharacter { get; }
    bool? IsKeyItem { get; }
    bool IsBoss { get; }
    bool IsShop { get; }
}
