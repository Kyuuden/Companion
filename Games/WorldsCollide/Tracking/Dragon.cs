using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Rendering.Transforms;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing.Shapes;
using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using static BizHawk.Common.XlibImports;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

public class Dragon : IDisposable, INotifyPropertyChanged, IImageWithOverlay
{
    private readonly Seed _seed;

    private Bitmap? _image;
    private Bitmap? _overlay;
    private ISprite? _overlaySprite;
    private Reward? _reward;
    private TimeSpan? _whenDefeated;
    private bool _isDefeated;
    private string _description = "";

    public Dragon(Seed seed, Enums.Dragons dragon)
    {
        _seed = seed;
        DragonType = dragon;
        Description = _seed.Descriptors.GetDescription(dragon);
        _seed.PropertyChanged += Settings_PropertyChanged;
        Description = CreateDescription();
        SetImage();
    }

    public Size DefaultSize => new(40, 40);

    private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Seed.SpriteSet))
        {
            SetImage();
        }
    }

    public int Id => (int)DragonType;
    public Enums.Dragons DragonType { get; }
    public string Description
    {
        get => _description;
        private set
        {
            if (_description == value)
                return;

            _description = value;
            NotifyPropertyChanged();
        }
    }

    public bool IsDefeated
    {
        get => _isDefeated;
        set
        {
            if (_isDefeated == value)
                return;

            _isDefeated = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }

    public TimeSpan? WhenDefeated
    {
        get => _whenDefeated;
        set
        {
            if (_whenDefeated == value || _whenDefeated.HasValue)
                return;

            _whenDefeated = value;
            NotifyPropertyChanged();
        }
    }

    public Reward? Reward
    {
        get => _reward;
        set
        {
            if (_reward == value || _reward.HasValue)
                return;

            _reward = value;
            Description = CreateDescription();
            NotifyPropertyChanged();
            SetImage();
        }
    }

    public Bitmap? Image
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

    public Bitmap? Overlay
    {
        get => _overlay;
        set
        {
            if (_overlay == value)
                return;

            _overlay = value;
            NotifyPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetImage()
    {
        var sprite = _seed.SpriteSet.Get(DragonType);
        if (sprite != null)
            Image = sprite.Render();

        if (IsDefeated && Reward.HasValue)
        {
            ISprite? overlay;
            ISprite? overlaySprite = null;
            var characterReward = Reward.ToCharacter();
            var esper = Reward.ToEsper();

            if (characterReward.HasValue)
                overlay = _seed.Sprites.Characters.Get(characterReward.Value, Pose.Celebrate1);
            else if (esper.HasValue)
                overlay= _seed.Sprites.Items.Get(Item.Magicite);
            else
                overlay = _seed.Sprites.Items.Get(Item.Chest);

            if (overlay != null)
            {
                var bmpData = BitmapDataFactory.CreateBitmapData(32, 32);
                bmpData.FillRectangle(new Color32(96, 0, 0, 0), new Rectangle(Point.Empty, bmpData.Size));

                overlaySprite = new BasicSprite(bmpData);
                overlaySprite = overlaySprite.Overlay(overlay, new Point(bmpData.Width - overlay.Size.Width, bmpData.Height - overlay.Size.Height));
            }

            if (_overlaySprite is ITemporarySprite)
                _overlaySprite.Dispose();

            _overlaySprite = overlaySprite;

            Overlay = _overlaySprite?.Render();
        }
        else
        {
            if (_overlaySprite is ITemporarySprite)
                _overlaySprite.Dispose();

            _overlaySprite = null;
            Overlay = null;
        }
    }

    private string CreateDescription()
    {
        var description = $"{_seed.Descriptors.GetDescription(DragonType)}\n";

        if (Reward.HasValue && WhenDefeated.HasValue)
        {
            description += "\n\nRewards:\n";
            description += $"{_seed.Descriptors.GetDescription(Reward.Value)} at {WhenDefeated.Value:hh':'mm':'ss'.'ff}\n";
        }

        return description;
    }

    public void Dispose()
    {
        if (_overlaySprite is ITemporarySprite)
        {
            _overlaySprite.Dispose();
            _overlaySprite = null;
        }

        _seed.PropertyChanged -= Settings_PropertyChanged;
    }
}
