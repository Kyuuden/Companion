using BizHawk.FreeEnterprise.Companion.Database;
using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.FlagSet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Locations
    {
        private readonly PersistentStorage storage;
        private readonly IFlagSet? _flagSet;
        private KeyItems? _keyItems;
        private byte[] _checkedLocations;

        public Locations(PersistentStorage storage, IFlagSet? flagSet)
        {
            this.storage = storage;
            _flagSet = flagSet;
            _checkedLocations = new byte[WRAMAddresses.CheckedLocationsBytes];
        }

        public bool UpdateCheckedLocations(TimeSpan now, byte[] checkedLocations, out List<uint> updatedIndexes)
        {
            updatedIndexes = new List<uint>();

            if (_checkedLocations.SequenceEqual(checkedLocations))
                return false;

            var changedLocations = checkedLocations.Zip(_checkedLocations, (l, r) => (byte)(l & ~r)).ToArray();

            for (var i = 0u; i < changedLocations.Length * 8; i++)
            {
                if (changedLocations.Read<bool>(i))
                {
                    storage.LocationCheckedTimes[(int)i] = now;
                    updatedIndexes.Add(i);
                }                
            }

            checkedLocations.CopyTo(_checkedLocations, 0);

            return true;
        }

        public bool UpdateKeyItems(KeyItems keyItems)
        {
            var beforeUpdate = GetAvailableKeyItemLocations();
            _keyItems = keyItems.Clone();
            var afterUpdate = GetAvailableKeyItemLocations();
            beforeUpdate.SymmetricExceptWith(afterUpdate);
            return beforeUpdate.Count > 0;
        }

        public IList<KeyItemLocationType> CheckedKeyItemLocations
            => Enum.GetValues(typeof(KeyItemLocationType)).Cast<KeyItemLocationType>().Where(l => _checkedLocations.Read<bool>((uint)l)).ToList();

        public IList<CharacterLocationType> CheckedCharacterLocations
            => Enum.GetValues(typeof(CharacterLocationType)).Cast<CharacterLocationType>().Where(l => _checkedLocations.Read<bool>((uint)l)).ToList();

        public HashSet<string> GetAvailableKeyItemLocations()
            => new HashSet<KeyItemLocationType>(
                keyItemRequirementDictionary
                .Where(kvp => (_flagSet?.CanHaveKeyItem(kvp.Key) ?? false) 
                    && kvp.Value.IsAvailable(_keyItems, _flagSet, new HashSet<KeyItemLocationType>(CheckedKeyItemLocations)))
                .Select(kvp => kvp.Key))
            .Except(CheckedKeyItemLocations)
            .Select(location => TextLookup.GetName(location) ?? location.ToString())
            .ToHashSet();

        public HashSet<string> GetAvailableCharacterLocations()
            => new HashSet<CharacterLocationType>(
                characterRequirementDictionary
                .Where(kvp => (_flagSet?.CanHaveCharacter(kvp.Key) ?? false) 
                    && kvp.Value.IsAvailable(_keyItems, _flagSet, new HashSet<CharacterLocationType>(CheckedCharacterLocations)))
                .Select(kvp => kvp.Key))
            .Except(CheckedCharacterLocations)
            .Select(location => TextLookup.GetName(location) ?? location.ToString())
            .ToHashSet();

        private static readonly Dictionary<KeyItemLocationType, LocationRequirements<KeyItemLocationType>> keyItemRequirementDictionary = new Dictionary<KeyItemLocationType, LocationRequirements<KeyItemLocationType>>();
        private static readonly Dictionary<CharacterLocationType, LocationRequirements<CharacterLocationType>> characterRequirementDictionary = new Dictionary<CharacterLocationType, LocationRequirements<CharacterLocationType>>();

        static Locations()
        {
            keyItemRequirementDictionary[KeyItemLocationType.StartingItem]             = new(World.Overworld)   { };
            keyItemRequirementDictionary[KeyItemLocationType.AntlionNest]              = new(World.Overworld)   { };
            keyItemRequirementDictionary[KeyItemLocationType.DefendingFabul]           = new(World.Overworld)   { };
            keyItemRequirementDictionary[KeyItemLocationType.MtOrdeals]                = new(World.Overworld)   {};
            keyItemRequirementDictionary[KeyItemLocationType.BaronInn]                 = new(World.Overworld)   {};
            keyItemRequirementDictionary[KeyItemLocationType.BaronCastle]              = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.BaronKey) };
            keyItemRequirementDictionary[KeyItemLocationType.EdwardInToroia]           = new(World.Overworld)   {};
            keyItemRequirementDictionary[KeyItemLocationType.CaveMagnes]               = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.TwinHarp) };
            keyItemRequirementDictionary[KeyItemLocationType.TowerOfZot]               = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.EarthCrystal) };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlBoss]           = new(World.Underground) {};
            keyItemRequirementDictionary[KeyItemLocationType.SuperCannon]              = new(World.Underground) { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.TowerKey) };
            keyItemRequirementDictionary[KeyItemLocationType.Luca]                     = new(World.Underground) {};
            keyItemRequirementDictionary[KeyItemLocationType.SealedCave]               = new(World.Underground) { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.LucaKey) };
            keyItemRequirementDictionary[KeyItemLocationType.FeymarchChest]            = new(World.Underground) {};
            keyItemRequirementDictionary[KeyItemLocationType.RatTailTrade]             = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.Hook) && ki.IsFound(KeyItemType.RatTail) };
            keyItemRequirementDictionary[KeyItemLocationType.Shelia1]                  = new(World.Underground) {};
            keyItemRequirementDictionary[KeyItemLocationType.Shelia2]                  = new(World.Underground) { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.Pan), CheckLocations = (cl) => cl.Contains(KeyItemLocationType.FromTheSylphs) };
            keyItemRequirementDictionary[KeyItemLocationType.FeymarchQueen]            = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.FeymarchKing]             = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.OdinThrone]               = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.BaronKey) };
            keyItemRequirementDictionary[KeyItemLocationType.FromTheSylphs]            = new(World.Underground) { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.Pan) };
            keyItemRequirementDictionary[KeyItemLocationType.CaveBahamut]              = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.MurasameAltar]            = new(World.Moon)        {};
            keyItemRequirementDictionary[KeyItemLocationType.CrystalSwordAltar]        = new(World.Moon)        {};
            keyItemRequirementDictionary[KeyItemLocationType.WhiteSpearAltar]          = new(World.Moon)        {};
            keyItemRequirementDictionary[KeyItemLocationType.RibbonChest1]             = new(World.Moon)        {};
            keyItemRequirementDictionary[KeyItemLocationType.RibbonChest2]             = new(World.Moon)        {};
            keyItemRequirementDictionary[KeyItemLocationType.MasamuneAltar]            = new(World.Moon)        {};
            keyItemRequirementDictionary[KeyItemLocationType.TowerOfZotTrappedChest]   = new(World.Overworld)   { };
            keyItemRequirementDictionary[KeyItemLocationType.EblanTrappedChest1]       = new(World.Overworld)   { };
            keyItemRequirementDictionary[KeyItemLocationType.EblanTrappedChest2]       = new(World.Overworld)   { };
            keyItemRequirementDictionary[KeyItemLocationType.EblanTrappedChest3]       = new(World.Overworld)   { };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest1]  = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest2]  = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest3]  = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.LowerBabIlTrappedChest4]  = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.CaveEblanTrappedChest]    = new(World.Overworld)   {  CheckKeyItems = (ki) => ki.IsFound(KeyItemType.Hook) };
            keyItemRequirementDictionary[KeyItemLocationType.UpperBabIlTrappedChest]   = new(World.Overworld)   {  CheckKeyItems = (ki) => ki.IsFound(KeyItemType.Hook) };
            keyItemRequirementDictionary[KeyItemLocationType.CaveOfSummonsTrappedChest]= new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest1]   = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest2]   = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest3]   = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest4]   = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest5]   = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest6]   = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.SylphCaveTrappedChest7]   = new(World.Underground) { };
            keyItemRequirementDictionary[KeyItemLocationType.GiantOfBabIlTrappedChest] = new(World.Overworld)   {  CheckKeyItems = (ki) => ki.IsFound(KeyItemType.DarknessCrystal)};
            keyItemRequirementDictionary[KeyItemLocationType.LunarPathTrappedChest]    = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest1]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest2]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest3]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest4]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest5]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest6]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest7]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest8]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.LunarCoreTrappedChest9]   = new(World.Moon)        { };
            keyItemRequirementDictionary[KeyItemLocationType.RydiasMom]                = new(World.Overworld)   { };
            keyItemRequirementDictionary[KeyItemLocationType.FallenGolbez]             = new(World.Overworld)   { CheckKeyItems = (ki) => false };
            keyItemRequirementDictionary[KeyItemLocationType.ObjectiveCompletion]      = new(World.Overworld)   { CheckKeyItems = (ki) => false };

            characterRequirementDictionary[CharacterLocationType.StartingPartner]      = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.KaipoInn]             = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.Package) };
            characterRequirementDictionary[CharacterLocationType.WateryPass]           = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.Damcyan]              = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.KaipoInfirmary]       = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.SandRuby) };
            characterRequirementDictionary[CharacterLocationType.MtHobbs]              = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.Mysidia1]             = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.Mysidia2]             = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.MtOrdeals]            = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.BaronInn]             = new(World.Overworld)   { };
            characterRequirementDictionary[CharacterLocationType.BaronCastle]          = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.BaronKey) };
            characterRequirementDictionary[CharacterLocationType.Zot1]                 = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.EarthCrystal) };
            characterRequirementDictionary[CharacterLocationType.Zot2]                 = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.EarthCrystal) };
            characterRequirementDictionary[CharacterLocationType.DwarfCastle]          = new(World.Underground) { };
            characterRequirementDictionary[CharacterLocationType.EblanCave]            = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.Hook) };
            characterRequirementDictionary[CharacterLocationType.Moon]                 = new(World.Moon)        { };
            characterRequirementDictionary[CharacterLocationType.Giant]                = new(World.Overworld)   { CheckKeyItems = (ki) => ki.IsFound(KeyItemType.DarknessCrystal) };
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
        public Func<HashSet<T>, bool> CheckLocations { get; set; } = l => true;

        public bool IsAvailable(KeyItems? items, IFlagSet? flagSet, HashSet<T> checkedLocations)
        {
            switch (World)
            {
                case World.Overworld: break;
                case World.Underground:
                    if (items == null || (!items.IsFound(KeyItemType.Hook) && !items.IsFound(KeyItemType.MagmaKey)))
                        return false;
                    break;
                case World.Moon:
                    if (items == null || !items.IsFound(KeyItemType.DarknessCrystal))
                        return false;
                    break;
            }

            return 
                (items != null && CheckKeyItems(items)) && 
                CheckLocations(checkedLocations);
        }
    }

}
