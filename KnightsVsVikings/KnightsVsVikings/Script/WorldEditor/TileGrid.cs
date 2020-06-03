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
        private int gridSizeX = 25;
        private int gridSizeY = 25;
        private Scene myScene;
        public GameObject[,] groundTileGrid;
        public GameObject[,] obstacleTileGrid;
        public GameObject[,] resourceTileGrid;
        public GameObject[,] buildingTileGrid;
        public GameObject[,] unitTileGrid;

        public TileGrid(Scene myScene)
        {
            this.myScene = myScene;
            groundTileGrid = new GameObject[gridSizeX, gridSizeY];
            obstacleTileGrid = new GameObject[gridSizeX, gridSizeY];
            resourceTileGrid = new GameObject[gridSizeX, gridSizeY];
            buildingTileGrid = new GameObject[gridSizeX, gridSizeY];
            unitTileGrid = new GameObject[gridSizeX, gridSizeY];
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
            CResourceTile resourceTile = new CResourceTile();

            sr.LayerDepth = 0f;

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CTile>(tile);
            go.AddComponent<CResourceTile>(resourceTile);
            

            go.Transform.Position = new Vector2((int)pos.X,(int)pos.Y) * tile.TileSize;
            go.Transform.Scale = new Vector2(1.0f, 1.0f);

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
