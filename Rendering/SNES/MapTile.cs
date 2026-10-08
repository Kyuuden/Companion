using System.Text;

namespace FF.Rando.Companion.Rendering.SNES;
public class MapTile
{
    public int Index { get; }
    public int Palette { get; }
    public bool FlipHoriztonal { get; }
    public bool FlipVertical { get; }
    public bool Priority { get; }

    public MapTile(byte low, byte high)
       : this((ushort)((high << 8) | low))
    {
    }

    public MapTile(ushort value)
    {
        Index = value & 0x3ff;
        Palette = ((value & 0x1c00) >> 10);
        Priority = (value & 0x2000) != 0;
        FlipHoriztonal = (value & 0x4000) != 0;
        FlipVertical = (value & 0x8000) != 0;
    }

    public override string ToString()
    {
        var sb = new StringBuilder($"T={Index:X};P={Palette:X}");
        if (Priority) sb.Append(";Prio");
        if (FlipHoriztonal) sb.Append(";FlipH");
        if (FlipVertical) sb.Append(";FlipV");
        return sb.ToString();
    }
}
