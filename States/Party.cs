using BizHawk.FreeEnterprise.Companion.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Party
    {
        public IReadOnlyList<Character> Characters { get; }

        public Party(byte[] partyData)
        {
            var temp = partyData.ReadMany<byte[]>(0, 64 * 8, 5).Select(data => new Character(data)).ToList();
            Characters = (new[] { temp[1], temp[3], temp[0], temp[4], temp[2] }).ToList();
        }

        public Party()
        {
            Characters = new List<Character>();
        }

        public override bool Equals(object obj) => obj is Party party && Characters.SequenceEqual(party.Characters);

        public override int GetHashCode()
        {
            return -604923257 + EqualityComparer<IReadOnlyList<Character>>.Default.GetHashCode(Characters);
        }
    }
}
