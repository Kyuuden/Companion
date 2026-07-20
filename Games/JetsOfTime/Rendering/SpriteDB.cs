using FF.Rando.Companion.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;
using FF.Rando.Companion.Rendering;

namespace FF.Rando.Companion.Games.JetsOfTime.Rendering;
internal class SpriteDB : IDisposable
{
    private readonly List<SpriteCollection> _characterHeaders = [];
    private readonly List<SpriteCollection> _npcHeaders = [];
    private readonly List<SpriteCollection> _monsterHeaders = [];
    private readonly PortraitSprites _portraitSprites;

    private readonly Container _container;

    public SpriteDB(Container container)
    {
        _container = container;

        _characterHeaders.AddRange(_container.Rom.ReadBytes(Data.Addresses.ROM.Sprites.CharacterHeaders).ReadMany<byte[]>(0, 5 * 8, 7).Select(b => new SpriteCollection(container, b)));
        _npcHeaders.AddRange(_container.Rom.ReadBytes(Data.Addresses.ROM.Sprites.NpcHeaders).ReadMany<byte[]>(0, 5 * 8, 256).Select(b => new SpriteCollection(container, b)));
        _monsterHeaders.AddRange(_container.Rom.ReadBytes(Data.Addresses.ROM.Sprites.MonsterHeaders).ReadMany<byte[]>(10 * 8).Select(b => new SpriteCollection(container, b)));

        _portraitSprites = new PortraitSprites(container);
    }

    public SpriteCollection GetCharacter(CharacterType characterType) => _characterHeaders[(int)characterType];

    public SpriteCollection GetNpc(NPCType npc) => _npcHeaders[(int)npc];

    public SpriteCollection GetMonster(MonsterType monsterType) => _monsterHeaders[(int)monsterType];

    public ISprite? GetPortrait(PortraitType portraitType) 
        => _portraitSprites.Get(portraitType);

    public void Dispose()
    {
        foreach (var item in _characterHeaders) item.Dispose();
        foreach (var item in _npcHeaders) item.Dispose();
        foreach (var item in _monsterHeaders) item.Dispose();

        _characterHeaders.Clear();
        _npcHeaders.Clear();
        _monsterHeaders.Clear();
    }
}

