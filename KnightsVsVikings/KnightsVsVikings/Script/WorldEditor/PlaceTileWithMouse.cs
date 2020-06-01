using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class PlaceTileWithMouse
    {
        int sizeOfTile = 128;
        Scene myScene;
        GameObject gameObject;

        ETileType tileType;
        TileGrid tileGrid;

        public PlaceTileWithMouse(Scene myScene, TileGrid tileGrid)
        {
            this.myScene = myScene;
            this.tileGrid = tileGrid;
        }

        public void MadeTileShow()
        {
            gameObject = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.TileSprite.Grass01);
            CTile tile = new CTile();

            sr.LayerDepth = 0.01f;
            sr.Color = Color.Gray;

            gameObject.AddComponent<CSpriteRenderer>(sr);
            gameObject.AddComponent<CTile>(tile);

            myScene.Instantiate(gameObject);
        }

        public void Update()
        {
            if (gameObject.IsActive == true && myScene.IsMouseOverUI == false)
            {
                MoveTileShow();
            }

            if (Input.GetMouseButtonDown(EMyMouseButtons.RightButton))
            {
                gameObject.IsActive = false;
            }

            if (Input.GetKeyDown(Keys.U))
            {
                UpdateGrid();
            }
        }

        public void UpdateGrid()
        {
            for (int x = 0; x < tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    GetTileData(ref tileGrid.groundTileGrid, new Vector2(x, y));
                }
            }
        }

        public void PickTile(ETileType tileType, TextureSheet2D image)
        {
            this.tileType = tileType;
            gameObject.IsActive = true;
            TextureSheet2D tmp;

            switch (tileType)
            {
                case ETileType.Grass:
                    tmp = SpriteContainer.Instance.TileSprite.Grass01;
                    break;
                case ETileType.Sand:
                    tmp = SpriteContainer.Instance.TileSprite.GrassWater03;
                    break;
                case ETileType.Water:
                    tmp = SpriteContainer.Instance.TileSprite.Water04;
                    break;
                default:
                    tmp = SpriteContainer.Instance.TileSprite.Grass01;
                    break;
            }

            gameObject.GetComponent<CSpriteRenderer>().SetSprite(image);
        }


        public void MoveTileShow()
        {
            int mousex = Mouse.GetState().Position.X;
            int mousey = Mouse.GetState().Position.Y;
            Vector2 newPosition = new Vector2(mousex, mousey);

            Vector2 worldPosition = Vector2.Transform(newPosition, Matrix.Invert(SceneController.Instance.Camera.Transform));

            int positonX = (int)(worldPosition.X / sizeOfTile) * sizeOfTile;
            int positonY = (int)(worldPosition.Y / sizeOfTile) * sizeOfTile;

            if (positonX < 0)
            {
                positonX -= sizeOfTile;
            }
            if (positonY < 0)
            {
                positonY -= sizeOfTile;
            }

            if (worldPosition.X > -sizeOfTile && worldPosition.X < 0.0f)
            {
                positonX = -sizeOfTile;
            }
            if (worldPosition.Y > -sizeOfTile && worldPosition.Y < 0.0f)
            {
                positonY = -sizeOfTile;
            }

            gameObject.Transform.Position = new Vector2(positonX, positonY);

            if (Input.GetMouseButton(EMyMouseButtons.LeftButton))
            {
                for (int x = 0; x < tileGrid.groundTileGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < tileGrid.groundTileGrid.GetLength(1); y++)
                    {
                        if (tileGrid.groundTileGrid[x, y].Transform.Position == gameObject.Transform.Position)
                        {
                            PlaceTile(x, y);
                        }
                    }
                }
            }
        }

        public void PlaceTile(int x, int y)
        {
            TextureSheet2D tmp;
            switch (tileType)
            {
                case ETileType.Grass:
                    tmp = SpriteContainer.Instance.TileSprite.Grass01;
                    break;
                case ETileType.Sand:
                    tmp = SpriteContainer.Instance.TileSprite.GrassWater03;
                    break;
                case ETileType.Water:
                    tmp = SpriteContainer.Instance.TileSprite.Water01;
                    break;
                default:
                    tmp = SpriteContainer.Instance.TileSprite.Grass01;
                    break;
            }

            tileGrid.groundTileGrid[x, y].GetComponent<CSpriteRenderer>().SetSprite(tmp);
            tileGrid.groundTileGrid[x, y].GetComponent<CTile>().TileType = tileType;
            tileGrid.groundTileGrid[x, y].GetComponent<CSpriteRenderer>().SetSprite(gameObject.GetComponent<CSpriteRenderer>().SpriteSheet);
            UpdateGrid();
        }

        public void GetTileData(ref GameObject[,] groundTileGrid, Vector2 gridPos)
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

            //int randomVal = Random.Range(0, 100);

            //if (randomVal < 15)
            //{
            //    tileData.sprite = waterSprites[46];
            //}
            //else if (randomVal >= 15 && randomVal < 35)
            //{

            //    tileData.sprite = waterSprites[48];
            //}
            //else
            //{
            //    tileData.sprite = waterSprites[47];
            //}
            if(groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType != ETileType.Water)
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
                    //_tile.TileType = ETileType.Grass;
                    //_sr.SpriteSheet = SpriteContainer.Instance.TileSprite.Grass01;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.Grass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.Grass01);
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
                else if ( com[1] == 'W'  && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
                {
                    // Left
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater06);
                }
                else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' &&  com[6] == 'W' )
                {
                    // Rigth
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater02);
                }
                else if (com[0] == 'E' && com[1] == 'E' &&  com[3] == 'E' && com[4] == 'W' && com[5] == 'E' && com[6] == 'E' )
                {
                    // Top
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater09);
                }
                else if ( com[1] == 'E' && com[2] == 'E' && com[3] == 'W' && com[4] == 'E' &&  com[6] == 'E' && com[7] == 'E')
                {
                    // Bottom
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater12);
                }
                //--- 2 side
                else if ( com[1] == 'W'  && com[3] == 'W' && com[4] == 'E' && com[6] == 'E' && com[7] == 'E')
                {
                    // Left Bottom
                    //_tile.TileType = ETileType.WaterGrass;
                    //_sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater15;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater13);
                }
                else if (com[1] == 'W'  && com[3] == 'E' && com[4] == 'W' && com[5] == 'E' && com[6] == 'E' )
                {
                    // Left Top
                    //_tile.TileType = ETileType.WaterGrass;
                    //_sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater13;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater15);
                }
                else if (com[1] == 'E' && com[2] == 'E' && com[3] == 'W' && com[4] == 'E'  && com[6] == 'W' )
                {
                    // Rigth Bottom
                    //_tile.TileType = ETileType.WaterGrass;
                    //_sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater16;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater14);
                }
                else if (com[0] == 'E' && com[1] == 'E' &&  com[3] == 'E' && com[4] == 'W'  && com[6] == 'W' )
                {
                    // Rigth Top
                    //_tile.TileType = ETileType.WaterGrass;
                    //_sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater14;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().TileType = ETileType.WaterGrass;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.GrassWater16);
                }
                else
                {
                    // Error Tile

                }
            }





        }
    }
}
