using BizHawk.Common;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.MysticQuestRandomizer.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.MysticQuestRandomizer;
internal class GameInfo
{
    private readonly List<Element> _elements = [];
    private readonly List<Companion> _companions = [];

    public int? RequiredSkyFragmentCount { get; private set; }

    public bool? RandomizedPazuzu { get; private set; }

    public IReadOnlyList<Element> Elements => _elements;

    public IReadOnlyList<Companion> Companions => _companions;

    public static GameInfo Parse(IMemorySpace rom, MysticQuestRandomizerSettings settings, RomData.Font font)
    {
        var _textConverter = new TextConverter();
        var info = new GameInfo();
        ReadOnlySpan<byte> data = rom.ReadBytes(0x81200L.RangeTo(0x82000L)).AsSpan();

        if (Search(data, _textConverter.TextToByte("No info available.")).HasValue)
            return info;

        var skyFragmentsPos = Search(data, _textConverter.TextToByte("Sky Fragments"));
        if (skyFragmentsPos.HasValue)
        {
            var countStart = skyFragmentsPos.Value.End.Value + 17;

            var str = _textConverter.SpanToText(data[countStart..(countStart + 2)]);
            if (int.TryParse(str, out var shardCount))
                info.RequiredSkyFragmentCount = shardCount;

            data = data[(countStart + 2)..];
        }

        var windCrystal = Search(data, _textConverter.TextToByte("Wind Crystal"));
        if (windCrystal.HasValue)
        {
            info.RandomizedPazuzu = true;
            data = data[windCrystal.Value.End.Value..];
        }

        var resists = Search(data, _textConverter.TextToByte("Resists & Weaknesses"));
        if (resists.HasValue)
        {
            data = data[(resists.Value.End.Value + 4)..];

            while (data[3] == 0xDC)
            {
                try
                {
                    var originalElement = ParseElement(data.Read<uint>(0, 24));
                    var newElement = ParseElement(data.Read<uint>(32, 24));
                    info._elements.Add(new Element(originalElement, newElement, settings.Elements, font));

                    data = data[8..];
                }
                catch
                {
                    break;
                }
            }
        }

        foreach (CompanionType c in Enum.GetValues(typeof(CompanionType)))
        {
            var companion = ParseCharcter(_textConverter, c, data);
            if (companion?.ExistsInSeed == true)
            {
                info._companions.Add(companion);
            }
        }

        return info;
    }

    private static Range? Search(ReadOnlySpan<byte> buffer, ReadOnlySpan<byte> target)
    {
        var index = buffer.IndexOf(target);

        if (index == -1)
            return null;

        return new Range(index, index + target.Length);
    }

    private static ElementsType ParseElement(uint sourceBytes)
        => sourceBytes switch
        {
            0x010016 => ElementsType.Earth,
            0x010116 => ElementsType.Water,
            0x010216 => ElementsType.Fire,
            0x010316 => ElementsType.Air,
            0x011016 => ElementsType.Holy,
            0x051216 => ElementsType.Axe,
            0x051116 => ElementsType.Bomb,
            0x050716 => ElementsType.Projectile,
            0x050816 => ElementsType.Doom,
            0x050916 => ElementsType.Stone,
            0x050A16 => ElementsType.Silence,
            0x050B16 => ElementsType.Blind,
            0x010C16 => ElementsType.Poison,
            0x010D16 => ElementsType.Paralysis,
            0x010E16 => ElementsType.Sleep,
            0x010F16 => ElementsType.Confusion,
            _ => throw new NotSupportedException()
        };

    private static SpellType? ParseSpell(ulong sourceBytes)
        => sourceBytes switch
        {
            0x164916164816L => SpellType.Exit,
            0x164B16164A16L => SpellType.Cure,
            0x164D16164C16L => SpellType.Heal,
            0x164F16164E16L => SpellType.Life,
            0x166116166016L => SpellType.Quake,
            0x166316166216L => SpellType.Blizzard,
            0x166516166416L => SpellType.Fire,
            0x166716166616L => SpellType.Aero,
            0x166916166816L => SpellType.Thunder,
            0x166B16166A16L => SpellType.White,
            0x166D16166C16L => SpellType.Meteor,
            0x166F16166E16L => SpellType.Flare,
            _ => null
        };

