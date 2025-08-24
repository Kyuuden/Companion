using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.MysticQuestRandomizer.Settings;
internal class MysticQuestRandomizerSettings : GameSettings
{
    public override string Name => "MysticQuestRandomizer";

    public override string DisplayName => "Mystic Quest Randomizer";

    public MysticQuestRandomizerSettings(JObject parent)
        : base(parent)
    {
    }
}
