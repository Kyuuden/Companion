using FF.Rando.Companion.Rendering;
using KGySoft.Drawing.Imaging;
using System;
using System.ComponentModel;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class ProgressiveKeyItem : KeyItemBase
{
    private readonly ISprite[] _sprites;
    private readonly string[] _descriptions;
    private byte _progress;

    public ProgressiveKeyItem(Container container, KeyItemType type, ISprite[] sprites, string[] descriptions)
        : base(container, type)
    {
        if (sprites.Length == 0) throw new ArgumentException();

        var tmp = sprites[0].RenderData();
        var greyscale = tmp.ToGrayscale();
        greyscale.AdjustBrightness(-0.66f);

        _sprites = [new BasicSprite(greyscale), ..sprites];
        _descriptions = descriptions;
        SetImage();
    }

    public byte Progress
    {
        get => _progress;
        set 
        {
            if (_progress >= _sprites.Length)
                throw new InvalidOperationException();

            if (_progress == value) 
                return;

            _progress = value;

            IsFound = _progress > 0;
            NotifyPropertyChanged();
            NotifyPropertyChanged(AltText);
            SetImage();
        }
    }

    public override string AltText => _progress == 0 ? _descriptions[0] : _descriptions[_progress - 1];

    protected override void SetImage()
    {
        var tmp = _sprites[_progress]?.Render();
        if (tmp != null)
            Image = tmp;
    }
}

