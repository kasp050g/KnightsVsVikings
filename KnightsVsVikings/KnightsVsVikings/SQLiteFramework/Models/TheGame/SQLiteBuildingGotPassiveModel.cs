﻿using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    // Lucas
    class SQLiteBuildingGotPassiveModel : SQLiteRowBase
    {
        public int BuildingId { get; set; }
        public int PassiveId { get; set; }

        public SQLiteBuildingGotPassiveModel(ISQLiteTable locatedInTable, int buildingId, int passiveId) : base(locatedInTable)
        {
            BuildingId = buildingId;
            PassiveId = passiveId;
        }
    }
}
