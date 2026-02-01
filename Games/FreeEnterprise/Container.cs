using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using FF.Rando.Companion.Settings;
using System;

namespace FF.Rando.Companion.Games.FreeEnterprise;
internal class Container : EmulationContainerBase
{
    public FreeEnterpriseSettings Settings { get; }
    public ISettings RootSettings { get; }

    public Container(ApiContainer container, IMemoryDomains domains, ISettings baseSettings)
        : base(container, domains)
    {
        RootSettings = baseSettings;

        if (baseSettings.GameSettings.TryGetValue("FreeEnterprise", out var settings))
            Settings = settings as FreeEnterpriseSettings ?? throw new InvalidOperationException();
        else
            throw new InvalidOperationException();
    }
}


