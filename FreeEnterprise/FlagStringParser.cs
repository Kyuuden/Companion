using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.FreeEnterprise;

public enum MajorFlag
{
    Objectives = 'O',
    KeyItems = 'K',
    Pass = 'P',
    Charactes ='C',
    Treasures = 'T',
    Shops = 'S',
    Bosses = 'B',
    Encounts = 'E',
    HarpOptions = 'O',
    Other = '-'
}

internal class ParsedFlags
{
    
}

internal class ParsedFlag
{
    public MajorFlag MajorFlag { get; }


}

internal class FlagStringParser
{
}
