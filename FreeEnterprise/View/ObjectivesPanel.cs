using FF.Rando.Companion.FreeEnterprise.Settings;
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
        if (Seed == null) 
            yield break;

        var groupCount = Seed.Objectives.Count();
        var gnum = 0;
        foreach (var group in Seed.Objectives)
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
        if (Seed == null || Settings == null)
            throw new InvalidOperationException();

        var unscaledSize = Settings.Unscale(Size);
        var charWidth = (unscaledSize.Width / 8) - 2;
        if (totalGroups == 1)
            yield return Seed.Font.RenderText(group.Name.ToUpper(), RomData.TextMode.Normal);
        else
            yield return Seed.Font.RenderText(group.Name.ToUpper().PadRight(charWidth - 11) + $"Group {groupNum,2}/{totalGroups,2}", RomData.TextMode.Normal);

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
                    ? RomData.TextMode.Disabled
                    : task.IsHardRequired
                        ? RomData.TextMode.Highlighted
                        : RomData.TextMode.Normal;

            var num = Seed.Font.RenderText($"{taskNum++,2}. ", taskColor);

            var description = task.IsCompleted && task.CompletedAt > TimeSpan.Zero
                ? $"{task.Description} - {task.CompletedAt.Value.ToString("hh':'mm':'ss'.'ff")}"
                : task.Description;

            var taskText = Seed.Font.RenderText(description, taskColor, charWidth - 4);
            var taskData = BitmapDataFactory.CreateBitmapData(new Size(num.Width + taskText.Width, Math.Max(num.Height, taskText.Height)), KnownPixelFormat.Format8bppIndexed, taskText.Palette);

            num.CopyTo(taskData);
            taskText.CopyTo(taskData, new Point(num.Width, 0));
            num.Dispose();
            taskText.Dispose();

            yield return taskData;
        }

        yield return Seed.Font.RenderText("-- REWARDS --", RomData.TextMode.Normal);
        foreach (var reward in group.Rewards)
        {
            var textmode = RomData.TextMode.Normal;
            if (reward.Required.HasValue)
            {
                switch (reward.Required.Value)
                {
                    case -1:
                        if (completed == group.Tasks.Count())
                            textmode = RomData.TextMode.Disabled;
                        break;
                    default:
                        if (completed >= reward.Required.Value && completedHardReq == hardReq)
                            textmode = RomData.TextMode.Disabled;
                        break;
                }
            }

            yield return Seed.Font.RenderText(reward.Description, textmode, charWidth);
        }
    }
}


//public class ObjectivesControl : PictureBox, IPanel, IScrollable
//{
//    private ISeed? _seed;
//    private ObjectivesSettings? _settings;
//    private List<List<IReadableBitmapData>> _groupBitmapData = [];
//    private IReadableBitmapData? _upArrow;
//    private IReadableBitmapData? _downArrow;

//    private int _scrollOffset;
//    private bool _canScrollDown;
//    private bool _canScrollUp;
//    private int _groupIndex;
//    private int _groupCount;
//    private bool _scrollingEnabled;

//    public ObjectivesControl()
//    {
//    }

//    public DockStyle DefaultDockStyle => DockStyle.Top;

//    public bool CanHaveFillDockStyle => true;

//    public int Priority => _settings?.Priority ?? int.MaxValue;

//    public bool InTopPanel => _settings?.InTopPanel ?? false;

//    public bool IsEnabledForScrolling
//    {
//        get => _scrollingEnabled; 
//        set
//        {
//            if (value ==  _scrollingEnabled) 
//                return;

//            _scrollingEnabled = value;
//            RenderImage();
//        }
//    }

//    public virtual void InitializeDataSources(ISeed seed, ObjectivesSettings objectivesSettings)
//    {
//        _seed = seed ?? throw new ArgumentNullException(nameof(seed));
//        _settings = objectivesSettings ?? throw new ArgumentNullException(nameof(objectivesSettings));

//        _settings.PropertyChanged += PropertyChanged;
//        _seed.PropertyChanged += PropertyChanged;

//        BackColor = _seed.BackgroundColor;
//        Visible = _settings.Enabled;

