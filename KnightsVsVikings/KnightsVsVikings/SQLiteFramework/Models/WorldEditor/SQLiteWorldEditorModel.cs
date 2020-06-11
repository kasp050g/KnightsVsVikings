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
    class SQLiteWorldEditorModel : SQLiteRowBase
    {
        public string Name { get; set; }

        public SQLiteWorldEditorModel()
        { }

        public SQLiteWorldEditorModel(ISQLiteTable locatedInTable, string name) : base(locatedInTable)
        {
            Name = name;
        }
    }
}
