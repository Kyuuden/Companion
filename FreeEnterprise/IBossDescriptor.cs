using FF.Rando.Companion.FreeEnterprise.Shared;

namespace FF.Rando.Companion.FreeEnterprise;

public interface IBossDescriptor
{
    string GetName(BossType bossType);
    string GetLocationName(BossLocationType bossLocationType);
}
