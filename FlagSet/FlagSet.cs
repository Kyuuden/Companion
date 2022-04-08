namespace BizHawk.FreeEnterprise.Companion.FlagSet
{
    public interface IFlagSet
    {
        bool KMain { get; }
        bool KSummon { get; }
        bool KMoon { get; }
        bool KTrap { get; }
        bool KFree { get; }
        bool KUnsafe { get; }
        bool CFree { get; }
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

        public abstract bool KMain { get; }
        public abstract bool KSummon { get; }
        public abstract bool KMoon { get; }
        public abstract bool KTrap { get; }
        public abstract bool KFree { get; }
        public abstract bool KUnsafe { get; }
        public abstract bool CFree { get; }
        public abstract int MaxParty { get; }
        public abstract int RequriedObjectiveCount { get; }
        public abstract bool OWinGame { get; }
        public abstract bool OWinCrystal { get; }
        public abstract bool No10KeyItemBonus { get; }
        public abstract bool CHero { get; }
        public abstract bool VanillaAgility { get; }
    }
}
