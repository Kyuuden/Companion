using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

internal class EventStatisticControl : StatisticControl<bool, Seed>
{
    private readonly ToolTip _toolTip;
    public EventType Event { get; }

    protected override bool GetStat() => Game.State.Events[Event];

    protected override string PropertyName => nameof(State.Events);

    internal EventStatisticControl(Seed seed, IPanelSettings settings, EventType eventType)
    : base(seed, settings, new Size(40, 40))
    {
        BackColor = Color.Transparent;
        Event = eventType;
        UpdateImage();
        seed.State.PropertyChanged += Seed_PropertyChanged;
        _toolTip = new ToolTip { ShowAlways = true };
        _toolTip.SetToolTip(this, Event.GetDescription());
    }

    protected override void Seed_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Seed_PropertyChanged(sender, e);
        switch (e.PropertyName)
        {
            case nameof(Seed.SpriteSet):
                UpdateImage();
                break;
        }
    }

    protected override void UpdateImage()
    {
        Image = Render();
    }

    protected override Image? Render()
    {
        return Game.SpriteSet?.Get(Event)?.Render(!GetStat());
    }
}
