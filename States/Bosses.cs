using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Bosses
    {
        private readonly Dictionary<BossType, bool> _seenBosses = new Dictionary<BossType, bool>();

        public IReadOnlyDictionary<BossType, bool> Seen => _seenBosses;

        public Bosses()
        {
            foreach (BossType boss in Enum.GetValues(typeof(BossType)))
            {
                _seenBosses[boss] = false;
            }
        }

        public bool this[BossType boss] => _seenBosses[boss];

        public void Swap(BossType boss)
        {
            _seenBosses[boss] = !_seenBosses[boss];
        }

        public int SeenCount => _seenBosses.Values.Count(v => v);
    }
}
