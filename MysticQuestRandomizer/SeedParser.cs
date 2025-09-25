using BizHawk.Client.Common;
using BizHawk.Common.BufferExtensions;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Settings;
using System;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class SeedParser : IGameParser
{
    public bool TryParseGameInfo(ApiContainer apiContainer, IMemoryDomains memoryDomains, ISettings rootSettings, IGameInfo gameInfo, out IGame? game)
    {
        game = null;
        try
        {
            var mqContainer = new Container(apiContainer, memoryDomains, rootSettings);
            var indentifier = mqContainer.Rom.ReadBytes(Addresses.ROM.Indentifier).BytesToHexString();

            if (!indentifier.Equals("9f9fa6aaabb4c1b7c2c0bccdb8c5", StringComparison.OrdinalIgnoreCase))
                return false;

            game = new Seed(gameInfo.Hash, mqContainer);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
