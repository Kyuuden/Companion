using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class RotatingKeyItem : KeyItemBase
{
    private readonly List<NPCType> npcTypes = Enum.GetValues(typeof(NPCType)).Cast<NPCType>().ToList();
    private int _npcIndex = 38;
    private int _index = 0;
    private Timer _timer;
    private SpriteDB _spriteDB;

    public RotatingKeyItem(Container container, SpriteDB spriteDB, KeyItemType type)
        : base(container, type)
    {
        _spriteDB = spriteDB;
        _timer = new Timer
        {
            Interval = 750
        };
        _timer.Tick += _timer_Tick;
        _timer.Start();
        SetImage();
    }

    private void _timer_Tick(object sender, EventArgs e)
    {
        //_index++;
        //if (_index == _spriteDB.GetNpc(npcTypes[_npcIndex]).Count)
        //{
        //    _index = 0;
        //    _npcIndex++;
        //    _npcIndex %= npcTypes.Count;
        //    Debug.WriteLine(npcTypes[_npcIndex]);
        //}

        _npcIndex++;
        _npcIndex %= npcTypes.Count;
        Debug.WriteLine(npcTypes[_npcIndex]);

        SetImage();
    }

    protected override void SetImage()
    {
        var sprite = _spriteDB.GetNpc(npcTypes[_npcIndex]).Get(_index);
        if (sprite != null)
            Image = sprite.Render();// !IsFound);
    }
}
