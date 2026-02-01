using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Settings;
using System;

namespace FF.Rando.Companion.Games.WorldsCollide;
internal class Container : EmulationContainerBase
{
    public WorldsCollideSettings Settings { get; }
    public ISettings RootSettings { get; }

    public Container(ApiContainer container, IMemoryDomains domains, ISettings baseSettings)
        : base(container, domains)
    {
        RootSettings = baseSettings;
        if (baseSettings.GameSettings.TryGetValue("WorldsCollide", out var settings))
            Settings = settings as WorldsCollideSettings ?? throw new InvalidOperationException();
        else
            throw new InvalidOperationException();
    }
}
