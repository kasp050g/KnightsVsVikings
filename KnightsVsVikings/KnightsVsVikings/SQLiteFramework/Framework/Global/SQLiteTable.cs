using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.ExtensionMethods.Dictionary;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Models.TheGame;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern.SQLCommands;
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
    // Lucas
    class SQLiteTable<T> : ISQLiteTable where T : ISQLiteRow
    {
        public string TableName { get; set; }

        public ISQLiteDBProvider Provider { get; }

        public ISQLiteMapper Mapper { get; } = new SQLiteMapper<T>();

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

            /* Her fjerner den alle properties fra SQLiteRowBase klassen.
             * Dette bliver gjort siden denne klasse ikke skal bruge en locatedInTable property,
             * eller en Id property når den skal instantiere tabellen i databasen.
             * Her kan det ses den første linje af SQLiteCommand'en er : Id INTEGER PRIMARY KEY, derfor er Id ikke et krav at finde some property her.
             */
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList().RemoveAllBaseProperties();

            Dictionary<string, Type> variables = properties.ToDictionary(kvp => kvp.Name, kvp => kvp.PropertyType);

            SQLiteCommand cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS '{TableName}' (Id INTEGER PRIMARY KEY, {variables.DictToSQLiteString()});", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}
