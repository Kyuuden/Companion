using FF.Rando.Companion.FreeEnterprise.RomData;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.FreeEnterprise;

internal abstract class LegacySeed : SeedBase
{
    private const int ZeromusDeathAnimation = 0x03F591;
    private const int MenuSaveNewGame = 0x019914;
    private const int MenuLoadSaveGame = 0x0198AD;

    private enum RunState
    {
        Loading,
        Menu,
        RunStarted,
        RunFinished,
        Unknown
    }

    private RunState _state;
    private Timer _loadingTimer;

    protected bool IsLoading => _state == RunState.Loading;

    public override bool RequiresMemoryEvents => true;

    protected abstract bool OWinGame { get; }

    public LegacySeed(string hash, Metadata metadata, Container container)
    : base(hash, metadata, container)
    {
        _state = RunState.Loading;
        _loadingTimer = new Timer
        {
            Enabled = true,
            Interval = 2000,
        };
        _loadingTimer.Tick += _loadingTimer_Tick;
        CreateCallbacks();
    }

    private void _loadingTimer_Tick(object sender, EventArgs e)
    {
        _loadingTimer.Stop();
        _loadingTimer.Dispose();
        _state = RunState.Menu;
        CreateCallbacks();
    }

    private void StartNewGame(uint address, uint value, uint flags)
    {
        RemoveCallbacks();
        _state = RunState.RunStarted;
        Started = true;
        CreateCallbacks();
    }

    private void Flash(uint address, uint value, uint flags)
    {
        RemoveCallbacks();
        Victory = true;
        _state = RunState.RunFinished;
    }

    private void CreateCallbacks()
    {
        try
        {
            switch (_state)
            {
                case RunState.RunStarted:
                    if (Game.MemoryEvents == null)
                        throw new KeyNotFoundException();

                    Game.MemoryEvents?.AddExecCallback(Flash, ZeromusDeathAnimation, "System Bus");
                    break;
                case RunState.Menu:
                    if (Game.MemoryEvents == null)
                        throw new KeyNotFoundException();

                    Game.MemoryEvents?.AddExecCallback(StartNewGame, MenuSaveNewGame, "System Bus");
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
                    Game.MemoryEvents?.RemoveMemoryCallback(Flash);
                    break;
                case RunState.Menu:
                    Game.MemoryEvents?.RemoveMemoryCallback(StartNewGame);
                    break;
            }
        }
        catch (KeyNotFoundException) //snes9X core doesn't support exec callbacks
        {
        }
    }

    public override void OnNewFrame()
    {
        base.OnNewFrame();

        if (!IsLoading && OWinGame)
        {
            var time = Game.Wram.ReadBytes(Shared.Addresses.WRAM.EndGameTime);
            if (time.Any(t => t != 0))
            {
                RemoveCallbacks();
                Victory = true;
                _state = RunState.RunFinished;
            }
        }
    }
}
