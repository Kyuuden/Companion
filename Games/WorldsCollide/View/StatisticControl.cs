using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

internal class StatisticControl : StatisticControl<int, Seed>
{
    private readonly ToolTip _toolTip;
    public Statistic Statistic { get; }

    protected override int GetStat() => Game.State.GetStatistic(Statistic);

    protected override string PropertyName => Statistic.ToString();

    internal StatisticControl(Seed seed, IPanelSettings settings, Statistic statistic)
        : base(seed, settings, new Size(40, 40))
    {
        BackColor = Color.Transparent;
        Statistic = statistic;
        BackgroundImageLayout = ImageLayout.Zoom;
        UpdateBaseImage();
        UpdateStat();
        seed.State.PropertyChanged += Seed_PropertyChanged;
        _toolTip = new ToolTip { ShowAlways = true };
        _toolTip.SetToolTip(this, statistic.GetDescription());
    }

    protected override void Seed_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Seed_PropertyChanged(sender, e);
        switch (e.PropertyName)
        {
            case nameof(Seed.SpriteSet):
                UpdateBaseImage();
                break;
            case nameof(Seed.PrimaryFontColor):
                UpdateImage();
                break;
        }
    }

    protected override void UpdateImage()
    {
        Image?.Dispose();
        Image = null;
        Image = Render();
    }

    private void UpdateBaseImage()
    {
        BackgroundImage = Game.SpriteSet?.Get(Statistic)?.Render();
    }

    protected override Image Render()
    {
        var text = Game.Font.RenderText($"{Stat}", TextMode.Normal);
        var data = BitmapDataFactory.CreateBitmapData(OriginalSize);
        var dest = new Point(OriginalSize.Width - text.Width, (OriginalSize.Height - text.Height) / 2);
        text.DrawInto(data, dest);
        return data.ToBitmap();
    }
}
