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
        int sizeOfTile = 128 / 2;
        Scene myScene;
        GameObject gameObjectTileMouse;
        CSpriteRenderer sr;

        BuildingFactory buildingFactory = new BuildingFactory();

        EFaction faction;
        ETeam team;
        ETileType groundTileType;
        EResourcesType resourcesType;
        EBuildingType buildingType;
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
                UpdateGrid(tileGrid);
            }
        }

        public void UpdateGrid(TileGrid _tileGrid)
        {
            for (int x = 0; x < _tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    GetTileData(_tileGrid.groundTileGrid, new Vector2(x, y));
                }
            }

            for (int x = 0; x < _tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    CheckForBuildReset(_tileGrid.buildingTileGrid, _tileGrid.groundTileGrid, new Vector2(x, y));
                }
            }

            for (int x = 0; x < _tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < _tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    CheckForBuild(_tileGrid.buildingTileGrid, _tileGrid.groundTileGrid, new Vector2(x, y));
                }
            }
        }

        public void PickTile(EBuildingType buildingType)
        {
            this.buildingType = buildingType;
            CurrentSelectedTileObject = ECurrentSelectedTileObject.Build;
            gameObjectTileMouse.IsActive = true;
            TextureSheet2D tmp = SpriteContainer.Instance.TileSprite.Delete;

            sr.OffSet = new Vector2(1 * -sizeOfTile, 3 * -sizeOfTile);

            switch (buildingType)
            {
                case EBuildingType.TownHall:
                    tmp = SpriteContainer.Instance.SpriteSheet["GrayTent"];
                    break;
                case EBuildingType.ArcheryRange:
                    tmp = SpriteContainer.Instance.SpriteSheet["GrayTent"];
                    break;
                case EBuildingType.Blacksmith:
                    tmp = SpriteContainer.Instance.SpriteSheet["GrayTent"];
                    break;
                case EBuildingType.Tower:
                    tmp = SpriteContainer.Instance.SpriteSheet["GrayTent"];
                    break;
                case EBuildingType.Barracks:
                    tmp = SpriteContainer.Instance.SpriteSheet["GrayTent"];
                    break;
                case EBuildingType.GatheringStation:
                    tmp = SpriteContainer.Instance.SpriteSheet["GrayTent"];
                    break;
                case EBuildingType.Field:
                    tmp = SpriteContainer.Instance.TileSprite.Wheatfield;
                    sr.OffSet = new Vector2(1 * -sizeOfTile, 1 * -sizeOfTile);
                    break;
                default:
                    break;
            }

            gameObjectTileMouse.GetComponent<CSpriteRenderer>().SetSprite(tmp);
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
                                    PlaceBuild(x, y);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void PlaceBuild(int x, int y)
        {
            TextureSheet2D tmp = null;
            Vector2 _offset = new Vector2(0, 0);

            GameObject go = buildingFactory.Creaft(buildingType);

            go.Transform.Position = new Vector2((x - 1) * tileGrid.TileSize.X, (y - 3) * tileGrid.TileSize.Y);



            switch (buildingType)
            {
                case EBuildingType.TownHall:

                    break;
                case EBuildingType.ArcheryRange:
                    break;
                case EBuildingType.Blacksmith:
                    break;
                case EBuildingType.Tower:
                    break;
                case EBuildingType.Barracks:
                    break;
                case EBuildingType.GatheringStation:
                    break;
                case EBuildingType.Field:
                    go.Transform.Position = new Vector2((x - 1) * tileGrid.TileSize.X, (y - 1) * tileGrid.TileSize.Y);
                    break;
                default:
                    break;
            }

            if (buildingType != EBuildingType.Field)
            {
                switch (team)
                {
                    case ETeam.Team01:
                        go.GetComponent<CSpriteRenderer>().Color = Color.Red;
                        break;
                    case ETeam.Team02:
                        go.GetComponent<CSpriteRenderer>().Color = Color.Blue;
                        break;
                    case ETeam.Team03:
                        go.GetComponent<CSpriteRenderer>().Color = Color.Green;
                        break;
                    case ETeam.Team04:
                        go.GetComponent<CSpriteRenderer>().Color = Color.Yellow;
                        break;
                    case ETeam.Team05:
                        break;
                    case ETeam.Team06:
                        break;
                    case ETeam.Team07:
                        break;
                    case ETeam.Team08:
                        break;
                    default:
                        break;
                }
            }


            if (tileGrid.buildingTileGrid[x, y] != null)
            {
                myScene.Destroy(tileGrid.buildingTileGrid[x, y]);
            }

            if (buildingType != EBuildingType.Nothing)
            {
                myScene.Instantiate(go);
                tileGrid.buildingTileGrid[x, y] = go;
            }
            else
            {
                tileGrid.buildingTileGrid[x, y] = null;
            }

            UpdateGrid(tileGrid);
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

            UpdateGrid(tileGrid);
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

            UpdateGrid(tileGrid);
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
            if (groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsOccupied == true)
            {
                groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Blue;
            }
            else if (groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere == false)
            {
                groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Pink;
            }
            else
            {
                groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CSpriteRenderer>().Color = Color.Red;
            }
        }

        private void CheckForBuildReset(GameObject[,] buildingTileGrid, GameObject[,] groundTileGrid, Vector2 gridPos)
        {
            groundTileGrid[(int)gridPos.X, (int)gridPos.Y].GetComponent<CTile>().IsCanBuildHere = true;
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
                else
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
    }
}
