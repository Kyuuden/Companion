using BizHawk.FreeEnterprise.Companion.Sprites;
using System;

namespace BizHawk.FreeEnterprise.Companion
{
    public class RomData : IDisposable
    {
        public RomData(MemorySpace rom)
        {
            Font = new Font(rom);
            CharacterSprites = new Characters(rom);
        }

        public Font Font { get; }
        public Characters CharacterSprites { get; }
            
        public void Dispose()
        {
            Font.Dispose();
            CharacterSprites.Dispose();
        }
    }
}
