using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.WorldEditor
{
    class SQLiteBuildingWorldEditorModel : SQLiteRowBase
    {
        public int WorldId { get; set; }
        public int BuildingTypeId { get; set; }
        public int Team { get; set; }
        public int Faction { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }

        public SQLiteBuildingWorldEditorModel()
        { }

        public SQLiteBuildingWorldEditorModel(ISQLiteTable locatedInTable, int worldId, int buildingTypeId, int team, int faction, int xpos, int ypos) : base(locatedInTable)
        {
            WorldId = worldId;
            BuildingTypeId = buildingTypeId;
            Team = team;
            Faction = faction;
            Xpos = xpos;
            Ypos = ypos;
        }
    }
}
