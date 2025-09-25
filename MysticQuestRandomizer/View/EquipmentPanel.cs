using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MysticQuestRandomizer.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.MysticQuestRandomizer.View;
internal partial class EquipmentPanel : FlowPanel<EquipmentSettings>
{
    public EquipmentPanel() :base()
    {
        WrapAfter = 8;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    protected override Control[] GenerateControls(Seed seed)
    {
        if (Game == null || Settings == null)
            return [];

        _weapons = Game.Weapons.Select(w => new WeaponControl(Game, Settings, w)).ToList();
        _armors = Game.Armors.Select(a => new ArmorControl(Game, Settings, a)).ToList();
        _spells = Game.Spells.Select(a => new SpellControl(Game, Settings, a)).ToList();
        _keyItems = Game.KeyItems.Select(a => new KeyItemControl(Game, Settings, a)).ToList();
        _skyShards = new SkyShardsStats(Game, Settings);

        return Enumerable.Empty<Control>()
            .Concat(_weapons)
            .Concat(_armors)
            .Concat(_spells)
            .Concat(_keyItems)
            .Concat(_skyShards.Yield())
            .ToArray();
    }

    protected override int GetItemWidth(ControlCollection controlCollection)
    {
        return Enumerable.Empty<Control>()
            .Concat(_weapons)
            .Concat(_armors)
            .Concat(_spells)
            .Concat(_keyItems).Max(c=> c.Width) + 8;
    }

    protected override void SortControls(ControlCollection controlCollection, int columns)
    {
        if (columns == 8)
        {
            var left = Enumerable.Empty<Control>().Concat(_weapons).Concat(_armors).Concat(_spells).ToList();

            int leftCount = 0;
            int rightCount = 0;

            for (int i = 0; i < controlCollection.Count-1; i++)
            {
                if (i % 8 < 4)
                    controlCollection.SetChildIndex(left[leftCount++], i);
                else
                    controlCollection.SetChildIndex(_keyItems[rightCount++], i);
            }
        }
        else
            base.SortControls(controlCollection, columns);
    }

    private List<WeaponControl> _weapons = [];
    private List<ArmorControl> _armors = [];
    private List<SpellControl> _spells = [];
    private List<KeyItemControl> _keyItems = [];
    private SkyShardsStats? _skyShards;
}
