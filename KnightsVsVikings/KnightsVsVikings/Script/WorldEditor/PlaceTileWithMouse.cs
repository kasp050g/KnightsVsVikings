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
        UnitFactory unitFactory = new UnitFactory();
        ResourcesFactory resourcesFactory = new ResourcesFactory();

        EFaction faction;
        ETeam team;
        ETileType groundTileType;
        EResourcesType resourcesType;
        EBuildingType buildingType;
        EUnitType unitType;
        TileGrid tileGrid;
        ECurrentSelectedTileObject CurrentSelectedTileObject;

        public EFaction Faction { get => faction; set => faction = value; }
        public GameObject GameObjectTileMouse { get => gameObjectTileMouse; set => gameObjectTileMouse = value; }
        public ETeam Team { get => team; set => team = value; }

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

            gameObjectTileMouse.IsActive = false;
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
            _tileGrid.UpdateGrid();
        }

        public void PickTile(EUnitType unitType)
        {
            this.unitType = unitType;
            CurrentSelectedTileObject = ECurrentSelectedTileObject.Unit;
            gameObjectTileMouse.IsActive = true;
            TextureSheet2D tmp = SpriteContainer.Instance.TileSprite.Delete;

            sr.OffSet = new Vector2(0.5f * -sizeOfTile, 0.5f * -sizeOfTile);

            switch (unitType)
            {
                case EUnitType.Nothing:
                    break;
                case EUnitType.Worker:
                    tmp = (EFaction.Knights == faction ? SpriteContainer.Instance.UnitImageSprite.KnightWorker : SpriteContainer.Instance.UnitImageSprite.VikingWorker);
                    break;
                case EUnitType.Bowman:
                    tmp = (EFaction.Knights == faction ? SpriteContainer.Instance.UnitImageSprite.KnightBowman : SpriteContainer.Instance.UnitImageSprite.VikingBowman);
                    break;
                case EUnitType.Footman:
                    tmp = (EFaction.Knights == faction ? SpriteContainer.Instance.UnitImageSprite.KnightFootman : SpriteContainer.Instance.UnitImageSprite.VikingFootman);
                    break;
                case EUnitType.Spearman:
                    tmp = (EFaction.Knights == faction ? SpriteContainer.Instance.UnitImageSprite.KnightSpear : SpriteContainer.Instance.UnitImageSprite.VikingSpear);
                    break;
                default:
                    break;
            }

            gameObjectTileMouse.GetComponent<CSpriteRenderer>().SetSprite(tmp);
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
            int mousex = MouseSettings.Instance.GetMouseState().X;
            int mousey = MouseSettings.Instance.GetMouseState().Y;
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
                                    PlaceUnit(x, y);
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

        public void PlaceUnit(int x, int y)
        {
            GameObject go = unitFactory.Creaft(unitType, faction, team);

            go.Transform.Position = new Vector2((x - 0) * tileGrid.TileSize.X, (y - 0) * tileGrid.TileSize.Y);



            if (tileGrid.unitTileGrid[x, y] != null)
            {
                myScene.Destroy(tileGrid.unitTileGrid[x, y]);
            }

            if (unitType != EUnitType.Nothing)
            {
                myScene.Instantiate(go);
                tileGrid.unitTileGrid[x, y] = go;
            }
            else
            {
                tileGrid.unitTileGrid[x, y] = null;
            }

            UpdateGrid(tileGrid);
        }

        public void PlaceBuild(int x, int y)
        {
            TextureSheet2D tmp = null;
            Vector2 _offset = new Vector2(0, 0);

            GameObject go = buildingFactory.Creaft(buildingType,faction,team);

            go.Transform.Position = new Vector2(x  * tileGrid.TileSize.X, y  * tileGrid.TileSize.Y);

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
            GameObject go = resourcesFactory.Creaft(resourcesType);

            go.Transform.Position = new Vector2((x - 0) * tileGrid.TileSize.X, (y - 0) * tileGrid.TileSize.Y);

            if (tileGrid.resourceTileGrid[x, y] != null)
            {
                myScene.Destroy(tileGrid.resourceTileGrid[x, y]);
            }

            if (resourcesType != EResourcesType.Nothing)
            {
                myScene.Instantiate(go);
                tileGrid.resourceTileGrid[x, y] = go;
            }
            else
            {
                tileGrid.resourceTileGrid[x, y] = null;
            }

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
    }
}
