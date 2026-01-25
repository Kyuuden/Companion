using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Settings;
using System;
using FF.Rando.Companion.WorldsCollide.RomData;
using BizHawk.Common.BufferExtensions;

namespace FF.Rando.Companion.WorldsCollide;
public class SeedParser : IGameParser
{
    public bool TryParseGameInfo(ApiContainer apiContainer, IMemoryDomains memoryDomains, ISettings rootSettings, IGameInfo gameInfo, out IGame? game)
    {
        game = null;
        try
        {
            var wcContainer = new Container(apiContainer, memoryDomains, rootSettings);
            var indentifier = wcContainer.Rom.ReadBytes(Addresses.ROM.Indentifier).BytesToHexString();

            //                       F F V I   W o r l d s   C o l l i d e
            if (!indentifier.Equals("85859588FF96A8ABA59DACFF82A8A5A5A29D9E00", StringComparison.OrdinalIgnoreCase))
                return false;

            game = new Seed(gameInfo.Hash, wcContainer);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
