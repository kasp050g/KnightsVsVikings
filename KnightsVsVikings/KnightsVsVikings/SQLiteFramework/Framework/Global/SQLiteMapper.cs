using KnightsVsVikings.ExtensionMethods.Dictionary;
using KnightsVsVikings.SQLiteFramework.Interfaces;
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

    /// <summary>
    /// Translates SQLite data to C#.
    /// </summary>
    /// <typeparam name="T">ISQLiteRow to translate to.</typeparam>
    class SQLiteMapper<T> : ISQLiteMapper where T : ISQLiteRow
    {
        /// <summary>
        /// Maps rows from a SQLite database.
        /// </summary>
        /// <param name="reader">Reads from the SQLite database.</param>
        /// <param name="readFromTable">SQLite Table to read from.</param>
        /// <returns>Return a list of rows from a SQLite database.</returns>
        public List<ISQLiteRow> MapRowsFromReader(IDataReader reader, ISQLiteTable readFromTable)
        {
            List<ISQLiteRow> result = new List<ISQLiteRow>();

            while(reader.Read())
            {
                ISQLiteRow row = CreateRow(reader);

                row.LocatedInTable = readFromTable;

                result.Add(row);
            }

            return result;
        }

        /// <summary>
        /// Reads data from the rows in a SQLite database.
        /// </summary>
        /// <param name="reader">Parse the data from SQLite to C#.</param>
        /// <param name="properties">A list of properties to compare Types from.</param>
        /// <returns>List of data.</returns>
        private List<dynamic> ReaderVariables(IDataReader reader, List<PropertyInfo> properties)
        {
            List<dynamic> result = new List<dynamic>();

            result.Add(reader.GetInt32(0)); // <= Id, Index, Identifier

            // Her udforsker readeren hvilken Type den skal finde. F.eks.: reader.GetInt32(x).
            // Dette bliver gjort gennem hver af de fundne properties.
            for (int i = 1; i < typeof(T).GetProperties().Length - 1; i++)
                result.Add(reader.Get(properties.ElementAt(i).PropertyType, i));

            return result;
        }

        /// <summary>
        /// Creates a new ISQLiteRow.
        /// </summary>
        /// <param name="reader">Transfers data from the database to the row.</param>
        /// <returns>Returns a new ISQLiteRow.</returns>
        private ISQLiteRow CreateRow(IDataReader reader)
        {
            // Skaber en ny instans af T;et given ISQLiteRow.
            T row = (T)Activator.CreateInstance(typeof(T));

            // Henter alle properties fra T typen; et given ISQLiteRow.
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList().RemoveBaseProperties();

            List<dynamic> values = ReaderVariables(reader, properties);

            // Indsætter alle de hentet værdier fra readeren ind under alle de fundne properties.
            for (int i = 0; i < properties.Count; i++)
                properties.ElementAt(i).SetValue(row, values[i]);

            return row;
        }
    }
}
