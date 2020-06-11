using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    // Lucas
    public class SQLiteRepository : ISQLiteRepository
    {
        public ISQLiteTable[] RepositoryTables { get; set; }

        public SQLiteRepository(ISQLiteTable[] tables)
        {
            RepositoryTables = tables;
        }
    }
}
