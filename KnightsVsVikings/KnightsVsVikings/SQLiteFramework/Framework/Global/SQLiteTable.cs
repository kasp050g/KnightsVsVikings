using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.ExtensionMethods.Dictionary;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    class SQLiteTable<T> : ISQLiteTable where T : ISQLiteRow
    {
        public string TableName { get; set; }

        public ISQLiteDBProvider Provider { get; }

        public ISQLiteMapper Mapper { get; } = new SQLiteMapper<T>();

        public T Rows = (T)Activator.CreateInstance(typeof(T));

        public SQLiteTable(string tableName, ISQLiteDBProvider provider)
        {
            TableName = tableName;
            Provider = provider;

            InstantiateSQLiteTable();
        }

        private void InstantiateSQLiteTable()
        {
            IDbConnection connection = Provider.CreateConnection();
            connection.Open();

            Dictionary<string, Type> variables = typeof(T).GetProperties().ToDictionary(pair => pair.Name, pair => pair.PropertyType);

            SQLiteCommand cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS '{TableName}' (Id INTEGER PRIMARY KEY, {variables.DictToSQLiteString()});", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
