using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    class SQLiteFactionModel : SQLiteRowBase
    {
        public string Faction { get; set; }

        public SQLiteFactionModel(ISQLiteTable locatedInTable, string faction) : base(locatedInTable)
        {
            Faction = faction;
        }
    }
}
