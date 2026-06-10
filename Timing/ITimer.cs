using System;

namespace FF.Rando.Companion.Timing;

public enum TimerStatus
{
    New,
    Ready,
    Running,
    Paused,
    Error
}

public interface ITimer : IDisposable
{
    bool ShowLocally { get; }
    TimeSpan? Elapsed { get; }
    TimerStatus Status { get; }
    void Initialize();
    void Start();
    void Stop();
    void Pause();
    void Resume();
    void Info(string message);
}
