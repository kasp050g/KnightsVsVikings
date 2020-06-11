using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.WorldEditor
{
    // Lucas
    class SQLiteTileWorldEditorModel : SQLiteRowBase
    {
        public int WorldId { get; set; }
        public int TileTypeId { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }

        public SQLiteTileWorldEditorModel()
        { }

        public SQLiteTileWorldEditorModel(ISQLiteTable locatedInTable, int worldId, int tileTypeId, int xpos, int ypos) : base(locatedInTable)
        {
            WorldId = worldId;
            TileTypeId = tileTypeId;
            Xpos = xpos;
            Ypos = ypos;
        }
    }
}
