using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Games.WorldsCollide.Tracking;
using FF.Rando.Companion.View;
using System.Drawing;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public partial class CharacterControl : ImageControl<Seed, Character>
{
    private readonly Size _imageSize = new Size(40, 40);

    public CharacterControl(Seed seed, CharacterSettings settings, Character character) : base(seed, settings, character)
    {
        BackColor = Color.Transparent;
    }

    protected override Size ImageSize => _imageSize;
}
