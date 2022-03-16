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

        private byte[] _locationData = new byte[16];

        public Locations(IFlagSet? flagSet)
        {
            _flagSet = flagSet;
            CheckedLocations = new HashSet<KeyItemLocationType>();
        }

        public bool UpdateCheckedLocations(byte[] checkedLocations)
        {
            if (_locationData.Skip(4).SequenceEqual(checkedLocations.Skip(4))) //first 4 bytes are character locations
                return false;

            _locationData = checkedLocations;
            CheckedLocations = new HashSet<KeyItemLocationType>(
                Enum.GetValues(typeof(KeyItemLocationType)).Cast<KeyItemLocationType>().Where(location => _locationData.Read<bool>((uint)location)));

            return true;
        }

        public bool UpdateKeyItems(KeyItems keyItems)
        {
            var beforeUpdate = GetAvailableLocations();
            _keyItems = keyItems;
            var afterUpdate = GetAvailableLocations();
            beforeUpdate.SymmetricExceptWith(afterUpdate);
            return beforeUpdate.Count > 0;
        }

        public HashSet<KeyItemLocationType> CheckedLocations { get; private set; }

        public HashSet<string> GetAvailableLocations()
            => new HashSet<KeyItemLocationType>(requirementDictionary.Where(kvp => kvp.Value.IsAvailable(_keyItems, _flagSet, CheckedLocations)).Select(kvp => kvp.Key))
            .Except(CheckedLocations)
            .Select(location => DescriptionLookup.GetDescription(location) ?? location.ToString())
            .ToHashSet();

        private static readonly Dictionary<KeyItemLocationType, LocationRequirements> requirementDictionary = new Dictionary<KeyItemLocationType, LocationRequirements>();

        static Locations()
        {
            requirementDictionary[KeyItemLocationType.StartingItem]             = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.AntlionNest]              = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.DefendingFabul]           = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.MtOrdeals]                = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.BaronInn]                 = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.BaronCastle]              = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.BaronKey].Found };
            requirementDictionary[KeyItemLocationType.EdwardInToroia]           = new(World.Overworld)   { CheckFlags = (fs) => fs.KFree };
            requirementDictionary[KeyItemLocationType.CaveMagnes]               = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.TwinHarp].Found };
            requirementDictionary[KeyItemLocationType.TowerOfZot]               = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.EarthCrystal].Found };
            requirementDictionary[KeyItemLocationType.LowerBabIlBoss]           = new(World.Underground) { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.SuperCannon]              = new(World.Underground) { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.TowerKey].Found };
            requirementDictionary[KeyItemLocationType.Luca]                     = new(World.Underground) { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.SealedCave]               = new(World.Underground) { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.LucaKey].Found };
            requirementDictionary[KeyItemLocationType.FeymarchChest]            = new(World.Underground) { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.RatTailTrade]             = new(World.Overworld)   { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.Hook].Found && ki[KeyItemType.RatTail].Found };
            requirementDictionary[KeyItemLocationType.Shelia1]                  = new(World.Underground) { CheckFlags = (fs) => fs.KMain };
            requirementDictionary[KeyItemLocationType.Shelia2]                  = new(World.Underground) { CheckFlags = (fs) => fs.KMain, CheckKeyItems = (ki) => ki[KeyItemType.Pan].Found, CheckLocations = (cl) => cl.Contains(KeyItemLocationType.FromTheSylphs) };
            requirementDictionary[KeyItemLocationType.FeymarchQueen]            = new(World.Underground) { CheckFlags = (fs) => fs.KSummon };
            requirementDictionary[KeyItemLocationType.FeymarchKing]             = new(World.Underground) { CheckFlags = (fs) => fs.KSummon };
            requirementDictionary[KeyItemLocationType.OdinThrone]               = new(World.Overworld)   { CheckFlags = (fs) => fs.KSummon, CheckKeyItems = (ki) => ki[KeyItemType.BaronKey].Found };
            requirementDictionary[KeyItemLocationType.FromTheSylphs]            = new(World.Underground) { CheckFlags = (fs) => fs.KSummon, CheckKeyItems = (ki) => ki[KeyItemType.Pan].Found };
            requirementDictionary[KeyItemLocationType.CaveBahamut]              = new(World.Moon)        { CheckFlags = (fs) => fs.KSummon };
            requirementDictionary[KeyItemLocationType.MurasameAltar]            = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            requirementDictionary[KeyItemLocationType.CrystalSwordAltar]        = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            requirementDictionary[KeyItemLocationType.WhiteSpearAltar]          = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            requirementDictionary[KeyItemLocationType.RibbonChest1]             = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            requirementDictionary[KeyItemLocationType.RibbonChest2]             = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            requirementDictionary[KeyItemLocationType.MasamuneAltar]            = new(World.Moon)        { CheckFlags = (fs) => fs.KMoon };
            requirementDictionary[KeyItemLocationType.TowerOfZotTrappedChest]   = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.EblanTrappedChest1]       = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.EblanTrappedChest2]       = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.EblanTrappedChest3]       = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest1]  = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest2]  = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest3]  = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest4]  = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.CaveEblanTrappedChest]    = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap, CheckKeyItems = (ki) => ki[KeyItemType.Hook].Found };
            requirementDictionary[KeyItemLocationType.UpperBabIlTrappedChest]   = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap, CheckKeyItems = (ki) => ki[KeyItemType.Hook].Found };
            requirementDictionary[KeyItemLocationType.CaveOfSummonsTrappedChest]= new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.SylphCaveTrappedChest1]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.SylphCaveTrappedChest2]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.SylphCaveTrappedChest3]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.SylphCaveTrappedChest4]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.SylphCaveTrappedChest5]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.SylphCaveTrappedChest6]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.SylphCaveTrappedChest7]   = new(World.Underground) { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.GiantOfBabIlTrappedChest] = new(World.Overworld)   { CheckFlags = (fs) => fs.KTrap, CheckKeyItems = (ki) => ki[KeyItemType.DarknessCrystal].Found};
            requirementDictionary[KeyItemLocationType.LunarPathTrappedChest]    = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest1]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest2]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest3]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest4]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest5]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest6]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest7]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest8]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.LunarCoreTrappedChest9]   = new(World.Moon)        { CheckFlags = (fs) => fs.KTrap && (fs.KMoon || fs.KUnsafe) };
            requirementDictionary[KeyItemLocationType.RydiasMom]                = new(World.Overworld)   { CheckFlags = (fs) => !fs.KFree };
            requirementDictionary[KeyItemLocationType.FallenGolbez]             = new(World.Overworld)   { CheckFlags = (fs) => false, CheckKeyItems = (ki) => false };
            requirementDictionary[KeyItemLocationType.ObjectiveCompletion]      = new(World.Overworld)   { CheckFlags = (fs) => false, CheckKeyItems = (ki) => false };
        }
    }

    public enum World { Overworld, Underground, Moon }

    public class LocationRequirements
    {
        public World World { get; }

        public LocationRequirements(World world)
        {
            World = world;
        }

        public Func<KeyItems, bool> CheckKeyItems { get; set; } = ks => true;
        public Func<IFlagSet, bool> CheckFlags { get; set; } = fs => true;
        public Func<HashSet<KeyItemLocationType>, bool> CheckLocations { get; set; } = l => true;

        public bool IsAvailable(KeyItems? items, IFlagSet? flagSet, HashSet<KeyItemLocationType> checkedLocations)
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
                (items == null || CheckKeyItems(items)) && 
                (flagSet == null || CheckFlags(flagSet)) && 
                CheckLocations(checkedLocations);
        }
    }

}
