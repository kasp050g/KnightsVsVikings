using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Interfaces
{
    public interface ISQLiteTable
    {
        string TableName { get; set; }
        ISQLiteDBProvider Provider { get; }
        ISQLiteMapper Mapper { get; }
    }
}
