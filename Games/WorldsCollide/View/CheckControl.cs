using FF.Rando.Companion.Games.WorldsCollide.Tracking;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

internal partial class CheckControl : PictureBox
{
    private readonly ToolTip _toolTip;
    private readonly ICheck? _check;
    private readonly Seed _seed;
    private ISprite? _sprite;
    private int _lastProgress;

    private readonly Size _defaultSize = new(40, 40);

    public CheckControl(ICheck? item, Seed seed)
    {
        DoubleBuffered = true;
        Size = _defaultSize;
        SizeMode = PictureBoxSizeMode.Zoom;
        BackgroundImageLayout = ImageLayout.Zoom;
        BackColor = Color.Transparent;

        _toolTip = new ToolTip { ShowAlways = true };

        if (item != null)
        {
            _check = item;
            _check.Updated += CheckUpdated;
            _toolTip.SetToolTip(this, item.Description);
        }

        _seed = seed;
        _seed.PropertyChanged += SeedUpdated;

        _sprite = GetSprite();

        CheckUpdated();
    }

    private ISprite? GetSprite()
    {
        return _check switch
        {
            ProgressiveCheck pc => _seed.SpriteSet.Get(pc.CompletedStages.Any() ? pc.CompletedStages.Last() : pc.Stages.First()),
            IEventCheck bc => _seed.SpriteSet.Get(bc.Event),
            _ => null
        };
    }

    private bool IsIncomplete()
    {
        return _check switch
        {
            ProgressiveCheck pc => pc.CompletedStages.Count == 0,
            ICheck bc => !bc.IsComplete,
            _ => true
        };
    }

    private void SeedUpdated(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Seed.SpriteSet))
        {
            _sprite = GetSprite();
            CheckUpdated();
        }
    }

    private void CheckUpdated()
    {
        BackgroundImage = _sprite?.Render(IsIncomplete());

        if (_check is ProgressiveCheck progressiveCheck && _lastProgress != progressiveCheck.CompletedStages.Count)
        {
            Image?.Dispose();
            if (progressiveCheck.CompletedStages.Count > 0)
            {
                _lastProgress = progressiveCheck.CompletedStages.Count;
                var stage = BitmapDataFactory.CreateBitmapData(_defaultSize);
                _seed.Font.RenderText(stage, new Point(_defaultSize.Width - 8, _defaultSize.Height - 8), (progressiveCheck.CompletedStages.Count).ToString(), TextMode.Normal);
                Image = stage.ToBitmap();
            }
        }
    }
}
