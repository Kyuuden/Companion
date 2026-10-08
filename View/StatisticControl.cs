using FF.Rando.Companion.Games;
using FF.Rando.Companion.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.View;

public abstract class StatisticControl<T, TGame> : PictureBox, IScalableControl where TGame : IGame
{
    protected T Stat { get; private set; }
    protected TGame Game { get; private set; }
    protected IPanelSettings Settings { get; private set; }

    protected abstract T GetStat();
    protected abstract string PropertyName { get; }

    protected Size OriginalSize { get; }

    public StatisticControl(TGame game, IPanelSettings settings, Size defaultSize)
    {
        Game = game ?? throw new ArgumentNullException();
        Settings = settings ?? throw new ArgumentNullException();

        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        SuspendLayout();
        Size = OriginalSize = defaultSize;
        DoubleBuffered = true;
        BackColor = Game.BackgroundColor;
        Margin = new Padding(4);
        SizeMode = PictureBoxSizeMode.Zoom;
        BackgroundImageLayout = ImageLayout.Stretch;
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
        UpdateStat();
    }

    protected virtual void UpdateStat()
    {
        Stat = GetStat();
        UpdateImage();
    }

    protected virtual void UpdateImage()
    {
        Image?.Dispose();
        Image = null;
        Image = Render();
        Size = OriginalSize.Scale(Settings.ScaleFactor);
    }

    protected abstract Image? Render();

    public virtual void Rescale()
    {
        Size = OriginalSize.Scale(Settings.ScaleFactor);
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
