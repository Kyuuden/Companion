using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;

namespace FF.Rando.Companion.Games;

public interface IGameParser
{
    bool TryParseGameInfo(ApiContainer apiContainer, IMemoryDomains memoryDomains, ISettings rootSettings, IGameInfo gameInfo, ITimer timer, out IGame? game);
}