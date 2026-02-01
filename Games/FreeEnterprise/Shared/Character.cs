using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.FreeEnterprise.Shared;

internal class Character : ICharacter, IDisposable
{
    public Character(PartySettings settings, Sprites sprites)
    {
        _settings = settings;
        _sprites = sprites;
        _image = _sprites.GetBlankCharacter();
        settings.PropertyChanged += SettingsChanged;
    }

    private void SettingsChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PartySettings.Pose))
            SetImage();
    }

    private Bitmap _image;
    private byte fashion;
    private bool isAnchor;
    private CharacterType type;
    private byte slot;
    private byte id = 0;
    private readonly PartySettings _settings;
    private readonly Sprites _sprites;

    public byte Id
    {
        get => id;
        set
        {
            if (id == value)
                return;

            id = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }
    public byte Slot
    {
        get => slot;
        set
        {
            if (slot == value)
                return;

            slot = value;
            NotifyPropertyChanged();
        }
    }
    public CharacterType Type
    {
        get => type;
        set
        {
            if (type == value)
                return;

            type = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }
    public byte Fashion
    {
        get => fashion;
        set
        {
            if (fashion == value)
                return;

            fashion = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }
    public bool IsAnchor
    {
        get => isAnchor;
        set
        {
            if (isAnchor == value)
                return;

            isAnchor = value;
            NotifyPropertyChanged();
        }
    }

    public Bitmap Image
    {
        get => _image;
        set
        {
            if (_image == value)
                return;

            _image = value;
            NotifyPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetImage()
    {
        Image = Id == 0
            ? _sprites.GetBlankCharacter()
            : _sprites.GetCharacterImage(Type, Fashion, _settings.Pose);
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Dispose()
    {
        _settings.PropertyChanged -= SettingsChanged;
    }
}
