using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Interfaces.Patterns.CommandPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern.SQLCommands
{
    // Lucas
    class GetRowCommand : ICommandSQLiteSingle, ISQLiteOutput, ISQLiteInput
    {
        public List<ISQLiteRow> ResultRows { get; private set; } = null;
        public ISQLiteTable ExecuteOnTable { get; set; }
        public PropertyInfo Column { get; set; }
        public object Data { get; set; }

        public void Execute()
        {
            IDbConnection connection = ExecuteOnTable.Provider.CreateConnection();
            connection.Open();

            SQLiteCommand cmd;

            if (Column != null && Data != null)
                cmd = new SQLiteCommand($"SELECT * FROM {ExecuteOnTable.TableName} WHERE {Column.Name} = {Data.ObjectToSQLiteString()}", (SQLiteConnection)connection);
            else
                cmd = new SQLiteCommand($"SELECT * FROM {ExecuteOnTable.TableName}", (SQLiteConnection)connection);

            SQLiteDataReader reader = cmd.ExecuteReader();

            //ResultRows = ExecuteOnTable.Mapper.MapRowsFromReader(reader, ExecuteOnTable);

            try { ResultRows = ExecuteOnTable.Mapper.MapRowsFromReader(reader, ExecuteOnTable); }
            catch { ResultRows = null; }

            connection.Close();
        }
    }
}
