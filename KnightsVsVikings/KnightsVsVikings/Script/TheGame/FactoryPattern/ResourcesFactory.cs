using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class ResourcesFactory
    {
        #region Singleton
        private static ResourcesFactory instance;
        public static ResourcesFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourcesFactory();
                }
                return instance;
            }
        }
        #endregion

        public GameObject Creaft(EResourcesType resourcesType)
        {
            // Main GameObject
            GameObject go = new GameObject();
            CResourceTile resourceTile = new CResourceTile(resourcesType);

            go.AddComponent<CResourceTile>(resourceTile);

            go.Transform.Scale *= 0.5f;

            return go;
        }
    }
}
