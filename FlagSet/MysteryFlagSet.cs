using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizHawk.FreeEnterprise.Companion.FlagSet
{
    public class MysteryFlagSet : IFlagSet
    {














        public int MaxParty { get; set; }

        public int RequriedObjectiveCount { get; set; }

        public bool OWinGame { get; set; }

        public bool OWinCrystal { get; set; }

        public bool No10KeyItemBonus { get; set; }

        public bool CHero { get; set; }

        public bool VanillaAgility { get; set; }

        public bool? CanHaveCharacter(CharacterLocationType location)
        {
            throw new NotImplementedException();
        }

        public bool? CanHaveKeyItem(KeyItemLocationType location)
        {
            throw new NotImplementedException();
        }
    }
}
