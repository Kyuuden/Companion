using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime;
using System.Security.Policy;
using System.Text;
using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;

namespace FF.Rando.Companion.FreeEnterprise;

public class SeedParser : IGameParser
{
    public bool TryParseGameInfo(ApiContainer apiContainer, IMemoryDomains memoryDomains, ISettings rootSettings, IGameInfo gameInfo, out IGame? game)
    {
        game = null;
        try
        {
            var feContainer = new Container(apiContainer, memoryDomains, rootSettings);

            var jsonDataLength = feContainer.Rom.Read<uint>(Shared.Addresses.ROM.MetadataLength);
            if (jsonDataLength <= 0)
                throw new InvalidOperationException("No metadata found");

            var jsonData = feContainer.Rom.ReadBytes(Shared.Addresses.ROM.MetadataAddress.WithLength(jsonDataLength));
            var json = Encoding.UTF8.GetString(jsonData);
            var jObj = JObject.Parse(json);
            var metadata = new Metadata(jObj);
            var version = jObj["version"]?.ToObject<string>();

            switch (version)
            {
                case "v5.0.0-a.1":
                    game = new _5._0._0.Seed(gameInfo.Hash, metadata, feContainer);
                    return true;
                case "v4.6.0":
                    game = new _4._6._0.Seed(gameInfo.Hash, metadata, feContainer);
                    return true;
                case "v4.6.1.Gale":
                    game = new _4._6._1.Gale.Seed(gameInfo.Hash, metadata, feContainer);
                    return true;
                default:
                    throw new NotSupportedException($"Unsupported Free Enterprise version: {version}");
            }

        }
        catch
        {
            return false;
        }
    }
}
