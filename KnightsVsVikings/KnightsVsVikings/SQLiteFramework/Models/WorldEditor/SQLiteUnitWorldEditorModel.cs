﻿using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.WorldEditor
{
    // Lucas
    class SQLiteUnitWorldEditorModel : SQLiteRowBase
    {
        public int WorldId { get; set; }
        public int UnitTypeId { get; set; }
        public int Team { get; set; }
        public int Faction { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }

        public SQLiteUnitWorldEditorModel()
        { }

        public SQLiteUnitWorldEditorModel(ISQLiteTable locatedInTable, int worldId, int unitTypeId, int team, int faction, int xpos, int ypos) : base(locatedInTable)
        {
            WorldId = worldId;
            UnitTypeId = unitTypeId;
            Team = team;
            Faction = faction;
            Xpos = xpos;
            Ypos = ypos;
        }
    }
}
