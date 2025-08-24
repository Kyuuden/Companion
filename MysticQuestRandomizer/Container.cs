using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.MysticQuestRandomizer;
internal class Container : EmulationContainerBase
{
    public MysticQuestRandomizerSettings Settings { get; }
    public ISettings RootSettings { get; }

    public Container(ApiContainer container, IMemoryDomains domains, ISettings baseSettings) 
        : base(container, domains)
    {
        RootSettings = baseSettings;

        if (baseSettings.GameSettings.TryGetValue("MysticQuestRandomizer", out var settings))
            Settings = settings as MysticQuestRandomizerSettings ?? throw new InvalidOperationException();
        else
            throw new InvalidOperationException();
    }
}
