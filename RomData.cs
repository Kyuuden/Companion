using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.Sprites;
using System;

namespace BizHawk.FreeEnterprise.Companion
{
    public class RomData : IDisposable
    {
        public RomData(MemorySpace rom, Settings settings)
        {
            Font = new Font(rom, settings);
            CharacterSprites = new Characters(rom);
            Overlays = new Overlays(rom, Font);
        }

        public Font Font { get; }
        public Characters CharacterSprites { get; }
        public Overlays Overlays { get; }
            
        public void Dispose()
        {
            Font.Dispose();
            CharacterSprites.Dispose();
            Overlays.Dispose();
        }
    }
}
