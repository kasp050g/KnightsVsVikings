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
        private int gridSizeX = 50;
        private int gridSizeY = 50;
        private Scene myScene;

        public Vector2 TileSize = new Vector2(128 / 2, 128 / 2);

        public GameObject[,] groundTileGrid;
        public GameObject[,] obstacleTileGrid;
        public GameObject[,] resourceTileGrid;
        public GameObject[,] buildingTileGrid;
        public GameObject[,] unitTileGrid;

        public bool ShowGridColor { get; set; } = true;

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
            CTile tile = new CTile(TileSize);
            CResourceTile resourceTile = new CResourceTile();

            sr.LayerDepth = 0f;

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CTile>(tile);
            go.AddComponent<CResourceTile>(resourceTile);            

            go.Transform.Position = new Vector2((int)pos.X,(int)pos.Y) * TileSize;
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

        public void UpdateGrid()
        {
            TileGrid _tileGrid = this;


            for (int x = 0; x < _tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    CheckForBuildAndUnitReset(_tileGrid.buildingTileGrid, _tileGrid.groundTileGrid, new Vector2(x, y));
                }
            }

            for (int x = 0; x < _tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    CheckForBuild(_tileGrid.buildingTileGrid, _tileGrid.groundTileGrid, new Vector2(x, y));
                    CheckForUnit(_tileGrid.unitTileGrid, _tileGrid.groundTileGrid, new Vector2(x, y));
                }
            }

            for (int x = 0; x < _tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    GetTileData(_tileGrid.groundTileGrid, new Vector2(x, y));
                }
            }
        }

        public void GetTileData(GameObject[,] groundTileGrid, Vector2 gridPos)
        {
            string com = string.Empty;

            for (int x = -1; x <= 1; x++)//Runs through all neighbours
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0) //Makes sure that we aren't checking our self
                    {
                        //If the value is a watertile
                        if (x + gridPos.X >= 0 && x + gridPos.X < groundTileGrid.GetLength(0) && y + gridPos.Y >= 0 && y + gridPos.Y < groundTileGrid.GetLength(1))
                        {
                            if (groundTileGrid[x + (int)gridPos.X, y + (int)gridPos.Y].GetComponent<CTile>().TileType == ETileType.Water)
                            {
                                com += 'W';
                            }
                            else
                            {
                                com += 'E';
                            }
                        }
                        else
                        {
                            com += "E";
                        }
                    }
                }
            }

            groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.White;
            groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsBlock = true;

            if (groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType != ETileType.Water)
            {
                if (com[0] == 'W' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
                {
                    //Bottom Left
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater08);
                }
                else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'W' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
                {
                    //Top Left
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater05);
                }
                else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
                {
                    // No Water
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.Grass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.Grass01);
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsBlock = false;

                    SetTypeOfTile(groundTileGrid, gridPos);
                }
                else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'W' && com[6] == 'E' && com[7] == 'E')
                {
                    // Bottom Rigth
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater04);
                }
                else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'W')
                {
                    // Top Rigth
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater01);
                }
                //--- to the side
                else if (com[1] == 'W' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
                {
                    // Left
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater06);
                }
                else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[6] == 'W')
                {
                    // Rigth
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater02);
                }
                else if (com[0] == 'E' && com[1] == 'E' && com[3] == 'E' && com[4] == 'W' && com[5] == 'E' && com[6] == 'E')
                {
                    // Top
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater09);
                }
                else if (com[1] == 'E' && com[2] == 'E' && com[3] == 'W' && com[4] == 'E' && com[6] == 'E' && com[7] == 'E')
                {
                    // Bottom
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater12);
                }
                //--- 2 side
                else if (com[1] == 'W' && com[3] == 'W' && com[4] == 'E' && com[6] == 'E' && com[7] == 'E')
                {
                    // Left Bottom
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater13);
                }
                else if (com[1] == 'W' && com[3] == 'E' && com[4] == 'W' && com[5] == 'E' && com[6] == 'E')
                {
                    // Left Top
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater15);
                }
                else if (com[1] == 'E' && com[2] == 'E' && com[3] == 'W' && com[4] == 'E' && com[6] == 'W')
                {
                    // Rigth Bottom
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater14);
                }
                else if (com[0] == 'E' && com[1] == 'E' && com[3] == 'E' && com[4] == 'W' && com[6] == 'W')
                {
                    // Rigth Top
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater16);
                }
                else
                {
                    // Error Tile
                }
            }
        }

        private void SetTypeOfTile(GameObject[,] groundTileGrid, Vector2 gridPos)
        {
            if (groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().ResourcesType != EResourcesType.Nothing)
            {
                groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsOccupied = true;
            }
            else
            {
                groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsOccupied = false;
            }

            // Color Tile Test
            if(ShowGridColor == true)
            {
                if (groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsOccupied == true)
                {
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Blue;
                }
                else if (groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere == false)
                {
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Pink;
                }
                else if (groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsUnitOccupied == true)
                {
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Green;
                }
                else
                {
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Red;
                }
            }
            else
            {
                groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.White;
            }
        }

        private void CheckForBuildAndUnitReset(GameObject[,] buildingTileGrid, GameObject[,] groundTileGrid, Vector2 gridPos)
        {
            groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere = true;
            groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsUnitOccupied = false;
        }

        private void CheckForBuild(GameObject[,] buildingTileGrid, GameObject[,] groundTileGrid, Vector2 gridPos)
        {
            if (buildingTileGrid[(int)gridPos.X, (int)gridPos.Y] != null)
            {
                if (buildingTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CBuilding>().BuildingType != EBuildingType.Field)
                {
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y + 1].GetComponent<CTile>().IsCanBuildHere = false;

                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X - 1, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X + 1, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere = false;

                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y - 1].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X - 1, (int)gridPos.Y - 1].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X + 1, (int)gridPos.Y - 1].GetComponent<CTile>().IsCanBuildHere = false;
                }
                else if (buildingTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CBuilding>().BuildingType == EBuildingType.Field)
                {
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y + 1].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X + 1, (int)gridPos.Y + 1].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X - 1, (int)gridPos.Y + 1].GetComponent<CTile>().IsCanBuildHere = false;

                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X - 1, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X + 1, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere = false;

                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y - 1].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X - 1, (int)gridPos.Y - 1].GetComponent<CTile>().IsCanBuildHere = false;
                    groundTileGrid[(int)gridPos.X + 1, (int)gridPos.Y - 1].GetComponent<CTile>().IsCanBuildHere = false;
                }
            }
        }

        private void CheckForUnit(GameObject[,] unitTileGrid, GameObject[,] groundTileGrid, Vector2 gridPos)
        {
            if (unitTileGrid[(int)gridPos.X, (int)gridPos.Y] != null)
            {
                if (unitTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CUnit>() != null)
                {
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsUnitOccupied = true;
                }
            }
        }
    }
}
