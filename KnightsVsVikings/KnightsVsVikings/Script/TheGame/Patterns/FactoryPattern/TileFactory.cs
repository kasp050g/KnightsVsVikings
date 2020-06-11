using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class TileFactory : IFactory
    {
        public GameObject Create(string type)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.Pixel);
            CTile tile = new CTile((ETileType)Enum.Parse(typeof(ETileType), type));

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CTile>(tile);

            sr.LayerDepth = 0.0001f;
            go.Transform.Scale *= 1.0f;

            return go;
        }
    }
}
