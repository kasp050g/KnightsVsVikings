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
    class DeleteRowCommand : ICommandSQLiteSingle, ISQLiteInput
    {
        public ISQLiteTable ExecuteOnTable { get; set; }
        public PropertyInfo Column { get; set; }
        public object Data { get; set; }

        public void Execute()
        {
            IDbConnection connection = ExecuteOnTable.Provider.CreateConnection();
            connection.Open();

            SQLiteCommand cmd = new SQLiteCommand($"DELETE FROM '{ExecuteOnTable.TableName}' WHERE {Column.Name} = {Data.ObjectToSQLiteString()};", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
