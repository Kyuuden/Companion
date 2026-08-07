using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Timing;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class KeyItem : KeyItemBase
{
    private readonly ISprite? _sprite;

    public KeyItem(ITimer timer, KeyItemType type, ISprite? sprite)
        :base(timer, type)
    {
        _sprite = sprite;
        SetImage();
    }

    protected override void SetImage()
    {
        var tmp = _sprite?.Render(!IsFound);
        if (tmp != null)
            Image = tmp;
    }
}

