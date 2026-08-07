using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.Games.JetsOfTime.Rendering;
internal class Sprites : IDisposable
{
    private readonly List<SpriteCollection> _characterHeaders = [];
    private readonly List<SpriteCollection> _npcHeaders = [];
    private readonly List<SpriteCollection> _monsterHeaders = [];
    private readonly Portraits _portraitSprites;

    private readonly Seed _seed;

    public Sprites(Seed seed)
    {
        _seed = seed;

        _characterHeaders.AddRange(_seed.Rom.ReadBytes(Data.Addresses.ROM.Sprites.CharacterHeaders).ReadMany<byte[]>(0, 5 * 8, 7).Select(b => new SpriteCollection(seed, b)));
        _npcHeaders.AddRange(_seed.Rom.ReadBytes(Data.Addresses.ROM.Sprites.NpcHeaders).ReadMany<byte[]>(0, 5 * 8, 256).Select(b => new SpriteCollection(seed, b)));
        _monsterHeaders.AddRange(_seed.Rom.ReadBytes(Data.Addresses.ROM.Sprites.MonsterHeaders).ReadMany<byte[]>(10 * 8).Select(b => new SpriteCollection(seed, b)));

        _portraitSprites = new Portraits(_seed.Rom);
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

