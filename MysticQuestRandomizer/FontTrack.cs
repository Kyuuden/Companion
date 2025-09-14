using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.MysticQuestRandomizer;

public class FontTrack : IImageTracker
{
    public FontTrack(Bitmap b)
    {
        Image = b;
    }

    public Bitmap Image { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
}
