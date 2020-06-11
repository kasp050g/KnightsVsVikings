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
    class SQLiteCampaignChapterModel : SQLiteRowBase
    {
        public string Name { get; set; }
        public int Unlock { get; set; }
        public string Faction { get; set; }

        public SQLiteCampaignChapterModel(ISQLiteTable locatedInTable, string name, int unlock, string faction) : base(locatedInTable)
        {
            Name = name;
            Unlock = unlock;
            Faction = faction;
        }
    }
}
