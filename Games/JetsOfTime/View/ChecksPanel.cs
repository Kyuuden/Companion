using FF.Rando.Companion.Games.JetsOfTime.Settings;
using KGySoft.Drawing.Imaging;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.JetsOfTime.View;
internal class ChecksPanel : ScrollablePanel<ChecksSettings>
{
    protected override bool CombinePages => Settings?.CombineEras == true;

    protected override int ScrollLines => Settings?.ScrollLines ?? 2;

    protected override IEnumerable<List<IReadableBitmapData>> GeneratePageBitmaps()
    {
        return [];
    }
}
