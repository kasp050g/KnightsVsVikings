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
    // Lucas
    public static class Singletons
    {
        public static AstarGlobal AstarGlobalSingleton { get; private set; } = Singleton<AstarGlobal>.Instance;
        public static LevelInformation LevelInformationSingleton { get; private set; } = Singleton<LevelInformation>.Instance;
        public static TableContainer TableContainerSingleton { get; private set; } = Singleton<TableContainer>.Instance;

        public static UnitFactory UnitFactorySingleton { get; private set; } = Singleton<UnitFactory>.Instance;
        public static BuildingFactory BuildingFactorySingleton { get; private set; } = Singleton<BuildingFactory>.Instance;
        public static ResourcesFactory ResourcesFactorySingleton { get; private set; } = Singleton<ResourcesFactory>.Instance;
        public static TileFactory TileFactorySingleton { get; private set; } = Singleton<TileFactory>.Instance;
    }
}
