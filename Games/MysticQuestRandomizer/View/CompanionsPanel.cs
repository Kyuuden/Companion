using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.MysticQuestRandomizer;
using FF.Rando.Companion.Games.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.View;

internal class CompanionsPanel : ScrollablePanel<Seed, CompanionsSettings>
{
    private readonly Dictionary<Arrow, IReadableBitmapData> _arrows = [];
    private int _maxNameWidth;

    public override void InitializeDataSources(Seed game, CompanionsSettings settings)
    {
        base.InitializeDataSources(game, settings);

        _maxNameWidth = Game!.Companions.Any()
             ? Game!.Companions.Max(c => c.Type.ToString().Length + 1)
             : 0;

        _arrows.Clear();
        _arrows[Arrow.Right] = Game!.Font.RenderText(">");
        _arrows[Arrow.Left] = Game!.Font.RenderText(">").CopyRotateFlip(RotateFlipType.RotateNoneFlipX);
        _arrows[Arrow.Up] = Game!.Font.RenderText(">").CopyRotateFlip(RotateFlipType.Rotate90FlipY);
        _arrows[Arrow.Down] = Game!.Font.RenderText(">").CopyRotateFlip(RotateFlipType.Rotate90FlipNone);
    }

    protected override bool CombinePages => Settings?.CombineCompanions == true;

    protected override int ScrollLines => 1;

    protected override int SpaceBetweenLines => 4;

    protected override void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.PropertyChanged(sender, e);

        if (e.PropertyName == nameof(CompanionsSettings.CombineCompanions) ||
            e.PropertyName == nameof(CompanionsSettings.ImportantSpells) ||
            e.PropertyName == nameof(Seed.Companions))
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

        var unscaledSize = Size.Unscale(Settings.ScaleFactor);
        var charWidth = unscaledSize.Width / 8 - 2;
        if (charWidth < 4)
            yield break;

        var name = Game.Font.RenderText(companion.Type.ToString().ToUpperInvariant() + ":");
        var spellWidth = charWidth - _maxNameWidth;
        var shownSpells = companion.Spells.Where(s => Settings.Spells.Contains(s.Type));

        if (spellWidth < 4)
            yield break;

        if (shownSpells.Any())
        {
            var first = true;
            foreach (var group in shownSpells.Chunk(spellWidth / 3))
            {
                var list = group.ToList();
                var spells = list.Select(s => Game.Sprites.GetSpellData(s.Type)).ToList();
                var spellRow = BitmapDataFactory.CreateBitmapData(new Size(charWidth * 8, 24));

                if (first)
                {
                    first = false;
                    name.DrawInto(spellRow, new Point(0, 8));
                }

                for (int i = 0; i < list.Count; i++)
                {
                    spells[i].DrawInto(spellRow, new Point((charWidth - spellWidth) * 8 + i * 24, 0));
                    Game.Font.RenderText(ToSmallNumbers(list[i].Level.ToString().PadLeft(2))).DrawInto(spellRow, new Point((charWidth - spellWidth) * 8 + i * 24, 16));
                }

                yield return spellRow;
            }
        }
        else
        {
            yield return name;
        }

        var taskNum = 1;
        foreach (var quest in companion.Quests)
        {
            var color = quest.IsCompleted ? TextMode.Special : TextMode.Normal;
            var num = Game.Font.RenderText($"{taskNum++,1}.", color);
            var taskText = Game.Font.RenderText(quest.Description, color, charWidth - 2);
            var questData = BitmapDataFactory.CreateBitmapData(new Size(num.Width + taskText.Width, Math.Max(num.Height, taskText.Height)), KnownPixelFormat.Format8bppIndexed, taskText.Palette);

            num.CopyTo(questData);
            taskText.CopyTo(questData, new Point(num.Width, 0));
            num.Dispose();
            taskText.Dispose();

            yield return questData;
        }

        if (companion.Quests.Any())
            yield return Game.Font.RenderText("");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (var item in _arrows)
            {
                item.Value.Dispose();
            }
            _arrows.Clear();
        }

        base.Dispose(disposing);
    }

    private string ToSmallNumbers(string s) => string.Concat(s.Select(c => char.IsDigit(c) ? $"[{c}]" : c.ToString()));
}
