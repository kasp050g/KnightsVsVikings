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
    class InsertMultipleRowsCommand : ICommandSQLiteSingle, ISQLiteOutput
    {
        public ISQLiteTable ExecuteOnTable { get; set; }
        public List<ISQLiteRow> ResultRows { get; set; }

        public void Execute()
        {
            IDbConnection connection = ExecuteOnTable.Provider.CreateConnection();
            connection.Open();

            string cmdInput = $"INSERT INTO '{ExecuteOnTable.TableName}' ({ResultRows.First().GetPropertiesToString()}) VALUES ";

            foreach(ISQLiteRow row in ResultRows)
            {
                cmdInput += $"({row.GetValuesToString()})";

                if (row == ResultRows.Last())
                    cmdInput += ";";
                else
                    cmdInput += ", ";
            }

            SQLiteCommand cmd = new SQLiteCommand(cmdInput, (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
