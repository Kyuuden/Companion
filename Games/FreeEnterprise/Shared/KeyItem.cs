using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.FreeEnterprise.Shared;

internal class KeyItem : IKeyItem, IDisposable
{
    private Bitmap? _image;
    private TimeSpan? whenUsed;
    private TimeSpan? whenFound;
    private bool isUsed;
    private bool isFound;
    private string whereFound = string.Empty;
    private readonly SeedBase _seed;

    public KeyItem(SeedBase seed, KeyItemType type, bool isTrackable = true)
    {
        _seed = seed;
        Id = (int)type;
        Name = seed.KeyItemDescriptor.GetName(type);
        AltText = seed.KeyItemDescriptor.GetDescription(type);
        _seed.Settings.KeyItems.PropertyChanged += SettingsChanged;
        SetImage();
        IsTrackable = isTrackable;
    }

    private void SettingsChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(KeyItemSettings.KeyItemStyle))
            SetImage();
    }

    public int Id { get; }
    public string Name { get; }
    public string AltText { get; }

    public bool IsTrackable { get; }

    public string WhereFound
    {
        get => whereFound;
        set
        {
            if (whereFound == value)
                return;

            whereFound = value;
            NotifyPropertyChanged();
        }
    }

    public bool IsFound
    {
        get => isFound;
        set
        {
            if (isFound == value)
                return;

            isFound = value;
            NotifyPropertyChanged();
            SetImage();

            if (IsFound)
                WhenFound = _seed.Timer.Elapsed;
        }
    }

    public bool IsUsed
    {
        get => isUsed;
        set
        {
            if (isUsed == value)
                return;

            isUsed = value;
            NotifyPropertyChanged();
            SetImage();

            if (IsUsed)
                whenUsed = _seed.Timer.Elapsed;
        }
    }

    public TimeSpan? WhenFound
    {
        get => whenFound;
        private set
        {
            if (whenFound == value || whenFound.HasValue)
                return;

            whenFound = value;
            NotifyPropertyChanged();
        }
    }

    public TimeSpan? WhenUsed
    {
        get => whenUsed;
        private set
        {
            if (whenUsed == value || whenUsed.HasValue)
                return;

            whenUsed = value;
            NotifyPropertyChanged();
        }
    }

    public Bitmap Image
    {
        get => _image!;
        set
        {
            if (_image == value)
                return;

            _image = value;
            NotifyPropertyChanged();
        }
    }

    private void SetImage()
    {
        Image = _seed.Settings.KeyItems.KeyItemStyle switch
        {
            KeyItemStyle.Icons => ResourceLookup.GetKeyItemIcon((KeyItemType)Id, IsFound, IsUsed),
            KeyItemStyle.Text => _seed.Font.RenderText(Name, IsUsed ? TextMode.Normal : IsFound ? TextMode.Highlighted : TextMode.Disabled, null).ToBitmap(),
            _ => throw new InvalidOperationException()
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Dispose()
    {
        _seed.Settings.KeyItems.PropertyChanged -= SettingsChanged;
    }
}
