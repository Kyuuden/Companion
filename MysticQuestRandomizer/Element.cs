using FF.Rando.Companion.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class Element : IImageTracker
{
    private readonly ElementsType _original;
    private readonly ElementsType _updated;
    private readonly int padding;
    private readonly RomData.Font _font;
    private readonly ElementsSettings _settings;
    private Bitmap? _image;

    internal Element(ElementsType original, ElementsType updated, ElementsSettings settings, RomData.Font font)
    {
        _original = original;
        _updated = updated;
        _font = font;
        _settings = settings;
        _settings.PropertyChanged += SettingsChanged;
        padding = Enum.GetValues(typeof(ElementsType)).OfType<ElementsType>().Select(e=>e.ToString().Length).Max();
        SetImage();
    }

    private void SettingsChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ElementsSettings.ElementsStyle))
            SetImage();
    }

    public Bitmap Image
    {
        get => _image!;
        set
        {
            if (_image == value)
                return;
            _image?.Dispose();
            _image = null;
            _image = value;
            NotifyPropertyChanged();
        }
    }

    private void SetImage()
    {
        Image = _font.RenderText($"{GetStringFor(_original)}>{GetStringFor(_updated)} ").ToBitmap();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string GetStringFor(ElementsType elementType)
        => _settings.ElementsStyle switch
        {
            ElementsStyle.Icons => $"[{elementType.ToString()}]",
            ElementsStyle.Text => $"{elementType.ToString()}",
            _ => elementType.ToString()
        };
}
