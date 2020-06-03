using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Interfaces.Patterns.CommandPattern;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern.SQLCommands
{
    class InsertRowCommand : ICommandSQLiteSingle
    {
        public ISQLiteTable ExecuteOnTable { get; set; }
        public ISQLiteRow RowToInsert { get; set; } = null;
        public bool IsUnique { get; set; } = false;

        public void Execute()
        {
            if (IsUnique)
                if (!CheckIfRowExists())
                    CodeToExecute();
                else
                    CodeToExecute();
        }

        private void CodeToExecute()
        {
            var connection = ExecuteOnTable.Provider.CreateConnection();
            connection.Open();

            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO '{ExecuteOnTable.TableName}' ({RowToInsert.GetPropertiesToString()}) VALUES ({RowToInsert.GetValuesToString()});", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            SQLiteCommand getID = new SQLiteCommand("SELECT last_insert_rowid();", (SQLiteConnection)connection);
            int lastID = (int)getID.ExecuteScalar();

            RowToInsert.ID = lastID;

            connection.Close();
        }

        private bool CheckIfRowExists()
        {
            try
            {
                List<ISQLiteRow> rowsToCompare = ExecuteOnTable.GetAll();

                foreach (ISQLiteRow row in rowsToCompare)
                    if (RowToInsert.Equals(row))
                        return true;

                return false;
            }

            catch
            { return false; }
        }

        //private string GetProperties()
        //{
        //    List<string> result = new List<string>();
        //
        //    PropertyInfo[] properties = RowToInsert.GetType().GetProperties();
        //
        //    foreach (PropertyInfo property in properties)
        //        result.Add(property.Name);
        //
        //    return string.Join(", ", result);
        //}
        //
        //private string GetValues()
        //{
        //    List<string> result = new List<string>();
        //
        //    PropertyInfo[] properties = RowToInsert.GetType().GetProperties();
        //
        //    foreach (PropertyInfo property in properties)
        //        result.Add((string)property.GetValue(RowToInsert).ObjectToSQLiteString());
        //
        //    return string.Join(", ", result);
        //}
    }
}
