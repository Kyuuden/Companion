using FF.Rando.Companion.Games.FreeEnterprise.Shared;

namespace FF.Rando.Companion.Games.FreeEnterprise;

public interface IKeyItemDescriptor
{
    string GetDescription(KeyItemType item);
    string GetName(KeyItemType item);
}
