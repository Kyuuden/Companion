using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.View;

internal abstract class StatisticControl<T> : StatisticControl<T, Seed> where T : struct
{
    protected abstract string GetStatText();

    protected abstract string Description { get; }

    private readonly ToolTip _toolTip = new() {  ShowAlways = true };

    internal StatisticControl(Seed seed, PanelSettings settings)
        : this(seed, settings, new Size(36, 24))
    { }

    internal StatisticControl(Seed seed, PanelSettings settings, Size minimumSize)
        : base(seed, settings, minimumSize)
    {
        BackColor = Color.Transparent;
        UpdateImage();
        _toolTip.SetToolTip(this, Description);
        seed.State.PropertyChanged += Seed_PropertyChanged;
    }

    protected abstract IReadableBitmapData Icon { get; }

    protected override Image Render()
    {
        var icon = Icon;
        var text = Game.Font.RenderText(GetStatText(), TextMode.Disabled);
        var data = BitmapDataFactory.CreateBitmapData(MinimumSize);

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
