using System;
using System.Text;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using Newtonsoft.Json.Linq;

namespace FF.Rando.Companion.FreeEnterprise;

internal static class SeedFactory
{
    public static SeedBase Create(string hash, Container container)
    {
        if (container == null) throw new ArgumentNullException(nameof(container));

        var jsonDataLength = container.Rom.Read<uint>(Shared.Addresses.ROM.MetadataLength);
        if (jsonDataLength <= 0)
            throw new InvalidOperationException("No metadata found");

        var jsonData = container.Rom.ReadBytes(Shared.Addresses.ROM.MetadataAddress.WithLength(jsonDataLength));
        var json = Encoding.UTF8.GetString(jsonData);
        var jObj = JObject.Parse(json);
        var metadata = new Metadata(jObj);
        var version = jObj["version"]?.ToObject<string>();

        switch (version)
        {
            case "v5.0.0-a.1":
                return new _5._0._0.Seed(hash, metadata, container);
            default:
                throw new NotSupportedException($"Unsupported Free Enterprise version: {version}" );
        }
    }
}
