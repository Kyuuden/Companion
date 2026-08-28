using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.Games.JetsOfTime.Rendering;
internal interface IRenderInstruction
{

}

internal class SetBlock : IRenderInstruction
{
    public required int Layer { get; init; }
    public required int X { get; init; }
    public required int Y { get; init; }
    public required int BlockId { get; init; }
}

internal class CopyBlocks : IRenderInstruction
{
    public required int SourceLayer { get; init; }
    public required int DestinationLayer { get; init; }
    public required int SourceX { get; init; }
    public required int DestinationX { get; init; }
    public required int SourceY { get; init; }
    public required int DestinationY { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
}