using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    // Lucas
    class SQLiteUnitGotPassiveModel : SQLiteRowBase
    {
        public int UnitId { get; set; }
        public int PassiveId { get; set; }

        public SQLiteUnitGotPassiveModel(ISQLiteTable locatedInTable, int unitId, int passiveId) : base(locatedInTable)
        {
            UnitId = unitId;
            PassiveId = passiveId;
        }
    }
}
