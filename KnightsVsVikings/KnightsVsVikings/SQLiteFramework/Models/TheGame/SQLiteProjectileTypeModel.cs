using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    class SQLiteProjectileTypeModel : SQLiteRowBase
    {
        public int ProjectileType { get; set; }

        public SQLiteProjectileTypeModel(ISQLiteTable locatedInTable, int projectileType) : base(locatedInTable)
        {
            ProjectileType = projectileType;
        }
    }
}
