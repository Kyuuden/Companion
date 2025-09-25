using KGySoft.CoreLibraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise.GaleswiftFork;
internal class TextFlags : IFlags
{
    private readonly HashSet<uint> _hardRequiredObjectives = [];

    public TextFlags(string flagString)
    {
        var flags = flagString
            .Split([' '], StringSplitOptions.RemoveEmptyEntries)
            .GroupBy(s => s[0])
            .ToDictionary(
                g => char.ToUpperInvariant(g.Key),
                g => g.SelectMany(s => s[1..].Split('/')).ToList());


        if (flags.TryGetValue('O', out var objectiveFlags))
        {
            CClassicGiant = SubFlagExists("mode", "classicgiant", objectiveFlags);
            OWinCrystal = SubFlagExists("win", "crystal", objectiveFlags);
            OWinGame = SubFlagExists("win", "game", objectiveFlags);
            NumRequiredObjectives = FindAndParseByteFlag("req", objectiveFlags) ?? -1;

            var hardreq = objectiveFlags.FirstOrDefault(f => f.StartsWith("hardreq:", StringComparison.InvariantCultureIgnoreCase));
            if (hardreq != null)
            {
                foreach (var hString in hardreq.Split(':').Last().Split(','))
                {
                    if (uint.TryParse(hString, out var hardnum))
                        _hardRequiredObjectives.Add(hardnum-1);
                }
            }

            GatedObjectiveNum = FindAndParseByteFlag("gated", objectiveFlags) ?? 0;
        }

        if (flags.TryGetValue('K', out var keyItemFlags))
        {
            KForge = FlagExists("forge", keyItemFlags);
            KMain = FlagExists("main", keyItemFlags);
            KMoon = FlagExists("moon", keyItemFlags);
            KPink = FlagExists("pink", keyItemFlags);
            KSummon = FlagExists("summon", keyItemFlags);
            KUnsafe = FlagExists("unsafe", keyItemFlags);
            KUnsafer = FlagExists("unsafer", keyItemFlags);

            var miabs = keyItemFlags.FirstOrDefault(f => f.StartsWith("miab:", StringComparison.InvariantCultureIgnoreCase));
            if (miabs != null)
            {
                var miabFlags = miabs[5..].Split(',');
                KMaibAbove = FlagExists("above", miabFlags);
                KMaibAll = FlagExists("standard", miabFlags);
                KMaibBelow = FlagExists("below", miabFlags);
                KMaibLST = FlagExists("lst", miabFlags);
                KMaibStandard = FlagExists("all", miabFlags);
            }

            var nofree = keyItemFlags.FirstOrDefault(f => f.StartsWith("nofree", StringComparison.InvariantCultureIgnoreCase));
            if (nofree != null)
            {
                if (nofree.EndsWith("dwarf", StringComparison.InvariantCultureIgnoreCase))
                    KNoFreeMode = KNoFreeMode.DwarfCastle;
                else if (nofree.EndsWith("package", StringComparison.InvariantCultureIgnoreCase))
                    KNoFreeMode = KNoFreeMode.Package;
                else
                    KNoFreeMode = KNoFreeMode.Standard;
            }
            else
                KNoFreeMode = KNoFreeMode.Disabled;
        }

        if (flags.TryGetValue('C', out var characterFlags))
        {
            CHero = FlagExists("hero", characterFlags);
            CNoEarned = FlagExists("noearned", characterFlags);
            CNoFree = FlagExists("nofree", characterFlags);
            CNoPartner = FlagExists("nopartner", characterFlags);
            CSuperhero = FlagExists("superhero", characterFlags);
            MaxPartySize = FindAndParseByteFlag("party", characterFlags) ?? 5;
        }

        if (flags.TryGetValue('-', out var otherFlags))
        {
            VanillaAgility = SubFlagExists("agility", "vanilla", otherFlags) || SubFlagExists("vanilla", "agility", otherFlags);
            var xp = otherFlags.FirstOrDefault(f => f.StartsWith("exp:", StringComparison.InvariantCultureIgnoreCase));
            if (xp != null)
            {
                var xpFlags = xp[4..].Split(',');

                XCrystalBonus = FlagExists("crystalbonus", xpFlags);
                XNoKeyBonus = FlagExists("nokeybonus", xpFlags);
                XSmallParty = FlagExists("smallparty", xpFlags);

                if (FlagExists("kicheckbonus2", xpFlags))
                    XKeyItemCheckBonus = KeyItemCheckXpBonus._2Percent;
                else if (FlagExists("kicheckbonus5", xpFlags))
                    XKeyItemCheckBonus = KeyItemCheckXpBonus._5Percent;
                else if (FlagExists("kicheckbonus10", xpFlags))
                    XKeyItemCheckBonus = KeyItemCheckXpBonus._10Percent;
                else if (FlagExists("kicheckbonus_num", xpFlags))
                    XKeyItemCheckBonus = KeyItemCheckXpBonus.Split;
                else
                    XKeyItemCheckBonus = KeyItemCheckXpBonus.None;

                if (FlagExists("objectivebonus5", xpFlags))
                    XObjectiveBonus = ObjectiveXpBonus._5Percent;
                else if (FlagExists("objectivebonus10", xpFlags))
                    XObjectiveBonus = ObjectiveXpBonus._10Percent;
                else if (FlagExists("objectivebonus25", xpFlags))
                    XObjectiveBonus = ObjectiveXpBonus._25Percent;
                else if (FlagExists("objectivebonus_num", xpFlags))
                    XObjectiveBonus = ObjectiveXpBonus.Split;
                else
                    XObjectiveBonus = ObjectiveXpBonus.None;

                if (FlagExists("zonkbonus2", xpFlags))
                    XKeyItemZonkXpBonus = KeyItemZonkXpBonus._2Percent;
                else if (FlagExists("zonkbonus5", xpFlags))
                    XKeyItemZonkXpBonus = KeyItemZonkXpBonus._5Percent;
                else if (FlagExists("zonkbonus10", xpFlags))
                    XKeyItemZonkXpBonus = KeyItemZonkXpBonus._10Percent;
                else
                    XKeyItemZonkXpBonus = KeyItemZonkXpBonus.None;

                if (FlagExists("miabbonus50", xpFlags))
                    XMiabXpBonus = MiabXpBonus._50Percent;
                else if (FlagExists("miabbonus100", xpFlags))
                    XMiabXpBonus = MiabXpBonus._100Percent;
                else
                    XMiabXpBonus = MiabXpBonus.None;

                if (FlagExists("moonbonus100", xpFlags))
                    XMoonXpBonus = MoonXpBonus._100Percent;
                else if (FlagExists("moonbonus200", xpFlags))
                    XMoonXpBonus = MoonXpBonus._200Percent;
                else
                    XMoonXpBonus = MoonXpBonus.None;
            }
        }
    }

