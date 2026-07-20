using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Games.JetsOfTime.Settings;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.Timing;
using System;

namespace FF.Rando.Companion.Games.JetsOfTime;

internal class Container : EmulationContainerBase
{
    public JetsOfTimeSettings Settings { get; }
    public ISettings RootSettings { get; }

    public Container(ApiContainer container, IMemoryDomains domains, ISettings baseSettings, ITimer timer)
        : base(container, domains, timer)
    {
        RootSettings = baseSettings;
        if (baseSettings.GameSettings.TryGetValue("JetsOfTime", out var settings))
            Settings = settings as JetsOfTimeSettings ?? throw new InvalidOperationException();
        else
            throw new InvalidOperationException();
    }
}
