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
    class SQLiteUnitModel : SQLiteRowBase, ISQLiteRow
    {

        public int FactionId { get; set; }
        public int UnitTypeId { get; set; }
        public int ProjectileTypeId { get; set; }
        public int StatsId { get; set; }
        public string Name { get; set; }

        public SQLiteUnitModel()
        { }

        public SQLiteUnitModel(ISQLiteTable locatedInTable, int factionId, int unitTypeId, int projectileTypeId, int statsId, string name) : base(locatedInTable)
        {
            FactionId = factionId;
            UnitTypeId = unitTypeId;
            ProjectileTypeId = projectileTypeId;
            StatsId = statsId;
            Name = name;
        }
    }
}
