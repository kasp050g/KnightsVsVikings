using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Interfaces
{
    public interface ISQLiteMapper
    {
        List<ISQLiteRow> MapRowsFromReader(IDataReader reader, ISQLiteTable readFromTable);
    }
}
