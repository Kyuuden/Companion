using System;
using System.ComponentModel;

namespace FF.Rando.Companion.FreeEnterprise;

public interface ITask : INotifyPropertyChanged
{
    string Description { get; }
    TimeSpan? CompletedAt { get; }
    bool IsCompleted { get; }
}
