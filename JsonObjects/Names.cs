using Newtonsoft.Json;
using System.Collections.Generic;

namespace BizHawk.FreeEnterprise.Companion
{
    public class Names
    {
        [JsonProperty("keyItemNames")]
        public IList<string>? KeyItemNames { get; set; }

        [JsonProperty("locationNames")]
        public IList<string>? LocationNames { get; set; }
    }
}
