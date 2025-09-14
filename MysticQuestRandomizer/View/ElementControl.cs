using FF.Rando.Companion.View;
using FF.Rando.Companion.MysticQuestRandomizer.Settings;
using FF.Rando.Companion.Settings;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;

internal class ElementControl(Seed seed, ElementsSettings settings, Element element) : ImageControl<Seed, Element>(seed, settings, element)
{
}