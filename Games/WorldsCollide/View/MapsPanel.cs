using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Games.WorldsCollide.Tracking;
using FF.Rando.Companion.View;
using KGySoft.CoreLibraries;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static BizHawk.Common.AVIWriterImports.AVISTREAMINFOW;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

internal class MapsPanel : PictureBox, IScrollablePanel
{
    private readonly Size _markerSize = new(18, 18);
    private readonly Seed _seed;
    private readonly Worlds _worlds;
    private readonly double _aspect;
    private readonly RegionedToolTip _toolTip;

    private bool _scrollingEnabled;
    private int _worldIndex = 0;

    public MapsPanel(Seed seed)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));
        _worlds = seed.State.Worlds;

        DoubleBuffered = true;
        BackgroundImageLayout = ImageLayout.Zoom;
        SizeMode = PictureBoxSizeMode.Zoom;
        Margin = new(0);

        foreach (var world in _worlds.Items)
        {
            world.Map.Render();
            world.Updated += World_Updated;
        }

        _seed.Settings.PropertyChanged += SettingsChanged;
        _seed.State.PropertyChanged += StateChanged;

        BackColor = _seed.BackgroundColor;
        CanScrollChanged?.Invoke(this, EventArgs.Empty);

        var size = _worlds.Items[0].Map.Size;
        _aspect = (double)size.Width / size.Height;

        _toolTip = new(this);
    }

    private void StateChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(State.CurrentLocation) && e.PropertyName != nameof(State.WorldPosition))
            return;

        if (_seed.Settings?.Follow == true)
        {
            var newIndex = _worlds!.Items.AsEnumerable().IndexOf(w => (ushort)w.Type == _seed!.State.CurrentLocation);
            if (newIndex == -1)
                return;

            if (newIndex != _worldIndex)
            {
                _worldIndex = newIndex;
                Render();
            }
        }
    }

    private void SettingsChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(WorldsCollideSettings.CharacterRewardColor):
            case nameof(WorldsCollideSettings.EsperRewardColor):
            case nameof(WorldsCollideSettings.ItemRewardColor):
                Render();
            break;
        }
    }

    private void World_Updated(object sender, EventArgs e)
    {
        if (sender == _worlds!.Items[_worldIndex])
            Render();
    }

    private Size _lastSize;

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (_seed == null)
            return;

        if (Height <= 8 || Width <= 8)
            return;

        if (Size.Height > 0 && Size.Width > 0 && IsHandleCreated && (_lastSize.IsEmpty || _lastSize != Size))
        {
            _lastSize = Size;
            HandleResize();
        }
    }

    protected void HandleResize()
    {
        var newAspect = (double)Width / Height;
        if (_aspect / newAspect > 0.05)
        {
            switch (Dock)
            {
                case DockStyle.None:
                case DockStyle.Fill:
                    return;
                case DockStyle.Top:
                case DockStyle.Bottom:
                    Height = (int)(Width / _aspect);
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    Width = (int)(Height * _aspect);
                    break;
            }
        }
        else
        {
            Render();
        }
    }

    protected void Render()
    {
        if (_worldIndex == -1)
            return;

        if (_worldIndex < _worlds.Items.Count)
            RenderImage(_worlds.Items[_worldIndex]!);
    }

    private void RenderImage(World world)
    {
        try
        {
            if (!Visible)
                return;

            Image?.Dispose();
            Image = null;

            BackgroundImage = world.Map.Render();
            _toolTip.ClearRegions();

            var markers = BitmapDataFactory.CreateBitmapData(world.Map.Size);

            var sb = new StringBuilder();
            sb.AppendLine($"{world.Description}:\n");

            if (_seed?.Started ?? false)
            {
                foreach (var loc in world.Locations.Where(l => l.ChecksAvailable))
                {
                    var rect = DrawMarker(loc, markers);
                    _toolTip.AddRegion(rect, string.Join("\n", [$"{loc.Description}:", .. loc.Checks.Where(c => c.IsAvailable && !c.IsComplete).Select(c => c.ToString())]));
                }
            }

            Image = markers.ToBitmap();
        }
        catch (Exception)
        {
            //This will throw when zoom level is too high, so lets not crash everything.
        }
    }

    private Rectangle DrawMarker(CheckLocation loc, IReadWriteBitmapData bitmapData)
    {
        var rect = new Rectangle(loc.Location, _markerSize);
        var checks = loc.Checks.Where(c => c.IsAvailable && !c.IsComplete).ToList();
        var colors = GetColors(checks.Aggregate(RewardType.None, (r, c) => r | c.GetPossibleRewards())).ToList();

        switch (colors.Count)
        {
            case 3:
                Point p1 = rect.Location;
                Point p2 = p1 with { X = p1.X + 12 };
                Point p3 = p1 with { X = rect.Right };
                Point p4 = p1 with { X = rect.Right, Y = rect.Bottom };
                Point p5 = p1 with { Y = rect.Bottom };
                Point p6 = p1 with { Y = p1.Y + 12 };
                Point c = new(p1.X + 9, p1.Y + 9);

                bitmapData.FillPolygon(colors[0], (IEnumerable<Point>)[c, p6, p1, p2]);
                bitmapData.FillPolygon(colors[1], (IEnumerable<Point>)[c, p2, p3, p4]);
                bitmapData.FillPolygon(colors[2], (IEnumerable<Point>)[c, p4, p5, p6]);
                break;
            case 2:
                bitmapData.FillPolygon(colors[0], (IEnumerable<Point>)[rect.Location, rect.Location with { X = rect.Right }, rect.Location with { Y = rect.Bottom }]);
                bitmapData.FillPolygon(colors[1], (IEnumerable<Point>)[rect.Location with { X = rect.Right }, rect.Location with { Y = rect.Bottom }, rect.Location with { X = rect.Right, Y = rect.Bottom }]);
                break;
            case 1:
                bitmapData.FillRectangle(colors[0], rect);
                break;
        }

        if (colors.Any())
            bitmapData.DrawRectangle(Color.Black, rect);

        return rect;
    }

    private IEnumerable<Color> GetColors(RewardType rewardType)
    {
        if (rewardType.IsFlagSet(RewardType.Character))
            yield return _seed.Settings.CharacterRewardColor;
        if (rewardType.IsFlagSet(RewardType.Esper))
            yield return _seed.Settings.EsperRewardColor;
        if (rewardType.IsFlagSet(RewardType.Item))
            yield return _seed.Settings.ItemRewardColor;
    }

    public bool IsEnabledForScrolling
    {
        get => _scrollingEnabled;
        set
        {
            if (value == _scrollingEnabled)
                return;

            _scrollingEnabled = value;
            Render();
        }
    }

    public bool CanScroll => true;

    public event EventHandler? CanScrollChanged;

    public void ScrollDown()
    {
    }

    public void ScrollLeft()
    {
        _worldIndex = _worldIndex == 0 ? _worlds!.Items.Count - 1 : _worldIndex - 1;
        Render();
    }

    public void ScrollRight()
    {
        _worldIndex = (_worldIndex + 1) % _worlds!.Items.Count;
        Render();
    }

    public void ScrollUp()
    {
    }
}