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
    class RenameTableCommand : ICommandSQLiteSingle
    {
        public ISQLiteTable ExecuteOnTable { get; set; }
        public string NewTableName { get; set; }

        public void Execute()
        {
            IDbConnection connection = ExecuteOnTable.Provider.CreateConnection();
            connection.Open();

            SQLiteCommand cmd = new SQLiteCommand($"ALTER TABLE '{ExecuteOnTable.TableName}' RENAME TO '{NewTableName}';", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            ExecuteOnTable.TableName = NewTableName;

            connection.Close();
        }
    }
}
