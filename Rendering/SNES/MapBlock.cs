using KGySoft.CoreLibraries;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Rendering.SNES;

public class MapBlock
{
    private readonly MapTile[] _mapTiles = new MapTile[4];

    public bool FlipHorizontal { get; init; }
    public bool FlipVertical { get; init; }

    public MapTile UpperLeft
    {
        get => _mapTiles[0];
        set => _mapTiles[0] = value;
    }

    public MapTile UpperRight
    {
        get => _mapTiles[1];
        set => _mapTiles[1] = value;
    }

    public MapTile LowerLeft
    {
        get => _mapTiles[2];
        set => _mapTiles[2] = value;
    }

    public MapTile LowerRight
    {
        get => _mapTiles[3];
        set => _mapTiles[3] = value;
    }

    public bool Complete => _mapTiles.All(t => t != null);

    public IEnumerable<MapTile> Tiles => _mapTiles.Where(t => t != null);

    public void Add(MapTile tile)
    {
        var index = _mapTiles.IndexOf((MapTile?)null);
        if (index == -1)
            throw new InvalidOperationException("Block is Full");

        _mapTiles[index] = tile;
    }

    public MapBlock()
    {

    }

    public MapBlock(IEnumerable<MapTile> tiles)
    {
        foreach (var t in tiles)
            Add(t);
    }
}
