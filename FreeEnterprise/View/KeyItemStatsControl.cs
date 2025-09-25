using System.Linq;
using FF.Rando.Companion.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class KeyItemStatsControl : StatisticControl<int>
{
    public KeyItemStatsControl(ISeed seed, PanelSettings settings) : base(seed, settings)
    {
    }

    protected override string PropertyName => nameof(ISeed.KeyItems);

    protected override IReadableBitmapData GetIcon() => Game.Sprites.GetNpcImage(new int[2, 2] { { 480, 481 }, { 482, 483 } }, 8);

    protected override int GetStat() => Game.KeyItems.Count(ki => ki.IsFound && ki.IsTrackable);

    protected override string GetStatText() => $"{Stat,2}/{Game.KeyItems.Count(ki => ki.IsTrackable)}";
}