using KnightsVsVikings.SQLiteFramework;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Generics
{
    public static class Singletons
    {
        public static TableContainer TableContainerSingleton = Singleton<TableContainer>.Instance;
    }
}
