using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Interfaces.Patterns.CommandPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern.SQLCommands
{
    // Lucas
    class UpdateRowCommand : ICommandSQLiteSingle
    {
        public ISQLiteTable ExecuteOnTable { get; set; }
        public int[] Ids { get; set; } = null;
        public Dictionary<PropertyInfo, object> UpdatedData { get; set; } = new Dictionary<PropertyInfo, object>();

        public void Execute()
        {
            IDbConnection connection = ExecuteOnTable.Provider.CreateConnection();
            connection.Open();

            SQLiteCommand cmd;

            if (Ids.Length == 1)
                cmd = new SQLiteCommand($"UPDATE '{ExecuteOnTable.TableName}' SET {UpdateDataToQuery(UpdatedData)} WHERE Id = {Ids[0]};", (SQLiteConnection)connection);
            else if (Ids.Length > 1)
                cmd = new SQLiteCommand($"UPDATE '{ExecuteOnTable.TableName}' SET {UpdateDataToQuery(UpdatedData)} WHERE Id IN ({string.Join(", ", Ids)});", (SQLiteConnection)connection);
            else
                cmd = new SQLiteCommand($"UPDATE '{ExecuteOnTable.TableName}' SET {UpdateDataToQuery(UpdatedData)};", (SQLiteConnection)connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        private string UpdateDataToQuery(Dictionary<PropertyInfo, object> pairs)
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<PropertyInfo, object> pair in pairs)
                result.Add($"{pair.Key.Name} = {pair.Value.ObjectToSQLiteString()}");

            return string.Join(", ", result);
        }
    }
}
