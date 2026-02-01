using FF.Rando.Companion.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;

public abstract class StatisticControl<T, TGame> : PictureBox, IScalableControl where TGame : IGame
{
    protected T Stat { get; private set; }
    protected TGame Game { get; private set; }
    protected PanelSettings Settings { get; private set; }

    protected abstract T GetStat();
    protected abstract string PropertyName { get; }


    public StatisticControl(TGame game, PanelSettings settings, Size minimumSize)
    {
        Game = game ?? throw new ArgumentNullException();
        Settings = settings ?? throw new ArgumentNullException();

        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        SuspendLayout();
        Size = new System.Drawing.Size(56, 16);
        MinimumSize = minimumSize;
        DoubleBuffered = true;
        BackColor = Game.BackgroundColor;
        Margin = new Padding(4);
        SizeMode = PictureBoxSizeMode.Zoom;
        Name = "ImageControl";
        Stat = GetStat();
        UpdateImage();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        ResumeLayout(false);

        Game.PropertyChanged += Seed_PropertyChanged;
    }

    protected virtual void Seed_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IGame.BackgroundColor))
            BackColor = Game.BackgroundColor;

        if (e.PropertyName != PropertyName)
            return;

        var newStat = GetStat();
        if (newStat?.Equals(Stat) == true) return;

        Stat = newStat;
        UpdateImage();
    }

    protected void UpdateImage()
    {
        Image?.Dispose();
        Image = null;
        Image = Render();
        Size = Image.Size.Scale(Settings.ScaleFactor);
    }

    protected abstract System.Drawing.Image Render();

    public void Rescale()
    {
        Size = Image.Size.Scale(Settings.ScaleFactor);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Game != null)
                Game.PropertyChanged -= Seed_PropertyChanged;
        }

        base.Dispose(disposing);
    }
}
