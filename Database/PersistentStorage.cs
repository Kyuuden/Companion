using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizHawk.FreeEnterprise.Companion.Database
{
    public class PersistentStorage
    {
        private const string dbName = "FreeEnterpriseCompanion.db";
        private readonly ISQLiteApi sql;
        private TimeSpan elapsedTime;

        public PersistentStorage(ISQLiteApi sqLiteApi, string hash)
        {
            sql = sqLiteApi;
            sql.OpenDatabase(dbName);
            sql.WriteCommand("CREATE TABLE IF NOT EXISTS RunTimes               (Hash TEXT NOT NULL PRIMARY KEY, RunTime TEXT NOT NULL);");
            sql.WriteCommand("CREATE TABLE IF NOT EXISTS KeyItemFoundTimes      (Hash TEXT NOT NULL, KeyItem INT NOT NULL,   Time TEXT NOT NULL, PRIMARY KEY (Hash, KeyItem));");
            sql.WriteCommand("CREATE TABLE IF NOT EXISTS KeyItemUsedTimes       (Hash TEXT NOT NULL, KeyItem INT NOT NULL,   Time TEXT NOT NULL, PRIMARY KEY (Hash, KeyItem));");
            sql.WriteCommand("CREATE TABLE IF NOT EXISTS LocationCheckTimes     (Hash TEXT NOT NULL, Location INT NOT NULL,  Time TEXT NOT NULL, PRIMARY KEY (Hash, Location));");
            sql.WriteCommand("CREATE TABLE IF NOT EXISTS ObjectiveCompleteTimes (Hash TEXT NOT NULL, Objective INT NOT NULL, Time TEXT NOT NULL, PRIMARY KEY (Hash, Objective));");
            sql.WriteCommand("CREATE TABLE IF NOT EXISTS BossCheckedTimes       (Hash TEXT NOT NULL, Boss INT NOT NULL,      Time TEXT NOT NULL, PRIMARY KEY (Hash, Boss));");

            Hash = hash;
            if (!TimeSpan.TryParse(ReadSql($"SELECT Runtime FROM RunTimes WHERE Hash = '{Hash}'")?.Values?.FirstOrDefault()?.ToString(), out elapsedTime))
                elapsedTime = TimeSpan.Zero;

            KeyItemFoundTimes = new TimeStorage<KeyItemType>(this, "KeyItemFoundTimes", "KeyItem");
            KeyItemUsedTimes = new TimeStorage<KeyItemType>(this, "KeyItemUsedTimes", "KeyItem");
            LocationCheckedTimes = new TimeStorage<int>(this, "LocationCheckTimes", "Location");
            ObjectiveCompleteTimes = new TimeStorage<int>(this, "ObjectiveCompleteTimes", "Objective");
            BossCheckedTimes = new TimeStorage<BossType>(this, "BossCheckedTimes", "Boss", true);
        }

        public TimeSpan ElapsedTime
        {
            get => elapsedTime;
            set
            {
                if (Hash == null || value < elapsedTime)
                    return;

                elapsedTime = value;
                sql.WriteCommand($"INSERT OR REPLACE INTO RunTimes (Hash, RunTime) VALUES ('{Hash}', '{value:c}')");
            }
        }

        public string? Hash { get; private set; }

        public TimeStorage<KeyItemType> KeyItemFoundTimes { get; }

        public TimeStorage<KeyItemType> KeyItemUsedTimes { get; }

        public TimeStorage<int> LocationCheckedTimes { get; }

        public TimeStorage<int> ObjectiveCompleteTimes { get; }

        public TimeStorage<BossType> BossCheckedTimes { get; }

        IDictionary<string, object>? ReadSql(string command)
            => sql.ReadCommand(command) as IDictionary<string, object>;

        public class TimeStorage<T> where T: struct
        {
            public TimeStorage(PersistentStorage parent, string tableName, string keyName, bool canDelete = false)
            {
                this.parent = parent;
                this.tableName = tableName;
                this.keyName = keyName;
                this.canDelete = canDelete;
                var dict = parent.ReadSql($"SELECT {keyName}, Time FROM {tableName} WHERE Hash = '{parent.Hash}'");
                if (dict != null)
                {
                    foreach (var tuple in dict.GroupBy(kvp => kvp.Key.Split(' ').Last()).Select(group => (group.First().Value, group.Last().Value)))
                        _data[Convert.ToInt32(tuple.Item1)] = TimeSpan.Parse(tuple.Item2.ToString());
                }
            }

            private Dictionary<int, TimeSpan> _data = new();
            private readonly PersistentStorage parent;
            private readonly string tableName;
            private readonly string keyName;
            private readonly bool canDelete;

            public TimeSpan? this[T key]
            {
                get => _data.TryGetValue(Convert.ToInt32(key), out var timeSpan) ? timeSpan : null;
                set
                {
                    if (!value.HasValue && canDelete)
                    {
                        _data.Remove(Convert.ToInt32(key));
                        parent.sql.WriteCommand($"DELETE FROM {tableName} WHERE Hash = '{parent.Hash}' AND {keyName} = {Convert.ToInt32(key)}");
                        return;
                    }
                    else if (!value.HasValue || _data.TryGetValue(Convert.ToInt32(key), out var timeSpan) && timeSpan < value)
                        return;

                    _data[Convert.ToInt32(key)] = value.Value;
                    parent.sql.WriteCommand($"INSERT OR REPLACE INTO {tableName} (Hash, {keyName}, Time) VALUES ('{parent.Hash}', {Convert.ToInt32(key)}, '{value.Value:c}')");
                }
            }
        }
    }
}
