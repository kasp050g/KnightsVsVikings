using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class TileFactory : Factory
    {
        #region Singleton
        private static TileFactory instance;
        public static TileFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TileFactory();
                }
                return instance;
            }
        }
        #endregion

        public GameObject Creaft(ETileType tileType)
        {
            // Main GameObject
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.Pixel);
            CTile tile = new CTile(tileType);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CTile>(tile);

            sr.LayerDepth = 0.0001f;

            go.Transform.Scale *= 1.0f;

            return go;
        }
    }
}
