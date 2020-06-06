using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.WorldEditor
{
    class SQLiteResourceWorldEditorModel : SQLiteRowBase
    {
        public int WorldId { get; set; }
        public int ResourceTypeId { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }

        public SQLiteResourceWorldEditorModel(ISQLiteTable locatedInTable, int worldId, int resourceTypeId, int xpos, int ypos) : base(locatedInTable)
        {
            WorldId = worldId;
            ResourceTypeId = resourceTypeId;
            Xpos = xpos;
            Ypos = ypos;
        }
    }
}
