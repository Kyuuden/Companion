using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FF.Rando.Companion.MysticQuestRandomizer.RomData;

public class TextEncoding : Encoding
{
    public const byte BorderTopLeft = 0xB7;
    public const byte BorderTop = 0xBC;
    public const byte BorderTopRight = 0xB9;
    public const byte BorderLeft = 0xBB;
    public const byte BorderRight = 0xC0;
    public const byte BorderBottomLeft = 0xB8;
    public const byte BorderBottom = 0xC1;
    public const byte BorderBottomRight = 0xBA;

    private readonly Dictionary<char, byte> _toRom;
    private readonly Dictionary<byte, char> _fromRom;
    private readonly Dictionary<string, char> _symbols;

    public TextEncoding()
    {
        _toRom = new()
        {
            { 'Ñ', 0x40},
            { 'Ð', 0x41},
            { 'Þ', 0x42},
            { '÷', 0x43},
            { '¿', 0x44},
            { '@', 0x45},
            { '#', 0x46},
            { '*', 0x47},
            { '^', 0x48},
            { '~', 0x49},

            { '0', 0x50},
            { '1', 0x51},
            { '2', 0x52},
            { '3', 0x53},
            { '4', 0x54},
            { '5', 0x55},
            { '6', 0x56},
            { '7', 0x57},
            { '8', 0x58},
            { '9', 0x59},
            { 'A', 0x5A},
            { 'B', 0x5B},
            { 'C', 0x5C},
            { 'D', 0x5D},
            { 'E', 0x5E},
            { 'F', 0x5F},
            { 'G', 0x60},
            { 'H', 0x61},
            { 'I', 0x62},
            { 'J', 0x63},
            { 'K', 0x64},
            { 'L', 0x65},
            { 'M', 0x66},
            { 'N', 0x67},
            { 'O', 0x68},
            { 'P', 0x69},
            { 'Q', 0x6A},
            { 'R', 0x6B},
            { 'S', 0x6C},
            { 'T', 0x6D},
            { 'U', 0x6E},
            { 'V', 0x6F},
            { 'W', 0x70},
            { 'X', 0x71},
            { 'Y', 0x72},
            { 'Z', 0x73},
            { 'a', 0x74},
            { 'b', 0x75},
            { 'c', 0x76},
            { 'd', 0x77},
            { 'e', 0x78},
            { 'f', 0x79},
            { 'g', 0x7A},
            { 'h', 0x7B},
            { 'i', 0x7C},
            { 'j', 0x7D},
            { 'k', 0x7E},
            { 'l', 0x7F},
            { 'm', 0x80},
            { 'n', 0x81},
            { 'o', 0x82},
            { 'p', 0x83},
            { 'q', 0x84},
            { 'r', 0x85},
            { 's', 0x86},
            { 't', 0x87},
            { 'u', 0x88},
            { 'v', 0x89},
            { 'w', 0x8A},
            { 'x', 0x8B},
            { 'y', 0x8C},
            { 'z', 0x8D},
            { '!', 0x8E},
            { '?', 0x8F},

            { ',', 0x90},
            { '\'', 0x91},
            { '.', 0x92},
            { '"', 0x93},
            { ';', 0x96},
            { ':', 0x97},
            //{ 'u', 0x98},...
            { '/', 0x99},
            { '-', 0x9A},
            { '&', 0x9B},
            { '>', 0x9C},
            { '%', 0x9D},
            //{ '!', 0x9E},
            //{ '?', 0x9F},
            { ' ', 0xBF },
            {'¤', 0xC2 },
            {'ö', 0xC3 },
            {'½', 0xC4 },
            {'¾', 0xC5 },
            {'¼', 0xCF },
            {'¬', 0xD0 },
            {'¨', 0xD1 },
            {'¯', 0xC6 },
            {'¢', 0xC7 },
            {'£', 0xC8 },
            {'¥', 0xC9 },
            {'[', 0xCA },
            {'|', 0xCB },
            {']', 0xCC },
            {'{', 0xCD },
            {'`', 0xCE },
        };

        _fromRom = _toRom.ToDictionary(k => k.Value, k => k.Key);

        _symbols = new Dictionary<string, char>
        {
            { "earth",      '¤' },//
            { "water",      'ö' },//
            { "fire",       '½' },//
            { "air",        '¾' },//
            { "holy",       '¼' },//
            { "axe",        '¨' },//
            { "bomb",       '¬' },//
            { "projectile", '¯' },//
            { "doom",       '¢' },//
            { "stone",      '£' },//
            { "paralysis",  ']' },//
            { "sleep",      '{' },//
            { "confusion",  '`' },//
            { "poison",     '|' },//
            { "blind",      '[' },//
            { "silence",    '¥' },//
            { "0",          'Ñ' },
            { "1",          'Ð' },
            { "2",          'Þ' },
            { "3",          '÷' },
            { "4",          '¿' },
            { "5",          '@' },
            { "6",          '#' },
            { "7",          '*' },
            { "8",          '^' },
            { "9",          '~' },
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
