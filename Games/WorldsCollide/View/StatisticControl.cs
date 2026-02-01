using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public abstract class StatisticControl<T> : StatisticControl<T, Seed> where T : struct
{
    public Statistic Statistic { get; }

    protected abstract string GetStatText();

    internal StatisticControl(Seed seed, PanelSettings settings, Statistic statistic)
        : base(seed, settings, new Size(36, 24))
    {
        BackColor = Color.Transparent;
        Statistic = statistic;
        UpdateImage();
    }

    protected override void Seed_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Seed_PropertyChanged(sender, e);
        switch (e.PropertyName)
        {
            case nameof(Seed.SpriteSet):
            case nameof(Seed.PrimaryFontColor):
                UpdateImage();
                break;
        }
    }

    protected override Image Render()
    {
        var icon = Game.SpriteSet.Get(Statistic)!.RenderData();
        var text = Game.Font.RenderText(GetStatText());
        var combined = PaletteExtensions.Combine(icon.Palette!, text.Palette!);
        var data = BitmapDataFactory.CreateBitmapData(MinimumSize, KnownPixelFormat.Format8bppIndexed, combined);

        var destinationRect = new Rectangle(
            0, (MinimumSize.Height - icon.Height) / 2,
            icon.Width, icon.Height);

        icon.DrawInto(data, destinationRect, KGySoft.Drawing.ScalingMode.NearestNeighbor);

        destinationRect = new Rectangle(
            MinimumSize.Width - text.Width, (MinimumSize.Height - text.Height) / 2,
            text.Width, text.Height);

        text.DrawInto(data, destinationRect, KGySoft.Drawing.ScalingMode.NearestNeighbor);
        return data.ToBitmap();
    }
}
