using BizHawk.Client.Common;
using BizHawk.Common;
using BizHawk.Common.CollectionExtensions;
using BizHawk.Emulation.Common;
using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MemoryManagement;
using FF.Rando.Companion.MysticQuestRandomizer.RomData;
using FF.Rando.Companion.MysticQuestRandomizer.View;
using FF.Rando.Companion.Settings;
using KGySoft.CoreLibraries;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class Seed : IGame
{
    private readonly TextConverter _textConverter = new TextConverter();

    private readonly Weapons _weapons;
    private readonly Armors _armors;
    private readonly Spells _spells;
    private readonly KeyItems _keyitems;
    private readonly List<Element> _elements = [];

    private int? _requiredSkyFragmentCount;
    private bool _randomizedPazuzu;
    private readonly List<Companion> _companions = [];

    internal readonly RomData.Font Font;
    internal readonly Sprites Sprites;

    internal Seed(string hash, Container container)
    {
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        MQRContainer = container ?? throw new ArgumentNullException(nameof(container));

        Settings = container.Settings;
        Sprites = new Sprites(MQRContainer.Rom);

        Font = new RomData.Font(MQRContainer.Rom);

        _weapons = new Weapons(Sprites);
        _armors = new Armors(Sprites);
        _spells = new Spells(Sprites);
        _keyitems = new KeyItems(Sprites);

        AnalyzeGameInfo(MQRContainer.Rom);
    }

    private void AnalyzeGameInfo(IMemorySpace rom)
    {
        ReadOnlySpan<byte> gameinfo = rom.ReadBytes(0x81200L.RangeTo(0x82000L)).AsSpan();

        if (Search(gameinfo, _textConverter.TextToByte("No info available.")).HasValue)
            return;

        var skyFragmentsPos = Search(gameinfo, _textConverter.TextToByte("Sky Fragments"));
        if (skyFragmentsPos.HasValue)
        {
            var countStart = skyFragmentsPos.Value.End.Value + 17;

            var str = _textConverter.SpanToText(gameinfo[countStart..(countStart + 2)]);
            if (int.TryParse(str, out var shardCount))
                _requiredSkyFragmentCount = shardCount;

            gameinfo = gameinfo.Slice(countStart + 2);
        }

        var windCrystal = Search(gameinfo, _textConverter.TextToByte("Wind Crystal"));
        if (windCrystal.HasValue)
        {
            _randomizedPazuzu = true;
            gameinfo = gameinfo.Slice(windCrystal.Value.End.Value);
        }

        var resists = Search(gameinfo, _textConverter.TextToByte("Resists & Weaknesses"));
        if (resists.HasValue)
        {
            gameinfo = gameinfo.Slice(resists.Value.End.Value + 4);

            while (gameinfo[3] == 0xDC)
            {
                try
                {
                    var originalElement = GetElement(gameinfo.Read<uint>(0, 24));
                    var newElement = GetElement(gameinfo.Read<uint>(32, 24));
                    _elements.Add(new Element(originalElement, newElement, Settings.Elements, Font));

                    gameinfo = gameinfo.Slice(8);
                }
                catch
                {
                    break;
                }
            }
        }

        foreach (CompanionType c in Enum.GetValues(typeof(CompanionType)))
        {
            var character = ParseCharcter(c, gameinfo);
            if (character?.ExistsInSeed == true)
            {
                _companions.Add(character);
            }
        }
    }

    public Range? Search(ReadOnlySpan<byte> buffer, ReadOnlySpan<byte> target)
    {
        var index = buffer.IndexOf(target);

        if (index == -1)
            return null;

        return new Range(index, index + target.Length);
    }

    private ElementsType GetElement(uint sourceBytes)
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

    private SpellType? GetSpell(ulong sourceBytes)
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

    private Companion? ParseCharcter(CompanionType character, ReadOnlySpan<byte> buffer)
    {
        var nameBytes = _textConverter.TextToByte(character.ToString());

        var start = buffer.IndexOf(nameBytes);
        if (start == -1) return null;

        var characterBuffer = buffer.Slice(start);
        var c = new Companion(character);

        var spellsStart = nameBytes.Length + 12 + _textConverter.TextToByte("Spells").Length + 4;

        var spells = new List<SpellType>();
        SpellType? currentSpell;
        do
        {
            currentSpell = GetSpell(characterBuffer.Read<ulong>(spellsStart * 8, 48));
            if (currentSpell != null)
            {
                spells.Add(currentSpell.Value);
                spellsStart += 7;
            }
        } while(currentSpell.HasValue);

        spellsStart += spells.Count * 7 + 2; //skip second line of sprites;

        for (var i = 0; i < spells.Count; i++)
        {
            var levelString = _textConverter.SpanToText(characterBuffer[spellsStart..(spellsStart+2)]);
            if (byte.TryParse(levelString, out var level))
                c.AddSpell(level, spells[i]);

            spellsStart += 3;
        }

        if (spells.Count >= 10)
        {
            // todo do it again.
        }

        var quests = Search(characterBuffer, _textConverter.TextToByte("Quests\n\n"));
        if (quests.HasValue)
        {
            var questStart = quests.Value.End.Value + 7;
            var characterEndMarker = new byte[] { 0x25, 0x0C, 0x15, 0x19, 0x15, 0x19 };
            var end = characterBuffer[questStart..].IndexOf(characterEndMarker);

            if (end > 0)
            {
                end--;
                var questsbuffer = characterBuffer[questStart..][..end];
                var questStartMarker = new byte[] { 0x05, 0x3B };
                var nextQuestStart = -1;
                do
                {
                    nextQuestStart = questsbuffer.IndexOf(questStartMarker);
                    if (nextQuestStart > 0)
                    {
                        c.AddQuest(_textConverter.SpanToText(questsbuffer[..nextQuestStart]));
                        questsbuffer = questsbuffer[(nextQuestStart + 7)..];
                    }
                } while (nextQuestStart > 0);

                c.AddQuest(_textConverter.SpanToText(questsbuffer));
            }
        }

        return c;
    }

    public string Hash { get; }

    public Bitmap Icon => MysticQuest.crystal_light;

    public Color BackgroundColor => Color.Black;

    public bool Started => false;

    public TimeSpan Elapsed => TimeSpan.Zero;

    public bool RequiresMemoryEvents => false;

    public IEmulationContainer Container => MQRContainer;

    internal Container MQRContainer { get; }

    internal Settings.MysticQuestRandomizerSettings Settings { get; }

    internal ISettings RootSettings => MQRContainer.RootSettings;

    GameSettings IGame.Settings => Settings;

    public event Action<string>? ButtonPressed;

    private ImmutableHashSet<string> _lastPressedButtons = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    public IEnumerable<Weapon> Weapons => _weapons.Items;

    public IEnumerable<Armor> Armors => _armors.Items;

    public IEnumerable<Spell> Spells => _spells.Items;

    public IEnumerable<KeyItem> KeyItems => _keyitems.Items;

    public IEnumerable<Element> Elements => _elements;

    public IEnumerable<Companion> Companions => _companions;

    public Control CreateControls()
    {
        var control =  new MysticQuestRandomizerControl();
        control.InitializeDataSources(this);
        return control;
    }

    public void Dispose()
    {
        Sprites.Dispose();
    }

    public void OnNewFrame()
    {
        var pressed = ImmutableHashSet.CreateRange(MQRContainer.Input.GetPressedButtons());
        foreach (var b in pressed.Except(_lastPressedButtons))
        {
            ButtonPressed?.Invoke(b);
        }
        _lastPressedButtons = pressed;

        if (MQRContainer.Emulation.FrameCount() % MQRContainer.RootSettings.TrackingInterval == 0)
        {
            ReadOnlySpan<byte> wramData = Container.Wram.ReadBytes(Addresses.WRAM.WramRegion).AsSpan();

            //var checkedNpcs = wramData.Slice(Addresses.WRAM.CheckedNpcBits);
            //var checkedLocations = wramData.Slice(Addresses.WRAM.CheckedlocationBits);
            //var checkedBattlefields = wramData.Slice(Addresses.WRAM.CheckedBattlefieldBits);

            var shardCount = wramData.Slice(Addresses.WRAM.FoundShards);
            var keyItemsFound = wramData.Slice(Addresses.WRAM.FoundKeyItemBits);
            var weapons = wramData[Addresses.WRAM.FoundWeaponBits];
            var armors = wramData[Addresses.WRAM.FoundArmorBits];
            var spells = wramData[Addresses.WRAM.FoundSpellBits];

            if (_weapons.Update(Elapsed, weapons))
                NotifyPropertyChanged(nameof(Weapons));

            if (_armors.Update(Elapsed, armors))
                NotifyPropertyChanged(nameof(Armors));

            if (_spells.Update(Elapsed, spells))
                NotifyPropertyChanged(nameof(Spells));

            if (_keyitems.Update(Elapsed, keyItemsFound, shardCount[0]))
                NotifyPropertyChanged(nameof(KeyItems));
        }
    }

    public void Pause()
    {
        //throw new NotImplementedException();
    }

    public void Unpause()
    {
       // throw new NotImplementedException();
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
