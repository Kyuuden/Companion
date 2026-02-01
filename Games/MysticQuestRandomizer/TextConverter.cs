using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.Rando.Companion.Games.MysticQuestRandomizer;
internal class TextConverter
{
    private readonly List<(string, int)> _textDTE =
    [
        ("\n", 0x01),
        ("|", 0x06), // enemy name linefeed if in box, otherwise space
		("#", 0x36), // end of box
		("Crystal", 0x3d),
        ("Rainbow Road", 0x3e), // DTE in DTE...
		("th", 0x3f),
        ("e ", 0x40),
        ("the ", 0x41),
        ("t ", 0x42),
        ("ou", 0x43),
        ("you", 0x44),
        ("s ", 0x45),
        ("to ", 0x46),
        ("in", 0x47),
        ("ing ", 0x48),
        ("l ", 0x49),
        ("ll ", 0x4a),
        ("er", 0x4b),
        ("d ", 0x4c),
        (", ", 0x4d),
        ("'s ", 0x4e),
        ("an", 0x4f),
		// 0x50 05 1d 9e 00 04
		// 0x51 05 1e 9e 00 04 0x1bb25
		("ight", 0x52),
        ("...", 0x53),
        ("on", 0x54),
        ("you ", 0x55), // dte
		("en", 0x56),
        ("ha", 0x57),
        ("ow", 0x58),
        ("y ", 0x59),
        ("of ", 0x5a),
        ("Th", 0x5b),
        ("or", 0x5c),
        ("I'll ", 0x5d), //dte
		("ea", 0x5e),
        ("is ", 0x5f), //dte
		("es", 0x60),
		// 0x61 08 62 81 ?? 
		// 0x62 08 8a 87 ??
		("wa", 0x63),
        ("again", 0x64), // dte
		("st", 0x65),
        ("I ", 0x66),
        ("ve ", 0x67), //dte
		("ed ", 0x68), //dte
		("om", 0x69),
        ("er ", 0x6a), //dte
		("p ", 0x6b),
        ("ack", 0x6c),
        ("ust ", 0x6d), //dte
		("!#", 0x6e),
        ("!\n", 0x6f),
        ("that ", 0x70), //dte
		("prophecy", 0x71),
        ("o ", 0x72),
        (".\n", 0x73),
        (".#", 0x74),
        ("I'm ", 0x75),
        ("el", 0x76),
        ("with ", 0x77), //dte
		("a ", 0x78),
        ("Spencer", 0x79), //dte
		("ma", 0x7a),
        ("in ", 0x7b), //dte
		("monst", 0x7c), //dte
		("k ", 0x7d),
        ("'t ", 0x7e), //dte

		("0", 0x90),
        ("1", 0x91),
        ("2", 0x92),
        ("3", 0x93),
        ("4", 0x94),
        ("5", 0x95),
        ("6", 0x96),
        ("7", 0x97),
        ("8", 0x98),
        ("9", 0x99),

        ("A", 0x9a),
        ("B", 0x9b),
        ("C", 0x9c),
        ("D", 0x9d),
        ("E", 0x9e),
        ("F", 0x9f),
        ("G", 0xa0),
        ("H", 0xa1),
        ("I", 0xa2),
        ("J", 0xa3),
        ("K", 0xa4),
        ("L", 0xa5),
        ("M", 0xa6),
        ("N", 0xa7),
        ("O", 0xa8),
        ("P", 0xa9),
        ("Q", 0xaa),
        ("R", 0xab),
        ("S", 0xac),
        ("T", 0xad),
        ("U", 0xae),
        ("V", 0xaf),
        ("W", 0xb0),
        ("X", 0xb1),
        ("Y", 0xb2),
        ("Z", 0xb3),

        ("a", 0xb4),
        ("b", 0xb5),
        ("c", 0xb6),
        ("d", 0xb7),
        ("e", 0xb8),
        ("f", 0xb9),
        ("g", 0xba),
        ("h", 0xbb),
        ("i", 0xbc),
        ("j", 0xbd),
        ("k", 0xbe),
        ("l", 0xbf),
        ("m", 0xc0),
        ("n", 0xc1),
        ("o", 0xc2),
        ("p", 0xc3),
        ("q", 0xc4),
        ("r", 0xc5),
        ("s", 0xc6),
        ("t", 0xc7),
        ("u", 0xc8),
        ("v", 0xc9),
        ("w", 0xca),
        ("x", 0xcb),
        ("y", 0xcc),
        ("z", 0xcd),

        ("!", 0xce),
        ("?", 0xcf),
        (",", 0xd0),
        ("'", 0xd1),
        (".", 0xd2),
        ("<\"", 0xd3), // opening apostroph
		("\">", 0xd4), // closing apostroph
		(".\">", 0xd5),
        (";", 0xd6),
        (":", 0xd7),
        ("[...]", 0xd8), // small 3 dots
		("/", 0xd9),
        ("-", 0xda),
        ("&", 0xdb),
        (">", 0xdc),
        ("%", 0xdd),

        (" ", 0xff)
    ];

    public byte[] TextToByte(string text, bool enabledte = true)
    {
        byte[] byteText = new byte[text.Length];

        var orderedDTE = enabledte ? _textDTE.OrderByDescending(x => x.Item1.Length) : _textDTE.Where(x => x.Item2 < 0x3D || x.Item2 > 0x7F).OrderByDescending(x => x.Item1.Length);

        string blackoutString = "************";

        foreach (var dte in orderedDTE)
        {
            for (int index = 0; ; index += dte.Item1.Length)
            {
                index = text.IndexOf(dte.Item1, index);
                if (index == -1)
                    break;
                text = text.Remove(index, dte.Item1.Length).Insert(index, blackoutString[..dte.Item1.Length]);
                byteText[index] = (byte)dte.Item2;
            }
        }
        return byteText.Where(x => x != 0x00).ToArray();
    }

    public string BytesToText(byte[] byteSeries)
    {
        StringBuilder sb = new(byteSeries.Length);

        foreach (var byteInSeries in byteSeries)
        {
            int letterIndex = _textDTE.FindIndex(x => x.Item2 == byteInSeries);
            sb.Append(letterIndex >= 0 ? _textDTE[letterIndex].Item1 : $"[{byteInSeries:X2}]");
        }

        return sb.ToString();
    }

    public string SpanToText(ReadOnlySpan<byte> byteSpan, bool ignoreUnknown = false)
    {
        StringBuilder sb = new(byteSpan.Length);

        foreach (var b in byteSpan)
        {
            int letterIndex = _textDTE.FindIndex(x => x.Item2 == b);

            if (letterIndex >= 0)
                sb.Append(_textDTE[letterIndex].Item1);
            else if (!ignoreUnknown)
                sb.Append($"[{b:X2}]");
        }

        return sb.ToString();
    }

}
