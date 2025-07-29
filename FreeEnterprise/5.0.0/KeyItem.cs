using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.FreeEnterprise.Shared;
using KGySoft.Drawing.Imaging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;

internal class KeyItem : IKeyItem, IDisposable
{
    private Bitmap? _image;
    private TimeSpan? whenUsed;
    private TimeSpan? whenFound;
    private bool isUsed;
    private bool isFound;
    private string whereFound = string.Empty;
    private readonly KeyItemSettings _settings;
    private readonly RomData.Font _font;

    public KeyItem(KeyItemSettings settings, RomData.Font font,  Descriptors descriptors, KeyItemType type)
    {
        _settings = settings;
        _font = font;
        Id = (int)type;
        Name = descriptors.GetKeyItemName(type);
        Description = descriptors.GetKeyItemDescription(type);
        _settings.PropertyChanged += SettingsChanged;
        SetImage();
    }

    private void SettingsChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(KeyItemSettings.KeyItemStyle))
            SetImage();
    }

    public int Id { get; }
    public string Name { get; }
    public string Description { get; }

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
        }
    }

    public TimeSpan? WhenFound
    {
        get => whenFound; 
        set
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
        set
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

    public bool IsGameAsset => _settings.KeyItemStyle == KeyItemStyle.Text;

    private void SetImage()
    {
        Image = _settings.KeyItemStyle switch
        {
            KeyItemStyle.Icons => ResourceLookup.GetKeyItemIcon((KeyItemType)Id, IsFound, IsUsed),
            KeyItemStyle.Text => _font.RenderText(Name, IsUsed ? TextMode.Normal : IsFound ? TextMode.Highlighted : TextMode.Disabled, null).ToBitmap(),
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
        _settings.PropertyChanged -= SettingsChanged;
    }
}
