using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.WorldEditor.SQLiteLoadSave
{
    // Kasper
    public class SQLite_Unit
    {
        public EUnitType UnitType { get; set; }
        public ETeam Team { get; set; }
        public EFaction Faction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
