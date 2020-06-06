using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.WorldEditor
{
    class SQLiteDoodadWorldEditorModel : SQLiteRowBase
    {
        public int WorldId { get; set; }
        public int DoodadTypeId { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }

        public SQLiteDoodadWorldEditorModel(ISQLiteTable locatedInTable, int worldId, int doodadTypeId, int xpos, int ypos) : base(locatedInTable)
        {
            WorldId = worldId;
            DoodadTypeId = doodadTypeId;
            Xpos = xpos;
            Ypos = ypos;
        }
    }
}
