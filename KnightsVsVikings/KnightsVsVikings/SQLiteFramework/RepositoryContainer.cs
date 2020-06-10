using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework
{
    public class RepositoryContainer
    {
        public ISQLiteRepository UnitRepository { get; set; }

        public RepositoryContainer()
        {
            InstantiateRepositories();
        }

        private void InstantiateRepositories()
        {
            ISQLiteTable[] unitReposTables = new ISQLiteTable[]
            {
                Singletons.TableContainerSingleton.UnitTable,
                Singletons.TableContainerSingleton.FactionTable,
                Singletons.TableContainerSingleton.StatsTable
            };

            UnitRepository = new SQLiteRepository(unitReposTables);
        }
    }
}
