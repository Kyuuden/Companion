using FF.Rando.Companion.Games.FreeEnterprise.RomData;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.FreeEnterprise;

internal abstract class LegacySeed : SeedBase
{
    private const uint ZeromusDeathAnimation = 0x03F591;
    private const uint MenuSaveNewGame = 0x019914;
    //private const int MenuLoadSaveGame = 0x0198AD;

    private enum RunState
    {
        Loading,
        Menu,
        RunStarted,
        RunFinished,
        Unknown
    }

    private RunState _state;
    private readonly Timer _loadingTimer;

    protected bool IsLoading => _state == RunState.Loading;

    public override bool RequiresMemoryEventsForTiming => true;

    protected abstract bool OWinGame { get; }

    public LegacySeed(string hash, Metadata metadata, EmulationContainer<FreeEnterpriseSettings> container)
        : base(hash, metadata, container)
    {
        _state = RunState.Loading;
        _loadingTimer = new Timer
        {
            Enabled = true,
            Interval = 2000,
        };
        _loadingTimer.Tick += LoadingTimer_Tick;
        CreateCallbacks();
    }

    private void LoadingTimer_Tick(object sender, EventArgs e)
    {
        _loadingTimer.Stop();
        _loadingTimer.Dispose();
        _state = RunState.Menu;
        CreateCallbacks();
    }

    private uint? StartNewGame(uint address, uint value, uint flags)
    {
        RemoveCallbacks();
        _state = RunState.RunStarted;
        Started = true;
        CreateCallbacks();
        return null;
    }

    private uint? Flash(uint address, uint value, uint flags)
    {
        RemoveCallbacks();
        Victory = true;
        _state = RunState.RunFinished;
        return null;
    }

    private void CreateCallbacks()
    {
        try
        {
            switch (_state)
            {
                case RunState.RunStarted:
                    if (MemoryEvents == null)
                        throw new KeyNotFoundException();

                    MemoryEvents?.AddExecCallback(Flash, ZeromusDeathAnimation, "System Bus");
                    break;
                case RunState.Menu:
                    if (MemoryEvents == null)
                        throw new KeyNotFoundException();

                    MemoryEvents?.AddExecCallback(StartNewGame, MenuSaveNewGame, "System Bus");
                    break;
            }
        }
        catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
        {
        }
    }

    private void RemoveCallbacks()
    {
        try
        {
            switch (_state)
            {
                case RunState.RunStarted:
                    MemoryEvents?.RemoveMemoryCallback(Flash);
                    break;
                case RunState.Menu:
                    MemoryEvents?.RemoveMemoryCallback(StartNewGame);
                    break;
            }
        }
        catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
        {
        }
    }

    protected override bool CheckIfVictory()
    {
        if (!IsLoading && OWinGame)
        {
            var time = Wram.ReadBytes(Shared.Addresses.WRAM.EndGameTime);
            if (time.Any(t => t != 0))
            {
                RemoveCallbacks();
                _state = RunState.RunFinished;
                return true;
            }
        }

        return base.CheckIfVictory();
    }
}
