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
public class LocationsControl : PictureBox, IPanel
{
    private ISeed? _seed;
    private LocationsSettings? _settings;
    private List<List<IReadableBitmapData>> _locationTypeBitmapData = [];
    private IReadableBitmapData? _upArrow;
    private IReadableBitmapData? _downArrow;

    private int _scrollOffset;
    private bool _canScrollDown;
    private bool _canScrollUp;
    private int _groupIndex;
    private int _groupCount;

    public LocationsControl()
    {
    }

    public DockStyle DefaultDockStyle => DockStyle.Top;

    public bool CanHaveFillDockStyle => true;

    public int Priority => _settings?.Priority ?? int.MaxValue;
    public bool InTopPanel => _settings?.InTopPanel ?? false;

    public virtual void InitializeDataSources(ISeed seed, LocationsSettings locationsSettings)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));
        _settings = locationsSettings ?? throw new ArgumentNullException(nameof(locationsSettings));

        _settings.PropertyChanged += PropertyChanged;
        _seed.PropertyChanged += PropertyChanged;
        _seed.ButtonPressed += ButtonPressed;

        BackColor = _seed.BackgroundColor;
        Visible = _settings.Enabled;

        if (Height <= 8 || Width <= 8)
            return;

        _downArrow = _seed.Sprites.GetArrow(false);
        _upArrow = _seed.Sprites.GetArrow(true);

        if (_settings.ShowKeyItemChecks && _seed.AvailableLocations.Any(l=>l.IsKeyItem))
            _locationTypeBitmapData.Add(GenerateData("KEY ITEM LOCATIONS", _seed.AvailableLocations.Where(l => l.IsKeyItem)).ToList());

        if (_settings.ShowCharacterChecks && _seed.AvailableLocations.Any(l=>l.IsCharacter))
            _locationTypeBitmapData.Add(GenerateData("CHARACTER LOCATIONS", _seed.AvailableLocations.Where(l => l.IsCharacter)).ToList());

        if (_settings.ShowShopChecks && _seed.AvailableLocations.Any(l=>l.IsShop))
            _locationTypeBitmapData.Add(GenerateData("SHOP LOCATIONS", _seed.AvailableLocations.Where(l => l.IsShop)).ToList());

        _groupCount = _locationTypeBitmapData.Count;

        RenderImage();
    }

    private void ButtonPressed(string obj)
    {
        if (_settings == null)
            return;

        //if (obj.Equals(_settings.NextGroupButton, StringComparison.InvariantCultureIgnoreCase))
        //{
        //    if (_settings.CombineObjectiveGroups)
        //        return;

        //    _groupIndex = (_groupIndex + 1) % _groupBitmapData.Count;
        //    _scrollOffset = 0;
        //    _canScrollUp = false;
        //    RenderImage();
        //}
        //else if (obj.Equals(_settings.PreviousGroupButton, StringComparison.InvariantCultureIgnoreCase))
        //{
        //    if (_settings.CombineObjectiveGroups)
        //        return;

        //    _groupIndex = _groupIndex == 0 ? _groupBitmapData.Count - 1 : _groupIndex - 1;
        //    _scrollOffset = 0;
        //    _canScrollUp = false;
        //    RenderImage();
        //}
        //else if (obj.Equals(_settings.ScrollDownButton, StringComparison.InvariantCultureIgnoreCase))
        //{
        //    if (_canScrollDown)
        //    {
        //        _scrollOffset += _settings.ScrollLines;
        //        _canScrollUp = true;
        //        RenderImage();
        //    }
        //}
        //else if (obj.Equals(_settings.ScrollUpButton, StringComparison.InvariantCultureIgnoreCase))
        //{
        //    if (_canScrollUp)
        //    {
        //        _scrollOffset -= _settings.ScrollLines;
        //        _canScrollUp = _scrollOffset > 0;
        //        RenderImage();
        //    }
        //}
    }

    private void RenderImage()
    {
        if (_settings == null || _locationTypeBitmapData.Count == 0)
            return;

        if (_settings.CombineLocationTypes)
            RenderImage(_locationTypeBitmapData.SelectMany(d => d));
        else
            RenderImage(_locationTypeBitmapData[_groupIndex]);
    }

    private void RenderImage(IEnumerable<IReadableBitmapData> data)
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

    private IEnumerable<IReadableBitmapData> GenerateData(string header, IEnumerable<ILocation> locations)
    {
        if (_seed == null || _settings == null)
            throw new InvalidOperationException();

        var unscaledSize = _settings.Unscale(Size);
        var charWidth = (unscaledSize.Width / 8) - 2;
        //if (_groupCount == 1)
            yield return _seed.Font.RenderText(header.ToUpper(), RomData.TextMode.Normal);
        //else
           // yield return _seed.Font.RenderText(header.ToUpper().PadRight(charWidth - 11) + $"Page {groupnum,2}/{_groupCount,2}", RomData.TextMode.Normal);

        foreach (var loc in locations)
        {
            var marker = _seed.Font.RenderText("•",  RomData.TextMode.Normal);
            var locText = _seed.Font.RenderText(loc.Description,  RomData.TextMode.Normal, charWidth - 2);
            var locData = BitmapDataFactory.CreateBitmapData(new Size(marker.Width + locText.Width, Math.Max(marker.Height, locText.Height)), KnownPixelFormat.Format8bppIndexed, locText.Palette);

            marker.CopyTo(locData);
            locText.CopyTo(locData, new Point(marker.Width, 0));
            marker.Dispose();
            locText.Dispose();

            yield return locData;
        }
    }

    private void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ISeed.AvailableLocations):
            case nameof(LocationsSettings.ScaleFactor):
            case nameof(LocationsSettings.ShowCharacterChecks):
            case nameof(LocationsSettings.ShowKeyItemChecks):
            case nameof(LocationsSettings.ShowShopChecks):
                RegerateData();
                break;
            case nameof(LocationsSettings.CombineLocationTypes):
                RenderImage();
                break;
            case nameof(ISeed.BackgroundColor):
                BackColor = _seed?.BackgroundColor ?? BackColor;
                break;
            case nameof(LocationsSettings.Enabled):
                Visible = _settings?.Enabled ?? Visible;
                break;
        }
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
        if (_seed == null || _settings == null)
            return;

        Image?.Dispose();
        foreach (var l in _locationTypeBitmapData)
            foreach (var b in l)
                b.Dispose();
        _locationTypeBitmapData.Clear();

        if (_settings.ShowKeyItemChecks && _seed.AvailableLocations.Any(l => l.IsKeyItem))
            _locationTypeBitmapData.Add(GenerateData("KEY ITEM LOCATIONS", _seed.AvailableLocations.Where(l => l.IsKeyItem)).ToList());

        if (_settings.ShowCharacterChecks && _seed.AvailableLocations.Any(l => l.IsCharacter))
            _locationTypeBitmapData.Add(GenerateData("CHARACTER LOCATIONS", _seed.AvailableLocations.Where(l => l.IsCharacter)).ToList());

        if (_settings.ShowShopChecks && _seed.AvailableLocations.Any(l => l.IsShop))
            _locationTypeBitmapData.Add(GenerateData("SHOP LOCATIONS", _seed.AvailableLocations.Where(l => l.IsShop)).ToList());

        _groupCount = _locationTypeBitmapData.Count;
        RenderImage();
    }
}
