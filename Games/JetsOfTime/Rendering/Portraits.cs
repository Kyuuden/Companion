using FF.Rando.Companion.Extensions;
using System.Collections.Generic;
using System;
using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using KGySoft.CoreLibraries;
using FF.Rando.Companion.MemoryManagement;

namespace FF.Rando.Companion.Games.JetsOfTime.Rendering;

internal class Portraits
{
    private readonly Dictionary<PortraitType, ISprite?> _sprites = [];

    public Portraits(IMemorySpace rom)
    {
        var tileData = rom.ReadBytes(Data.Addresses.ROM.PortraitTiles).AsSpan();
        var palettes = rom.ReadBytes(Data.Addresses.ROM.PortraitPalettes).AsSpan();

        var epochTileData = rom.ReadBytes(Data.Addresses.ROM.EpochPortraitTiles);
        var epochPalette = rom.ReadBytes(Data.Addresses.ROM.EpochPortraitPalette);

        foreach (PortraitType character in Enum.GetValues(typeof(PortraitType)))
        {
            var sprite = character switch
            {
                PortraitType.Epoch => Generate(epochTileData, epochPalette),
                _ => Generate(
                    tileData.Slice(0x480 * (int)character, 0x480), 
                    palettes.Slice(0x20 * PaletteIndex(character), 0x20)),
            };

            _sprites[character] = sprite;
        }
    }

    private static int PaletteIndex(PortraitType portraitType)
    {
        return portraitType switch
        {
            PortraitType.Chrono => 3,
            PortraitType.Lucca => 5,
            PortraitType.Marle => 6,
            PortraitType.Frog => 2,
            PortraitType.Robo => 1,
            PortraitType.Ayla => 4,
            PortraitType.Magus => 0,
            PortraitType.Epoch => throw new NotImplementedException(),
            _ => 0,
        };
    }

    public ISprite? Get(PortraitType portraitType)
        => _sprites.GetValueOrDefault(portraitType);

    private static ISprite Generate(ReadOnlySpan<byte> tiles, ReadOnlySpan<byte> paletteData)
    {
        List<byte[,]> _tiles = [];
        for (var i = 0; i < 36; i++)
        {
            _tiles.Add(tiles.Slice(0x20 * i, 0x20).DecodeTile(4));
        }

        var palette = paletteData.DecodePalette();

        var bmpData = BitmapDataFactory.CreateBitmapData(new System.Drawing.Size(48, 48), KnownPixelFormat.Format8bppIndexed, palette);

        for (var i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].DrawInto(bmpData, i % 6 * 8, i / 6 * 8);
        }

        return new BasicSprite(bmpData);
    }
}

