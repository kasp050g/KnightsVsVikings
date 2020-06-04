using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.WorldEditor
{
    class SQLiteDoodadTypeWorldEditorModel : SQLiteRowBase
    {
        public int DoodadType { get; set; }

        public SQLiteDoodadTypeWorldEditorModel(ISQLiteTable locatedInTable, int doodadType) : base(locatedInTable)
        {
            DoodadType = doodadType;
        }
    }
}