    private static Companion? ParseCharcter(TextConverter textConverter, CompanionType character, ReadOnlySpan<byte> buffer)
    {
        var characterEndMarker = new byte[] { 0x25, 0x0C, 0x15, 0x19, 0x15, 0x19 };
        var nameBytes = textConverter.TextToByte(character.ToString());

        var start = buffer.IndexOf(nameBytes);
        if (start == -1) return null;

        var end = buffer[start..].IndexOf(characterEndMarker);
        if (end == -1) return null;

        var characterBuffer = buffer.Slice(start, end);
        var c = new Companion(character);

        var spellsStart = nameBytes.Length + 12 + textConverter.TextToByte("Spells").Length + 4;

        var spells = new List<SpellType>();
        SpellType? currentSpell;
        do
        {
            if (spellsStart + 6 > characterBuffer.Length)
                break;

            currentSpell = ParseSpell(characterBuffer.Read<ulong>(spellsStart * 8, 48));
            if (currentSpell != null)
            {
                spells.Add(currentSpell.Value);
                spellsStart += 7;
            }
        } while (currentSpell.HasValue);

        spellsStart += spells.Count * 7 + 2; //skip second line of sprites;

        if (spells.Count >= 10)
            spellsStart -= 2;

        for (var i = 0; i < spells.Count; i++)
        {
            var levelString = textConverter.SpanToText(characterBuffer[spellsStart..(spellsStart + 2)]);
            if (byte.TryParse(levelString, out var level))
                c.AddSpell(level, spells[i]);

            spellsStart += 3;
        }

        if (spells.Count >= 10)
        {
            spells.Clear();
            do
            {
                if (spellsStart + 6 > characterBuffer.Length)
                    break;

                currentSpell = ParseSpell(characterBuffer.Read<ulong>(spellsStart * 8, 48));
                if (currentSpell != null)
                {
                    spells.Add(currentSpell.Value);
                    spellsStart += 7;
                }
            } while (currentSpell.HasValue);

            spellsStart += spells.Count * 7 + 2; //skip second line of sprites;

            if (spells.Count >= 10)
                spellsStart -= 2;

            for (var i = 0; i < spells.Count; i++)
            {
                var levelString = textConverter.SpanToText(characterBuffer[spellsStart..(spellsStart + 2)]);
                if (byte.TryParse(levelString, out var level))
                    c.AddSpell(level, spells[i]);

                spellsStart += 3;
            }
        }

        var quests = Search(characterBuffer, textConverter.TextToByte("Quests\n\n"));
        if (quests.HasValue)
        {
            var questStartMarker = new byte[] { 0x05, 0x3B };
            var questsbuffer = characterBuffer[(quests.Value.End.Value + 2)..];
            int nextQuestStart;
            do
            {
                nextQuestStart = questsbuffer.IndexOf(questStartMarker);
                if (nextQuestStart > 0)
                {
                    c.AddQuest(questsbuffer[0], textConverter.SpanToText(questsbuffer[5..nextQuestStart]));
                    questsbuffer = questsbuffer[(nextQuestStart + 2)..];
                }
            } while (nextQuestStart > 0);

            c.AddQuest(questsbuffer[0], textConverter.SpanToText(questsbuffer[5..]));
        }

        return c;
    }

    internal bool UpdateQuests(TimeSpan elapsed, ReadOnlySpan<byte> quests)
    {
        var updated = false;
        foreach (var quest in Companions.SelectMany(c => c.Quests))
        {
            updated |= quest.Update(elapsed, quests);
        }

        return updated;
    }
}
