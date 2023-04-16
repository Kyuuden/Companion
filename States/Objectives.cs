using BizHawk.FreeEnterprise.Companion.Configuration;
using BizHawk.FreeEnterprise.Companion.Database;
using BizHawk.FreeEnterprise.Companion.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Objectives
    {
        private readonly Settings _settings;
        private readonly PersistentStorage.TimeStorage<int> _objectiveTimeStorage;
        private readonly List<string> _objectives;
        private byte[] _completionData;

        public Objectives(Settings settings, PersistentStorage.TimeStorage<int> objectiveTimeStorage)
            : this(settings, new List<string>(), objectiveTimeStorage)
        {
        }

        public Objectives(Settings settings, IEnumerable<string> objectives, PersistentStorage.TimeStorage<int> objectiveTimeStorage)
        {
            _objectives = objectives.ToList();
            _completionData = new byte[this._objectives.Count];
            _settings = settings;
            _objectiveTimeStorage = objectiveTimeStorage;
        }

        public bool Update(TimeSpan now, byte[] data)
        {
            var newCompletion = data.Take(_objectives.Count).ToList();
            if (!_completionData.SequenceEqual(newCompletion))
            {
                newCompletion.CopyTo(_completionData);
                for (int i = 0; i < _completionData.Length; i++)
                    if (_completionData[i] != 0)
                        _objectiveTimeStorage[i] = now;

                return true;
            }

            return false;
        }

        public int NumCompleted => _completionData.Count(d => d != 0);

        public IEnumerable<ObjectiveStatus> Statuses
        {
            get
            {
                for (int i = 0; i < _objectives.Count; i++)
                    yield return new (_settings, _objectives[i], _completionData[i] != 0, _objectiveTimeStorage[i]);
            }
        }
    }

    public class ObjectiveStatus
    {
        private readonly Settings _settings;

        public ObjectiveStatus(Settings settings, string description, bool isComplete, TimeSpan? completeTime)
        {
            _settings = settings;
            Description = description;
            IsComplete = isComplete;
            CompleteTime = completeTime;
        }

        public string Description { get; }
        public bool IsComplete { get; }
        public TimeSpan? CompleteTime { get; }

        public override string ToString()
            => $"{Description}{(IsComplete && CompleteTime.HasValue ? $" - {CompleteTime.Value.ToString(_settings.TimeFormatString)}" : string.Empty)}";
    }
}
