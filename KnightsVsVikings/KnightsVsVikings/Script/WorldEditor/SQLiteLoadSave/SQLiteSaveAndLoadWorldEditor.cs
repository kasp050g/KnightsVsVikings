using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.WorldEditor.SQLiteLoadSave
{
    public class SQLiteSaveAndLoadWorldEditor
    {
        private Vector2 tileSize = new Vector2(128 / 2, 128 / 2);

        public SQLiteSaveAndLoadWorldEditor()
        {

        }

        #region Save
        public void SaveSQLite(TileGrid tileGrid)
        {
            List<SQLite_Ground> grounds = new List<SQLite_Ground>();
            List<SQLite_Builing> builings = new List<SQLite_Builing>();
            List<SQLite_Unit> units = new List<SQLite_Unit>();
            List<SQLite_Resource> resources = new List<SQLite_Resource>();

            for (int x = 0; x < tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    if (tileGrid.groundTileGrid != null)
                    {
                        SQLite_Ground ground = new SQLite_Ground();
                        ground.tileType = tileGrid.groundTileGrid[x, y].AddComponent<CTile>().TileType;
                        ground.X = x;
                        ground.Y = y;

                        grounds.Add(ground);
                    }
                }
            }

            for (int x = 0; x < tileGrid.buildingTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.buildingTileGrid.GetLength(1); y++)
                {
                    if (tileGrid.buildingTileGrid != null)
                    {
                        SQLite_Builing building = new SQLite_Builing();
                        building.BuildingType = tileGrid.buildingTileGrid[x, y].AddComponent<CBuilding>().BuildingType;
                        building.Team = tileGrid.buildingTileGrid[x, y].AddComponent<CBuilding>().Team;
                        building.Faction = tileGrid.buildingTileGrid[x, y].AddComponent<CBuilding>().Faction;
                        building.X = x;
                        building.Y = y;

                        builings.Add(building);
                    }
                }
            }

            for (int x = 0; x < tileGrid.unitTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.unitTileGrid.GetLength(1); y++)
                {
                    if (tileGrid.unitTileGrid != null)
                    {
                        SQLite_Unit unit = new SQLite_Unit();
                        unit.UnitType = tileGrid.unitTileGrid[x, y].AddComponent<CUnit>().UnitType;
                        unit.Team = tileGrid.unitTileGrid[x, y].AddComponent<CUnit>().Team;
                        unit.Faction = tileGrid.unitTileGrid[x, y].AddComponent<CUnit>().Faction;
                        unit.X = x;
                        unit.Y = y;

                        units.Add(unit);
                    }
                }
            }

            for (int x = 0; x < tileGrid.resourceTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.resourceTileGrid.GetLength(1); y++)
                {
                    if (tileGrid.unitTileGrid != null)
                    {
                        SQLite_Resource resource = new SQLite_Resource();
                        resource.resourcesType = tileGrid.resourceTileGrid[x, y].AddComponent<CResourceTile>().ResourcesType;
                        resource.X = x;
                        resource.Y = y;

                        resources.Add(resource);
                    }
                }
            }

            DoTheSQLSave(grounds,builings,units,resources);
        }

        private void DoTheSQLSave(List<SQLite_Ground> grounds, List<SQLite_Builing> builings, List<SQLite_Unit> units, List<SQLite_Resource> resources,int ID = 0)
        {
            // TODO: Save in it the SQLite
        }
        #endregion

        #region Load
        public void LoadSQLite(TileGrid tileGrid,int ID)
        {
            List<SQLite_Ground> grounds = LoadGround(ID);
            List<SQLite_Builing> builings = LoadBuilding(ID);
            List<SQLite_Unit> units = LoadUnit(ID);
            List<SQLite_Resource> resources = LoadResource(ID);

            foreach (SQLite_Ground item in grounds)
            {
                GameObject go = TileFactory.Instance.Creaft(item.tileType);
                go.Transform.Position = new Vector2(tileSize.X * item.X, tileSize.Y * item.Y);
                tileGrid.groundTileGrid[item.X, item.Y] = go;
            }

            foreach (SQLite_Builing item in builings)
            {
                GameObject go = BuildingFactory.Instance.Creaft(item.BuildingType, item.Faction, item.Team);
                go.Transform.Position = new Vector2(tileSize.X * item.X, tileSize.Y * item.Y);
                tileGrid.buildingTileGrid[item.X, item.Y] = go;
            }

            foreach (SQLite_Unit item in units)
            {
                GameObject go = UnitFactory.Instance.Creaft(item.UnitType, item.Faction, item.Team);
                go.Transform.Position = new Vector2(tileSize.X * item.X, tileSize.Y * item.Y);
                tileGrid.unitTileGrid[item.X, item.Y] = go;
            }

            foreach (SQLite_Resource item in resources)
            {
                GameObject go = ResourcesFactory.Instance.Creaft(item.resourcesType);
                go.Transform.Position = new Vector2(tileSize.X * item.X, tileSize.Y * item.Y);
                tileGrid.resourceTileGrid[item.X, item.Y] = go;
            }
        }
        private List<SQLite_Ground> LoadGround(int ID)
        {
            List<SQLite_Ground> grounds = new List<SQLite_Ground>();

            // TODO: Load SQLite ground here

            return grounds;
        }
        private List<SQLite_Builing> LoadBuilding(int ID)
        {
            List<SQLite_Builing> builings = new List<SQLite_Builing>();

            // TODO: Load SQlite Build here

            return builings;
        }
        private List<SQLite_Unit> LoadUnit(int ID)
        {
            List<SQLite_Unit> units = new List<SQLite_Unit>();

            // TODO: Load SQLite Unit here

            return units;
        }
        private List<SQLite_Resource> LoadResource(int ID)
        {
            List<SQLite_Resource> resources = new List<SQLite_Resource>();

            // TODO: Load SQLite resources

            return resources;
        }
        #endregion
    }
}
