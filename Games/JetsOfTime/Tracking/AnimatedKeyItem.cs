using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Timing;
using System;
using System.Drawing;
using System.Windows.Forms;
using HorizontalAlignment = FF.Rando.Companion.Rendering.HorizontalAlignment;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class AnimatedKeyItem : KeyItemBase
{
    private readonly SpriteCollection _spriteCollection;
    private int _index = 0;
    private readonly Timer _timer;
    private readonly RotateFlipType? _rotateFlipType;

    public AnimatedKeyItem(ITimer timer, KeyItemType type, SpriteCollection collection, int interval = 1000, RotateFlipType? rotateFlipType = null)
        :base(timer, type)
    {
        _spriteCollection = collection;
        _timer = new Timer { Interval = interval };
        _timer.Tick += NextFrame;
        _rotateFlipType = rotateFlipType;
        SetImage();
    }

    private void NextFrame(object sender, EventArgs e)
    {
        _index++;
        _index %= _spriteCollection.Count;
        SetImage();
    }

    protected override void SetImage()
    {
        if (IsFound && !_timer.Enabled)
        {
            _index = 0;
            _timer.Start();
        }
        else if(!IsFound && _timer.Enabled)
        {
            _index = 0;
            _timer.Stop();
        }

        var baseImage = _spriteCollection.Get(_index)?.Pad(new Size(32, 32), HorizontalAlignment.Center, VerticalAlignment.Center);

        if (_rotateFlipType.HasValue && baseImage != null)
        {
            baseImage = baseImage.RotateFlip(_rotateFlipType.Value);
        }

        var image = baseImage?.Render(!IsFound);
        if (image != null)
            Image = image;
    }
}
