using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MysticQuestRandomizer.RomData;
using FF.Rando.Companion.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class KeyItem : IImageTracker
{
    private readonly KeyItemType _itemType;
    private Bitmap? _image;
    private TimeSpan? _whenFound;
    private bool _isFound;
    private readonly Sprites _sprites;
    private readonly bool _blank;

    internal KeyItem(KeyItemType type, Sprites sprites, bool blank = false)
    {
        _itemType = type;
        _sprites = sprites;
        _blank = blank;
        SetImage();
    }

    public int Id => (int)_itemType;

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
        }
    }

    public TimeSpan? WhenFound
    {
        get => _whenFound;
        set
        {
            if (_whenFound == value || _whenFound.HasValue)
                return;

            _whenFound = value;
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetImage()
    {
        if (_blank)
            Image = new Bitmap(16, 16);
        else
            Image = _sprites.GetKeyItem(_itemType, IsFound);
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

internal class KeyItems(Sprites sprites, bool shatteredSkyCoin)
{
    private readonly IReadOnlyList<KeyItem> _items =
        [
            new KeyItem(KeyItemType.Elixer, sprites),
            new KeyItem(KeyItemType.TreeWither, sprites),
            new KeyItem(KeyItemType.WakeWater, sprites),
            new KeyItem(KeyItemType.VenusKey, sprites),
            new KeyItem(KeyItemType.MultiKey, sprites),
            new KeyItem(KeyItemType.GasMask, sprites),
            new KeyItem(KeyItemType.MagicMirror, sprites),
            new KeyItem(KeyItemType.ThunderRock, sprites),
            new KeyItem(KeyItemType.CapitansCap, sprites),
            new KeyItem(KeyItemType.LibraCrest, sprites),
            new KeyItem(KeyItemType.GeminiCrest, sprites),
            new KeyItem(KeyItemType.MobiusCrest, sprites),
            new KeyItem(KeyItemType.SandCoin, sprites),
            new KeyItem(KeyItemType.RiverCoin, sprites),
            new KeyItem(KeyItemType.SunCoin, sprites),
            new KeyItem(KeyItemType.SkyCoin, sprites, shatteredSkyCoin),
        ];

    public IReadOnlyList<KeyItem> Items => _items;

    public bool Update(TimeSpan time, ReadOnlySpan<byte> found)
    {
        var updated = false;

        foreach (var keyitem in _items)
        {
            var isfound = found.Read<bool>(keyitem.Id);
            if (isfound != keyitem.IsFound)
            {
                updated = true;
                keyitem.WhenFound = time;
                keyitem.IsFound = isfound;
            }
        }

        return updated;
    }
}