//        if (Height <= 8 || Width <= 8)
//            return;

//        _downArrow = _seed.Sprites.GetArrow(RomData.Arrow.Down);
//        _upArrow = _seed.Sprites.GetArrow(RomData.Arrow.Up);

//        _groupCount = _seed.Objectives.Count();
//        var gnum = 0;
//        foreach (var group in _seed.Objectives)
//        {
//            _groupBitmapData.Add(GenerateData(group, ++gnum).ToList());
//        }

//        RenderImage();
//    }

//    private void RenderImage()
//    {
//        if (_settings == null || _groupBitmapData.Count == 0)
//            return;

//        if (_settings.CombineObjectiveGroups)
//            RenderImage(_groupBitmapData.SelectMany(d => d));
//        else
//            RenderImage(_groupBitmapData[_groupIndex]);
//    }

//    private void RenderImage(IEnumerable<IReadableBitmapData> data)
//    {
//        if (_seed == null || _settings == null)
//            throw new InvalidOperationException();

//        Image?.Dispose();
//        var unscaledSize = _settings.Unscale(Size);
//        unscaledSize = new Size((unscaledSize.Width / 8) * 8, (unscaledSize.Height / 8) * 8);
//        var y = 8;

//        if (unscaledSize.Width <= 0 || unscaledSize.Height <= 0)
//            return;

//        IReadWriteBitmapData? rootImage = null;
//        var baseImage = _seed.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8, _upArrow?.Palette);

//        _canScrollDown = false;
//        foreach (var line in data.Skip(_scrollOffset))
//        {
//            if (line.Height + y > baseImage.Height - 8)
//            {
//                _canScrollDown = true;
//                break;
//            }

//            line.CopyTo(baseImage, new Point(8, y));
//            y += (line.Height + 8);
//        }

//        if (_canScrollDown && IsEnabledForScrolling)
//            _downArrow?.DrawInto(baseImage, new Point(unscaledSize.Width - 16, unscaledSize.Height - 20));

//        if (_canScrollUp && IsEnabledForScrolling)
//            _upArrow?.DrawInto(baseImage, new Point(unscaledSize.Width - 16, 0));

//        _seed.Sprites.GetArrow(RomData.Arrow.Left)?.DrawInto(baseImage, new Point(0, 8));
//        _seed.Sprites.GetArrow(RomData.Arrow.Right)?.DrawInto(baseImage, new Point(unscaledSize.Width - 16, 8));

//        if (rootImage != null)
//        {
//            baseImage.CopyTo(rootImage, new Point(0, 24));
//            Image = rootImage.ToBitmap();
//        }
//        else Image = baseImage.ToBitmap();
//    }

//    private IEnumerable<IReadableBitmapData> GenerateData(IObjectiveGroup group, int groupnum)
//    {
//        if (_seed == null || _settings == null)
//            throw new InvalidOperationException();

//        var unscaledSize = _settings.Unscale(Size);
//        var charWidth = (unscaledSize.Width / 8) - 2;
//        if (_groupCount == 1)
//            yield return _seed.Font.RenderText(group.Name.ToUpper(), RomData.TextMode.Normal);
//        else
//            yield return _seed.Font.RenderText(group.Name.ToUpper().PadRight(charWidth - 11) + $"Group {groupnum,2}/{_groupCount,2}", RomData.TextMode.Normal);

//        var taskNum = 1;
//        var completed = 0;
//        var hardReq = 0;
//        var completedHardReq = 0;
//        foreach (var task in group.Tasks)
//        {
//            if (task.IsCompleted) completed++;
//            if (task.IsHardRequired) hardReq++;
//            if (task.IsCompleted && task.IsHardRequired) completedHardReq++;

//            var taskColor = task.IsCompleted
//                    ? RomData.TextMode.Disabled
//                    : task.IsHardRequired
//                        ? RomData.TextMode.Highlighted
//                        : RomData.TextMode.Normal;

//            var num = _seed.Font.RenderText($"{taskNum++,2}. ", taskColor);

//            var description = task.IsCompleted && task.CompletedAt > TimeSpan.Zero
//                ? $"{task.Description} - {task.CompletedAt.Value.ToString("hh':'mm':'ss'.'ff")}"
//                : task.Description;

