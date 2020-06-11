using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Interfaces
{
    // Lucas

    /// <summary>
    /// Handles connection with a SQLite database.
    /// </summary>
    public interface ISQLiteDBProvider
    {
        /// <summary>
        /// Creates a connection with an assigned SQLite database.
        /// </summary>
        IDbConnection CreateConnection();
    }
}
