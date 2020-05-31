using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.WorldEditor
{
    public class TileGrid
    {
        private Scene myScene;
        private GameObject[,] tileGrid = new GameObject[4,4];

        public TileGrid(Scene myScene)
        {
            this.myScene = myScene;
        }
        public void MakeTileGrid()
        {
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    MadeTile(new Vector2(x,y));
                }
            }
        }

        private void MadeTile(Vector2 pos)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.TileSprite.Grass01);
            CTile tile = new CTile();

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CTile>(tile);

            go.Transform.Position = pos * tile.TileSize;

            myScene.Instantiate(go);
        }
    }
}
