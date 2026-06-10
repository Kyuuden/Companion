using System;
using System.Diagnostics;

namespace FF.Rando.Companion.Timing;
internal class InternalTimer : ITimer
{
    private readonly Stopwatch _stopwatch = new();
    private bool _isRunning = false;
    private bool _isPaused = false;

    public bool ShowLocally => true;

    public bool IsRunning => _isRunning;
    

    public TimeSpan? Elapsed => _stopwatch.Elapsed;

    public TimerStatus Status
    {
        get
        {
            if (_isRunning && !_isPaused) return TimerStatus.Running;
            if (_isRunning) return TimerStatus.Paused;
            return TimerStatus.Ready;
        }
    }

    public void Info(string message)
    {
    }

    public void Initialize()
    {
        _isRunning = false;
        _stopwatch.Reset();
    }

    public void Start()
    {
        _isRunning = true;
        _stopwatch.Start();
    }

    public void Stop()
    {
        _isRunning = false;
        _stopwatch.Stop();
    }

    public void Pause()
    {
        if (!_isRunning || _isPaused)
            return;

        _stopwatch.Stop();
    }

    public void Resume()
    {
        if (!_isRunning || !_isPaused)
            return;

        _stopwatch.Start();
    }

    public void Dispose()
    {
    }
}
