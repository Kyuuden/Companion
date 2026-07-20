using FF.Rando.Companion.Rendering;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class KeyItem : KeyItemBase
{
    private readonly ISprite? _sprite;

    public KeyItem(Container container, KeyItemType type, ISprite? sprite)
        :base(container, type)
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

