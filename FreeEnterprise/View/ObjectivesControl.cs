using FF.Rando.Companion.FreeEnterprise._5._0._0;
using FF.Rando.Companion.FreeEnterprise.Settings;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise.View;
public class ObjectivesControl : PictureBox
{
    private ISeed? _seed;
    private ObjectivesSettings? _settings;
    private List<List<IReadableBitmapData>> _groupBitmapData = [];
    private IReadableBitmapData? _upArrow;
    private IReadableBitmapData? _downArrow;

    private int _scrollOffset;
    private bool _canScrollDown;
    private bool _canScrollUp;
    private int _groupIndex;
    private int _groupCount;

    public ObjectivesControl()
    {
    }

    public virtual void InitializeDataSources(ISeed seed, ObjectivesSettings objectivesSettings)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));
        _settings = objectivesSettings ?? throw new ArgumentNullException(nameof(objectivesSettings));

        _settings.PropertyChanged += PropertyChanged;
        _seed.PropertyChanged += PropertyChanged;
        _seed.ButtonPressed += ButtonPressed;

        BackColor = _seed.BackgroundColor;
        Visible = _settings.Enabled;

        if (Height <= 8 || Width <= 8)
            return;

        _downArrow = _seed.Sprites.GetArrow(false);
        _upArrow = _seed.Sprites.GetArrow(true);

        _groupCount = _seed.Objectives.Count();
        var gnum = 0;
        foreach (var group in _seed.Objectives)
        {
            _groupBitmapData.Add(GenerateData(group, ++gnum).ToList());
        }

        RenderImage(_groupBitmapData[_groupIndex]);
    }

    private void ButtonPressed(string obj)
    {
        Debug.WriteLine($"Button Pressed: {obj}");

        if (_settings == null)
            return;

        if (obj.Equals(_settings.NextGroupButton, StringComparison.InvariantCultureIgnoreCase))
        {
            _groupIndex = (_groupIndex + 1) % _groupBitmapData.Count;
            _scrollOffset = 0;
            _canScrollUp = false;
            RenderImage(_groupBitmapData[_groupIndex]);
        }
        else if (obj.Equals(_settings.PreviousGroupButton, StringComparison.InvariantCultureIgnoreCase))
        {
            _groupIndex = _groupIndex == 0 ? _groupBitmapData.Count - 1 : _groupIndex - 1;
            _scrollOffset = 0;
            _canScrollUp = false;
            RenderImage(_groupBitmapData[_groupIndex]);
        }
        else if (obj.Equals(_settings.ScrollDownButton, StringComparison.InvariantCultureIgnoreCase))
        {
            if (_canScrollDown)
            {
                _scrollOffset += 1;
                _canScrollUp = true;
                RenderImage(_groupBitmapData[_groupIndex]);
            }
        }
        else if (obj.Equals(_settings.ScrollUpButton, StringComparison.InvariantCultureIgnoreCase))
        {
            if (_canScrollUp)
            {
                _scrollOffset -= 1;
                _canScrollUp = _scrollOffset > 0;
                RenderImage(_groupBitmapData[_groupIndex]);
            }
        }
    }

    private void RenderImage(List<IReadableBitmapData> data)
    {
        if (_seed == null || _settings == null)
            throw new InvalidOperationException();

        Image?.Dispose();
        var unscaledSize = _settings.Unscale(Size);
        unscaledSize = new Size((unscaledSize.Width / 8) * 8, (unscaledSize.Height / 8) * 8);
        var y = 8;

        if (unscaledSize.Width <= 0 || unscaledSize.Height <= 0)
            return;

        IReadWriteBitmapData? rootImage = null;
        var baseImage = _seed.Font.RenderBox(unscaledSize.Width / 8, unscaledSize.Height / 8, _upArrow?.Palette);

        _canScrollDown = false;
        foreach (var line in data.Skip(_scrollOffset))
        {
            if (line.Height + y > baseImage.Height - 8)
            {
                _canScrollDown = true;
                break;
            }

            line.CopyTo(baseImage, new Point(8, y));
            y += (line.Height + 8);
        }

        if (_canScrollDown)
            _downArrow?.DrawInto(baseImage, new Point(unscaledSize.Width - 16, unscaledSize.Height - 20));

        if (_canScrollUp)
            _upArrow?.DrawInto(baseImage, new Point(unscaledSize.Width - 16, 0));

        if (rootImage != null)
        {
            baseImage.CopyTo(rootImage, new Point(0, 24));
            Image = rootImage.ToBitmap();
        }
        else Image = baseImage.ToBitmap();
    }

    private IEnumerable<IReadableBitmapData> GenerateData(IObjectiveGroup group, int groupnum)
    {
        if (_seed == null || _settings == null)
            throw new InvalidOperationException();

        var unscaledSize = _settings.Unscale(Size);
        var charWidth = (unscaledSize.Width / 8) - 2;
        yield return _seed.Font.RenderText(group.Name.ToUpper().PadRight(charWidth - 11) + $"Group {groupnum,2}/{_groupCount,2}", RomData.TextMode.Normal);

        var taskNum = 1;
        var completed = 0;
        foreach (var task in group.Tasks)
        {
            if (task.IsCompleted) completed++;

            var num = _seed.Font.RenderText($"{taskNum++,2}. ", task.IsCompleted ? RomData.TextMode.Disabled : RomData.TextMode.Normal);


            var description = task.IsCompleted && task.CompletedAt > TimeSpan.Zero
                ? $"{task.Description} - {task.CompletedAt.Value.ToString("hh':'mm':'ss'.'ff")}"
                : task.Description;

            var taskText = _seed.Font.RenderText(description, task.IsCompleted ? RomData.TextMode.Disabled : RomData.TextMode.Normal, charWidth - 4);
            var taskData = BitmapDataFactory.CreateBitmapData(new Size(num.Width + taskText.Width, Math.Max(num.Height, taskText.Height)), KnownPixelFormat.Format8bppIndexed, taskText.Palette);

            num.CopyTo(taskData);
            taskText.CopyTo(taskData, new Point(num.Width, 0));
            num.Dispose();
            taskText.Dispose();

            yield return taskData;
        }

        yield return _seed.Font.RenderText("-- REWARDS --", RomData.TextMode.Normal);
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
                        if (completed >= reward.Required.Value)
                            textmode = RomData.TextMode.Disabled;
                        break;
                }
            }

            yield return _seed.Font.RenderText(reward.Description, textmode, charWidth);
        }
    }

    private void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ObjectivesSettings.ScaleFactor))
            RegerateData();
        if (e.PropertyName == nameof(ISeed.Objectives))
            RegerateData();
        if (e.PropertyName == nameof(ISeed.BackgroundColor))
            BackColor = _seed?.BackgroundColor ?? BackColor;
        if (e.PropertyName == nameof(ObjectivesSettings.Enabled))
            Visible = _settings?.Enabled ?? Visible;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_seed == null)
            return;

        if (Height <= 8 || Width <= 8)
            return;

        _canScrollUp = false;
        _canScrollDown = false;
        _scrollOffset = 0;


        if (Size.Height > 0 && Size.Width > 0 && IsHandleCreated)
            BeginInvoke(() => RegerateData());
    }

    private void RegerateData()
    {
        if (_seed == null)
            return;

        Image?.Dispose();
        foreach (var l in _groupBitmapData)
            foreach (var b in l)
                b.Dispose();
        _groupBitmapData.Clear();

        var gnum = 0;
        foreach (var group in _seed.Objectives)
        {
            _groupBitmapData.Add(GenerateData(group, ++gnum).ToList());
        }
        RenderImage(_groupBitmapData[_groupIndex]);
    }
}
