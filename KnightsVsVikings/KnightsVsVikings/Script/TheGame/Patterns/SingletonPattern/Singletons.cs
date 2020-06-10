using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.SQLiteFramework;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern
{
    public static class Singletons
    {
        public static AstarGlobal AstarGlobalSingleton = Singleton<AstarGlobal>.Instance;
        public static LevelInformation LevelInformationSingleton = Singleton<LevelInformation>.Instance;
        public static TableContainer TableContainerSingleton = Singleton<TableContainer>.Instance;
        public static RepositoryContainer RepositoryContainerSingleton = Singleton<RepositoryContainer>.Instance;
        //public static GlobalProperties GlobalPropertiesSingleton = Singleton<GlobalProperties>.Instance;
        public static Scene SceneSingleton = Singleton<Scene>.Instance;
    }
}
