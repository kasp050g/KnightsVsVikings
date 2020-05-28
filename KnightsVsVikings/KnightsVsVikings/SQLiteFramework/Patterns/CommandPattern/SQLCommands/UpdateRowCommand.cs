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
    class UpdateRowCommand : ICommandSQLiteSingle
    {
        public ISQLiteTable ExecuteOnTable { get; set; }
        public object Data { get; set; } = null;
        public object[] MultipleData { get; set; } = null;
        //public ISQLiteRow UpdateData { get; set; } = null;

        public void Execute()
        {
            IDbConnection connection = ExecuteOnTable.Provider.CreateConnection();
            connection.Open();

            SQLiteCommand cmd;

            if (Data != null)
                cmd = new SQLiteCommand($"UPDATE '{ExecuteOnTable.TableName}' SET ", (SQLiteConnection)connection);


                connection.Close();
        }
    }
}
