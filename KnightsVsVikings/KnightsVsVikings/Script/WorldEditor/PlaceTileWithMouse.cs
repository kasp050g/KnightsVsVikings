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
        int sizeOfTile = 128 /2;
        Scene myScene;
        GameObject gameObjectTileMouse;
        CSpriteRenderer sr;

        ETileType groundTileType;
        EResourcesType resourcesType;
        TileGrid tileGrid;
        ECurrentSelectedTileObject CurrentSelectedTileObject;

        public PlaceTileWithMouse(Scene myScene, TileGrid tileGrid)
        {
            this.myScene = myScene;
            this.tileGrid = tileGrid;
        }

        public void MadeTileShow()
        {
            gameObjectTileMouse = new GameObject();
            sr = new CSpriteRenderer(SpriteContainer.Instance.TileSprite.Grass01);
            CTile tile = new CTile();

            sr.LayerDepth = 0.01f;
            sr.Color = Color.Gray;

            gameObjectTileMouse.AddComponent<CSpriteRenderer>(sr);
            gameObjectTileMouse.AddComponent<CTile>(tile);

            myScene.Instantiate(gameObjectTileMouse);
        }

        public void Update()
        {
            if (gameObjectTileMouse.IsActive == true && myScene.IsMouseOverUI == false)
            {
                MoveTileShow();
            }

            if (Input.GetMouseButtonDown(EMyMouseButtons.RightButton))
            {
                gameObjectTileMouse.IsActive = false;
            }

            if (Input.GetKeyDown(Keys.U))
            {
                UpdateGrid(tileGrid.groundTileGrid);
            }
        }

        public void UpdateGrid(GameObject[,] _tileGrid)
        {
            for (int x = 0; x < _tileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _tileGrid.GetLength(1); y++)
                {
                    GetTileData(ref _tileGrid, new Vector2(x, y));
                }
            }
        }

        public void PickTile(ETileType tileType, TextureSheet2D image)
        {
            this.groundTileType = tileType;
            CurrentSelectedTileObject = ECurrentSelectedTileObject.Ground;
            gameObjectTileMouse.IsActive = true;
            TextureSheet2D tmp;

            sr.OffSet = new Vector2(0, 0);

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

            gameObjectTileMouse.GetComponent<CSpriteRenderer>().SetSprite(image);
        }

        public void PickTile(EResourcesType resourcesType, TextureSheet2D image)
        {
            this.resourcesType = resourcesType;
            gameObjectTileMouse.IsActive = true;
            CurrentSelectedTileObject = ECurrentSelectedTileObject.Resource;
            TextureSheet2D tmp;

            switch (resourcesType)
            {
                case EResourcesType.Nothing:
                    tmp = SpriteContainer.Instance.TileSprite.Delete;
                    sr.OffSet = new Vector2(0 * -sizeOfTile, 0 * -sizeOfTile);
                    break;
                case EResourcesType.Gold:
                    tmp = SpriteContainer.Instance.TileSprite.Gold;
                    sr.OffSet = new Vector2(0 * -sizeOfTile, 1 * -sizeOfTile);
                    break;
                case EResourcesType.Stone:
                    tmp = SpriteContainer.Instance.TileSprite.Stone;
                    sr.OffSet = new Vector2(0 * -sizeOfTile, 1 * -sizeOfTile);
                    break;
                case EResourcesType.Wood:
                    tmp = SpriteContainer.Instance.TileSprite.Wood;
                    sr.OffSet = new Vector2(1 * -sizeOfTile, 4 * -sizeOfTile);
                    break;
                case EResourcesType.Food:
                    tmp = SpriteContainer.Instance.TileSprite.Wheatfield;
                    sr.OffSet = new Vector2(1 * -sizeOfTile, 1 * -sizeOfTile);
                    break;
                default:
                    break;
            }

            gameObjectTileMouse.GetComponent<CSpriteRenderer>().SetSprite(image);
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

            gameObjectTileMouse.Transform.Position = new Vector2(positonX, positonY);

            if (Input.GetMouseButton(EMyMouseButtons.LeftButton))
            {
                for (int x = 0; x < tileGrid.groundTileGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < tileGrid.groundTileGrid.GetLength(1); y++)
                    {
                        if (tileGrid.groundTileGrid[x, y].Transform.Position == gameObjectTileMouse.Transform.Position)
                        {
                            switch (CurrentSelectedTileObject)
                            {
                                case ECurrentSelectedTileObject.Ground:
                                    PlaceTile(x, y);
                                    break;
                                case ECurrentSelectedTileObject.Resource:
                                    PlaceResource(x, y);
                                    break;
                                case ECurrentSelectedTileObject.Unit:
                                    break;
                                case ECurrentSelectedTileObject.Build:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void PlaceResource(int x, int y)
        {
            TextureSheet2D tmp = null;
            Vector2 _offset = new Vector2(0, 0);

            switch (resourcesType)
            {
                case EResourcesType.Nothing:
                    //tmp = SpriteContainer.Instance.TileSprite.Delete;
                    //_offset = new Vector2(0, 0);
                    tileGrid.groundTileGrid[x, y].GetComponent<CResourceTile>().UpdateResourcesSprite(resourcesType);
                    break;
                case EResourcesType.Gold:
                    //tmp = SpriteContainer.Instance.TileSprite.Gold;
                    //_offset = new Vector2(0, -128);
                    tileGrid.groundTileGrid[x, y].GetComponent<CResourceTile>().UpdateResourcesSprite(resourcesType);
                    break;
                case EResourcesType.Stone:
                    //tmp = SpriteContainer.Instance.TileSprite.Stone;
                    //_offset = new Vector2(0, -128);
                    tileGrid.groundTileGrid[x, y].GetComponent<CResourceTile>().UpdateResourcesSprite(resourcesType);
                    break;
                case EResourcesType.Wood:
                    //tmp = SpriteContainer.Instance.TileSprite.Wood;
                    //_offset = new Vector2(-128, -4 * 128);
                    tileGrid.groundTileGrid[x, y].GetComponent<CResourceTile>().UpdateResourcesSprite(resourcesType);
                    break;
                case EResourcesType.Food:
                    //tmp = SpriteContainer.Instance.TileSprite.Wheatfield;
                    //_offset = new Vector2(-128, -128);
                    tileGrid.groundTileGrid[x, y].GetComponent<CResourceTile>().UpdateResourcesSprite(resourcesType);
                    break;
                default:
                    break;
            }

            if (tmp != null)
            {
                tileGrid.resourceTileGrid[x, y].GetComponent<CSpriteRenderer>().SetSprite(tmp);
                //tileGrid.resourceTileGrid[x, y].GetComponent<CSpriteRenderer>().OffSet = _offset;
            }

            //tileGrid.groundTileGrid[x, y].GetComponent<CTile>().ResourcesType = resourcesType;

            UpdateGrid(tileGrid.groundTileGrid);
        }

        public void PlaceTile(int x, int y)
        {
            TextureSheet2D tmp;
            switch (groundTileType)
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
            tileGrid.groundTileGrid[x, y].GetComponent<CTile>().TileType = groundTileType;

            UpdateGrid(tileGrid.groundTileGrid);
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
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsBlock = false;
                    groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().SetSprite(SpriteContainer.Instance.TileSprite.Grass01);

                    if(groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().ResourcesType != EResourcesType.Nothing)
                    {
                        groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsOccupied = true;
                    }
                    else
                    {
                        groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsOccupied = false;
                    }

                    if (groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsOccupied == true)
                    {
                        groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Blue;
                    }
                    else
                    {
                        groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Red;
                    }
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
    }
}
