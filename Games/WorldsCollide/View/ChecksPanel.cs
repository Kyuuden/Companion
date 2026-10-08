using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.Games.WorldsCollide.Tracking;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

internal partial class ChecksPanel : FlowLayoutPanel
{
    private readonly SemaphoreSlim arrageSemaphore = new(1);
    private const int Columns = 10;
    private readonly Seed _seed;

    public ChecksPanel(Seed seed)
    {
        _seed = seed ?? throw new ArgumentNullException(nameof(seed));
        DoubleBuffered = true;
        BackgroundImageLayout = ImageLayout.Stretch;
        Margin = new(0);

        _seed.PropertyChanged += SettingsChanged;
        _seed.Settings.PropertyChanged += SettingsChanged;
        _seed.Settings.BorderSettings.PropertyChanged += SettingsChanged;

        var distinctChecks = _seed.State.Worlds.Items
            .SelectMany(w => w.Locations.SelectMany(l => l.Checks))
            .Distinct(new CheckComparer())
            .ToList();

        var eventChecks = distinctChecks.Where(c => (c is IEventCheck bc && bc.Event.IsCheck()) || c is ProgressiveCheck).ToList();
        var gatedEventChecks = eventChecks.OfType<ICharacterGate>().ToList();
        var checksByCharacter = gatedEventChecks.GroupBy(c => c.CharacterGate).ToDictionary(g => g.Key, g => new List<ICheck>([_seed.State.GetCharacter(g.Key), .. g.OfType<ICheck>()]));
        var openChecks = eventChecks.Except(gatedEventChecks.OfType<ICheck>()).ToList();
        var dragons = distinctChecks.Where(c => c is BasicCheck bc && bc.Event.IsDragonLocation()).ToList();
        var statues = distinctChecks.Where(c => (c is IEventCheck bc && bc.Event.IsStatue())).ToList();

        var checksInOrder = new List<ICheck?>();
        checksInOrder.AddRange(checksByCharacter[EventType.LOCKE_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.TERRA_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.EDGAR_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.SABIN_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.CELES_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.RELM_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.GAU_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.SHADOW_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.SETZER_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.MOG_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.CYAN_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.STRAGO_IN_PARTY]);
        checksInOrder.AddRange(checksByCharacter[EventType.UMARO_IN_PARTY]);
        checksInOrder.AddRange(openChecks);
        checksInOrder.Add(null);
        checksInOrder.Add(null);
        checksInOrder.AddRange(checksByCharacter[EventType.GOGO_IN_PARTY]);
        checksInOrder.Add(null);
        checksInOrder.AddRange(dragons);
        //checksInOrder.AddRange(openChecks.Take(3));
        //checksInOrder.AddRange(dragons.Take(4));
        //checksInOrder.Add(null);
        //checksInOrder.AddRange(checksByCharacter[EventType.GOGO_IN_PARTY]);
        //checksInOrder.AddRange(openChecks.Skip(3));
        //checksInOrder.AddRange(dragons.Skip(4));
        //checksInOrder.AddRange(statues);

        SuspendLayout();
        BackColor = _seed.BackgroundColor;
        int col = 0;
        foreach (var check in checksInOrder)
        {
            var control = new CheckControl(check, _seed) { Margin = new(2) };
            Controls.Add(control);
            col++;
            SetFlowBreak(control, col == Columns);
            if (col == Columns)
                col = 0;
        }
        ResumeLayout(false);
        PerformLayout();
    }

    private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Seed.SelectedBackground):
            case nameof(Seed.BackgroundPalettes):
            case nameof(WorldsCollideBorderSettings.BackgroundsEnabled):
                ReplaceBackgroundImage(true);
                break;
            case nameof(BorderSettings.BordersEnabled):
            case nameof(BorderSettings.BorderScaleFactor):
                Arrange();
                break;
        }
    }

    private Size _lastSize;

    protected override void OnDockChanged(EventArgs e)
    {
        if (IsHandleCreated)
            Arrange();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (_lastSize.IsEmpty || _lastSize != Size)
        {
#if DEBUG
            Debug.WriteLine($"Checks Arranging: {_lastSize} to {Size}");
#endif
            _lastSize = Size;
            Arrange();
            
        }

        if (Parent == null)
            return;

        switch (Dock)
        {
            case DockStyle.Top:
                Location = new Point(0, Parent.Padding.Top);
                break;
            case DockStyle.Bottom:
                Location = new Point(0, Parent.Bottom - Parent.Padding.Bottom - Height);
                break;
        }
    }

#if DEBUG
    protected override void OnMove(EventArgs e)
    {
        Debug.WriteLine($"Checks Moved: {Size} at {Location} on {Parent?.Size}");
    }
#endif

    private void Arrange()
    {
        if (_seed == null || Controls.Count == 0)
            return;

        if (!Visible)
            return;

        if (!arrageSemaphore.Wait(0))
            return;

        try
        {
            SuspendLayout();
            Padding = _seed.Settings.BorderSettings.BordersEnabled
                ? new Padding(_seed.Settings.BorderSettings.BorderScaleFactor.TileSize())
                : new Padding(0);

            var paddedWidth = Width - Padding.Horizontal - (Columns * 4);
            if (paddedWidth < Columns)
                return;

            var itemSize = paddedWidth / Columns;

            _seed.State.EffectiveZoomRate = itemSize / 40.0f;

            foreach (Control c in Controls)
            {
                c.SuspendLayout();
                c.Size = new Size(itemSize, itemSize);
                c.ResumeLayout(false);
            }
            ResumeLayout(false);
            PerformLayout();

            var bot = Controls.OfType<Control>().OrderByDescending(c => c.Bottom).FirstOrDefault();
            Height = bot.Bottom + bot.Margin.Bottom + Padding.Bottom;
            Refresh();

            if (_seed.Settings.BorderSettings.BordersEnabled)
            {
                ReplaceBackgroundImage();
            }
            else
            {
                BackgroundImage?.Dispose();
                BackgroundImage = null;
            }
        }
        finally
        {
            arrageSemaphore.Release();
        }
    }

    protected void ReplaceBackgroundImage(bool force = false)
    {
        if (_seed == null)
            return;

        var unscaledSize = Size.Unscale(_seed.Settings.BorderSettings.BorderScaleFactor);
        if (unscaledSize == BackgroundImage?.Size && !force)
            return;

        BackgroundImage?.Dispose();
        BackgroundImage = null;
        BackgroundImage = _seed?.Backgrounds.Render(
            _seed.SelectedBackground,
            unscaledSize,
            _seed.Settings.BorderSettings.BordersEnabled,
            ((WorldsCollideBorderSettings)_seed.Settings.BorderSettings).BackgroundsEnabled);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            if (_seed == null)
                return;

            _seed.PropertyChanged -= SettingsChanged;
            _seed.Settings.PropertyChanged -= SettingsChanged;
            _seed.Settings.BorderSettings.PropertyChanged -= SettingsChanged;
        }
    }
}