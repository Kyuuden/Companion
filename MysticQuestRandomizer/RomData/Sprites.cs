using FF.Rando.Companion.Extensions;
using FF.Rando.Companion.MemoryManagement;
using KGySoft.Drawing.Imaging;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace FF.Rando.Companion.MysticQuestRandomizer.RomData;
internal class Sprites : IDisposable
{
    private readonly byte[] _defaultSkyCoin = [0x0d, 0x08, 0x1f, 0x00, 0x37, 0x07, 0x6b, 0x0f, 0xdf, 0x9f, 0xfc, 0x3e, 0x7f, 0x3f, 0xff, 0x3f, 0x0a, 0x00, 0x0f, 0x18, 0xbe, 0x35, 0xac, 0x2c, 0x70, 0x10, 0xf8, 0x00, 0xec, 0xe0, 0xd6, 0xf0, 0xfb, 0xf9, 0x3f, 0x7c, 0xff, 0xfc, 0xff, 0xfc, 0x90, 0x00, 0xf0, 0x18, 0x7d, 0xac, 0x34, 0x34, 0x7d, 0x3f, 0xfc, 0x3f, 0xbe, 0x2f, 0xff, 0x97, 0x5c, 0x0f, 0x2f, 0x07, 0x1b, 0x00, 0x0d, 0x08, 0xb4, 0x3c, 0x6e, 0x96, 0x2c, 0x17, 0x04, 0x0a, 0xbf, 0xfc, 0x3f, 0xfc, 0x7f, 0xf4, 0xff, 0xe9, 0x3e, 0xf0, 0xfc, 0xe0, 0x78, 0x00, 0x30, 0x10, 0x2c, 0x3c, 0x74, 0x69, 0x30, 0xe0, 0x80, 0xd0];

    private readonly List<byte[,]> _skyCoin;
    private readonly List<byte[,]> _itemData;
    private readonly List<byte[,]> _characterData;
    private readonly List<byte[,]> _resistanceData;

    private readonly List<Palette> _palettes;

    private readonly Dictionary<ArmorType, SpriteDefinition> _armorBuilders;
    private readonly Dictionary<WeaponType, SpriteDefinition> _weaponBuilders;
    private readonly Dictionary<SpellType, SpriteDefinition> _spellBuilders;
    private readonly Dictionary<KeyItemType, SpriteDefinition> _keyItemBuilders;
    private bool disposedValue;

