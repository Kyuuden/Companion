using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace FF.Rando.Companion.Games.FreeEnterprise;

public class SeedParser : IGameParser
{
    public bool TryParseGameInfo(ApiContainer apiContainer, IMemoryDomains memoryDomains, ISettings rootSettings, IGameInfo gameInfo, ITimer timer, out IGame? game)
    {
        game = null;
        try
        {
            var feContainer = new Container(apiContainer, memoryDomains, rootSettings, timer);

            var jsonDataLength = feContainer.Rom.Read<uint>(Shared.Addresses.ROM.MetadataLength);
            if (jsonDataLength <= 0)
                throw new InvalidOperationException("No metadata found");

            var jsonData = feContainer.Rom.ReadBytes(Shared.Addresses.ROM.MetadataAddress.WithLength(jsonDataLength));
            var json = Encoding.UTF8.GetString(jsonData);
            var jObj = JObject.Parse(json);
            var version = jObj["version"]?.Value<string>();

            switch (version)
            {
                case string s when s.StartsWith("v5"):
                    var addressString = jObj["metadata_addr"]?.Value<string>();
                    var lengthString = jObj["metadata_len"]?.Value<string>();

                    if (addressString != null && addressString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        addressString = addressString[2..];

                    if (lengthString != null && lengthString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        lengthString = lengthString[2..];

                    if (addressString != null && long.TryParse(addressString, System.Globalization.NumberStyles.HexNumber, null, out var address) &&
                        lengthString != null && int.TryParse(lengthString, System.Globalization.NumberStyles.HexNumber, null, out var length))
                    {
                        var compressedMetadata = feContainer.Rom.ReadBytes(address.WithLength(length));
                        game = new _5._0._0.Seed(gameInfo.Hash, new CompressedMetadata(version, compressedMetadata), feContainer);
                        return true;
                    }

                    game = new _5._0._0.Seed(gameInfo.Hash, new Metadata(jObj), feContainer);
                    return true;
                case string s when s.EndsWith(".Gale"):
                    game = new GaleswiftFork.Seed(gameInfo.Hash, new Metadata(jObj), feContainer);
                    return true;
                case string s when s.StartsWith("v4.6.0"):
                    game = new _4._6._0.Seed(gameInfo.Hash, new Metadata(jObj), feContainer);
                    return true;
                case string s:
                    throw new NotSupportedException($"Unsupported Free Enterprise version: {s}");
                default:
                    return false;
            }
        }
        catch
        {
            return false;
        }
    }
}
