using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MysticQuestRandomizer.RomData;
using FF.Rando.Companion.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class Spell : IImageTracker
{
    private readonly SpellType _spellType;
    private Bitmap? _image;
    private TimeSpan? _whenFound;
    private bool _isFound;
    private Sprites _sprites;

    internal Spell(SpellType type, Sprites sprites)
    {
        _spellType = type;
        _sprites = sprites;
        SetImage();
    }

    public int Id => (int)_spellType;

    public bool IsFound
    {
        get => _isFound;
        set
        {
            if (_isFound == value)
                return;

            _isFound = value;
            NotifyPropertyChanged();
            SetImage();
        }
    }

    public TimeSpan? WhenFound
    {
        get => _whenFound;
        set
        {
            if (_whenFound == value || _whenFound.HasValue)
                return;

            _whenFound = value;
            NotifyPropertyChanged();
        }
    }

    public Bitmap Image
    {
        get => _image!;
        set
        {
            if (_image == value)
                return;

            _image = value;
            NotifyPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetImage()
    {
        Image = _sprites.GetSpell(_spellType, IsFound);
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

internal class Spells
{
    private readonly IReadOnlyList<Spell> _items;

    public Spells(Sprites sprites)
    {
        _items =
        [
            new Spell(SpellType.Exit, sprites),
            new Spell(SpellType.Cure, sprites),
            new Spell(SpellType.Heal, sprites),
            new Spell(SpellType.Life, sprites),
            new Spell(SpellType.Quake, sprites),
            new Spell(SpellType.Blizzard, sprites),
            new Spell(SpellType.Fire, sprites),
            new Spell(SpellType.Aero, sprites),
            new Spell(SpellType.Thunder, sprites),
            new Spell(SpellType.White, sprites),
            new Spell(SpellType.Meteor, sprites),
            new Spell(SpellType.Flare, sprites),
        ];
    }

    public IReadOnlyList<Spell> Items => _items;

    public bool Update(TimeSpan time, ReadOnlySpan<byte> found)
    {
        var updated = false;

        foreach (var keyitem in _items)
        {
            var isfound = found.Read<bool>(keyitem.Id);
            if (isfound != keyitem.IsFound)
            {
                updated = true;
                keyitem.WhenFound = time;
                keyitem.IsFound = isfound;
            }
        }

        return updated;
    }

}
