using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Interfaces
{
    // Lucas
    public interface ISQLiteRepository
    {
        ISQLiteTable[] RepositoryTables { get; }
    }
}
