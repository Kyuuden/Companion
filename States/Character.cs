using BizHawk.FreeEnterprise.Companion.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Character
    {
        private readonly byte[] _data;

        public Character(List<byte> data) : this(data.ToArray()) { }

        public Character(byte[] data)
        {
            _data = data;
        }

        public override bool Equals(object? obj)
        {
            return obj is Character character
                && ID == character.ID
                && Class == character.Class;
        }

        public override int GetHashCode() => ID << 8 | (int)Class;

        public byte ID => _data.Read<byte>(0, 5);

        public CharacterType Class => _data.Read<CharacterType>(8, 4);

        public override string ToString()
            => ID == 0 ? "None" : $"{Class} ({ID})";
    }
}
