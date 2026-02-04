using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Rendering.Transforms;
using FF.Rando.Companion.View;
using KGySoft.Drawing.Imaging;
using KGySoft.Drawing.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

public class Check : IDisposable, INotifyPropertyChanged, IImageWithOverlay
{
    private readonly Seed _seed;

    private Bitmap? _image;
    private Bitmap? _overlay;
    private ISprite? _overlaySprite;
    private TimeSpan? _whenCompleted;
    private Reward? _reward;
    private bool _isCompleted;
    private bool _isAvailable;
    private bool _isVisible = true;
    private List<Check> _linkedChecks = [];
    private string _description = "";

    public Check(Seed seed, Events @event)
    {
        _seed = seed;
        Event = @event;
        CharacterGate = Event.GetRequirements().FirstOrDefault(r => r.IsCharacter());
        seed.PropertyChanged += Settings_PropertyChanged;
        Description = CreateDescription();
        SetImage();
    }

    private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Seed.SpriteSet))
        {
            SetImage();
        }
    }

    public int Id => (int)Event;
    public Events Event { get; }

    public List<Check> LinkedChecks
    {
        get => _linkedChecks;
        set
        {
            if (_linkedChecks.Select(c => c.Id).ToHashSet().SetEquals(value.Select(c => c.Id)))
                return;

            foreach (var check in _linkedChecks)
                check.PropertyChanged -= Check_PropertyChanged;

            _linkedChecks = value;

            foreach (var check in _linkedChecks)
                check.PropertyChanged += Check_PropertyChanged;

            NotifyPropertyChanged();
            Description = CreateDescription();
            SetImage();
        }
    }

    private string CreateDescription()
    {
        var description = $"{_seed.Descriptors.GetDescription(Event)}\n";
        var req = Event.GetRequirements();

        if (req.Count > 0)
        {
            description += "\nRequirements:\n";
            description += string.Join("\n", req.Select(_seed.Descriptors.GetDescription));
        }

        if (Reward.HasValue && WhenCompleted.HasValue)
        {
            description += "\n\nRewards:\n";
            description += $"{_seed.Descriptors.GetDescription(Reward.Value)} at {WhenCompleted.Value:hh':'mm':'ss'.'ff}\n";
        }

        foreach (var check in _linkedChecks)
        {
            description += "\n\n";
            description += check.CreateDescription();
        }

        return description;
    }

    private void Check_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Reward))
        {
            Description = CreateDescription();
        }
        if (e.PropertyName == nameof(Overlay))
        {
            SetImage();
        }
    }

    public Events? CharacterGate { get; }

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

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            if (_isCompleted == value)
                return;

            _isCompleted = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }

    public bool IsAvailable
    {
        get => _isAvailable;
        set
        {
            if (_isAvailable == value)
                return;

            _isAvailable = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible == value)
                return;

            _isVisible = value;
            NotifyPropertyChanged();
        }
    }

    public TimeSpan? WhenCompleted
    {
        get => _whenCompleted;
        set
        {
            if (_whenCompleted == value || _whenCompleted.HasValue)
                return;

            _whenCompleted = value;
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

    public Size DefaultSize => new(40, 40);

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetImage()
    {
        var sprite = _seed.SpriteSet.Get(Event);
        if (sprite != null)
            Image = sprite.Render(!IsAvailable);

        if (!IsAvailable)
        {
            Overlay = null;
        }
        else
        {
            var rewards = Reward.Yield().Concat(LinkedChecks.Select(c => c.Reward));
            var overlayparts = new List<ISprite>();
            ISprite? overlay = null;
            foreach (var reward in Reward.Yield().Concat(LinkedChecks.Select(c => c.Reward)))
            {
                var characterReward = reward.ToCharacter();
                var esper = reward.ToEsper();

                if (characterReward.HasValue)
                    overlayparts.Add(_seed.Sprites.Characters.Get(characterReward.Value, Pose.Celebrate1)!);
                else if (esper.HasValue)
                    overlayparts.Add(_seed.Sprites.Items.Get(Item.Magicite));
                else if (reward.HasValue)
                    overlayparts.Add(_seed.Sprites.Items.Get(Item.Chest));
            }

            if (overlayparts.Any())
            {
                var width = Math.Max(32, (LinkedChecks.Count + 1) * 16);
                var bmpData = BitmapDataFactory.CreateBitmapData(width, width);
                if (overlayparts.Count == (LinkedChecks.Count + 1))
                    bmpData.FillRectangle(new Color32(96, 0, 0, 0), new Rectangle(Point.Empty, bmpData.Size));

                overlay = new BasicSprite(bmpData);
                var x = width;

                foreach (var part in overlayparts.Reverse<ISprite>())
                {
                    x -= part.Size.Width;
                    overlay = overlay.Overlay(part, new Point(x, overlay.Size.Height - part.Size.Height));
                }
            }

            if (_overlaySprite is ITemporarySprite)
                _overlaySprite.Dispose();

            _overlaySprite = overlay;

            Overlay = _overlaySprite?.Render();
        }
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
