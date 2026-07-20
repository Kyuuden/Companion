using BizHawk.Client.EmuHawk;
using FF.Rando.Companion.Games.JetsOfTime.Rendering;
using FF.Rando.Companion.Games.WorldsCollide.Settings.SpriteSet;
using FF.Rando.Companion.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.Games.JetsOfTime.Tracking;

internal class Bosses
{
    private readonly Container _container;
    private readonly SpriteDB _spriteDB;
    private readonly LocationDB _locationDB;

    public Bosses(Container container, SpriteDB spriteDB, LocationDB locationDB)
    {
        _container = container;
        _spriteDB = spriteDB;
        _locationDB = locationDB;

        Values = new List<BossType>(
        [
            BossType.Yakra,
            BossType.YakraXIII,
            BossType.DragonTank,
            BossType.Zombor,
            BossType.Retinite,
            BossType.Heckran,
            BossType.BlackTyrano,
            BossType.RustTyrano,
            BossType.Guardian,
            BossType.RSeries,
            BossType.MasaAndMune,
            BossType.Nizbel,
            BossType.Magus,
            BossType.GigaGaia,
            BossType.MotherBrain,
            BossType.SunOfTheSun,
            BossType.Zeal,
            BossType.Golem
        ]).Select(CreateTracker).ToList();
    }

    public IReadOnlyList<Boss> Values { get; }

    private Boss CreateTracker(BossType type)
    {
        //return new Boss(_container, type, CreateTestSprites(type, MonsterType.Masamune, 0));
        return new Boss(_container, type, CreateSprite(type));
    }

    private ISprite? CreateTestSprites(BossType type, MonsterType monsterType, int offset)
    {
        return _spriteDB.GetMonster(monsterType).Get((int)type + offset)?.Crop(new Rectangle(8, 4, 48, 48));
    }

