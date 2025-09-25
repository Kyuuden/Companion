using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing.Shapes;
using System;
using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.FreeEnterprise.View;
public partial class KeyItemControl : ImageControl<ISeed, IKeyItem>
{
    private readonly FreeEnterpriseToolTip _toolTip;

    public KeyItemControl(ISeed seed, KeyItemSettings setting, IKeyItem keyItem)
        : base(seed, setting, keyItem)
    {
        _toolTip = new FreeEnterpriseToolTip(setting);
        _toolTip.SetToolTip(this, keyItem.Name.Trim());
        GenerateToolTip();
    }

    protected override void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Value_PropertyChanged(sender, e);
        switch (e.PropertyName)
        {
            case (nameof(IKeyItem.IsFound)):
            case (nameof(IKeyItem.IsUsed)):
            case (nameof(ISeed.BackgroundColor)):
                GenerateToolTip();
                break;
        }
    }

    private void GenerateToolTip()
    {
        var description = Game.Font.RenderText(Value.Description, TextMode.Normal, 28);
        IReadableBitmapData? found;
        IReadableBitmapData? used = null;
        if (Value.IsFound)
        {
            found = Value.WhenFound.HasValue && Value.WhenFound > TimeSpan.Zero
                ? Game.Font.RenderText($"Received at {Value.WhenFound.Value:hh':'mm':'ss'.'ff} from {Value.WhereFound ?? ""}", TextMode.Special, 28)
                : Game.Font.RenderText($"Received from:\n{Value.WhereFound ?? ""}", TextMode.Special, 28);
            if (!Value.IsUsed)
                used = Game.Font.RenderText("(unused)", TextMode.Highlighted, 28);
            else if (Value.WhenUsed.HasValue && Value.WhenUsed > TimeSpan.Zero)
                used = Game.Font.RenderText($"Used at {Value.WhenUsed.Value:hh':'mm':'ss'.'ff}", TextMode.Special, 28);
        }
        else
        {
            found = Game.Font.RenderText("(not yet found)", TextMode.Disabled, 28);
        }

        var toolTipImage = BitmapDataFactory.CreateBitmapData(new Size(240, Value.IsUsed && Value.WhenUsed > TimeSpan.Zero ? 80 : 64));
        toolTipImage.FillRectangle(Game.BackgroundColor, new Rectangle(default, toolTipImage.Size));

        var border = Value.IsUsed && Value.WhenUsed > TimeSpan.Zero
            ? Game.Font.RenderBox(30, 10)
            : Game.Font.RenderBox(30, 8);
        border.DrawInto(toolTipImage);

        description.DrawInto(toolTipImage, new Point(8, 8));
        found?.DrawInto(toolTipImage, new Point(8, 40));
        used?.DrawInto(toolTipImage,
            Value.IsUsed && Value.WhenUsed > TimeSpan.Zero
                ? new Point(8, 48 + found?.Height ?? 0)
                : new Point(168, 32));

        _toolTip.Image = toolTipImage.ToBitmap();

        description.Dispose();
        found?.Dispose();
        used?.Dispose();
        border?.Dispose();
        toolTipImage.Dispose();
    }
}
