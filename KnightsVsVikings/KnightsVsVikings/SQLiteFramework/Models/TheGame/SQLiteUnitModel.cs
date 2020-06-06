using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    class SQLiteUnitModel : SQLiteRowBase, ISQLiteRow
    {

        public int FactionId { get; set; }
        public int UnitTypeId { get; set; }
        public int ProjectileTypeId { get; set; }
        public int StatsId { get; set; }
        public string Name { get; set; }
        //public string DisplayImageName { get; set; }
        //public string DisplayIconName { get; set; }

        public SQLiteUnitModel()
        { }

        public SQLiteUnitModel(ISQLiteTable locatedInTable, int factionId, int unitTypeId, int projectileTypeId, int statsId, string name) : base(locatedInTable)//, string displayImageName, string displayIconName) : base(locatedInTable)
        {
            FactionId = factionId;
            UnitTypeId = unitTypeId;
            ProjectileTypeId = projectileTypeId;
            StatsId = statsId;
            Name = name;
            //DisplayImageName = displayImageName;
            //DisplayIconName = displayIconName;
        }
    }
}
