using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class TileGrid
    {
        private Scene myScene;
        public GameObject[,] groundTileGrid = new GameObject[10,10];
        public GameObject[,] obstacleTileGrid = new GameObject[10,10];
        public GameObject[,] resourceTileGrid = new GameObject[10,10];
        public GameObject[,] buildingTileGrid = new GameObject[10,10];
        public GameObject[,] unitTileGrid = new GameObject[10,10];

        public TileGrid(Scene myScene)
        {
            this.myScene = myScene;
        }
        public void MakeTileGrid()
        {
            // Test SQLite
            TestSqlite();
        }
        private void LoadfromSQLite(int mapID)
        {
            //TODO: Load from SQLite
        }

        private GameObject MadeTile(Vector2 pos,TextureSheet2D textureSheet)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(textureSheet);
            CTile tile = new CTile();

            sr.LayerDepth = 0f;

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CTile>(tile);

            go.Transform.Position = new Vector2((int)pos.X,(int)pos.Y) * tile.TileSize;

            myScene.Instantiate(go);
            return go;
        }

        private void TestSqlite()
        {
            for (int x = 0; x < groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < groundTileGrid.GetLength(1); y++)
                {
                    groundTileGrid[x, y] = MadeTile(new Vector2(x, y), SpriteContainer.Instance.TileSprite.Grass01);
                }
            }
        }
    }
}
