using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using FF.Rando.Companion.WorldsCollide.Settings;
using KGySoft.Drawing.Imaging;
using System.Drawing;

namespace FF.Rando.Companion.WorldsCollide.View;

public abstract class StatisticControl<T> : StatisticControl<T, Seed> where T : struct
{
    public Statistic Statistic { get; }

    protected abstract string GetStatText();

    internal StatisticControl(Seed seed, PanelSettings settings, Statistic statistic)
        : base(seed, settings)
    {
        MinimumSize = new Size(112, 32);
        Game.Settings.PropertyChanged += Settings_PropertyChanged;
        Statistic = statistic;
    }

    private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(WorldsCollideSettings.Sprites))
            UpdateImage();
    }

    protected override Image Render()
    {
        var icon = Game.Settings.SpriteSet.Get(Game.Sprites, Statistic).RenderData();
        var text = Game.Font.RenderText(GetStatText());
        var combined = PaletteExtensions.Combine(icon.Palette!, text.Palette!);
        var data = BitmapDataFactory.CreateBitmapData(new Size(112, 32), KnownPixelFormat.Format8bppIndexed, combined);

        icon.DrawInto(data, new Rectangle(0, 0, 32, 32), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        text.DrawInto(data, new Rectangle(32, 8, 80, 16), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        return data.ToBitmap();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Game != null)
                Game.Settings.PropertyChanged -= Settings_PropertyChanged;
        }

        base.Dispose(disposing);
    }
}
