using System;
using System.Windows.Forms;
using FF.Rando.Companion.FreeEnterprise.Settings;

namespace FF.Rando.Companion.FreeEnterprise.View;
public class ImageControl<T> : PictureBox, IScalableControl where T : IImageTracker
{
    protected T Value { get; }
    protected ISeed Seed { get; }
    protected PanelSettings Settings { get;}

    public ImageControl(ISeed seed, PanelSettings settings, T item)
    {
        Seed = seed ?? throw new ArgumentNullException();
        Value = item ?? throw new ArgumentNullException();
        Settings = settings ?? throw new ArgumentNullException();

        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        SuspendLayout();
        Size = new System.Drawing.Size(32, 32);
        DoubleBuffered = true;
        BackColor = Seed.BackgroundColor;
        Margin = new Padding(4);
        SizeMode = PictureBoxSizeMode.Zoom;
        Value.PropertyChanged += Value_PropertyChanged;
        Seed.PropertyChanged += Value_PropertyChanged;
        Name = "ImageControl";
        UpdateImage();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
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
            case nameof(ISeed.BackgroundColor):
                BackColor = Seed.BackgroundColor;
                break;
        }
    }

    protected virtual void UpdateImage()
    {
        MinimumSize = Value.Image.Size;
        Image = Value.Image;
        Size = Settings.Scale(Value.Image.Size);
    }

    public virtual void Rescale()
    {
        Size = Settings.Scale(Image.Size);
    }
}
