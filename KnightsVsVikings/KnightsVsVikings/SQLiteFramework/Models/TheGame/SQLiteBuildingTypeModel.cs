using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    class SQLiteBuildingTypeModel : SQLiteRowBase
    {
        public int BuildingType { get; set; }

        public SQLiteBuildingTypeModel(ISQLiteTable locatedInTable, int buildingType) : base(locatedInTable)
        {
            BuildingType = buildingType;
        }
    }
}
