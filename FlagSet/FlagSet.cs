namespace BizHawk.FreeEnterprise.Companion.FlagSet
{
    public interface IFlagSet
    {
        bool? CanHaveKeyItem(KeyItemLocationType location);
        bool? CanHaveCharacter(CharacterLocationType location);

        int MaxParty { get; }
        int RequriedObjectiveCount { get; }
        bool OWinGame { get; }
        bool OWinCrystal { get; }
        bool No10KeyItemBonus { get; }
        bool CHero { get; }
        bool VanillaAgility { get; }
    }

    public abstract class FlagSet : IFlagSet
    {
        protected abstract int MinDataLength { get; }

        public byte[] Data { get; }
        protected FlagSet(byte[] data)
        {
            Data = new byte[MinDataLength];
            data.CopyTo(Data, 0);
        }

        public abstract int MaxParty { get; }
        public abstract int RequriedObjectiveCount { get; }
        public abstract bool OWinGame { get; }
        public abstract bool OWinCrystal { get; }
        public abstract bool No10KeyItemBonus { get; }
        public abstract bool CHero { get; }
        public abstract bool VanillaAgility { get; }
        public abstract bool? CanHaveKeyItem(KeyItemLocationType location);
        public abstract bool? CanHaveCharacter(CharacterLocationType location);
    }
}
