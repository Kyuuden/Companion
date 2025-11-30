using FF.Rando.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.MysticQuestRandomizer;
public class GameState
{
    private byte[]? _state;
    private readonly byte[] _knownFlags = [0x01, 0x02, 0x12, 0x13, 0x14, 0x1D, 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x2B, 0x34, 0x35, 0x36, 0x3A, 0x3D, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4D, 0x4E, 0x4F, 0x53, 0x5A, 0x5B, 0x5C, 0x5D, 0x5D, 0x5F, 0x62, 0x63, 0x64, 0x68, 0x6C, 0x6D, 0x6E, 0x70, 0x73, 0x74, 0x7D, 0x7E, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF, 0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE, 0xBF, 0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC9, 0xCF, 0xD0, 0xD1, 0xD2, 0xE0, 0xE3, 0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF, 0xF0, 0xF9];
    private readonly byte[] _unknownFlags;

    public GameState()
    {
        _unknownFlags = Enumerable
            .Range(0,255)
            .Select(i => ReversedBitOrderIndex((byte)i))
            .Except(_knownFlags.Select(ReversedBitOrderIndex))
            .ToArray();
    }


    public bool Update(TimeSpan time, ReadOnlySpan<byte> found)
    {
        if (_state == null || !found.SequenceEqual(_state))
        {
            if (_state != null)
            {
                foreach (var i in _unknownFlags)
                    if (found.Read<bool>(i) != _state.Read<bool>(i))
                        Debug.WriteLine($"Uknown Flag # {ReversedBitOrderIndex(i)} changed from {_state.Read<bool>(i)} to {found.Read<bool>(i)}");
            }

            _state = found.ToArray();

            return true;
        }

        return false;
    }

    private static byte ReversedBitOrderIndex(byte value)
    {
        var index = value / 8;
        var offset = value % 8;
        return (byte)((index * 8) + 7 - offset);
    }

    public bool FlamerusRexDefeated => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x01));
    public bool WakeWaterUsed => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x02));
    public bool IceGolemDefeated => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x12));
    public bool OldManSaved => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x13));
    public bool TreeWitherAcquired => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x14));
    public bool GiantTreeSet => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x1D));
    public bool MinotaurDefeated => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x1E));
    public bool KaeliCured => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x1F));
    public bool ShowBarrelMoved => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x20));
    public bool ShowBarrelNotMoved => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x21));
    public bool HillCollapsed => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x22));
    public bool SandCoinUsed => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x23));
    public bool ShowDullahanChest => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x2B));
    public bool ShowForestaBoulder => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x34));
    public bool WintryCaveCollapsed => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x35));
    public bool ShowFireburgBoulder => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x36));
    public bool GiantTreeUnset => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x3A));
    public bool ShowPazuzuBridge => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x3D));
    public bool ShowPazuzu1F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x40));
    public bool ShowPazuzu2F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x41));
    public bool ShowPazuzu3F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x42));
    public bool ShowPazuzu4F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x43));
    public bool ShowPazuzu5F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x44));
    public bool ShowPazuzu6F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x45));
    public bool ShowPazuzu7F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x46));
    public bool PazuzuSwitch2F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x47));
    public bool PazuzuSwitch4F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x48));
    public bool PazuzuSwitch6F => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x49));
    public bool PhoebeHouseVisited => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x4D));
    public bool ShowLibraTemplePhoebe => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x4E));
    public bool UseWakeWater => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x4F));
    public bool ShowFireburgTristam => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x53));
    public bool ShowSandTempleTristam => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x5A));
    public bool UseRiverCoin => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x5B));
    public bool BoulderRolled => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x5C));
    public bool DefeatMedusa => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x5D));
    public bool ShowMedusaChest => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x5D));
    public bool ShowFireburgReuben1 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x5F));
    public bool ShowForestaKaeli => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x62));
    public bool EnableMinotaurFight => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x63));
    public bool VolcanoErupted => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x64));
    public bool ShowWindiaKaeli => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x68));
    public bool ShowMysteriousManLevelForest => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x6C));
    public bool HideDiseasedTree => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x6D));
    public bool TalkToTristam => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x6E));
    public bool TalkToPhoebe => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x70));
    public bool ExitFallBasin => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x73));
    public bool TalkToGrenadeGuy => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x74));
    public bool ShowSickKaeli => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x7D));
    public bool ShowWindiaPhoebe => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0x7E));
    public bool KaeliQuest1 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xA9));
    public bool KaeliQuest2 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xAA));
    public bool KaeliQuest3 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xAB));
    public bool KaeliQuest4 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xAC));
    public bool TristamQuest1 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xAD));
    public bool TristamQuest2 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xAE));
    public bool TristamQuest3 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xAF));
    public bool TristamQuest4 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB0));
    public bool PhoebeQuest1 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB1));
    public bool PhoebeQuest2 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB2));
    public bool PhoebeQuest3 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB3));
    public bool PhoebeQuest4 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB4));
    public bool ReubenQuest1 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB5));
    public bool ReubenQuest2 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB6));
    public bool ReubenQuest3 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB7));
    public bool ReubenQuest4 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB8));
    public bool ShowBoneDungeonTristam => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xB9));
    public bool ShowForestaKaelisMom => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xBA));
    public bool ShowMineReuben => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xBB));
    public bool ShowFireburgReuben2 => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xBC));
    public bool ShowWindiaKaelisMom => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xBD));
    public bool ShowWintryCavePhoebe => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xBE));
    public bool ShowLevelForestKaeli => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xBF));
    public bool ShowCrabChest => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xC0));
    public bool ForestaHintGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xC1));
    public bool AquariaHintGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xC2));
    public bool FireburgHintGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xC3));
    public bool WindiaHintGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xC4));
    public bool SpencerCaveBombed => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xC5));
    public bool TristamBoneDungeonItemGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xC9));
    public bool RainbowRoad => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xCF));
    public bool AquariaSellerItemBought => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xD0));
    public bool FireburgSellerItemBought => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xD1));
    public bool WindiaSellerItemBought => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xD2));
    public bool SquidDefeated => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xE0));
    public bool KaeliOpenedPath => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xE3));
    public bool KaeliSecondItemGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xE8));
    public bool TristamFireburgItemGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xE9));
    public bool PhoebeWintryItemGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xEA));
    public bool ReubenMineItemGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xEB));
    public bool ArionItemGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xEC));
    public bool TristamChestUnopened => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xED));
    public bool VenusChestUnopened => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xEE));
    public bool SpencerItemGiven => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xEF));
    public bool ShowFigureForHP => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xF0));
    public bool ShowEnemies => _state != null && _state.Read<bool>(ReversedBitOrderIndex(0xF9));
}
