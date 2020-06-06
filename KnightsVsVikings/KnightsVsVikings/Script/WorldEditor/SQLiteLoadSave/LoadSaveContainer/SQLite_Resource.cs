using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.WorldEditor.SQLiteLoadSave
{
    public class SQLite_Resource
    {
        public EResourcesType resourcesType { get; set; }       
        public int X { get; set; }
        public int Y { get; set; }
    }
}
