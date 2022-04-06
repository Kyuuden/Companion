using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.FlagSet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Locations
    {
        private readonly IFlagSet? _flagSet;

        private KeyItems? _keyItems;

        public Locations(IFlagSet? flagSet)
        {
            _flagSet = flagSet;
            CheckedKeyItemLocations = new Dictionary<KeyItemLocationType, TimeSpan?>();
            CheckedCharacterLocations = new Dictionary<CharacterLocationType, TimeSpan?>();
        }

        public bool UpdateCheckedLocations(TimeSpan now, byte[] checkedLocations)
        {
            var updated = false;
            foreach (var location in Enum.GetValues(typeof(CharacterLocationType)).Cast<CharacterLocationType>())
            {
                if (checkedLocations.Read<bool>((uint)location) && !CheckedCharacterLocations.ContainsKey(location))
                {
                    CheckedCharacterLocations[location] = now;
                    updated = true;
                }
                else if (!checkedLocations.Read<bool>((uint)location) && CheckedCharacterLocations.ContainsKey(location))
                {
                    CheckedCharacterLocations.Remove(location);
                    updated = true;
                }
            }

            foreach (var location in Enum.GetValues(typeof(KeyItemLocationType)).Cast<KeyItemLocationType>())
            {
                if (checkedLocations.Read<bool>((uint)location) && !CheckedKeyItemLocations.ContainsKey(location))
                {
                    CheckedKeyItemLocations[location] = now;
                    updated = true;
                }
                else if (!checkedLocations.Read<bool>((uint)location) && CheckedKeyItemLocations.ContainsKey(location))
                {
                    CheckedKeyItemLocations.Remove(location);
                    updated = true;
                }
            }

            return updated;
        }

        public bool UpdateKeyItems(KeyItems keyItems)
        {
            var beforeUpdate = GetAvailableKeyItemLocations();
            _keyItems = keyItems;
            var afterUpdate = GetAvailableKeyItemLocations();
            beforeUpdate.SymmetricExceptWith(afterUpdate);
            return beforeUpdate.Count > 0;
        }

        public Dictionary<KeyItemLocationType, TimeSpan?> CheckedKeyItemLocations { get; }
        public Dictionary<CharacterLocationType, TimeSpan?> CheckedCharacterLocations { get; }

        public HashSet<string> GetAvailableKeyItemLocations()
            => new HashSet<KeyItemLocationType>(
                keyItemRequirementDictionary
                .Where(kvp => kvp.Value.IsAvailable(_keyItems, _flagSet, new HashSet<KeyItemLocationType>(CheckedKeyItemLocations.Keys))).Select(kvp => kvp.Key))
            .Except(CheckedKeyItemLocations.Keys)
            .Select(location => TextLookup.GetName(location) ?? location.ToString())
            .ToHashSet();

        public HashSet<string> GetAvailableCharacterLocations()
            => new HashSet<CharacterLocationType>(
                characterRequirementDictionary
                .Where(kvp => kvp.Value.IsAvailable(_keyItems, _flagSet, new HashSet<CharacterLocationType>(CheckedCharacterLocations.Keys))).Select(kvp => kvp.Key))
            .Except(CheckedCharacterLocations.Keys)
            .Select(location => TextLookup.GetName(location) ?? location.ToString())
            .ToHashSet();

        private static readonly Dictionary<KeyItemLocationType, LocationRequirements<KeyItemLocationType>> keyItemRequirementDictionary = new Dictionary<KeyItemLocationType, LocationRequirements<KeyItemLocationType>>();
        private static readonly Dictionary<CharacterLocationType, LocationRequirements<CharacterLocationType>> characterRequirementDictionary = new Dictionary<CharacterLocationType, LocationRequirements<CharacterLocationType>>();

        static Locations()
        {
            keyItemRequirementDictionary[KeyItemLocationType.StartingItem]             = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.AntlionNest]              = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.DefendingFabul]           = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.MtOrdeals]                = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.BaronInn]                 = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.BaronCastle]              = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.BaronKey].Found };
            keyItemRequirementDictionary[KeyItemLocationType.EdwardInToroia]           = new(World.Overworld)   { CheckFlags = (fs) => fs.KFree };
            keyItemRequirementDictionary[KeyItemLocationType.CaveMagnes]               = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.TwinHarp].Found };
            keyItemRequirementDictionary[KeyItemLocationType.TowerOfZot]               = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.EarthCrystal].Found };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlBoss]           = new(World.Underground) { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.SuperCannon]              = new(World.Underground) { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.TowerKey].Found };
            keyItemRequirementDictionary[KeyItemLocationType.Luca]                     = new(World.Underground) { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.SealedCave]               = new(World.Underground) { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.LucaKey].Found };
            keyItemRequirementDictionary[KeyItemLocationType.FeymarchChest]            = new(World.Underground) { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.RatTailTrade]             = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.Hook].Found && ki[KeyItemType.RatTail].Found };
            keyItemRequirementDictionary[KeyItemLocationType.Shelia1]                  = new(World.Underground) { CheckFlags = (fs) => fs.KMain };
            keyItemRequirementDictionary[KeyItemLocationType.Shelia2]                  = new(World.Underground) { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.Pan].Found, CheckLocations = (cl) => cl.Contains(KeyItemLocationType.FromTheSylphs) };
            keyItemRequirementDictionary[KeyItemLocationType.FeymarchQueen]            = new(World.Underground) { CheckFlags = (fs) => fs.KSummon };
            keyItemRequirementDictionary[KeyItemLocationType.FeymarchKing]             = new(World.Underground) { CheckFlags = (fs) => fs.KSummon };
            keyItemRequirementDictionary[KeyItemLocationType.OdinThrone]               = new(World.Overworld)   { CheckFlags = (fs) => fs.KSummon, CheckKeyItems = (ki) => ki[KeyItemType.BaronKey].Found };
            keyItemRequirementDictionary[KeyItemLocationType.FromTheSylphs]            = new(World.Underground) { CheckFlags = (fs) => fs.KSummon, CheckKeyItems = (ki) => ki[KeyItemType.Pan].Found };
            keyItemRequirementDictionary[KeyItemLocationType.CaveBahamut]              = new(World.Moon)        { CheckFlags = (fs) => fs.KSummon };
            keyItemRequirementDictionary[KeyItemLocationType.MurasameAltar]            = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            keyItemRequirementDictionary[KeyItemLocationType.CrystalSwordAltar]        = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            keyItemRequirementDictionary[KeyItemLocationType.WhiteSpearAltar]          = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            keyItemRequirementDictionary[KeyItemLocationType.RibbonChest1]             = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            keyItemRequirementDictionary[KeyItemLocationType.RibbonChest2]             = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            keyItemRequirementDictionary[KeyItemLocationType.MasamuneAltar]            = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            keyItemRequirementDictionary[KeyItemLocationType.TowerOfZotTrappedChest]   = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.EblanTrappedChest1]       = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.EblanTrappedChest2]       = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.EblanTrappedChest3]       = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest1]  = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest2]  = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest3]  = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest4]  = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.CaveEblanTrappedChest]    = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap, CheckKeyItems = (ki) => ki[KeyItemType.Hook].Found };
            keyItemRequirementDictionary[KeyItemLocationType.UpperBabIlTrappedChest]   = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap, CheckKeyItems = (ki) => ki[KeyItemType.Hook].Found };
            keyItemRequirementDictionary[KeyItemLocationType.CaveOfSummonsTrappedChest]= new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest1]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest2]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest3]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest4]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest5]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest6]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest7]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.GiantOfBabIlTrappedChest] = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap, CheckKeyItems = (ki) => ki[KeyItemType.DarknessCrystal].Found};
            keyItemRequirementDictionary[KeyItemLocationType.LunarPathTrappedChest]    = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest1]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest2]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest3]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest4]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest5]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest6]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest7]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest8]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest9]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            keyItemRequirementDictionary[KeyItemLocationType.RydiasMom]                = new(World.Overworld)   { CheckFlags = (fs) => !fs.KFree };
            keyItemRequirementDictionary[KeyItemLocationType.FallenGolbez]             = new(World.Overworld)   { CheckFlags = (fs) => false, CheckKeyItems = (ki) => false };
            keyItemRequirementDictionary[KeyItemLocationType.ObjectiveCompletion]      = new(World.Overworld)   { CheckFlags = (fs) => false, CheckKeyItems = (ki) => false };

            //characterRequirementDictionary[CharacterLocationType.StartingCharacter]    = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.StartingPartner]      = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.KaipoInn]             = new(World.Overworld)   { CheckKeyItems = (ki) => ki[KeyItemType.Package].Found };
            characterRequirementDictionary[CharacterLocationType.WateryPass]           = new(World.Overworld)   { CheckFlags = (fs) => fs.CFree };
            characterRequirementDictionary[CharacterLocationType.Damcyan]              = new(World.Overworld)   { CheckFlags = (fs) => fs.CFree };
            characterRequirementDictionary[CharacterLocationType.KaipoInfirmary]       = new(World.Overworld)   { CheckKeyItems = (ki) => ki[KeyItemType.SandRuby].Found };
            characterRequirementDictionary[CharacterLocationType.MtHobbs]              = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.Mysidia1]             = new(World.Overworld)   { CheckFlags = (fs) => fs.CFree };
            characterRequirementDictionary[CharacterLocationType.Mysidia2]             = new(World.Overworld)   { CheckFlags = (fs) => fs.CFree };
            characterRequirementDictionary[CharacterLocationType.MtOrdeals]            = new(World.Overworld)   { CheckFlags = (fs) => fs.CFree };
            characterRequirementDictionary[CharacterLocationType.BaronInn]             = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.BaronCastle]          = new(World.Overworld)   { CheckKeyItems = (ki) => ki[KeyItemType.BaronKey].Found };
            characterRequirementDictionary[CharacterLocationType.Zot1]                 = new(World.Overworld)   { CheckKeyItems = (ki) => ki[KeyItemType.EarthCrystal].Found };
            characterRequirementDictionary[CharacterLocationType.Zot2]                 = new(World.Overworld)   { CheckKeyItems = (ki) => ki[KeyItemType.EarthCrystal].Found };
            characterRequirementDictionary[CharacterLocationType.DwarfCastle]          = new(World.Underground) { };
            characterRequirementDictionary[CharacterLocationType.EblanCave]            = new(World.Overworld)   { CheckKeyItems = (ki) => ki[KeyItemType.Hook].Found };
            characterRequirementDictionary[CharacterLocationType.Moon]                 = new(World.Moon)        { };
            characterRequirementDictionary[CharacterLocationType.Giant]                = new(World.Overworld)   { CheckKeyItems = (ki) => ki[KeyItemType.DarknessCrystal].Found };
        }
    }

    public enum World { Overworld, Underground, Moon }

    public class LocationRequirements<T>
    {
        public World World { get; }

        public LocationRequirements(World world)
        {
            World = world;
        }

        public Func<KeyItems, bool> CheckKeyItems { get; set; } = ks => true;
        public Func<IFlagSet, bool> CheckFlags { get; set; } = fs => true;
        public Func<HashSet<T>, bool> CheckLocations { get; set; } = l => true;

        public bool IsAvailable(KeyItems? items, IFlagSet? flagSet, HashSet<T> checkedLocations)
        {
            switch (World)
            {
                case World.Overworld: break;
                case World.Underground:
                    if (items == null || (!items[KeyItemType.Hook].Found && !items[KeyItemType.MagmaKey].Found))
                        return false;
                    break;
                case World.Moon:
                    if (items == null || !items[KeyItemType.DarknessCrystal].Found)
                        return false;
                    break;
            }

            return 
                (items != null && CheckKeyItems(items)) && 
                (flagSet == null || CheckFlags(flagSet)) && 
                CheckLocations(checkedLocations);
        }
    }

}
