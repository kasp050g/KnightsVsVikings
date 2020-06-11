using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Interfaces
{
    // Lucas
    /// <summary>
    /// Defines the premise for a SQLite model; in other words a model for a list of columns for a SQLite table.
    /// </summary>
    public interface ISQLiteRow
    {
        int Id { get; set; }
        ISQLiteTable LocatedInTable { get; set; }
    }
}
