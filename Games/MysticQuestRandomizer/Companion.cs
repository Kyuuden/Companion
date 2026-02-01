using Newtonsoft.Json;
using System.Collections.Generic;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer;
public class Companion(CompanionType companion)
{
    private readonly List<CharacterSpell> _spells = [];
    private readonly List<CharacterQuest> _quests = [];

    public int Id => (int)Type;

    public CompanionType Type { get; } = companion;

    public bool ExistsInSeed => _quests.Count > 0 || _spells.Count > 0;

    public void AddQuest(byte flag, string description)
        => _quests.Add(new CharacterQuest(flag, description.Trim().Replace("\n", "")));

    public void AddSpell(byte level, SpellType type)
        => _spells.Add(new CharacterSpell(level, type));

    public IEnumerable<CharacterQuest> Quests => _quests;
    public IEnumerable<CharacterSpell> Spells => _spells;
}

public record CharacterSpell(byte Level, SpellType Type);
