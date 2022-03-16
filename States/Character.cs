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
            return obj is Character settings
                && _data.Length == settings._data.Length
                && _data.Take(2).SequenceEqual(settings._data.Take(2));
        }

        public override int GetHashCode()
        {
            return -1945990370 + EqualityComparer<byte[]>.Default.GetHashCode(_data);
        }

        public byte ID => _data.Read<byte>(0, 5);

        public bool IsBackRow => _data.Read<bool>(8 + 7, 1);
        public CharacterType Class => _data.Read<CharacterType>(8, 4);

        public override string ToString()
            => ID == 0 ? "None" : $"{Class} ({ID})";
    }
}
