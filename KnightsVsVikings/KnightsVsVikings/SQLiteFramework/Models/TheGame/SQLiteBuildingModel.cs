using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    class SQLiteBuildingModel : SQLiteRowBase
    {
        public int StatsId { get; set; }
        public int FactionId { get; set; }
        public int BuildingTypeId { get; set; }
        public int ProjectileTypeId { get; set; }
        public string Name { get; set; }
        public string DisplayImageName { get; set; }
        public string DisplayIconName { get; set; }

        public SQLiteBuildingModel(ISQLiteTable locatedInTable, int statsId, int factionId, int buildingTypeId, int projectileTypeId, string name, string displayImageName, string displayIconName) : base(locatedInTable)
        {
            StatsId = statsId;
            FactionId = factionId;
            BuildingTypeId = buildingTypeId;
            ProjectileTypeId = projectileTypeId;
            Name = name;
            DisplayImageName = displayImageName;
            DisplayIconName = displayIconName;
        }
    }
}
