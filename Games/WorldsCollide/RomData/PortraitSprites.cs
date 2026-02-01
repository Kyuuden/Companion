using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.WorldsCollide.RomData;

public class PortraitSprites : IDisposable
{
    private readonly Dictionary<Character, PortraitSprite> _portraitSprites = [];

    public PortraitSprites(IMemorySpace memorySpace)
    {
        var portraitTileData = memorySpace.ReadBytes(Addresses.ROM.PortraitSpriteTileData);
        var portraitPaletteData = memorySpace.ReadBytes(Addresses.ROM.PortraitSpritePaletteData);
        foreach (Character character in Enum.GetValues(typeof(Character)))
        {
            var index = (int)character;
            var portraitPalette = new ReadOnlySpan<byte>(portraitPaletteData, index * 32, 32).DecodePalette(new Color32());
            _portraitSprites[character] = new PortraitSprite(new ReadOnlySpan<byte>(portraitTileData, index * 25 * 32, 25 * 32), portraitPalette);
        }
    }

    public void Dispose()
    {
        foreach (var value in _portraitSprites.Values) value.Dispose();
        _portraitSprites.Clear();
    }

    public ISprite Get(Character character) => _portraitSprites[character];
}
