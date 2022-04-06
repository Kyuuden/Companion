using Newtonsoft.Json;
using System.Collections.Generic;

namespace BizHawk.FreeEnterprise.Companion
{
    public class Names
    {
        [JsonProperty("keyItemNames")]
        public IList<string>? KeyItemNames { get; set; }

        [JsonProperty("keyItemShortNames")]
        public IList<string>? KeyItemShortNames { get; set; }

        [JsonProperty("keyItemDescriptions")]
        public IList<string>? KeyItemDescriptions { get; set; }        

        [JsonProperty("locationNames")]
        public IList<string>? LocationNames { get; set; }

        [JsonProperty("characterLocationNames")]
        public IList<string>? CharacterLocationNames { get; set; }
    }
}
