using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MysticQuestRandomizer.RomData;
using FF.Rando.Companion.View;
using KGySoft.Drawing;
using KGySoft.Drawing.Imaging;
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
    private bool _isUsed;
    private TimeSpan? _whenUsed;
    private readonly Sprites _sprites;
    private readonly bool _blankWhenNotFound;
    private readonly Func<GameState, bool> _usedCheck;

    internal KeyItem(KeyItemType type, Sprites sprites, Func<GameState, bool> usedCheck, bool blankWhenNotFound = false, KeyItemType? imageKeyItemType = null)
    {
        Id = (int)(type);
        Type = type;
        _itemType = imageKeyItemType ?? type;
        _sprites = sprites;
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

    public TimeSpan? WhenUsed
    {
        get => _whenUsed;
        set
        {
            if (_whenUsed == value || _whenUsed.HasValue)
                return;

            _whenUsed = value;
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
        if (_blankWhenNotFound && !IsFound)
            Image = new Bitmap(16, 16);
        else
        {
            if (IsUsed)
            {
                var keyItemImage = _sprites.GetKeyItemData(_itemType, IsFound);
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
                Image = _sprites.GetKeyItem(_itemType, IsFound);
            }
        }
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
            new KeyItem(KeyItemType.Elixer, sprites, s => s.KaeliCured),
            new KeyItem(KeyItemType.TreeWither, sprites, s=> s.MinotaurDefeated),
            new KeyItem(KeyItemType.WakeWater, sprites, s => s.WakeWaterUsed),
            new KeyItem(KeyItemType.VenusKey, sprites, s => !s.VenusChestUnopened ),
            new KeyItem(KeyItemType.MultiKey, sprites, s=> s.TalkToGrenadeGuy),
            new KeyItem(KeyItemType.GasMask, sprites, s=> false),
            new KeyItem(KeyItemType.MagicMirror, sprites, s => false),
            new KeyItem(KeyItemType.ThunderRock, sprites, s => s.RainbowRoad),
            new KeyItem(KeyItemType.CapitansCap, sprites, s=> s.SpencerItemGiven),
            new KeyItem(KeyItemType.LibraCrest, sprites, s=> false),
            new KeyItem(KeyItemType.GeminiCrest, sprites, s=> false),
            new KeyItem(KeyItemType.MobiusCrest, sprites, s=> false),
            new KeyItem(KeyItemType.SandCoin, sprites, s=> s.SandCoinUsed),
            new KeyItem(KeyItemType.RiverCoin, sprites, s=> s.UseRiverCoin),
            new KeyItem(KeyItemType.SunCoin, sprites, s=> false),
            new KeyItem(KeyItemType.SkyCoin, sprites, s=> false, shatteredSkyCoin, KeyItemType.CompleteSkyCoin),
        ];

    public IReadOnlyList<KeyItem> Items => _items;

    public bool Update(TimeSpan time, ReadOnlySpan<byte> found, bool? skyCoinComplete)
    {
        var updated = false;

        foreach (var keyitem in _items)
        {
            var isfound = found.Read<bool>(keyitem.Id);
            if (keyitem.Type == KeyItemType.SkyCoin && skyCoinComplete.HasValue)
            {
                if (skyCoinComplete.Value != keyitem.IsFound)
                {
                    updated = true;
                    keyitem.WhenFound = time;
                    keyitem.IsFound = isfound;
                }
            }
            else if (isfound != keyitem.IsFound)
            {
                updated = true;
                keyitem.WhenFound = time;
                keyitem.IsFound = isfound;
            }
        }

        return updated;
    }

    public bool UpdateUsed(TimeSpan time, GameState flags)
    {
        var updated = false;

        foreach (var keyitem in _items)
        {
            var isUsed = keyitem.CheckUsed(flags);
            if (isUsed != keyitem.IsUsed)
            {
                updated = true;
                keyitem.WhenUsed = time;
                keyitem.IsUsed = isUsed;
            }
        }

        return updated;
    }
}
