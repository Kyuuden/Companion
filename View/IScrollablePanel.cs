using System;
using System.ComponentModel;

namespace FF.Rando.Companion.View;
internal interface IScrollablePanel : IPanel
{
    bool IsEnabledForScrolling { get; set; }
    void ScrollDown();
    void ScrollUp();
    void ScrollLeft();
    void ScrollRight();
    bool CanScroll { get; }
    event EventHandler? CanScrollChanged;
}
