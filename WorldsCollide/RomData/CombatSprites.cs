using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.WorldsCollide.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.WorldsCollide.RomData;

public class CombatSprites : IDisposable
{
    private readonly Dictionary<Monster, CombatSprite> _monsterSprites = [];
    private readonly Dictionary<Boss, CombatSprite> _bossSprites = [];
    private readonly Dictionary<Esper, CombatSprite> _esperSprites = [];

    public CombatSprites(IMemorySpace memorySpace) 
    {
        var templates = memorySpace.ReadBytes(Addresses.ROM.BattleSpriteDatabase).ReadMany<byte[]>(0, 5 * 8, 0x1A0).Select(b => new CombatSpriteTemplate(b)).ToList();
        var paletteData = memorySpace.ReadBytes(Addresses.ROM.BattleSpritePaletteData);
        var smallStencils = memorySpace.ReadBytes(Addresses.ROM.BattleSpriteSmallStencilData);
        var largeStencils = memorySpace.ReadBytes(Addresses.ROM.BattleSpriteLargeStencilData);
        var tileData = memorySpace.ReadBytes(Addresses.ROM.BattleSpriteTileData);
        foreach (Monster monster in Enum.GetValues(typeof(Monster)))
        {
            var template = templates[(int)monster];
            var stencils = template.LargeStencil ? largeStencils : smallStencils;

            if (template.StencilOffset < stencils.Length && template.StencilOffset + template.StencilSize < stencils.Length)
                _monsterSprites[monster] = new CombatSprite(template, tileData, paletteData, template.LargeStencil ? largeStencils : smallStencils);
        }

        foreach (Boss boss in Enum.GetValues(typeof(Boss)))
        {
            var template = templates[(int)boss];
            _bossSprites[boss] = new CombatSprite(template, tileData, paletteData, template.LargeStencil ? largeStencils : smallStencils);
        }

        foreach (Esper esper in Enum.GetValues(typeof(Esper)))
        {
            var template = templates[(int)esper];
            _esperSprites[esper] = new CombatSprite(template, tileData, paletteData, template.LargeStencil ? largeStencils : smallStencils);
        }
    }

    public ISprite Get(Monster monster) => _monsterSprites[monster];
    public ISprite Get(Boss boss) => _bossSprites[boss];
    public ISprite Get(Esper epser) => _esperSprites[epser];

    public void Dispose()
    {
        foreach (var value in _monsterSprites.Values) value.Dispose();
        _monsterSprites.Clear();
        foreach (var value in _bossSprites.Values) value.Dispose();
        _bossSprites.Clear();
        foreach (var value in _esperSprites.Values) value.Dispose();
        _esperSprites.Clear();
    }
}
