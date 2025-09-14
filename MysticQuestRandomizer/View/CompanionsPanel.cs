using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;

internal class CompanionsPanel : ScrollablePanel<Seed, CompanionsSettings>
{
    Dictionary<Arrow, IReadableBitmapData> _arrows = [];


    public override void InitializeDataSources(Seed game, CompanionsSettings settings)
    {
        base.InitializeDataSources(game, settings);

        _arrows.Clear();
        _arrows[Arrow.Right] = Game!.Font.RenderText(">");
        _arrows[Arrow.Left] = Game!.Font.RenderText(">").CopyRotateFlip(RotateFlipType.RotateNoneFlipX);
        _arrows[Arrow.Up] = Game!.Font.RenderText(">").CopyRotateFlip(RotateFlipType.Rotate90FlipY);
        _arrows[Arrow.Down] = Game!.Font.RenderText(">").CopyRotateFlip(RotateFlipType.Rotate90FlipNone);
    }


    protected override bool CombinePages => Settings?.CombineCompanions == true;

    protected override int ScrollLines => 1;
    protected override void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.PropertyChanged(sender, e);

        if (e.PropertyName == nameof(CompanionsSettings.CombineCompanions))
            RegerateData();
    }

    protected override IReadableBitmapData GenerateArrow(Arrow direction)
    {
        return _arrows[direction];
    }

    protected override IReadWriteBitmapData GenerateBackgroundImage(Size unscaledSize)
    {
        return Game?.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8)!;
    }

    protected override IReadableBitmapData GeneragePageCounter(int current, int total)
    {
        return Game?.Font.RenderText(ToSmallNumbers($"{current}/{total}"))!;
    }

    protected override IEnumerable<List<IReadableBitmapData>> GeneratePageBitmaps()
    {
        if (Game == null || Settings == null)
            yield break;

        foreach (var companion in Game.Companions)
            yield return GenerateCompanion(companion).ToList();
    }

    private IEnumerable<IReadableBitmapData> GenerateCompanion(Companion companion)
    {
        if (Game == null || Settings == null)
            yield break;

        var unscaledSize = Settings.Unscale(Size);
        var charWidth = (unscaledSize.Width / 8) - 2;
        if (charWidth < 4)
            yield break;


        yield return Game.Font.RenderText(companion.Type.ToString().ToUpperInvariant());

        if (companion.Spells.Any())
        {
            foreach(var group in companion.Spells.Chunk(charWidth/3))
            {
                var list = group.ToList();
                var spells = list.Select(s=> Game.Sprites.GetSpellData(s.Type)).ToList();
                var palette = PaletteExtensions.Combine(spells.Select(s => s.Palette!).Append(Game.Font.Palette));
                
                var spellRow = BitmapDataFactory.CreateBitmapData(new Size(24 * list.Count() - 8, 24));

                for (int i = 0; i < list.Count; i++)
                {
                    spells[i].DrawInto(spellRow, new Point(i*24, 0));
                    Game.Font.RenderText(ToSmallNumbers(list[i].Level.ToString().PadLeft(2))).DrawInto(spellRow, new Point(i * 24, 16));
                }

                yield return spellRow;
            }
        }

        foreach (var quest in companion.Quests)
            yield return Game.Font.RenderText(quest, charWidth);

        yield return Game.Font.RenderText("");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

        base.Dispose(disposing);
    }

    private string ToSmallNumbers(string s) => string.Concat(s.Select(c => char.IsDigit(c) ? $"[{c}]" : c.ToString()));
}