    private bool FlagExists(string flag, IEnumerable<string> flags)
        => flags.Any(f => f.Equals(flag, StringComparison.InvariantCultureIgnoreCase));

    private bool SubFlagExists(string flag, string subFlag, IEnumerable<string> flags)
        => flags.FirstOrDefault(f => f.StartsWith(flag, StringComparison.InvariantCultureIgnoreCase))?.Contains(subFlag, StringComparison.InvariantCultureIgnoreCase) ?? false;

    private byte? FindAndParseByteFlag(string flag, IEnumerable<string> flags)
    {
        var gated = flags.FirstOrDefault(f => f.StartsWith(flag, StringComparison.InvariantCultureIgnoreCase));
        if (gated != null && byte.TryParse(gated.Split(':').Last(), out var num))
            return num;

        return null;
    }

    public bool CClassicGiant { get; }

    public bool CHero { get; }

    public bool CNoEarned { get; }

    public bool CNoFree { get; }

    public bool CNoPartner { get; }

    public bool CSuperhero { get; }

    public int GatedObjectiveNum { get; }

    public bool KForge { get; }

    public bool KMaibAbove { get; }

    public bool KMaibAll { get; }

    public bool KMaibBelow { get; }

    public bool KMaibLST { get; }

    public bool KMaibStandard { get; }

    public bool KMain { get; }

    public bool KMoon { get; }

    public KNoFreeMode KNoFreeMode { get; }

    public bool KPink { get; }

    public bool KSummon { get; }

    public bool KUnsafe { get; }

    public bool KUnsafer { get; }

    public byte MaxPartySize { get; }

    public int NumRequiredObjectives { get; }
    
    public bool OWinCrystal { get; }

    public bool OWinGame { get; }

    public bool VanillaAgility { get; }

    public bool XCrystalBonus { get; }

    public KeyItemCheckXpBonus XKeyItemCheckBonus { get; }

    public KeyItemZonkXpBonus XKeyItemZonkXpBonus { get; }

    public MiabXpBonus XMiabXpBonus { get; }

    public MoonXpBonus XMoonXpBonus { get; }

    public bool XNoKeyBonus { get; }

    public ObjectiveXpBonus XObjectiveBonus { get; }

    public bool XSmallParty { get; }

    public bool IsHardRequired(uint objectiveNum)
    {
        return _hardRequiredObjectives.Contains(objectiveNum);
    }
}