//            var taskText = _seed.Font.RenderText(description, taskColor, charWidth - 4);
//            var taskData = BitmapDataFactory.CreateBitmapData(new Size(num.Width + taskText.Width, Math.Max(num.Height, taskText.Height)), KnownPixelFormat.Format8bppIndexed, taskText.Palette);

//            num.CopyTo(taskData);
//            taskText.CopyTo(taskData, new Point(num.Width, 0));
//            num.Dispose();
//            taskText.Dispose();

//            yield return taskData;
//        }

//        yield return _seed.Font.RenderText("-- REWARDS --", RomData.TextMode.Normal);
//        foreach (var reward in group.Rewards)
//        {
//            var textmode = RomData.TextMode.Normal;
//            if (reward.Required.HasValue)
//            {
//                switch (reward.Required.Value)
//                {
//                    case -1: 
//                        if (completed == group.Tasks.Count())
//                            textmode = RomData.TextMode.Disabled;
//                        break;
//                    default:
//                        if (completed >= reward.Required.Value && completedHardReq == hardReq)
//                            textmode = RomData.TextMode.Disabled;
//                        break;
//                }
//            }

//            yield return _seed.Font.RenderText(reward.Description, textmode, charWidth);
//        }
//    }

//    private void PropertyChanged(object sender, PropertyChangedEventArgs e)
//    {
//        if (e.PropertyName == nameof(ObjectivesSettings.ScaleFactor))
//            RegerateData();
//        if (e.PropertyName == nameof(ISeed.Objectives))
//            RegerateData();
//        if (e.PropertyName == nameof(ISeed.BackgroundColor))
//            BackColor = _seed?.BackgroundColor ?? BackColor;
//        if (e.PropertyName == nameof(ObjectivesSettings.Enabled))
//            Visible = _settings?.Enabled ?? Visible;
//    }

//    protected override void OnResize(EventArgs e)
//    {
//        base.OnResize(e);

//        if (_seed == null)
//            return;

//        if (Height <= 8 || Width <= 8)
//            return;

//        _canScrollUp = false;
//        _canScrollDown = false;
//        _scrollOffset = 0;


//        if (Size.Height > 0 && Size.Width > 0 && IsHandleCreated)
//            BeginInvoke(() => RegerateData());
//    }

//    private void RegerateData()
//    {
//        if (_seed == null)
//            return;

//        Image?.Dispose();
//        foreach (var l in _groupBitmapData)
//            foreach (var b in l)
//                b.Dispose();
//        _groupBitmapData.Clear();

//        var gnum = 0;
//        foreach (var group in _seed.Objectives)
//        {
//            _groupBitmapData.Add(GenerateData(group, ++gnum).ToList());
//        }
//        RenderImage();
//    }

//    public void ScrollDown()
//    {
//        if (_canScrollDown)
//        {
//            _scrollOffset += _settings?.ScrollLines ?? 2;
//            _canScrollUp = true;
//            RenderImage();
//        }
//    }

//    public void ScrollUp()
//    {
//        if (_canScrollUp)
//        {
//            _scrollOffset -= _settings?.ScrollLines ?? 2;
//            _canScrollUp = _scrollOffset > 0;
//            RenderImage();
//        }
//    }

//    public void ScrollLeft()
//    {
//        if (_settings?.CombineObjectiveGroups == true)
//            return;

//        _groupIndex = _groupIndex == 0 ? _groupBitmapData.Count - 1 : _groupIndex - 1;
//        _scrollOffset = 0;
//        _canScrollUp = false;
//        RenderImage();
//    }

//    public void ScrollRight()
//    {
//        if (_settings?.CombineObjectiveGroups == true)
//            return;

//        _groupIndex = (_groupIndex + 1) % _groupBitmapData.Count;
//        _scrollOffset = 0;
//        _canScrollUp = false;
//        RenderImage();
//    }

//    public bool CanScroll()
//    {
//        return _canScrollUp || _canScrollDown || (!_settings.CombineObjectiveGroups && _groupBitmapData.Count > 1);
//    }
//}
