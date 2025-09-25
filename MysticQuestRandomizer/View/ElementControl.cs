using FF.Rando.Companion.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;

internal class ElementControl(Seed seed, ElementsSettings settings, Element element) : ImageControl<Seed, Element>(seed, settings, element)
{
    protected override void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Value_PropertyChanged(sender, e);

        if (e.PropertyName == nameof(ElementsSettings.HideUnchanged) && Value.IsUnchanged)
            UpdateImage();
    }

    protected override void UpdateImage()
    {
        if (Value.IsUnchanged)
            Visible = !settings.HideUnchanged;

        base.UpdateImage();
    }
}