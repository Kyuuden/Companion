using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FF.Rando.Companion.FreeEnterprise.Settings;
using KGySoft.Drawing.Imaging;

namespace FF.Rando.Companion.FreeEnterprise.View;

public abstract class StatisticControl<T> : PictureBox, IScalableControl where T : struct
{
    protected T Stat { get; private set; }
    protected ISeed Seed { get; private set; }
    protected PanelSettings Settings { get; private set; }

    protected abstract T GetStat();
    protected abstract string PropertyName { get; }
    protected abstract string GetStatText();
    protected abstract IReadableBitmapData GetIcon();


    public StatisticControl(ISeed seed, PanelSettings settings)
    {
        Seed = seed ?? throw new ArgumentNullException();
        Settings = settings ?? throw new ArgumentNullException();

        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        SuspendLayout();
        Size = new System.Drawing.Size(56, 16);
        DoubleBuffered = true;
        BackColor = Seed.BackgroundColor;
        Margin = new Padding(4);
        SizeMode = PictureBoxSizeMode.Zoom;
        Name = "ImageControl";
        Stat = GetStat();
        UpdateImage();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        ResumeLayout(false);

        Seed.PropertyChanged += Seed_PropertyChanged;
    }

    private void Seed_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ISeed.BackgroundColor))
            BackColor = Seed.BackgroundColor;

        if (e.PropertyName != PropertyName)
            return;

        var newStat = GetStat();
        if (newStat.Equals(Stat)) return;

        Stat = newStat;
        UpdateImage();
    }

    private void UpdateImage()
    {
        MinimumSize = new System.Drawing.Size(112, 32);

        var icon = GetIcon();
        var text = Seed.Font.RenderText(GetStatText(), RomData.TextMode.Normal);

        var colors = new List<Color32>();
        foreach (var c in icon.Palette?.GetEntries() ?? []) colors.Add(c);
        foreach (var c in text.Palette?.GetEntries() ?? []) colors.Add(c);

        var combined = new Palette(colors);

        var data = BitmapDataFactory.CreateBitmapData(MinimumSize, KnownPixelFormat.Format8bppIndexed, combined);

        icon.DrawInto(data, new System.Drawing.Rectangle(0,0,32,32), KGySoft.Drawing.ScalingMode.NearestNeighbor);
        text.DrawInto(data, new System.Drawing.Rectangle(32, 8, 80, 16), KGySoft.Drawing.ScalingMode.NearestNeighbor);

        Image = data.ToBitmap();
        Size = Settings.Scale(Image.Size);
    }

    public void Rescale()
    {
        Size = Settings.Scale(Image.Size);
    }
}
