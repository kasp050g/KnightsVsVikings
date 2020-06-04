using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.WorldEditor
{
    class SQLiteTileTypeWorldEditorModel : SQLiteRowBase
    {
        public int TileType { get; set; }

        public SQLiteTileTypeWorldEditorModel(ISQLiteTable locatedInTable, int tileType) : base(locatedInTable)
        {
            TileType = tileType;
        }
    }
}
