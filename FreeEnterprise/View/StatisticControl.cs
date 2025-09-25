using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.FreeEnterprise.View;

public abstract class StatisticControl<T> : StatisticControl<T, ISeed> where T : struct
{
    protected abstract string GetStatText();
    protected abstract IReadableBitmapData GetIcon();


    public StatisticControl(ISeed seed, PanelSettings settings)
        :base(seed, settings)
    {
        MinimumSize = new Size(112, 32);
    }

    protected override Image Render()
    {
        var icon = GetIcon();
        var text = Game.Font.RenderText(GetStatText(), RomData.TextMode.Normal);
        var combined = PaletteExtensions.Combine(icon.Palette!, text.Palette!);
        var data = BitmapDataFactory.CreateBitmapData(new Size(112, 32), KnownPixelFormat.Format8bppIndexed, combined);

        icon.DrawInto(data, new Rectangle(0, 0, 32, 32), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        text.DrawInto(data, new Rectangle(32, 8, 80, 16), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        return data.ToBitmap();
    }
}
