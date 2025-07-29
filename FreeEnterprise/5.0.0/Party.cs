using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.FreeEnterprise.RomData;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.FreeEnterprise.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Rando.Companion.FreeEnterprise._5._0._0;
internal class Party : IDisposable
{
    private readonly IReadOnlyList<Character> _characters;
    private bool? _vanillaAgility;
    private bool? _chero;

    internal IReadOnlyList<Character> Characters => _characters;

    public Party(PartySettings settings, Sprites sprites, bool? vanillaAgility, bool? chero)
    {
        _vanillaAgility = vanillaAgility;
        _chero = chero;
        _characters = [new Character(settings, sprites), new Character(settings, sprites), new Character(settings, sprites), new Character(settings, sprites), new Character(settings, sprites)];
    }

    public bool Update(TimeSpan time, ReadOnlySpan<byte> currentParty, ReadOnlySpan<byte> axtorData)
    {
        var updated = false;

        for (byte i = 0; i < 5; i++)
        {
            var character = Characters[SlotToIndex(i)];
            var cData = currentParty[(i * 64)..];
            var cId = cData.Read<byte>(0, 5);
            var cClass = cData.Read<CharacterType>(8, 4);
            var fashion = axtorData[cId * 4 + 1];

            if (character.Id != cId)
            {
                updated = true; 
                character.Id = cId;
                character.Slot = i;
                character.Fashion = fashion;
                character.Type = cClass;
            }
        }

        if (updated && _chero.HasValue && _vanillaAgility.HasValue)
        {
            Character? anchor = null;
            if (_vanillaAgility.Value)
                anchor = Characters.OrderBy(c=>c.Slot).FirstOrDefault(c => c.Id != 0 && (c.Type == CharacterType.Cecil || c.Type == CharacterType.DarkKnightCecil));

            if (anchor == null && _chero.Value)
                anchor = Characters.OrderBy(c => c.Slot).FirstOrDefault(c => c.Id == 1);

            anchor ??= Characters.OrderBy(c => c.Slot).FirstOrDefault(c => c.Id != 0);

            if (anchor != null)
            {
                foreach (var c in Characters)
                    c.IsAnchor = false;

                anchor.IsAnchor = true;
            }
        }

        return updated;
    }

    private static byte SlotToIndex(byte slot) => slot switch
    {
        0 => 2,
        1 => 0,
        2 => 4,
        3 => 1,
        4 => 3,
        _ => throw new InvalidOperationException("Unkown character slot")
    };

    public void Dispose()
    {
        foreach (var c in Characters)
            c.Dispose();
    }
}
