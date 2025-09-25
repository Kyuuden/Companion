using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

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

            if (metadata.Version?.StartsWith("v5") == true)
            {
                game = new _5._0._0.Seed(gameInfo.Hash, metadata, feContainer);
                return true;
            }
            else if (metadata.Version?.EndsWith(".Gale") == true)
            {
                game = new GaleswiftFork.Seed(gameInfo.Hash, metadata, feContainer);
                return true;
            }
            else if (metadata.Version?.StartsWith("v4.6.0") == true)
            {
                game = new _4._6._0.Seed(gameInfo.Hash, metadata, feContainer);
                return true;
            }
            else
            {
                throw new NotSupportedException($"Unsupported Free Enterprise version: {metadata.Version}");
            }
        }
        catch
        {
            return false;
        }
    }
}
