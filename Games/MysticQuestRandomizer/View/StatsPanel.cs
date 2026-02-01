using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.MysticQuestRandomizer;
using FF.Rando.Companion.Games.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.View;

internal class StatsPanel : FlowPanel<StatsSettings>
{
    public StatsPanel()
        : base()
    {
        SpacingMode = SpacingMode.Columns;
        WrapContents = false;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Bottom;

    protected override Control[] GenerateControls(Seed seed)
        =>
        [
            new SkyShardsStats(seed, Settings!),
            //new BossStatsControl(seed, Settings!),
            //new TreasureStatsControl(seed, Settings!),
            //new XpStatsControl(seed, Settings!)
        ];
}

public abstract class TextStatisticControl<T> : StatisticControl<T, Seed> where T : struct
{
    protected abstract string GetStatText();
    protected abstract IReadableBitmapData GetIcon();


    public TextStatisticControl(Seed seed, PanelSettings settings)
        : base(seed, settings, new Size(56, 16))
    {
    }

    protected override Image Render()
    {
        var icon = GetIcon();
        var text = Game.Font.RenderText(GetStatText());
        var combined = PaletteExtensions.Combine(icon.Palette!, text.Palette!);
        var data = BitmapDataFactory.CreateBitmapData(new Size(56, 16), KnownPixelFormat.Format8bppIndexed, combined);

        icon.DrawInto(data, new Rectangle(0, 0, 16, 16), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        text.DrawInto(data, new Rectangle(16, 4, 40, 8), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        return data.ToBitmap();
    }
}

public class SkyShardsStats(Seed seed, PanelSettings settings) : TextStatisticControl<int>(seed, settings)
{
    protected override string PropertyName => nameof(Seed.CollectedSkyFragments);

    protected override IReadableBitmapData GetIcon() => Game.Sprites.GetKeyItemData(KeyItemType.SkyCoin);

    protected override int GetStat() => Game.CollectedSkyFragments;

    protected override string GetStatText() => $"{Stat,2}/{Game.RequiredSkyFragmentCount,2}";

    protected override Image Render()
    {
        if (!Game.RequiredSkyFragmentCount.HasValue || GetStat() >= Game.RequiredSkyFragmentCount)
            return new Bitmap(56, 16);

        var icon = GetIcon();
        var text = Game.Font.RenderText(GetStatText());
        var combined = PaletteExtensions.Combine(icon.Palette!, text.Palette!);
        var data = BitmapDataFactory.CreateBitmapData(new Size(56, 16), KnownPixelFormat.Format8bppIndexed, combined);

        icon.DrawInto(data, new Rectangle(0, 0, 16, 16), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        text.DrawInto(data, new Rectangle(10, 5, 40, 8), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        return data.ToBitmap();
    }
}