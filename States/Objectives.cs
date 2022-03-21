using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Objectives
    {
        public IReadOnlyList<string> Descriptions { get; }

        public List<TimeSpan?> Completions { get; private set; }

        public Objectives()
        {
            Descriptions = new List<string>();
            Completions = new List<TimeSpan?>();
        }

        public Objectives(IEnumerable<string> objectives)
        {
            Descriptions = objectives.ToList();
            Completions = Enumerable.Repeat((TimeSpan?)null, Descriptions.Count).ToList();
        }

        public bool UpdateCompletions(TimeSpan now, byte[] data)
        {
            var updated = false;
            var newCompletion = data.Take(Descriptions.Count).Select(b => b != 0).ToList();

            for (var i = 0; i < Math.Min(newCompletion.Count, Completions.Count); i ++)
            {
                if (newCompletion[i] && ! Completions[i].HasValue)
                {
                    Completions[i] = now;
                    updated = true;
                }
            }

            return updated;
        }
    }
}
