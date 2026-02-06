using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Drawing;

namespace FF.Rando.Companion.Games.WorldsCollide.View;
internal abstract class ScrollablePanel<TSettings> : ScrollablePanel<Seed,  TSettings> where TSettings : PanelSettings
{
    protected override IReadableBitmapData GenerateArrow(Arrow direction)
    {
        return BitmapDataFactory.CreateBitmapData(1,1);
    }

    protected override void HandleArrows(bool showUp, bool showDown, int currentLine, int maxLines, IReadWriteBitmapData data)
    {
        if (Game == null) return;

        IReadableBitmapData icon;
        if (showUp && showDown)
            icon = Game.Font.GetIcon(RomData.Icons.BothArrow);
        else if (showUp)
            icon = Game.Font.GetIcon(RomData.Icons.UpArrow);
        else if (showDown)
            icon = Game.Font.GetIcon(RomData.Icons.DownArrow);
        else
            return;

        var displayedLines = data.Height / (8 + SpaceBetweenLines);
        var maxCurrentLine = maxLines - displayedLines;

        var scrollPercent = Math.Min(1.0, (double)currentLine / maxCurrentLine);
        var ypos = (data.Height - icon.Height) * scrollPercent;

        icon.DrawInto(data, new Point(data.Width - icon.Width, (int)ypos));
    }

    protected override Bitmap? GenerateBackgroundImage(Size unscaledSize)
    {
        if (Game == null) return null;

        return Game.Backgrounds.Render(
            Game.SelectedBackground,
            unscaledSize,
            Game.Settings.BorderSettings.BordersEnabled,
            ((WorldsCollideBorderSettings)Game.Settings.BorderSettings).BackgroundsEnabled);
    }

    protected override IReadableBitmapData GeneragePageCounter(int current, int total)
    {
        return Game?.Font.RenderText($"{current}/{total}", TextMode.Normal)!;
    }
}
