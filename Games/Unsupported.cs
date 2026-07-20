using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games;
internal class Unsupported : IGame
{
    public required ISettings RootSettings { get; init; }
    public required string Hash { get; init; }

    public Bitmap Icon => null!;

    public Color BackgroundColor => Color.Black;

    public bool RequiresMemoryEvents => false;

    public IEmulationContainer Container => null!;

    public GameSettings Settings => null!;

#pragma warning disable CS0067
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067

    public Control CreateControls()
    {
        return new UnsupportedGameView(RootSettings);
    }

    public void Dispose()
    {
    }

    public void OnNewFrame()
    {
    }
}
