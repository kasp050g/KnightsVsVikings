using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Interfaces
{
    public interface ISQLiteRow
    {
        int ID { get; set; }
        ISQLiteTable LocatedInTable { get; set; }
    }
}
