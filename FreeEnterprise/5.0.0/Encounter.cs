using System;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class Encounter : IEncounter
{
    public Encounter(string locationDescription, TimeSpan when)
    {
        Location = locationDescription;
        WhenFound = when;
    }

    public string Location { get; }

    public bool IsDefeated { get; set; }

    public TimeSpan? WhenDefeated { get; set; }
    public TimeSpan WhenFound { get; }
}