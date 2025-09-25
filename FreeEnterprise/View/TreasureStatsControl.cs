using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class TreasureStatsControl : StatisticControl<int>
{
    public TreasureStatsControl(ISeed seed, PanelSettings settings) : base(seed, settings)
    {
    }

    protected override string PropertyName => nameof(ISeed.TreasureCount);

    protected override IReadableBitmapData GetIcon() => Game.Sprites.GetChestImage(RomData.Chest.Open);

    protected override int GetStat() => Game.TreasureCount;

    protected override string GetStatText() => $"{Stat,4}";
}