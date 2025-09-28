using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise.View;
public partial class BossControl : ImageControl<ISeed, IBoss>
{
    private readonly FreeEnterpriseToolTip _toolTip;

    public BossControl(ISeed seed, BossSettings settings, IBoss boss)
        :base(seed, settings, boss)
    {
        _toolTip = new FreeEnterpriseToolTip(settings);
        _toolTip.SetToolTip(this, boss.Name.Trim());
        GenerateToolTip();
    }

    protected override void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Value_PropertyChanged(sender, e);
        switch (e.PropertyName)
        {
            case (nameof(IBoss.Encounters)):
            case (nameof(ISeed.BackgroundColor)):
                GenerateToolTip();
                break;
        }
    }

    private void GenerateToolTip()
    {
        var description = Game.Font.RenderText(Value.Name, TextMode.Normal, 28);
        var encounters = new List<IReadableBitmapData>();

        if (Value.Encounters.Any())
        {
            foreach (var encounter in Value.Encounters)
            {
                encounters.Add(Game.Font.RenderText($"Found {(encounter.IsDefeated ? "and defeated " : "")} in {encounter.Location}", TextMode.Special, 36));
            }
        }
        else
        {
            encounters.Add(Game.Font.RenderText("(not yet found)", TextMode.Disabled, 28));
        }

        var toolTipImage = BitmapDataFactory.CreateBitmapData(new Size(304, Math.Max(64, encounters.Sum(e=> e.Height + 8) + 32)));
        toolTipImage.FillRectangle(Game.BackgroundColor, new Rectangle(default, toolTipImage.Size));

        var border = Game.Font.RenderBox(38, toolTipImage.Height / 8);
        border.DrawInto(toolTipImage);

        description.DrawInto(toolTipImage, new Point(8, 8));
        var y = 24;
        foreach (var e in encounters)
        {
            e.DrawInto(toolTipImage, new Point(8,y));
            y += e.Height + 8;
            e.Dispose();
        }

        _toolTip.Image = toolTipImage.ToBitmap();

        description.Dispose();
        border?.Dispose();
        toolTipImage.Dispose();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _toolTip.Dispose();
        }
        base.Dispose(disposing);
    }
}
