using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class ResourcesFactory : IFactory
    {
        public GameObject Create(string type)
        {
            GameObject go = new GameObject();
            CResourceTile resourceTile = new CResourceTile((EResourcesType)Enum.Parse(typeof(EResourcesType), type));

            go.AddComponent<CResourceTile>(resourceTile);

            go.Transform.Scale *= 0.5f;

            return go;
        }
    }
}
