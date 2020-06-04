using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    class SQLiteCanBuildUnitModel : SQLiteRowBase
    {
        public int UnitId { get; set; }
        public int BuildingId { get; set; }

        public SQLiteCanBuildUnitModel(ISQLiteTable locatedInTable, int unitId, int buildingId) : base(locatedInTable)
        {
            UnitId = unitId;
            BuildingId = buildingId;
        }
    }
}
