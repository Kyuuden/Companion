using BizHawk.FreeEnterprise.Companion.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.FreeEnterprise.Companion.State
{
    public class Objectives
    {
        private readonly PersistentStorage.TimeStorage<int> objectiveTimeStorage;
        private readonly List<string> objectives;
        private byte[] completionData;

        public Objectives(PersistentStorage.TimeStorage<int> objectiveTimeStorage)
            : this(new List<string>(), objectiveTimeStorage)
        {
        }

        public Objectives(IEnumerable<string> objectives, PersistentStorage.TimeStorage<int> objectiveTimeStorage)
        {
            this.objectives = objectives.ToList();
            completionData = new byte[this.objectives.Count];
            this.objectiveTimeStorage = objectiveTimeStorage;
        }

        public bool Update(TimeSpan now, byte[] data)
        {
            var newCompletion = data.Take(objectives.Count).ToList();
            if (!completionData.SequenceEqual(newCompletion))
            {
                newCompletion.CopyTo(completionData);
                for (int i = 0; i < completionData.Length; i++)
                    if (completionData[i] != 0)
                        objectiveTimeStorage[i] = now;

                return true;
            }

            return false;
        }

        public IEnumerable<ObjectiveStatus> Statuses
        {
            get
            {
                for (int i = 0; i < objectives.Count; i++)
                    yield return new (objectives[i], completionData[i] != 0, objectiveTimeStorage[i]);
            }
        }
    }

    public class ObjectiveStatus
    {
        public ObjectiveStatus(string description, bool isComplete, TimeSpan? completeTime)
        {
            Description = description;
            IsComplete = isComplete;
            CompleteTime = completeTime;
        }

        public string Description { get; }
        public bool IsComplete { get; }
        public TimeSpan? CompleteTime { get; }

        public override string ToString()
            => $"{Description}{(IsComplete && CompleteTime.HasValue ? $" - {CompleteTime.Value:c}" : string.Empty)}";
    }
}
