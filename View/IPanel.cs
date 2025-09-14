using System.Windows.Forms;

namespace FF.Rando.Companion.View;

public interface IPanel
{
    DockStyle DefaultDockStyle { get; }

    bool CanHaveFillDockStyle { get; }

    int Priority { get; }

    bool IsEnabled { get; }
}
