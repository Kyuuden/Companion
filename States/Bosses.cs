using BizHawk.FreeEnterprise.Companion.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Bosses
    {
        private readonly PersistentStorage storage;
        private readonly Func<TimeSpan> getNow;

        public IReadOnlyDictionary<BossType, bool> Seen =>
            Enum.GetValues(typeof(BossType))
            .OfType<BossType>()
            .ToDictionary(b => b, b => storage.BossCheckedTimes[b].HasValue);


        public Bosses(PersistentStorage storage, Func<TimeSpan> getNow)
        {
            this.storage = storage;
            this.getNow = getNow;
        }

        public TimeSpan? this[BossType boss] => storage.BossCheckedTimes[boss];

        public void Swap(BossType boss)
        {
            var hadTime = storage.BossCheckedTimes[boss].HasValue;
            storage.BossCheckedTimes[boss] = hadTime ? null : getNow();
        }

        public int SeenCount => Seen.Values.Count(v => v);
    }
}
