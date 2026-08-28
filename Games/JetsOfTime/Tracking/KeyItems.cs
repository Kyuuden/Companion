using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using FF.Rando.Companion.Rendering;
using FF.Rando.Companion.Timing;
using KGySoft.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class KeyItems : INotifyPropertyChanged
{
    private readonly ITimer _timer;
    private readonly IFlags _flags;
    private readonly Sprites _spriteDB;
    private readonly Dictionary<KeyItemType, KeyItemBase> _items;

    private readonly Dictionary<GameMode, List<KeyItemType>> _keyItemsByGameMode = new()
    {
        {
            GameMode.Standard,
            [
                KeyItemType.BentHilt,
                KeyItemType.BentSword,
                KeyItemType.Masamune,
                KeyItemType.GrandLeon,
                KeyItemType.HerosMedal,
                KeyItemType.RobosRibbon,

                KeyItemType.GateKey,
                KeyItemType.Dreamstone,
                KeyItemType.RubyKnife,
                KeyItemType.PrismShard,
                KeyItemType.MoonStone,
                KeyItemType.Jerky,

                KeyItemType.Pendant,
                KeyItemType.Clone,
                KeyItemType.ChronoTrigger,
                KeyItemType.TomasPop,
                KeyItemType.Magic,
                KeyItemType.JetsOfTime,
            ]
        },
        { 
            GameMode.LostWorlds,
            [
                KeyItemType.Pendant,
                KeyItemType.Clone,
                KeyItemType.ChronoTrigger,
                KeyItemType.Dreamstone,
                KeyItemType.RubyKnife,
            ]
        },
        { 
            GameMode.LegacyOfCyrus,
            [
                KeyItemType.BentHilt,
                KeyItemType.BentSword,
                KeyItemType.Masamune,
                KeyItemType.GrandLeon,
                KeyItemType.HerosMedal,
                KeyItemType.GateKey,
                KeyItemType.Pendant,
                KeyItemType.PrismShard,
                KeyItemType.TomasPop,
                KeyItemType.Jerky,
                KeyItemType.Dreamstone,
                KeyItemType.RobosRibbon,
                KeyItemType.JetsOfTime,
            ]
        },
        { 
            GameMode.IceAge,
            [
                KeyItemType.BentHilt,
                KeyItemType.BentSword,
                KeyItemType.Masamune,
                KeyItemType.GrandLeon,
                KeyItemType.HerosMedal,
                KeyItemType.RobosRibbon,
                KeyItemType.GateKey,
                KeyItemType.Dreamstone,
                KeyItemType.RubyKnife,
                KeyItemType.PrismShard,
                KeyItemType.MoonStone,
                KeyItemType.Jerky,
                KeyItemType.Pendant,
                KeyItemType.Clone,
                KeyItemType.ChronoTrigger,
                KeyItemType.TomasPop,
                KeyItemType.Magic,
                KeyItemType.JetsOfTime,
                KeyItemType.Tools,
            ]
        },
        { 
            GameMode.VanillaRando,
            [
                KeyItemType.BentHilt,
                KeyItemType.BentSword,
                KeyItemType.Masamune,
                KeyItemType.GrandLeon,
                KeyItemType.HerosMedal,
                KeyItemType.GateKey,
                KeyItemType.Dreamstone,
                KeyItemType.RubyKnife,
                KeyItemType.PrismShard,
                KeyItemType.MoonStone,
                KeyItemType.Jerky,
                KeyItemType.Pendant,
                KeyItemType.Clone,
                KeyItemType.ChronoTrigger,
                KeyItemType.TomasPop,
                KeyItemType.Magic,
                KeyItemType.JetsOfTime,
                KeyItemType.Tools,
            ]
        },
    };

    public event PropertyChangedEventHandler? PropertyChanged;

    public KeyItems(ITimer timer, IFlags flags, Sprites spriteDB)
    {
        _flags = flags;
        _timer = timer;
        _spriteDB = spriteDB;
        _flags.PropertyChanged += FlagsChanged;

        _items = new List<KeyItemType>(
        [
            KeyItemType.BentHilt,
            KeyItemType.BentSword,
            KeyItemType.Masamune,
            KeyItemType.GrandLeon,
            KeyItemType.HerosMedal,
            KeyItemType.RobosRibbon,

            KeyItemType.GateKey,
            KeyItemType.Dreamstone,
            KeyItemType.RubyKnife,
            KeyItemType.PrismShard,
            KeyItemType.MoonStone,
            KeyItemType.Jerky,

            KeyItemType.Pendant,
            KeyItemType.Clone,
            KeyItemType.ChronoTrigger,
            KeyItemType.TomasPop,
            KeyItemType.JetsOfTime,
            KeyItemType.Tools,

        ]).ToDictionary(t => t, CreateKeyItemTracker);

        FlagsChanged(null!, null!);
    }

    private void FlagsChanged(object sender, PropertyChangedEventArgs e)
    {
        foreach (var item in Items)
        {
            item.Exists = _keyItemsByGameMode[_flags.Mode ?? GameMode.Standard].Contains(item.Type);

            if (item.Type == KeyItemType.JetsOfTime)
                item.Exists = _flags.EpochFail ?? true;
        }
    }

    private KeyItemBase CreateKeyItemTracker(KeyItemType keyItemType)
    {
        return keyItemType switch
        {
            KeyItemType.MoonStone => new ProgressiveKeyItem(_timer, keyItemType, [CreateSprite(KeyItemType.MoonStone)!, CreateSprite(KeyItemType.SunStone)!], [KeyItemType.MoonStone.GetDescription(), KeyItemType.SunStone.GetDescription()]),
            _ => new KeyItem(_timer, keyItemType, CreateSprite(keyItemType)),
        };
    }

    private ISprite? CreateSprite(KeyItemType keyItemType)
    {
        return keyItemType switch
        {
            KeyItemType.Masamune => _spriteDB.GetNpc(NPCType.Melchior).Get(1)?
                                .Crop(new Rectangle(0, 0, 22, 32))
                                .Pad(new Size(32, 32)),

            KeyItemType.Pendant => _spriteDB.GetNpc(NPCType.Pendant).Get(0)?
                                .Pad(new Size(32, 32)),

            KeyItemType.GateKey => _spriteDB.GetNpc(NPCType.Gate_Key).Get(0)?
                                .Pad(new Size(32, 32)),

            KeyItemType.BentHilt => _spriteDB.GetNpc(NPCType.Broken_Masamune_blade).Get(1)?
                                .Pad(new Size(32, 32)),

            KeyItemType.BentSword => _spriteDB.GetNpc(NPCType.Broken_Masamune_blade).Get(0)?
                                .Pad(new Size(32, 32)),

            KeyItemType.HerosMedal => _spriteDB.GetNpc(NPCType.Heros_medal).Get(0)?
                                .Crop(new Rectangle(8, 8, 8, 8))
                                .Resize(new Size(16, 16))
                                .Pad(new Size(32, 32)),

            KeyItemType.Dreamstone => _spriteDB.GetNpc(NPCType.Dreamstone).Get(0)?
                                .Pad(new Size(32, 32)),

            KeyItemType.RobosRibbon => _spriteDB.GetMonster(MonsterType.AtroposXR).Get(4)?
                                .Crop(new Rectangle(0, 0, 24, 32))
                                .Pad(new Size(32, 32)),

            KeyItemType.PrismShard => _spriteDB.GetNpc(NPCType.Rainbow_shell).Get(0),

            KeyItemType.Jerky => _spriteDB.GetNpc(NPCType.Pink_lunch_bag).Get(0)?
                                .Pad(new Size(32, 32)),

            KeyItemType.MoonStone => _spriteDB.GetNpc(NPCType.Dead_sunstone).Get(0)?
                                .Pad(new Size(32, 32)),

            KeyItemType.SunStone => _spriteDB.GetNpc(NPCType.Dead_sunstone).Get(0)?
                                .TransformColors(color =>
                                {
                                    return color.ToArgbUInt32() switch
                                    {
                                        0xFFDECD9C => new Color32(246, 241, 219),
                                        0xFFAC946A => new Color32(232, 255, 160),
                                        0xFF735A39 => new Color32(216, 198, 97),
                                        0xFF413118 => new Color32(177, 138, 57),
                                        0xFF292010 => new Color32(152, 123, 55),
                                        0xFFF6F6FF => new Color32(252, 252, 246),
                                        0xFFC5C5D5 => new Color32(239, 239, 234),
                                        0xFF5A5A52 => new Color32(212, 206, 132),
                                        0xFF313931 => new Color32(165, 160, 92),
                                        0xFF292929 => new Color32(156, 139, 85),
                                        _ => color
                                    };
                                })
                                .Pad(new Size(32, 32)),

            KeyItemType.Clone => _spriteDB.GetCharacter(CharacterType.Chrono).Get(235)?
                                .Crop(new Rectangle(0, 14, 32, 32)),

            KeyItemType.ChronoTrigger => _spriteDB.GetNpc(NPCType.Time_egg).Get(0)?
                                .Crop(new Rectangle(0, 0, 8, 8))
                                .Resize(new Size(16, 16))
                                .Pad(new Size(32, 32)),

            KeyItemType.TomasPop => _spriteDB.GetNpc(NPCType.Soda_can).Get(0)?
                                .Crop(new Rectangle(0, 3, 8, 12))
                                .Pad(new Size(12, 12))
                                .Resize(new Size(24, 24))
                                .Pad(new Size(32, 32)),

            KeyItemType.GrandLeon => _spriteDB.GetNpc(NPCType.Masamune_Spinning).Get(6),

            KeyItemType.RubyKnife => _spriteDB.GetNpc(NPCType.Red_knife).Get(0)?
                                .Pad(new Size(32, 32)),

            KeyItemType.JetsOfTime => _spriteDB.GetNpc(NPCType.Flying_map_Epoch).Get(0)?
                                .RotateFlip(RotateFlipType.Rotate180FlipNone),
            _ => null,
        };
    }

    public IEnumerable<KeyItemBase> Items => _items.Values;

    public bool IsFound(KeyItemType keyItem) =>
        keyItem switch
        {
            KeyItemType.MoonStone when _items[KeyItemType.MoonStone] is ProgressiveKeyItem pki => pki.Progress > 0,
            KeyItemType.SunStone when _items[KeyItemType.MoonStone] is ProgressiveKeyItem pki => pki.Progress == 2,
            _ => _items[keyItem].IsFound
        };

    public bool Update(ReadOnlySpan<byte> inventory, ReadOnlySpan<byte> events, ReadOnlySpan<byte> equipped)
    {
        var updated = false;
        HashSet<KeyItemType> changed = [];

        foreach (var item in Items)
        {
            var isFound = false; 

            switch (item.Type)
            {
                case KeyItemType.Pendant:
                case KeyItemType.GateKey:
                case KeyItemType.PrismShard:
                case KeyItemType.ChronoTrigger:
                case KeyItemType.Dreamstone:
                case KeyItemType.Clone:
                    isFound = inventory.IndexOf(item.Id) != -1;
                    break;

                case KeyItemType.Magic:
                    isFound = (events[0xE1] & 0x02) != 0;
                    break;

                case KeyItemType.Masamune:
                    isFound = (events[0x103] & 0x02) != 0;
                    break;

                case KeyItemType.HerosMedal:
                    isFound = (inventory.IndexOf(item.Id) != -1) || equipped[0x16A] == item.Id;
                    break;

                case KeyItemType.RobosRibbon:
                    isFound = (inventory.IndexOf(item.Id) != -1) || equipped[0x11A] == item.Id;
                    break;

                case KeyItemType.GrandLeon:
                    isFound = (inventory.IndexOf(item.Id) != -1) || equipped[0x169] == item.Id;
                    break;

                case KeyItemType.BentHilt:
                    isFound = (inventory.IndexOf(item.Id) != -1) || events.Read<bool>(0x103 * 8 + 1);
                    break;
                case KeyItemType.BentSword:
                    isFound = (inventory.IndexOf(item.Id) != -1) || events.Read<bool>(0x103 * 8 + 1);
                    break;
                case KeyItemType.Jerky:
                    isFound = (inventory.IndexOf(item.Id) != -1) || events.Read<bool>(0x1D2 * 8 + 2);
                    break;
                case KeyItemType.RubyKnife:
                    isFound = (inventory.IndexOf(item.Id) != -1) || events.Read<bool>(0xF4 * 8 + 7);
                    break;
                case KeyItemType.TomasPop:
                    isFound = (inventory.IndexOf(item.Id) != -1) || events.Read<bool>(0x1A3 * 8 + 7);
                    break;
                case KeyItemType.JetsOfTime:
                    isFound = (inventory.IndexOf(item.Id) != -1) || events.Read<bool>(0xBA * 8 + 7);
                    break;

                case KeyItemType.MoonStone when item is ProgressiveKeyItem moonStone:
                    var moonStoneFlags = events[0x13A];
                    var isDroppedOff = ((moonStoneFlags & 0x04) != 0) && ((moonStoneFlags & 0x40) == 0);

                    if (inventory.IndexOf((byte)KeyItemType.SunStone) != -1)
                    {
                        moonStone.Progress = 2;
                    }
                    else if (inventory.IndexOf ((byte)KeyItemType.MoonStone) != -1)
                    {
                        moonStone.Progress = 1;
                    }
                    else if (isDroppedOff)
                    {
                        moonStone.Progress = 1;
                    }
                    else
                    {
                        moonStone.Progress = 0;
                    }
                    break;

                case KeyItemType.SunStone:
                    isFound = inventory.IndexOf(item.Id) != -1;
                    break;
            }

            if (isFound != item.IsFound)
            {
                updated = true;
                item.IsFound = isFound;
                changed.Add(item.Type);
            }
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Join(";", changed)));
        return updated;
    }
}