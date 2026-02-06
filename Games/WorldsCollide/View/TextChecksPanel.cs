using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

internal class TextChecksPanel : ScrollablePanel<TextChecksSettings>
{
    protected override bool CombinePages => false;

    protected override int ScrollLines => 3;

    protected override void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.PropertyChanged(sender, e);

        if (e.PropertyName == nameof(Seed.Checks) || e.PropertyName == nameof(Seed.DragonLocations))
            RegerateData();
    }

    protected override IEnumerable<List<IReadableBitmapData>> GeneratePageBitmaps()
    {
        if (Game == null || Settings == null)
            yield break;

        var unscaledSize = EffectiveSize.Unscale(Settings.ScaleFactor);

        var characterWidth = unscaledSize.Width / 8;
        var checkDictionary = new Dictionary<string, List<(string, bool)>>();
        var openChecks = new List<(string, bool)>();

        foreach (var check in Game.Checks.Concat(Game.DragonLocations))
        {
            if (!check.IsAvailable)
                continue;

            if (check.CharacterGate.HasValue)
            {
                var character = Game.Descriptors.GetDescription(check.CharacterGate.Value);
                if (!checkDictionary.TryGetValue(character, out var list))
                    checkDictionary[character] = [];

                checkDictionary[character].Add((Game.Descriptors.GetDescription(check.Event), check.IsCompleted));
            }
            else
                openChecks.Add((Game.Descriptors.GetDescription(check.Event), check.IsCompleted));
        }

        var lines = new List<(string, TextMode)>();
        foreach (var characterChecks in checkDictionary.OrderBy(kvp => kvp.Key))
        {
            lines.Add((characterChecks.Key, TextMode.Special));
            foreach (var check in characterChecks.Value.OrderBy(s => s))
            {
                lines.Add((check.Item1, check.Item2 ? TextMode.Disabled : TextMode.Normal));
            }
            lines.Add((" ", TextMode.Normal));
        }

        if (openChecks.Any())
        {
            lines.Add(("Open", TextMode.Special));
            foreach (var check in openChecks.OrderBy(s => s))
            {
                lines.Add((check.Item1, check.Item2 ? TextMode.Disabled : TextMode.Normal));
            }
        }

        var maxwidth = lines.Count == 0 ? 0 : lines.Select(l => l.Item1.Length).Max();

        if (maxwidth * 2 < characterWidth)
        {
            var data = new List<IReadWriteBitmapData>();
            // 2 Columns

            var destinationX = 0;
            var destinationLine = 0;

            foreach (var (text, mode) in lines)
            {
                if (destinationLine > (lines.Count / 2) && mode == TextMode.Special)
                {
                    destinationLine = 0;
                    destinationX = ((characterWidth / 2) + 1) * 8;
                }

                while (data.Count <= destinationLine)
                    data.Add(BitmapDataFactory.CreateBitmapData(new Size(unscaledSize.Width, 8)));

                Game.Font.RenderText(data[destinationLine++], new Point(destinationX, 0), text, mode);
            }

            yield return data.OfType<IReadableBitmapData>().ToList();
        }
        else
        {
            var data = new List<IReadableBitmapData>();
            foreach (var line in lines)
            {
                data.Add(Game.Font.RenderText(line.Item1, line.Item2));
            }
            yield return data;
        }
    }
}