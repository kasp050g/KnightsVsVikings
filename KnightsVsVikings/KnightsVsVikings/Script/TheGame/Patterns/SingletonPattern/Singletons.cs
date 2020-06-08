using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.SQLiteFramework;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern
{
    public static class Singletons
    {
        public static OldAstarGlobal AstarGlobalSingleton = Singleton<OldAstarGlobal>.Instance;
        public static LevelInformation LevelInformationSingleton = Singleton<LevelInformation>.Instance;
        public static TableContainer TableContainerSingleton = Singleton<TableContainer>.Instance;
        public static GameWorld GameWorldSingleton = Singleton<GameWorld>.Instance;
    }
}
