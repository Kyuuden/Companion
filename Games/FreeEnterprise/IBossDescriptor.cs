using FF.Rando.Companion.Games.FreeEnterprise.Shared;

namespace FF.Rando.Companion.Games.FreeEnterprise;

public interface IBossDescriptor
{
    string GetName(BossType bossType);
    string GetLocationName(BossLocationType bossLocationType);
}
