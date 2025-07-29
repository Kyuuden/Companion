using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace FF.Rando.Companion.FreeEnterprise.Shared;
public class TextEncoding : Encoding
{
    public const byte BorderTopLeft = 247;
    public const byte BorderTop = 248;
    public const byte BorderTopRight = 249;
    public const byte BorderLeft = 250;
    public const byte BorderRight = 251;
    public const byte BorderBottomLeft = 252;
    public const byte BorderBottom = 253;
    public const byte BorderBottomRight = 254;

    private readonly Dictionary<char, byte> _toRom;
    private readonly Dictionary<byte, char> _fromRom;
    private readonly Dictionary<string, char> _symbols;

    public TextEncoding()
    {
        _toRom = new Dictionary<char, byte>
        {
            { '\n', 0x01},
            { ' ', 0xFF},
            { '\'', 0xC0},
            { '.', 0xC1},
            { '-', 0xC2},
            { '_', 0xC3},
            { '!', 0xC4},
            { '?', 0xC5},
            { '%', 0xC6},
            { '/', 0xC7},
            { ':', 0xC8},
            { ',', 0xC9},
            { '&', 0xCA},
            { '+', 0xCB},
            { '(', 0xCC},
            { ')', 0xCD},
            { '0', 0x80},
            { '1', 0x81},
            { '2', 0x82},
            { '3', 0x83},
            { '4', 0x84},
            { '5', 0x85},
            { '6', 0x86},
            { '7', 0x87},
            { '8', 0x88},
            { '9', 0x89},
            { 'A', 0x42},
            { 'B', 0x43},
            { 'C', 0x44},
            { 'D', 0x45},
            { 'E', 0x46},
            { 'F', 0x47},
            { 'G', 0x48},
            { 'H', 0x49},
            { 'I', 0x4A},
            { 'J', 0x4B},
            { 'K', 0x4C},
            { 'L', 0x4D},
            { 'M', 0x4E},
            { 'N', 0x4F},
            { 'O', 0x50},
            { 'P', 0x51},
            { 'Q', 0x52},
            { 'R', 0x53},
            { 'S', 0x54},
            { 'T', 0x55},
            { 'U', 0x56},
            { 'V', 0x57},
            { 'W', 0x58},
            { 'X', 0x59},
            { 'Y', 0x5A},
            { 'Z', 0x5B},
            { 'a', 0x5C},
            { 'b', 0x5D},
            { 'c', 0x5E},
            { 'd', 0x5F},
            { 'e', 0x60},
            { 'f', 0x61},
            { 'g', 0x62},
            { 'h', 0x63},
            { 'i', 0x64},
            { 'j', 0x65},
            { 'k', 0x66},
            { 'l', 0x67},
            { 'm', 0x68},
            { 'n', 0x69},
            { 'o', 0x6A},
            { 'p', 0x6B},
            { 'q', 0x6C},
            { 'r', 0x6D},
            { 's', 0x6E},
            { 't', 0x6F},
            { 'u', 0x70},
            { 'v', 0x71},
            { 'w', 0x72},
            { 'x', 0x73},
            { 'y', 0x74},
            { 'z', 0x75},
            { 'À', 247 }, //Top Left Border
            { 'Á', 248 }, //Top border
            { 'Â', 249 }, //Top right border
            { 'Ã', 250 }, //Left border
            { 'Ä', 251 }, //right border
            { 'Å', 252 }, //bottom left
            { 'Æ', 253 }, //bottom
            { 'æ', 254 }, //bottom right
            { '¤', 0x21 }, // stone
            { 'ö', 0x22 }, // frog
            { '½', 0x23 }, // tiny
            { '¾', 0x24 }, // pig
            { '¼', 0x25 }, // mute
            { '¨', 0x26 }, // blind
            { '¬', 0x27 }, // poison 
            { '¯', 0x28 }, // floating
            { '¢', 0x29 }, // claw 
            { '£', 0x2A }, // rod
            { '¥', 0x2B }, // staff
            { '[', 0x2C }, // dark sword
            { '|', 0x2D }, // sword
            { ']', 0x2E }, // light sword
            { '{', 0x2F }, // spear
            { '`', 0x30 }, // knife
            { '}', 0x31 }, // katana
            { '<', 0x32 }, // shuriken
            { '>', 0x33 }, // boomerang
            { '«', 0x34 }, // axe
            { '»', 0x35 }, // wrench
            { '$', 0x36 }, // harp
            { '¹', 0x37 }, // bow
            { '²', 0x38 }, // arrow
            { '³', 0x39 }, // hammer 
            { '¦', 0x3A }, // whip
            { '®', 0x3B }, // shield
            { '©', 0x3C }, // helmet
            { '±', 0x3D }, // armor
            { '§', 0x3E }, // gauntlet
            { '•', 0x3F }, // black magic
            { 'º', 0x40 }, // white magic 
            { '·', 0x41 }, // call magic
            { 'Ñ', 0x76 }, // flat "M" 
            { 'Ð', 0x77 }, // flat "H"
            { 'Þ', 0x78 }, // flat "P"
            { '÷', 0x79 }, // tent
            { '¿', 0x7A }, // potion
            { '@', 0x7B }, // shirt
            { '#', 0x7C }, // ring
            { '*', 0x7D }, // crystal
            { '^', 0x7E }, // key
            { '~', 0x7F }, // tail
        };

        _fromRom = _toRom.ToDictionary(k => k.Value, k => k.Key);

        _symbols = new Dictionary<string, char>
        {
            { "stone",      '¤' },
            { "frog",       'ö' },
            { "tiny",       '½' },
            { "pig",        '¾' },
            { "mute",       '¼' },
            { "blind",      '¨' },
            { "poison",     '¬' },
            { "floating",   '¯' },
            { "claw",       '¢' },
            { "rod",        '£' },
            { "staff",      '¥' },
            { "darksword",  '[' },
            { "sword",      '|' },
            { "lightsword", ']' },
            { "spear",      '{' },
            { "knife",      '`' },
            { "katana",     '}' },
            { "shuriken",   '<' },
            { "boomerang",  '>' },
            { "axe",        '«' },
            { "wrench",     '»' },
            { "harp",       '$' },
            { "bow",        '¹' },
            { "arrow",      '²' },
            { "hammer",     '³' },
            { "whip",       '¦' },
            { "shield",     '®' },
            { "helmet",     '©' },
            { "armor",      '±' },
            { "gauntlet",   '§' },
            { "blackmagic", '•' },
            { "whitemagic", 'º' },
            { "callmagic",  '·' },
            { "flatm",      'Ñ' },
            { "flath",      'Ð' },
            { "flatp",      'Þ' },
            { "tent",       '÷' },
            { "potion",     '¿' },
            { "shirt",      '@' },
            { "ring",       '#' },
            { "crystal",    '*' },
            { "key",        '^' },
            { "tail",       '~' },
        };
    }

    public string ConvertTagsToSymbols(string s)
    {
        return Regex.Replace(s, "\\[(.*?)\\]", match => _symbols.TryGetValue(match.Groups[1].Value.ToLower().Replace(" ", ""), out var ch) ? $"{ch}" : "");
    }

    public override int GetByteCount(char[] chars, int index, int count)
    {
        return count;
    }

    public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
        var i = 0;
        for (; i < charCount; i++)
        {
            if (byteIndex + i >= bytes.Length || charIndex + i >= chars.Length)
                break;

            bytes[byteIndex+i] = _toRom[chars[charIndex+i]];
        }

        return i;
    }

    public override int GetCharCount(byte[] bytes, int index, int count)
    {
        return count;
    }

    public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
        var i = 0;
        for (; i < byteCount; i++)
        {
            if (byteIndex + i >= bytes.Length || charIndex + i >= chars.Length)
                break;

            chars[charIndex + i] = _fromRom[bytes[byteIndex+i]];
        }

        return i;
    }

    public override int GetMaxByteCount(int charCount)
    {
        return charCount;
    }

    public override int GetMaxCharCount(int byteCount)
    {
        return byteCount;
    }
}
