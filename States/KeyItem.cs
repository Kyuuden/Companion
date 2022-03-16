namespace BizHawk.FreeEnterprise.Companion.State
{
    public class KeyItem
    {
        public KeyItemType Key { get; }

        public string Description { get; }
        public bool Found { get; private set; }
        public bool Used { get; private set; }
        public KeyItemLocationType FoundLocation { get; private set; }

        public KeyItem(KeyItemType key)
        {
            Description = DescriptionLookup.GetDescription(key) ?? key.ToString();
            this.Key = key;
        }

        public string ShortDescription
            => Key switch
            {
                KeyItemType.Package => "Package",
                KeyItemType.SandRuby => "SandRuby",
                KeyItemType.LegendSword => "|Legend",
                KeyItemType.BaronKey => "^Baron",
                KeyItemType.TwinHarp => "$TwinHarp",
                KeyItemType.EarthCrystal => "*Earth",
                KeyItemType.MagmaKey => "^Magma",
                KeyItemType.TowerKey => "^Tower",
                KeyItemType.Hook => "Hook",
                KeyItemType.LucaKey => "^Luca",
                KeyItemType.DarknessCrystal => "*Darkness",
                KeyItemType.RatTail => "~Rat",
                KeyItemType.Adamant => "Adamant",
                KeyItemType.Pan => "Pan",
                KeyItemType.Spoon => "`Spoon",
                KeyItemType.PinkTail => "~Pink",
                KeyItemType.Crystal => "*Crystal",
                _ => ""
            };

        public bool FoundAt(KeyItemLocationType location)
        {
            if (Found)
                return false;

            FoundLocation = location;
            Found = true;
            return true;
        }

        public bool Use()
        {
            if (Used) return false;
            Used = true;
            return true;
        }

        public bool Reset()
        {
            if (!Found && !Used)
                return false;
            Found = false;
            Used = false;
            return true;
        }

        public override bool Equals(object? obj)
        {
            return obj is KeyItem item &&
                   Key == item.Key &&
                   Found == item.Found &&
                   Used == item.Used &&
                   FoundLocation == item.FoundLocation;
        }

        public override int GetHashCode()
        {
            int hashCode = -1847489661;
            hashCode = hashCode * -1521134295 + Key.GetHashCode();
            hashCode = hashCode * -1521134295 + Found.GetHashCode();
            hashCode = hashCode * -1521134295 + Used.GetHashCode();
            hashCode = hashCode * -1521134295 + FoundLocation.GetHashCode();
            return hashCode;
        }
    }
}
