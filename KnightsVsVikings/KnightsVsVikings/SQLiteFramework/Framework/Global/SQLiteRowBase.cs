using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    // Lucas
    /// <summary>
    /// Represents the base call for all SQLite models.
    /// </summary>
    public class SQLiteRowBase : ISQLiteRow
    {
        public int Id { get; set; }

        public ISQLiteTable LocatedInTable { get; set; }

        public SQLiteRowBase()
        { }

        public SQLiteRowBase(ISQLiteTable locatedInTable)
        {
            LocatedInTable = locatedInTable;
        }

        public SQLiteRowBase(int id, ISQLiteTable locatedInTable) : this(locatedInTable)
        {
            Id = id;
        }
    }
}
