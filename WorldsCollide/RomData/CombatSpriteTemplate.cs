using FF.Rando.Companion.Extensions;
using System;

namespace FF.Rando.Companion.WorldsCollide.RomData;

public class CombatSpriteTemplate
{
    public int TileOffset { get; }
    public int BitsPerPixel { get; } 
    public bool LargeStencil { get; }
    public int PaletteOffset { get; }
    public int StencilOffset { get; }
    public int StencilSize { get; }
    public int TileSize { get; }
    public int PaletteSize { get; }

    public CombatSpriteTemplate(byte[] data)
    {
        if (data.Length != 5) throw new ArgumentException(nameof(data));

        TileOffset = (data.Read<ushort>(0, 16) & 0x7FFF) * 8;
        BitsPerPixel = (data.Read<ushort>(0, 16) >> 15) == 1 ? 3 : 4;
        PaletteOffset = data.Read<byte>(23, 9) * 8;
        LargeStencil = (data[3] & 0x80) == 0x80;
        StencilSize = LargeStencil ? 32 : 8;
        StencilOffset = data[4] * 8;// StencilSize;
        TileSize = (BitsPerPixel == 3) ? 0x18 : 0x20;
        PaletteSize = (BitsPerPixel == 3) ? 0x10 : 0x20;
    }
}

