using FF.Rando.Companion.FreeEnterprise.Shared;

namespace FF.Rando.Companion.FreeEnterprise;

public interface IKeyItemDescriptor
{
    string GetDescription(KeyItemType item);
    string GetName(KeyItemType item);
}
