using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    public class SQLiteStatsModel : SQLiteRowBase
    {
        public SQLiteStatsModel(int id, ISQLiteTable locatedInTable) : base(id, locatedInTable)
        {
        }
    }
}
