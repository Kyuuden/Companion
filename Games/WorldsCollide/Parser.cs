using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Settings;
using System;
using BizHawk.Common.BufferExtensions;
using FF.Rando.Companion.Games.WorldsCollide.RomData;
using FF.Rando.Companion.Timing;

namespace FF.Rando.Companion.Games.WorldsCollide;
public class Parser : IGameParser
{
    public bool TryParseGameInfo(ApiContainer apiContainer, IMemoryDomains memoryDomains, ISettings rootSettings, IGameInfo gameInfo, ITimer timer, out IGame? game)
    {
        game = null;
        try
        {
            var container = new EmulationContainer<Settings.WorldsCollideSettings>(apiContainer, memoryDomains, timer, rootSettings, "WorldsCollide");
            var indentifierArea = container.Rom.ReadBytes(Addresses.ROM.IndentifierArea).BytesToHexString();

            //                       F F V I   W o r l d s   C o l l i d e
            if (!indentifierArea.Contains("85859588FF96A8ABA59DACFF82A8A5A5A29D9E00"))
                return false;

            game = new Seed(gameInfo.Hash, container);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
