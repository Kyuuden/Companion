using FF.Rando.Companion.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;
public class ImageControl<TGame, TImageSource> : PictureBox, IScalableControl where TGame : IGame where TImageSource : IImageTracker
{
    public TImageSource Value { get; }
    protected TGame Game { get; }
    protected PanelSettings Settings { get; }

    public ImageControl(TGame seed, PanelSettings settings, TImageSource item)
    {
        Game = seed ?? throw new ArgumentNullException();
        Value = item ?? throw new ArgumentNullException();
        Settings = settings ?? throw new ArgumentNullException();

        ((System.ComponentModel.ISupportInitialize)this).BeginInit();
        SuspendLayout();
        Size = new System.Drawing.Size(32, 32);
        DoubleBuffered = true;
        BackColor = Game.BackgroundColor;
        Margin = new Padding(4);
        SizeMode = PictureBoxSizeMode.Zoom;
        Value.PropertyChanged += Value_PropertyChanged;
        Game.PropertyChanged += Value_PropertyChanged;
        Settings.PropertyChanged += Value_PropertyChanged;
        Name = "ImageControl";
        UpdateImage();
        ((System.ComponentModel.ISupportInitialize)this).EndInit();
        ResumeLayout(false);
    }

    protected virtual void Value_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (Value == null)
            return;

        switch (e.PropertyName)
        {
            case nameof(IImageTracker.Image):
                UpdateImage();
                break;
            case nameof(IGame.BackgroundColor):
                BackColor = Game.BackgroundColor;
                break;
        }
    }

    protected virtual Size ImageSize => Value.Image.Size;

    protected virtual void UpdateImage()
    {
        if (Value.Image == null)
            return;

        MinimumSize = ImageSize;
        Image = Value.Image;
        Size = ImageSize.Scale(Settings.ScaleFactor);
    }

    public virtual void Rescale()
    {
        if (Value.Image == null)
            return;

        Size = ImageSize.Scale(Settings.ScaleFactor);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Value != null)
                Value.PropertyChanged -= Value_PropertyChanged;
            
            if (Game != null)
                Game.PropertyChanged -= Value_PropertyChanged;
            
            if (Settings != null)
                Settings.PropertyChanged -= Value_PropertyChanged;
        }

        base.Dispose(disposing);
    }
}
