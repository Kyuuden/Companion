using BizHawk.Client.Common;
using BizHawk.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Games.JetsOfTime.Data;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using System;

namespace FF.Rando.Companion.Games.JetsOfTime;
internal class Parser : IGameParser
{
    private static readonly byte[] HashScript = "0A11808182838485868788898A8B018C8D8E8F90919293949596970198999A9B9C9D9E9FA0A1A2A3010A01029A01A4A5A6A7A8A901AAABACADAEAF0A0002DA02A0A2B3A8B5A40101FFB6A0A8B3024200".HexStringToBytes();

    public bool TryParseGameInfo(ApiContainer apiContainer, IMemoryDomains memoryDomains, ISettings rootSettings, IGameInfo gameInfo, ITimer timer, out IGame? game)
    {
        game = null;
        try
        {
            var container = new Container(apiContainer, memoryDomains, rootSettings, timer);
            var hashScriptArea = container.Rom.ReadBytes(Addresses.ROM.SeedHashScript);

            if (hashScriptArea.AsSpan().IndexOf(HashScript) == -1)
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
