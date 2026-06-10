using FF.Rando.Companion.Extensions;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Text;

namespace FF.Rando.Companion.Games.FreeEnterprise.RomData;

public class Metadata
{
    public Metadata(JObject o)
    {
        Version = o[nameof(Version).ToSnakeCase()]?.ToObject<string>();
        Flags = o[nameof(Flags).ToSnakeCase()]?.ToObject<string>();
        BinaryFlags = o[nameof(BinaryFlags).ToSnakeCase()]?.ToObject<string>();
        Seed = o[nameof(Seed).ToSnakeCase()]?.ToObject<string>();
        Objectives = (o[nameof(Objectives).ToSnakeCase()]?.Children().ToList() ?? []).Select(ObjectiveFactory.Create).ToArray();
    }

    protected Metadata() { }

    public string? Version { get; protected set; }
    public string? Flags { get; protected set; }
    public string? BinaryFlags { get; protected set; }
    public string? Seed { get; protected set; }
    public Objective[]? Objectives { get; protected set; }
}

public class CompressedMetadata : Metadata
{
    public CompressedMetadata(string version, byte[] compressedData)
    {
        Version = version;
        var uncompressedPayload = GetUncompressedPayload(compressedData);
        var json = Encoding.UTF8.GetString(uncompressedPayload);
        var jObj = JObject.Parse(json);

        Flags = jObj[nameof(Flags).ToSnakeCase()]?.ToObject<string>();
        BinaryFlags = jObj[nameof(BinaryFlags).ToSnakeCase()]?.ToObject<string>();
        Seed = jObj[nameof(Seed).ToSnakeCase()]?.ToObject<string>();
        Objectives = (jObj[nameof(Objectives).ToSnakeCase()]?.Children().ToList() ?? []).Select(ObjectiveFactory.Create).ToArray();
    }

    private byte[] GetUncompressedPayload(byte[] data)
    {
        using var outputStream = new MemoryStream();
        using var inputStream = new MemoryStream(data);
        var zipInputStream = new ZipInputStream(inputStream);
        zipInputStream.GetNextEntry();
        zipInputStream.CopyTo(outputStream);
        return outputStream.ToArray();
    }
}