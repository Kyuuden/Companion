using System.Collections.Generic;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class Companion
{
    private readonly List<CharacterSpell> _spells = [];
    private readonly List<string> _quests = [];

    public Companion(CompanionType companion)
    {
        Type = companion;
    }

    public int Id => (int)Type;

    public CompanionType Type { get; }

    public bool ExistsInSeed => _quests.Count > 0 || _spells.Count > 0;

    public void AddQuest(string description)
        => _quests.Add(description.Trim().Replace("\n",""));

    public void AddSpell(byte level, SpellType type)
        => _spells.Add(new CharacterSpell(level, type));

    public IEnumerable<string> Quests => _quests;
    public IEnumerable<CharacterSpell> Spells => _spells;
}

public record CharacterSpell(byte Level, SpellType Type);