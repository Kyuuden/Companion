using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Objectives
    {
        public IReadOnlyList<string> Descriptions { get; }

        public List<bool> Completions { get; private set; }

        public Objectives()
        {
            Descriptions = new List<string>();
            Completions = new List<bool>();
        }

        public Objectives(IEnumerable<string> objectives)
        {
            Descriptions = objectives.ToList();
            Completions = Enumerable.Repeat(false, Descriptions.Count).ToList();
        }

        public bool UpdateCompletions(byte[] data)
        {
            var newCompletion = data.Take(Descriptions.Count).Select(b => b != 0).ToList();

            if (Completions.SequenceEqual(newCompletion))
                return false;

            Completions = newCompletion;
            return true;
        }
    }
}
