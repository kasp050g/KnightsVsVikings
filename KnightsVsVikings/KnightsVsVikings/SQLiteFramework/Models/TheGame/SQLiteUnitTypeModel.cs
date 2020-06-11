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
    class SQLiteUnitTypeModel : SQLiteRowBase
    {
        public int UnitType { get; set; }

        public SQLiteUnitTypeModel(ISQLiteTable locatedInTable, int unitType) : base(locatedInTable)
        {
            UnitType = unitType;
        }
    }
}
