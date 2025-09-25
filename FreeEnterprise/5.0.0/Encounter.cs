using System;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Encounter(string locationDescription, TimeSpan when) : IEncounter
{
    public string Location { get; } = locationDescription;

    public bool IsDefeated { get; set; }

    public TimeSpan? WhenDefeated { get; set; }
    public TimeSpan WhenFound { get; } = when;
}