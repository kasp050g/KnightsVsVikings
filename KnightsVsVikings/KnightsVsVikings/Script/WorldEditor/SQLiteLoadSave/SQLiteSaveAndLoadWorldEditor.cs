﻿using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Models.WorldEditor;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.WorldEditor.SQLiteLoadSave
{
    // Lukas
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
            List<SQLite_Building> builings = new List<SQLite_Building>();
            List<SQLite_Unit> units = new List<SQLite_Unit>();
            List<SQLite_Resource> resources = new List<SQLite_Resource>();

            for (int x = 0; x < tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    if (tileGrid.groundTileGrid[x, y] != null)
                    {
                        SQLite_Ground ground = new SQLite_Ground();
                        ground.tileType = tileGrid.groundTileGrid[x, y].GetComponent<CTile>().TileType;
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
                    if (tileGrid.buildingTileGrid[x,y] != null)
                    {
                        SQLite_Building building = new SQLite_Building();
                        building.BuildingType = tileGrid.buildingTileGrid[x, y].GetComponent<CBuilding>().BuildingType;
                        building.Team = tileGrid.buildingTileGrid[x, y].GetComponent<CBuilding>().Team;
                        building.Faction = tileGrid.buildingTileGrid[x, y].GetComponent<CBuilding>().Faction;
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
                    if (tileGrid.unitTileGrid[x, y] != null)
                    {
                        SQLite_Unit unit = new SQLite_Unit();
                        unit.UnitType = tileGrid.unitTileGrid[x, y].GetComponent<CUnit>().UnitType;
                        unit.Team = tileGrid.unitTileGrid[x, y].GetComponent<CUnit>().Team;
                        unit.Faction = tileGrid.unitTileGrid[x, y].GetComponent<CUnit>().Faction;
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
                    if (tileGrid.resourceTileGrid[x, y] != null)
                    {
                        SQLite_Resource resource = new SQLite_Resource();
                        resource.resourcesType = tileGrid.resourceTileGrid[x, y].GetComponent<CResourceTile>().ResourcesType;
                        resource.X = x;
                        resource.Y = y;

                        resources.Add(resource);
                    }
                }
            }

            DoTheSQLSave(grounds,builings,units,resources);
        }

        // Lucas & Kasper
        private void DoTheSQLSave(List<SQLite_Ground> grounds, List<SQLite_Building> buildings, List<SQLite_Unit> units, List<SQLite_Resource> resources, int ID = 0)
        {
            ISQLiteRow worldData = new SQLiteWorldEditorModel(Singletons.TableContainerSingleton.WorldEditorTable, "DefaultWorld");

            Singletons.TableContainerSingleton.WorldEditorTable.Insert(worldData);

            List<ISQLiteRow> groundsSQLite = new List<ISQLiteRow>(),
                             unitsSQLite = new List<ISQLiteRow>(),
                             buildingsSQLite = new List<ISQLiteRow>(),
                             resourcesSQLite = new List<ISQLiteRow>();

            foreach (SQLite_Ground ground in grounds)
            {
                groundsSQLite.Add(
                    new SQLiteTileWorldEditorModel(
                    Singletons.TableContainerSingleton.TileWorldEditorTable,
                    worldData.Id,
                    (int)ground.tileType,
                    ground.X,
                    ground.Y)
                    );
            }

            foreach (SQLite_Building building in buildings)
            {
                buildingsSQLite.Add(
                    new SQLiteBuildingWorldEditorModel(
                    Singletons.TableContainerSingleton.BuildingWorldEditorTable,
                    worldData.Id,
                    (int)building.BuildingType,
                    (int)building.Team,
                    (int)building.Faction,
                    building.X,
                    building.Y)
                    );
            }

            foreach (SQLite_Unit unit in units)
            {
                unitsSQLite.Add(
                    new SQLiteUnitWorldEditorModel(
                    Singletons.TableContainerSingleton.UnitWorldEditorTable,
                    worldData.Id,
                    (int)unit.UnitType,
                    (int)unit.Team,
                    (int)unit.Faction,
                    unit.X,
                    unit.Y)
                    );
            }

            foreach (SQLite_Resource resource in resources)
            {
                resourcesSQLite.Add(
                    new SQLiteResourceWorldEditorModel
                    (Singletons.TableContainerSingleton.ResourceWorldEditorTable,
                    worldData.Id,
                    (int)resource.resourcesType,
                    resource.X,
                    resource.Y)
                    );
            }

            if(groundsSQLite.Count != 0)
            Singletons.TableContainerSingleton.TileWorldEditorTable.InsertMultiple(groundsSQLite);

            if (buildingsSQLite.Count != 0)
                Singletons.TableContainerSingleton.BuildingWorldEditorTable.InsertMultiple(buildingsSQLite);

            if (unitsSQLite.Count != 0)
                Singletons.TableContainerSingleton.UnitWorldEditorTable.InsertMultiple(unitsSQLite);

            if (resourcesSQLite.Count != 0)
                Singletons.TableContainerSingleton.ResourceWorldEditorTable.InsertMultiple(resourcesSQLite);
        }
        #endregion

        #region Load
        public void LoadSQLite(TileGrid tileGrid,int ID,Scene myScene)
        {
            List<SQLite_Ground> grounds = LoadGround(ID);
            List<SQLite_Building> builings = LoadBuilding(ID);
            List<SQLite_Unit> units = LoadUnit(ID);
            List<SQLite_Resource> resources = LoadResource(ID);

            foreach (SQLite_Ground item in grounds)
            {
                GameObject go = Singletons.TileFactorySingleton.Create(item.tileType.ToString());
                go.Transform.Position = new Vector2(tileSize.X * item.X, tileSize.Y * item.Y);
                myScene.Instantiate(go);
                tileGrid.groundTileGrid[item.X, item.Y] = go;
            }

            foreach (SQLite_Building item in builings)
            {
                GameObject go = Singletons.BuildingFactorySingleton.Create(item.BuildingType.ToString(), item.Faction, item.Team);
                go.Transform.Position = new Vector2(tileSize.X * item.X, tileSize.Y * item.Y);
                if(item.BuildingType != EBuildingType.Field)
                {
                go.Transform.Position = new Vector2((item.X) * tileGrid.TileSize.X, (item.Y ) * tileGrid.TileSize.Y);
                }
                else
                {
                    go.Transform.Position = new Vector2((item.X) * tileGrid.TileSize.X, (item.Y) * tileGrid.TileSize.Y);
                }
                myScene.Instantiate(go);
                tileGrid.buildingTileGrid[item.X, item.Y] = go;
            }

            foreach (SQLite_Unit item in units)
            {
                GameObject go = Singletons.UnitFactorySingleton.Create(item.UnitType.ToString(), item.Faction, item.Team);
                go.Transform.Position = new Vector2(tileSize.X * item.X, tileSize.Y * item.Y);
                myScene.Instantiate(go);
                tileGrid.unitTileGrid[item.X, item.Y] = go;
            }

            foreach (SQLite_Resource item in resources)
            {
                GameObject go = Singletons.ResourcesFactorySingleton.Create(item.resourcesType.ToString());
                go.Transform.Position = new Vector2(tileSize.X * item.X, tileSize.Y * item.Y);
                myScene.Instantiate(go);
                tileGrid.resourceTileGrid[item.X, item.Y] = go;
            }
        }

        // Lucas
        private List<SQLite_Ground> LoadGround(int ID)
        {
            List<SQLite_Ground> grounds = new List<SQLite_Ground>();

            List<ISQLiteRow> convert = Singletons.TableContainerSingleton.TileWorldEditorTable.GetMultiple(PropertyFinder<SQLiteTileWorldEditorModel>.Find(x => x.WorldId), ID);

            for (int i = 0; i < convert.Count; i++)
            {
                SQLite_Ground ground = new SQLite_Ground();

                ground.tileType = (ETileType)(convert.ElementAt(i) as SQLiteTileWorldEditorModel).TileTypeId;
                ground.X = (convert.ElementAt(i) as SQLiteTileWorldEditorModel).Xpos;
                ground.Y = (convert.ElementAt(i) as SQLiteTileWorldEditorModel).Ypos;

                grounds.Add(ground);
            }

            return grounds;
        }

        // Lucas
        private List<SQLite_Building> LoadBuilding(int ID)
        {
            List<SQLite_Building> builings = new List<SQLite_Building>();

            List<ISQLiteRow> convert = Singletons.TableContainerSingleton.BuildingWorldEditorTable.GetMultiple(PropertyFinder<SQLiteBuildingWorldEditorModel>.Find(x => x.WorldId), ID);

            for (int i = 0; i < convert.Count; i++)
            {
                SQLite_Building builing = new SQLite_Building();

                builing.BuildingType = (EBuildingType)(convert.ElementAt(i) as SQLiteBuildingWorldEditorModel).BuildingTypeId;
                builing.Team = (ETeam)(convert.ElementAt(i) as SQLiteBuildingWorldEditorModel).Team;
                builing.Faction = (EFaction)(convert.ElementAt(i) as SQLiteBuildingWorldEditorModel).Faction;
                builing.X = (convert.ElementAt(i) as SQLiteBuildingWorldEditorModel).Xpos;
                builing.Y = (convert.ElementAt(i) as SQLiteBuildingWorldEditorModel).Ypos;

                builings.Add(builing);
            }

            return builings;
        }

        // Lucas
        private List<SQLite_Unit> LoadUnit(int ID)
        {
            List<SQLite_Unit> units = new List<SQLite_Unit>();

            List<ISQLiteRow> convert = Singletons.TableContainerSingleton.UnitWorldEditorTable.GetMultiple(PropertyFinder<SQLiteUnitWorldEditorModel>.Find(x => x.WorldId), ID);

            for (int i = 0; i < convert.Count; i++)
            {
                SQLite_Unit unit = new SQLite_Unit();

                unit.UnitType = (EUnitType)(convert.ElementAt(i) as SQLiteUnitWorldEditorModel).UnitTypeId;
                unit.Team = (ETeam)(convert.ElementAt(i) as SQLiteUnitWorldEditorModel).Team;
                unit.Faction = (EFaction)(convert.ElementAt(i) as SQLiteUnitWorldEditorModel).Faction;
                unit.X = (convert.ElementAt(i) as SQLiteUnitWorldEditorModel).Xpos;
                unit.Y = (convert.ElementAt(i) as SQLiteUnitWorldEditorModel).Ypos;

                units.Add(unit);
            }

            return units;
        }

        // Lucas
        private List<SQLite_Resource> LoadResource(int ID)
        {
            List<SQLite_Resource> resources = new List<SQLite_Resource>();

            List<ISQLiteRow> convert = Singletons.TableContainerSingleton.ResourceWorldEditorTable.GetMultiple(PropertyFinder<SQLiteResourceWorldEditorModel>.Find(x => x.WorldId), ID);

            for (int i = 0; i < convert.Count; i++)
            {
                SQLite_Resource resource = new SQLite_Resource();

                resource.resourcesType = (EResourcesType)(convert.ElementAt(i) as SQLiteResourceWorldEditorModel).ResourceTypeId;
                resource.X = (convert.ElementAt(i) as SQLiteResourceWorldEditorModel).Xpos;
                resource.Y = (convert.ElementAt(i) as SQLiteResourceWorldEditorModel).Ypos;

                resources.Add(resource);
            }

            return resources;
        }
        #endregion
    }
}