    public Sprites(IMemorySpace memorySpace)
    {
        _skyCoin = _defaultSkyCoin
            .ReadMany<byte[]>(0x18 * 8)
            .Select(data => data.DecodeTile(3))
            .ToList();

        _itemData = memorySpace.ReadBytes(Addresses.ROM.Items)
            .ReadMany<byte[]>(0x18 * 8)
            .Select(data => data.DecodeTile(3))
            .ToList();

        _characterData = memorySpace.ReadBytes(Addresses.ROM.Characters)
            .ReadMany<byte[]>(0x18 * 8)
            .Select(data => data.DecodeTile(3))
            .ToList();

        _resistanceData = memorySpace.ReadBytes(Addresses.ROM.Resitstances)
            .ReadMany<byte[]>(0x18 * 8)
            .Select(data => data.DecodeTile(3))
            .ToList();

        _palettes = memorySpace.ReadBytes(Addresses.ROM.Palettes)
            .ReadMany<byte[]>(16 * 8)
            .Select(paletteData => paletteData.DecodePalette(new Color32()))
            .ToList();

        _armorBuilders = new Dictionary<ArmorType, SpriteDefinition>()
        {
            { ArmorType.MysticRobe,  new SpriteDefinition(_itemData, new byte[2,2]{ { 0, 0 },{ 1, 1 }  },         _palettes[0] )},
            { ArmorType.RelicArmor,  new SpriteDefinition(_itemData, new byte[2,2]{ { 0, 0 },{ 1, 1 }  },         _palettes[0] )},
            { ArmorType.GaiasArmor,  new SpriteDefinition(_itemData, new byte[2,2]{ { 200, 201 }, { 216, 217 } }, _palettes[16] )},
            { ArmorType.NobleArmor,  new SpriteDefinition(_itemData, new byte[2,2]{ { 198, 199 }, { 214, 215 } }, _palettes[16] )},
            { ArmorType.SteelArmor,  new SpriteDefinition(_itemData, new byte[2,2]{ { 196, 197 }, { 212, 213 } }, _palettes[16] )},
            { ArmorType.ApolloHelm,  new SpriteDefinition(_itemData, new byte[2,2]{ { 194, 195 }, { 210, 211 } }, _palettes[16] )},
            { ArmorType.MoonHelm,    new SpriteDefinition(_itemData, new byte[2,2]{ { 192, 193 }, { 208, 209 } }, _palettes[18] )},
            { ArmorType.SteelHelm,   new SpriteDefinition(_itemData, new byte[2,2]{ { 174, 175 }, { 190, 191 } }, _palettes[18] )},
            { ArmorType.MagicRing,   new SpriteDefinition(_itemData, new byte[2,2]{ { 236, 237 }, { 252, 253 } }, _palettes[16] )},
            { ArmorType.Charm,       new SpriteDefinition(_itemData, new byte[2,2]{ { 234, 235 }, { 250, 251 } }, _palettes[16] )},
            { ArmorType.EtherShield, new SpriteDefinition(_itemData, new byte[2,2]{ { 232, 233 }, { 248, 249 } }, _palettes[16] )},
            { ArmorType.AegisShield, new SpriteDefinition(_itemData, new byte[2,2]{ { 230, 231 }, { 246, 247 } }, _palettes[16] )},
            { ArmorType.VenusShield, new SpriteDefinition(_itemData, new byte[2,2]{ { 228, 229 }, { 244, 245 } }, _palettes[16] )},
            { ArmorType.SteelShield, new SpriteDefinition(_itemData, new byte[2,2]{ { 226, 227 }, { 242, 243 } }, _palettes[16] )},
            { ArmorType.BlackRobe,   new SpriteDefinition(_itemData, new byte[2,2]{ { 0, 0 },{ 1, 1 }  },         _palettes[0] )},
            { ArmorType.FlameArmor,  new SpriteDefinition(_itemData, new byte[2,2]{ { 0, 0 },{ 1, 1 }  },         _palettes[0] )},
            { ArmorType.CupidLocket, new SpriteDefinition(_itemData, new byte[2,2]{ { 238, 239 }, { 254, 255 } }, _palettes[18] )},
        };

        _weaponBuilders = new Dictionary<WeaponType, SpriteDefinition>
        {
            {WeaponType.CharmClaw,   new SpriteDefinition(_itemData, new byte[2,2]{ { 142, 143 },{ 158, 159 }  }, _palettes[16] )},
            {WeaponType.CatClaw,     new SpriteDefinition(_itemData, new byte[2,2]{ { 140, 141 },{ 156, 157 }  }, _palettes[17] )},
            {WeaponType.GiantsAxe,   new SpriteDefinition(_itemData, new byte[2,2]{ { 138, 139 },{ 154, 155 }  }, _palettes[17] )},
            {WeaponType.BattleAxe,   new SpriteDefinition(_itemData, new byte[2,2]{ { 136, 137 },{ 152, 153 }  }, _palettes[17] )},
            {WeaponType.Axe,         new SpriteDefinition(_itemData, new byte[2,2]{ { 134, 135 },{ 150, 151 }  }, _palettes[17] )},
            {WeaponType.Excalibur,   new SpriteDefinition(_itemData, new byte[2,2]{ { 132, 133 },{ 148, 149 }  }, _palettes[16] )},
            {WeaponType.KnightSword, new SpriteDefinition(_itemData, new byte[2,2]{ { 130, 131 },{ 146, 147 }  }, _palettes[16] )},
            {WeaponType.SteelSword,  new SpriteDefinition(_itemData, new byte[2,2]{ { 128, 129 },{ 144, 145 }  }, _palettes[16] )},
            {WeaponType.MegaGrenade, new SpriteDefinition(_itemData, new byte[2,2]{ { 166, 167 },{ 182, 183 }  }, _palettes[15] )},
            {WeaponType.JumboBomb,   new SpriteDefinition(_itemData, new byte[2,2]{ { 164, 165 },{ 180, 181 }  }, _palettes[15] )},
            {WeaponType.Bomb,        new SpriteDefinition(_itemData, new byte[2,2]{ { 162, 163 },{ 178, 179 }  }, _palettes[18] )},
            {WeaponType.DragonClaw,  new SpriteDefinition(_itemData, new byte[2,2]{ { 160, 161 },{ 176, 177 }  }, _palettes[16] )},
        };

        _keyItemBuilders = new Dictionary<KeyItemType, SpriteDefinition>
        {
            {KeyItemType.ThunderRock,     new SpriteDefinition(_itemData, new byte[2,2]{ { 14, 15 },{ 30, 31 }  }, _palettes[17]) },
            {KeyItemType.MagicMirror,     new SpriteDefinition(_itemData, new byte[2,2]{ { 12, 13 },{ 28, 29 }  }, _palettes[16]) },
            {KeyItemType.GasMask,         new SpriteDefinition(_itemData, new byte[2,2]{ { 10, 11 },{ 26, 27 }  }, _palettes[19]) },
            {KeyItemType.MultiKey,        new SpriteDefinition(_itemData, new byte[2,2]{ {  8,  9 },{ 24, 25 }  }, _palettes[16]) },
            {KeyItemType.VenusKey,        new SpriteDefinition(_itemData, new byte[2,2]{ {  6,  7 },{ 22, 23 }  }, _palettes[17]) },
            {KeyItemType.WakeWater,       new SpriteDefinition(_itemData, new byte[2,2]{ {  4,  5 },{ 20, 21 }  }, _palettes[17]) },
            {KeyItemType.TreeWither,      new SpriteDefinition(_itemData, new byte[2,2]{ {  2,  3 },{ 18, 19 }  }, _palettes[17]) },
            {KeyItemType.Elixer,          new SpriteDefinition(_itemData, new byte[2,2]{ {  0,  1 },{ 16, 17 }  }, _palettes[18]) },
            {KeyItemType.CompleteSkyCoin, new SpriteDefinition(_skyCoin,  new byte[2,2]{ {  0,  1 },{  2,  3 }  }, _palettes[16]) },
            {KeyItemType.SkyCoin,         new SpriteDefinition(_itemData, new byte[2,2]{ { 46, 47 },{ 62, 63 }  }, _palettes[16]) },
            {KeyItemType.SunCoin,         new SpriteDefinition(_itemData, new byte[2,2]{ { 44, 45 },{ 60, 61 }  }, _palettes[19]) },
            {KeyItemType.RiverCoin,       new SpriteDefinition(_itemData, new byte[2,2]{ { 42, 43 },{ 58, 59 }  }, _palettes[16]) },
            {KeyItemType.SandCoin,        new SpriteDefinition(_itemData, new byte[2,2]{ { 40, 41 },{ 56, 57 }  }, _palettes[17]) },
            {KeyItemType.MobiusCrest,     new SpriteDefinition(_itemData, new byte[2,2]{ { 38, 39 },{ 54, 55 }  }, _palettes[17]) },
            {KeyItemType.GeminiCrest,     new SpriteDefinition(_itemData, new byte[2,2]{ { 36, 37 },{ 52, 53 }  }, _palettes[17]) },
            {KeyItemType.LibraCrest,      new SpriteDefinition(_itemData, new byte[2,2]{ { 34, 35 },{ 50, 51 }  }, _palettes[17]) },
            {KeyItemType.CapitansCap,     new SpriteDefinition(_itemData, new byte[2,2]{ { 32, 33 },{ 48, 49 }  }, _palettes[17]) },
        };

        _spellBuilders = new Dictionary<SpellType, SpriteDefinition>
        {
            {SpellType.Aero,     new SpriteDefinition(_itemData, new byte[2,2] { { 102, 103 }, { 118, 119 } }, _palettes[18]) },
            {SpellType.Fire,     new SpriteDefinition(_itemData, new byte[2,2] { { 100, 101 }, { 116, 117 } }, _palettes[18]) },
            {SpellType.Blizzard, new SpriteDefinition(_itemData, new byte[2,2] { {  98,  99 }, { 114, 115 } }, _palettes[18]) },
            {SpellType.Quake,    new SpriteDefinition(_itemData, new byte[2,2] { {  96,  97 }, { 112, 113 } }, _palettes[18]) },
            {SpellType.Life,     new SpriteDefinition(_itemData, new byte[2,2] { {  78,  79 }, {  94,  95 } }, _palettes[18]) },
            {SpellType.Heal,     new SpriteDefinition(_itemData, new byte[2,2] { {  76,  77 }, {  92,  93 } }, _palettes[18]) },
            {SpellType.Cure,     new SpriteDefinition(_itemData, new byte[2,2] { {  74,  75 }, {  90,  91 } }, _palettes[18]) },
            {SpellType.Exit,     new SpriteDefinition(_itemData, new byte[2,2] { {  72,  73 }, {  88,  89 } }, _palettes[18]) },
            {SpellType.Flare,    new SpriteDefinition(_itemData, new byte[2,2] { { 110, 111 }, { 126, 127 } }, _palettes[18]) },
            {SpellType.Meteor,   new SpriteDefinition(_itemData, new byte[2,2] { { 108, 109 }, { 124, 125 } }, _palettes[18]) },
            {SpellType.White,    new SpriteDefinition(_itemData, new byte[2,2] { { 106, 107 }, { 122, 123 } }, _palettes[18]) },
            {SpellType.Thunder,  new SpriteDefinition(_itemData, new byte[2,2] { { 104, 105 }, { 120, 121 } }, _palettes[18]) },
        };
    }

