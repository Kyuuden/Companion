using FF.Rando.Companion.Settings;
using System;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;

public class ImageWithOverlayControl<TGame, TImageSource> : PictureBox, IScalableControl where TGame : IGame where TImageSource : IImageWithOverlay
{
    public TImageSource Value { get; }
    protected TGame Game { get; }
    protected PanelSettings Settings { get; }

    public ImageWithOverlayControl(TGame seed, PanelSettings settings, TImageSource item)
    {
        Game = seed ?? throw new ArgumentNullException();
        Value = item ?? throw new ArgumentNullException();
        Settings = settings ?? throw new ArgumentNullException();

        ((System.ComponentModel.ISupportInitialize)this).BeginInit();
        SuspendLayout();
        MinimumSize = Size = Value.DefaultSize;
        DoubleBuffered = true;
        BackColor = Game.BackgroundColor;
        Margin = new Padding(4);
        BackgroundImageLayout = ImageLayout.Zoom;
        SizeMode = PictureBoxSizeMode.Zoom;
        Value.PropertyChanged += PropertyChanged;
        Game.PropertyChanged += PropertyChanged;
        Settings.PropertyChanged += PropertyChanged;
        Name = "ImageControl";
        UpdateImages();
        ((System.ComponentModel.ISupportInitialize)this).EndInit();
        ResumeLayout(false);
    }

    protected virtual void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (Value == null)
            return;

        switch (e.PropertyName)
        {
            case nameof(IImageWithOverlay.Image):
            case nameof(IImageWithOverlay.Overlay):
                UpdateImages();
                break;
            case nameof(IGame.BackgroundColor):
                BackColor = Game.BackgroundColor;
                break;
        }
    }

    protected virtual void UpdateImages()
    {
        BackgroundImage = Value.Image;
        Image = Value.Overlay;
        Size = Value.DefaultSize.Scale(Settings.ScaleFactor);
    }

    public virtual void Rescale()
    {
        Size = Value.DefaultSize.Scale(Settings.ScaleFactor);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Value != null)
                Value.PropertyChanged -= PropertyChanged;

            if (Game != null)
                Game.PropertyChanged -= PropertyChanged;

            if (Settings != null)
                Settings.PropertyChanged -= PropertyChanged;
        }

        base.Dispose(disposing);
    }
}