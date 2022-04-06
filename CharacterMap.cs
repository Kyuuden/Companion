using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizHawk.FreeEnterprise.Companion
{
    public static class CharacterMap
    {
        public const char BorderTopLeft = '\xDA';
        public const char BorderTop = '\xC2';
        public const char BorderTopRight = '\xBF';
        public const char BorderLeft = '\xC3';
        public const char BorderRight = '\xb4';
        public const char BorderBottomLeft = '\xC0';
        public const char BorderBottom = '\xc1';
        public const char BorderBottomRight = '\xD9';

        public static string ToGame(this string s)
            => s.Select(ToGame).Aggregate(new StringBuilder(), (seed, c) => seed.Append(c)).ToString();

        public static string FromGame(this string s)
            => s.Select(FromGame).Aggregate(new StringBuilder(), (seed, c) => seed.Append(c)).ToString();

        public static char ToGame(this char c)
            => (char)(c switch
            {
                ' ' => 0xFF,
                '\''=> 0xC0,
                '.' => 0xC1,
                '-' => 0xC2,
                '_' => 0xC3,
                '!' => 0xC4,
                '?' => 0xC5,
                '%' => 0xC6,
                '/' => 0xC7,
                ':' => 0xC8,
                ',' => 0xC9,
                '&' => 0xCA,
                '+' => 0xCB,
                '(' => 0xCC,
                ')' => 0xCD,
                '0' => 0x80,
                '1' => 0x81,
                '2' => 0x82,
                '3' => 0x83,
                '4' => 0x84,
                '5' => 0x85,
                '6' => 0x86,
                '7' => 0x87,
                '8' => 0x88,
                '9' => 0x89,
                'A' => 0x42,
                'B' => 0x43,
                'C' => 0x44,
                'D' => 0x45,
                'E' => 0x46,
                'F' => 0x47,
                'G' => 0x48,
                'H' => 0x49,
                'I' => 0x4A,
                'J' => 0x4B,
                'K' => 0x4C,
                'L' => 0x4D,
                'M' => 0x4E,
                'N' => 0x4F,
                'O' => 0x50,
                'P' => 0x51,
                'Q' => 0x52,
                'R' => 0x53,
                'S' => 0x54,
                'T' => 0x55,
                'U' => 0x56,
                'V' => 0x57,
                'W' => 0x58,
                'X' => 0x59,
                'Y' => 0x5A,
                'Z' => 0x5B,
                'a' => 0x5C,
                'b' => 0x5D,
                'c' => 0x5E,
                'd' => 0x5F,
                'e' => 0x60,
                'f' => 0x61,
                'g' => 0x62,
                'h' => 0x63,
                'i' => 0x64,
                'j' => 0x65,
                'k' => 0x66,
                'l' => 0x67,
                'm' => 0x68,
                'n' => 0x69,
                'o' => 0x6A,
                'p' => 0x6B,
                'q' => 0x6C,
                'r' => 0x6D,
                's' => 0x6E,
                't' => 0x6F,
                'u' => 0x70,
                'v' => 0x71,
                'w' => 0x72,
                'x' => 0x73,
                'y' => 0x74,
                'z' => 0x75,
                '*' => 125, //Crystal
                '^' => 126, //Key
                '~' => 127, //Tail
                '$' => 54,  //Note
                '|' => 46,  //Sword
                '`' => 48,  //Dagger
                '•' => 64,
                'º' => 65,
                BorderTopLeft => 247, //Top Left Border
                BorderTop => 248, //Top border
                BorderTopRight => 249, //Top right border
                BorderLeft => 250, //Left border
                BorderRight => 251, //right border
                BorderBottomLeft => 252, //bottom left
                BorderBottom => 253, //bottom
                BorderBottomRight => 254, //bottom right
                _ => 125
            });

        public static char FromGame(this char c)
            => ((int)c) switch
            {
                0xFF => ' ',
                0xC0 => '\'',
                0xC1 => '.',
                0xC2 => '-',
                0xC3 => '_',
                0xC4 => '!',
                0xC5 => '?',
                0xC6 => '%',
                0xC7 => '/',
                0xC8 => ':',
                0xC9 => ',',
                0xCA => '&',
                0xCB => '+',
                0xCC => '(',
                0xCD => ')',
                0x80 => '0',
                0x81 => '1',
                0x82 => '2',
                0x83 => '3',
                0x84 => '4',
                0x85 => '5',
                0x86 => '6',
                0x87 => '7',
                0x88 => '8',
                0x89 => '9',
                0x42 => 'A',
                0x43 => 'B',
                0x44 => 'C',
                0x45 => 'D',
                0x46 => 'E',
                0x47 => 'F',
                0x48 => 'G',
                0x49 => 'H',
                0x4A => 'I',
                0x4B => 'J',
                0x4C => 'K',
                0x4D => 'L',
                0x4E => 'M',
                0x4F => 'N',
                0x50 => 'O',
                0x51 => 'P',
                0x52 => 'Q',
                0x53 => 'R',
                0x54 => 'S',
                0x55 => 'T',
                0x56 => 'U',
                0x57 => 'V',
                0x58 => 'W',
                0x59 => 'X',
                0x5A => 'Y',
                0x5B => 'Z',
                0x5C => 'a',
                0x5D => 'b',
                0x5E => 'c',
                0x5F => 'd',
                0x60 => 'e',
                0x61 => 'f',
                0x62 => 'g',
                0x63 => 'h',
                0x64 => 'i',
                0x65 => 'j',
                0x66 => 'k',
                0x67 => 'l',
                0x68 => 'm',
                0x69 => 'n',
                0x6A => 'o',
                0x6B => 'p',
                0x6C => 'q',
                0x6D => 'r',
                0x6E => 's',
                0x6F => 't',
                0x70 => 'u',
                0x71 => 'v',
                0x72 => 'w',
                0x73 => 'x',
                0x74 => 'y',
                0x75 => 'z',
                125 => '*',  //Crystal
                126 => '^',  //Key
                127 => '~',  //Tail
                54 => '$',  //Note
                46 => '|',  //Sword
                48 => '`',  //Dagger
                64 => '•',
                65 => 'º',
                247 => BorderTopLeft, //Top Left Border
                248 => BorderTop, //Top border
                249 => BorderTopRight, //Top right border
                250 => BorderLeft, //Left border
                251 => BorderRight, //right border
                252 => BorderBottomLeft, //bottom left
                253 => BorderBottom, //bottom
                254 => BorderBottomRight, //bottom right
                _ => ' '
            };
    }
}
