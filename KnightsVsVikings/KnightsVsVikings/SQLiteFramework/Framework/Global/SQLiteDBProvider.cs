using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    // Lucas

    /// <summary>
    /// Handles connection with a SQLite database.
    /// </summary>
    class SQLiteDBProvider : ISQLiteDBProvider
    {
        // Denne string bliver benyttet til, at fortælle DBProvideren hvilken .db fil den skal tilkobles.
        private readonly string connectionString;

        /// <summary>
        /// Establishes a connection with a given database.
        /// </summary>
        /// <param name="connectionString">Defines what .db file to connect to.</param>
        public SQLiteDBProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Creates a connection with an assigned SQLite database.
        /// </summary>
        /// <returns>Returns SQLiteConnection.</returns>
        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
