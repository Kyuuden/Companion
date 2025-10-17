using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class LocationsPanel : ScrollablePanel<LocationsSettings>
{
    protected override bool CombinePages => Settings?.CombineLocationTypes == true;

    protected override int ScrollLines => Settings?.ScrollLines ?? 2;

    protected override IEnumerable<List<IReadableBitmapData>> GeneratePageBitmaps()
    {
        if (Game == null || Settings == null)
            yield break;

        if (Settings.ShowKeyItemChecks && Settings.ShowCharacterChecks && Game.AvailableLocations.Any(l=>l.IsKeyItem && l.IsCharacter))
            yield return GenerateData("KEY ITEM / CHARACTER CHECKS", Game.AvailableLocations.Where(l => l.IsKeyItem && l.IsCharacter).OrderBy(l => l.World).ThenBy(l => l.Description)).ToList();

        if (Settings.ShowKeyItemChecks && Game.AvailableLocations.Any(l => l.IsKeyItem && !l.IsCharacter))
            yield return GenerateData("KEY ITEM CHECKS", Game.AvailableLocations.Where(l => l.IsKeyItem && !l.IsCharacter).OrderBy(l => l.World).ThenBy(l => l.Description)).ToList();

        if (Settings.ShowCharacterChecks && Game.AvailableLocations.Any(l => l.IsCharacter && !l.IsKeyItem))
            yield return GenerateData("CHARACTER CHECKS", Game.AvailableLocations.Where(l => l.IsCharacter && !l.IsKeyItem).OrderBy(l => l.World).ThenBy(l => l.Description)).ToList();

        if (Settings.ShowShopChecks && Game.AvailableLocations.Any(l => l.IsShop))
            yield return GenerateData("SHOP CHECKS", Game.AvailableLocations.Where(l => l.IsShop).OrderBy(l => l.World).ThenBy(l => l.Description)).ToList();
    }

    protected override void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.PropertyChanged(sender, e);

        if (e.PropertyName == nameof(ISeed.AvailableLocations))
            RegerateData();
    }

    private IEnumerable<IReadableBitmapData> GenerateData(string header, IEnumerable<ILocation> locations)
    {
        if (Game == null || Settings == null)
            yield break;

        var unscaledSize = Settings.Unscale(Size);
        var charWidth = (unscaledSize.Width / 8) - 2;
        if (charWidth < 4)
            yield break;

        yield return Game.Font.RenderText(header.ToUpper(), TextMode.Normal);

        foreach (var loc in locations)
        {
            var textmode = loc.World switch
            {
                Shared.World.Main => TextMode.Normal,
                Shared.World.Underground => TextMode.Special,
                Shared.World.Moon => TextMode.Highlighted,
                _ => TextMode.Disabled
            };

            var marker = Game.Font.RenderText(" • ", textmode);
            var locText = Game.Font.RenderText(loc.Description, textmode, charWidth - 3);
            var locData = BitmapDataFactory.CreateBitmapData(new Size(marker.Width + locText.Width, Math.Max(marker.Height, locText.Height)), KnownPixelFormat.Format8bppIndexed, locText.Palette);

            marker.CopyTo(locData);
            locText.CopyTo(locData, new Point(marker.Width, 0));
            marker.Dispose();
            locText.Dispose();

            yield return locData;
        }
    }
}
