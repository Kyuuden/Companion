using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.View;
using KGySoft.Drawing;
using KGySoft.Drawing.Imaging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer.Tracking;
public class KeyItem : IImageTracker
{
    private readonly KeyItemType _itemType;
    private Bitmap? _image;
    private bool _isFound;
    private bool _isUsed;
    private bool _hasBeenFound;
    private readonly Seed _seed;
    private readonly bool _blankWhenNotFound;
    private readonly Func<GameState, bool> _usedCheck;

    internal KeyItem(Seed seed, KeyItemType type, Func<GameState, bool> usedCheck, bool blankWhenNotFound = false, KeyItemType? imageKeyItemType = null)
    {
        _seed = seed;
        Id = (int)type;
        Type = type;
        _itemType = imageKeyItemType ?? type;
        _blankWhenNotFound = blankWhenNotFound;
        _usedCheck = usedCheck;
        SetImage();
    }

    public int Id { get; }

    public KeyItemType Type { get; }

    public bool CheckUsed(GameState state) => _usedCheck(state);

    public bool IsFound
    {
        get => _isFound;
        set
        {
            if (_isFound == value)
                return;

            _isFound = value;
            NotifyPropertyChanged();
            SetImage();

            if (_isFound && !_hasBeenFound)
            {
                _hasBeenFound = true;
                _seed.Container.Timer.Info($"Found {Type.GetDescription()}");
            }
        }
    }

    public bool IsUsed
    {
        get => _isUsed;
        set
        {
            if (_isUsed == value)
                return;

            _isUsed = value;
            NotifyPropertyChanged();
            SetImage();
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

    public string AltText => Type.GetDescription();

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetImage()
    {
        if (_blankWhenNotFound && !IsFound)
            Image = new Bitmap(16, 16);
        else
        {
            if (IsUsed)
            {
                var keyItemImage = _seed.Sprites.GetKeyItemData(_itemType, IsFound);
                var data = BitmapDataFactory.CreateBitmapData(keyItemImage.Size);
                keyItemImage.CopyTo(data);

                var dropShadow = MysticQuest.Check.GetReadableBitmapData().ToGrayscale();
                dropShadow.DrawInto(data, new Rectangle(data.Width - 9, data.Height - 9, 9, 9));

                var check = MysticQuest.Check.GetReadableBitmapData();
                check.DrawInto(data, new Rectangle(data.Width - 9, data.Height - 9, 8, 8));

                Image = data.ToBitmap();
            }
            else
            {
                Image = _seed.Sprites.GetKeyItem(_itemType, IsFound);
            }
        }
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