    internal Bitmap GetDefault(EquipmentType equipmentType) 
        => equipmentType switch
        {
            EquipmentType.Sword => _weaponBuilders[WeaponType.SteelSword].Render(true),
            EquipmentType.Axe => _weaponBuilders[WeaponType.Axe].Render(true),
            EquipmentType.Bomb => _weaponBuilders[WeaponType.Bomb].Render(true),
            EquipmentType.Claw => _weaponBuilders[WeaponType.CatClaw].Render(true),
            EquipmentType.Armor => _armorBuilders[ArmorType.SteelArmor].Render(true),
            EquipmentType.Helmet => _armorBuilders[ArmorType.SteelHelm].Render(true),
            EquipmentType.Shield => _armorBuilders[ArmorType.SteelShield].Render(true),
            EquipmentType.Accessory => _armorBuilders[ArmorType.Charm].Render(true),
            _ => throw new NotImplementedException()
        };

    internal Bitmap GetEquipment<T>(T active) 
        => active switch
        {
            ArmorType r => _armorBuilders[r].Render(),
            WeaponType w => _weaponBuilders[w].Render(),
            _ => throw new NotSupportedException(),
        };

    internal Bitmap GetKeyItem(KeyItemType keyItemType, bool isFound) => _keyItemBuilders[keyItemType].Render(!isFound);

    internal IReadableBitmapData GetKeyItemData(KeyItemType keyItemType, bool isFound = true) => _keyItemBuilders[keyItemType].RenderData(!isFound);

    internal Bitmap GetSpell(SpellType spellType, bool isFound = true) => _spellBuilders[spellType].Render(!isFound);

    internal IReadableBitmapData GetSpellData(SpellType spellType, bool isFound = true) => _spellBuilders[spellType].RenderData(!isFound);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var value in _weaponBuilders.Values) value.Dispose();
                _weaponBuilders.Clear();
                foreach (var value in _armorBuilders.Values) value.Dispose();
                _armorBuilders.Clear();
                foreach (var value in _keyItemBuilders.Values) value.Dispose();
                _keyItemBuilders.Clear();
                foreach (var value in _spellBuilders.Values) value.Dispose();
                _spellBuilders.Clear();

                _itemData.Clear();
                _characterData.Clear();
                _resistanceData.Clear();
                _palettes.Clear();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
