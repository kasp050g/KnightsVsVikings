using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.ExtensionMethods.Dictionary;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Models.TheGame;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    class SQLiteTable<T> : ISQLiteTable where T : ISQLiteRow
    {
        public string TableName { get; set; }

        public ISQLiteDBProvider Provider { get; }

        public ISQLiteMapper Mapper { get; } = new SQLiteMapper<T>();

        //public T Rows = (T)Activator.CreateInstance(typeof(T));

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

            //Dictionary<string, Type> variables = typeof(T).GetProperties().ToDictionary(pair => pair.Name, pair => pair.PropertyType);

            List<PropertyInfo> baseProperties = typeof(SQLiteRowBase).GetProperties().ToList();
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();

            properties.RemoveAll(property => baseProperties.Exists(baseProperty => baseProperty.Name == property.Name));
            properties = properties.AsEnumerable().OrderBy(property => property.Name != "Id").ToList();

            Dictionary<string, Type> variables = properties.ToDictionary(kvp => kvp.Name, kvp => kvp.PropertyType);
            //List<dynamic> values = ReaderVariables(reader);

            //for (int i = 0; i < properties.Count; i++)
            //    properties.ElementAt(i).SetValue(row, values[i]);

            // return row;

            SQLiteCommand cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS '{TableName}' (Id INTEGER PRIMARY KEY, {variables.DictToSQLiteString()});", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
