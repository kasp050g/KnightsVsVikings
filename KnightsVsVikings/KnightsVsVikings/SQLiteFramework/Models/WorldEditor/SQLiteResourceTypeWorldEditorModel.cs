using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.WorldEditor
{
    class SQLiteResourceTypeWorldEditorModel : SQLiteRowBase
    {
        public int ResourceType { get; set; }

        public SQLiteResourceTypeWorldEditorModel(ISQLiteTable locatedInTable, int resourceType) : base(locatedInTable)
        {
            ResourceType = resourceType;
        }
    }
}