    private ISprite? CreateSprite(BossType type)
    {
        return type switch
        {
            BossType.Yakra => _spriteDB.GetMonster(MonsterType.Yakra).Get(17)?
                .Crop(new Rectangle(7, 0, 48, 48)),

            BossType.YakraXIII => _spriteDB.GetMonster(MonsterType.YakraXIII).Get(17)?
                .Crop(new Rectangle(7, 0, 48, 48)),

            BossType.DragonTank => _spriteDB.GetMonster(MonsterType.DragonTank).Get(0)?
                .Crop(new Rectangle(64, 8, 48, 48)),

            BossType.Guardian => _locationDB.Get(0xDB).Render(true, false, true)?
                .Crop(new Rectangle(64, 93, 128, 128)),

            BossType.RSeries => _spriteDB.GetMonster(MonsterType.RSeries).Get(49)?
                .Pad(new Size(48, 48)),

            BossType.Heckran => _spriteDB.GetMonster(MonsterType.Heckran).Get(04)?
                .Crop(new Rectangle(8, 0, 48, 48)),

            BossType.Zombor => _spriteDB.GetMonster(MonsterType.Zombor_LowerHalf).Get(1)?
                .Crop(new Rectangle(0, 0, 48, 16))
                .Pad(new Size(48, 48), HorizontalAlignment.Center, VerticalAlignment.Bottom)
                .Overlay(_spriteDB.GetMonster(MonsterType.Zombor_UpperHalf).Get(0)!, new Point(1, 0)),

            BossType.Retinite => _spriteDB.GetMonster(MonsterType.Zombor_SandMonster_LowerHalf).Get(1)?
                .Crop(new Rectangle(0, 0, 48, 8))
                .Pad(new Size(48, 48), HorizontalAlignment.Center, VerticalAlignment.Bottom)
                .Overlay(_spriteDB.GetMonster(MonsterType.Retinite).Get(0)!, new Point(8, 14))
                .Overlay(_spriteDB.GetMonster(MonsterType.Zombor_SandMonster_UpperHalf).Get(0)!, new Point(1, 0)),

            BossType.MasaAndMune => _spriteDB.GetMonster(MonsterType.Masamune).Get(4)?
                .Crop(new Rectangle(8, 02, 48, 48)),

            BossType.Nizbel => _spriteDB.GetMonster(MonsterType.Nizbel).Get(20)?
                .Crop(new Rectangle(8, 10, 48, 48)),

            BossType.Magus => _spriteDB.GetMonster(MonsterType.Magus3).Get(91)?
                .Pad(new Size(48, 48)),

            BossType.BlackTyrano => _spriteDB.GetMonster(MonsterType.BlackTyrano).Get(0)?
                .Crop(new Rectangle(0, 8, 48, 48)),

            BossType.RustTyrano => _spriteDB.GetMonster(MonsterType.RustTyrano).Get(0)?
                .Crop(new Rectangle(0, 8, 48, 48)),

            BossType.GigaGaia => _locationDB.Get(0x18D).Render(true, false, true)?
                .Crop(new Rectangle(64 + 16, 256 + 32, 96, 96)),

            BossType.MotherBrain => _spriteDB.GetMonster(MonsterType.Motherbrain).Get(0)?
                .Pad(new Size(80, 80), HorizontalAlignment.Center, VerticalAlignment.Top)
                .Overlay(_spriteDB.GetMonster(MonsterType.Motherbrain_Body).Get(0)!, new Point(0, 28)),

            BossType.SunOfTheSun =>
                _spriteDB.GetMonster(MonsterType.SonofSunFlame).Get(0)?
                .Pad(new Size(96, 96), verticalAlignment: VerticalAlignment.Top)
                .Overlay(_spriteDB.GetMonster(MonsterType.BossOrb2).Get(0)!, new Point(32, 20))
                .Overlay(_spriteDB.GetMonster(MonsterType.SonofSunFlame).Get(0)!, new Point(18, 20))
                .Overlay(_spriteDB.GetMonster(MonsterType.SonofSunFlame).Get(1)!, new Point(62, 20))
                .Overlay(_spriteDB.GetMonster(MonsterType.SonofSunFlame).Get(2)!, new Point(32, 40))
                .Overlay(_spriteDB.GetMonster(MonsterType.SonofSunFlame).Get(2)!, new Point(50, 40))
                .Crop(new Rectangle(24, 16, 48, 48)),

            BossType.Zeal => _spriteDB.GetMonster(MonsterType.Zeal).Get(0)?
                .Crop(new Rectangle(7, 4, 48, 48)),

            BossType.Golem => _spriteDB.GetMonster(MonsterType.Golem).Get(4)?
                .Crop(new Rectangle(8, 0, 48, 48)),
            _ => null
        };
    } 

    public bool Update(ReadOnlySpan<byte> events)
    {
        var updated = false;

        foreach (var boss in Values)
        {
            var isDefeated = boss.Type switch
            {
                BossType.Yakra => (events[0xD] & 0x01) != 0,
                BossType.YakraXIII => (events[0x50] & 0x40) != 0,
                BossType.DragonTank => (events[0x198] & 0x08) != 0,
                BossType.Guardian => (events[0xEC] & 0x01) != 0,
                BossType.RSeries => (events[0x103] & 0x40) != 0,
                BossType.Heckran => (events[0x1A3] & 0x08) != 0,
                BossType.Zombor => (events[0x101] & 0x02) != 0,
                BossType.Retinite => (events[0x1A3] & 0x01) != 0,
                BossType.MasaAndMune => (events[0xf3] & 0x20) != 0,
                BossType.Nizbel => (events[0x105] & 0x20) != 0,
                BossType.Magus => (events[0x1FF] & 0x04) != 0,
                BossType.BlackTyrano => (events[0xEC] & 0x80) != 0,
                BossType.RustTyrano => (events[0x1D2] & 0x40) != 0,
                BossType.GigaGaia => (events[0x100] & 0x20) != 0,
                BossType.MotherBrain => (events[0x13B] & 0x10) != 0,
                BossType.SunOfTheSun => (events[0x13A] & 0x02) != 0,
                BossType.Zeal => (events[0x67] & 0x07) != 0,
                BossType.Golem => (events[0x105] & 0x80) != 0,
                _ => false
            };

            if (isDefeated != boss.IsDefeated)
            {
                updated = true;
                boss.IsDefeated = isDefeated;
            }
        }

        return updated;
    }
}
