using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise.View;

public class ObjectivesPanel : ScrollablePanel<ObjectivesSettings>
{
    protected override bool CombinePages => Settings?.CombineObjectiveGroups == true;

    protected override int ScrollLines => Settings?.ScrollLines ?? 2;

    protected override IEnumerable<List<IReadableBitmapData>> GeneratePageBitmaps()
    {
        if (Game == null) 
            yield break;

        var groupCount = Game.Objectives.Count();
        var gnum = 0;
        foreach (var group in Game.Objectives)
        {
            yield return GenerateData(group, ++gnum, groupCount).ToList();
        }
    }

    protected override void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.PropertyChanged(sender, e);

        if (e.PropertyName == nameof(ISeed.Objectives))
            RegerateData();
    }

    private IEnumerable<IReadableBitmapData> GenerateData(IObjectiveGroup group, int groupNum, int totalGroups)
    {
        if (Game == null || Settings == null)
            yield break;

        var unscaledSize = Settings.Unscale(Size);
        var charWidth = (unscaledSize.Width / 8) - 2;
        if (charWidth < 5)
            yield break;

        if (totalGroups == 1)
            yield return Game.Font.RenderText(group.Name.ToUpper(), TextMode.Normal);
        else
            yield return Game.Font.RenderText(group.Name.ToUpper().PadRight(Math.Max(0,charWidth - 11)) + $"Group {groupNum,2}/{totalGroups,2}", TextMode.Normal);

        var taskNum = 1;
        var completed = 0;
        var hardReq = 0;
        var completedHardReq = 0;
        foreach (var task in group.Tasks)
        {
            if (task.IsCompleted) completed++;
            if (task.IsHardRequired) hardReq++;
            if (task.IsCompleted && task.IsHardRequired) completedHardReq++;

            var taskColor = task.IsCompleted
                    ? TextMode.Disabled
                    : task.IsHardRequired
                        ? TextMode.Highlighted
                        : TextMode.Normal;

            var num = Game.Font.RenderText($"{taskNum++,2}. ", taskColor);

            var description = task.IsCompleted && task.CompletedAt > TimeSpan.Zero
                ? $"{task.Description} - {task.CompletedAt.Value:hh':'mm':'ss'.'ff}"
                : task.Description;

            var taskText = Game.Font.RenderText(description, taskColor, charWidth - 4);
            var taskData = BitmapDataFactory.CreateBitmapData(new Size(num.Width + taskText.Width, Math.Max(num.Height, taskText.Height)), KnownPixelFormat.Format8bppIndexed, taskText.Palette);

            num.CopyTo(taskData);
            taskText.CopyTo(taskData, new Point(num.Width, 0));
            num.Dispose();
            taskText.Dispose();

            yield return taskData;
        }

        yield return Game.Font.RenderText("-- REWARDS --", TextMode.Normal);
        foreach (var reward in group.Rewards)
        {
            var textmode = TextMode.Normal;
            if (reward.Required.HasValue)
            {
                switch (reward.Required.Value)
                {
                    case -1:
                        if (completed == group.Tasks.Count())
                            textmode = TextMode.Disabled;
                        break;
                    default:
                        if (completed >= reward.Required.Value && completedHardReq == hardReq)
                            textmode = TextMode.Disabled;
                        break;
                }
            }

            yield return Game.Font.RenderText(reward.Description, textmode, charWidth);
        }
    }
}