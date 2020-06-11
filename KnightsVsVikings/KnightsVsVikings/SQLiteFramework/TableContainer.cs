using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Models.TheGame;
using KnightsVsVikings.SQLiteFramework.Models.WorldEditor;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework
{
    // Lucas
    public class TableContainer
    {
        private ISQLiteDBProvider provider = new SQLiteDBProvider("Data Source=KV.db; Version=3; new=true");

        public ISQLiteTable UnitTable { get; set; }
        public ISQLiteTable UnitTypeTable { get; set; }
        public ISQLiteTable CanBuildUnitTable { get; set; }
        public ISQLiteTable UnitGotPassiveTable { get; set; }
        public ISQLiteTable UnitWorldEditorTable { get; set; }
        public ISQLiteTable UnitTypeWorldEditorTable { get; set; }
        public ISQLiteTable BuildingTable { get; set; }
        public ISQLiteTable BuildingTypeTable { get; set; }
        public ISQLiteTable CanBuildPassiveTable { get; set; }
        public ISQLiteTable BuildingGotPassiveTable { get; set; }
        public ISQLiteTable DoodadWorldEditorTable { get; set; }
        public ISQLiteTable DoodadTypeWorldEditorTable { get; set; }
        public ISQLiteTable ResourceWorldEditorTable { get; set; }
        public ISQLiteTable ResourceTypeWorldEditorTable { get; set; }
        public ISQLiteTable BuildingWorldEditorTable { get; set; }
        public ISQLiteTable TileWorldEditorTable { get; set; }
        public ISQLiteTable TileTypeWorldEditorTable { get; set; }
        public ISQLiteTable WorldEditorTable { get; set; }
        public ISQLiteTable CampaignChapterTable { get; set; }
        public ISQLiteTable FactionTable { get; set; }
        public ISQLiteTable PassiveTable { get; set; }
        public ISQLiteTable StatsTable { get; set; }
        public ISQLiteTable ProjectileTypeTable { get; set; }

        public TableContainer()
        {
            InstantiateTables();
            AddRows();
        }

        private void InstantiateTables()
        {
            UnitTable = new SQLiteTable<SQLiteUnitModel>("UnitTable", provider);
            UnitTypeTable = new SQLiteTable<SQLiteUnitTypeModel>("UnitTypeTable", provider);
            CanBuildUnitTable = new SQLiteTable<SQLiteCanBuildUnitModel>("CanBuildUnitTable", provider);
            UnitGotPassiveTable = new SQLiteTable<SQLiteUnitGotPassiveModel>("UnitGotPassiveTable", provider);
            UnitWorldEditorTable = new SQLiteTable<SQLiteUnitWorldEditorModel>("UnitWorldEditorTable", provider);
            UnitTypeWorldEditorTable = new SQLiteTable<SQLiteUnitTypeWorldEditorModel>("UnitTypeWorldEditorTable", provider);
            BuildingTable = new SQLiteTable<SQLiteBuildingModel>("BuildingTable", provider);
            BuildingTypeTable = new SQLiteTable<SQLiteBuildingTypeModel>("BuildingTypeTable", provider);
            CanBuildPassiveTable = new SQLiteTable<SQLiteCanBuildPassiveModel>("CanBuildPassiveTable", provider);
            BuildingGotPassiveTable = new SQLiteTable<SQLiteBuildingGotPassiveModel>("BuildingGotPassiveTable", provider);
            DoodadWorldEditorTable = new SQLiteTable<SQLiteDoodadWorldEditorModel>("DoodadWorldEditorTable", provider);
            DoodadTypeWorldEditorTable = new SQLiteTable<SQLiteDoodadTypeWorldEditorModel>("DoodadTypeWorldEditorTable", provider);
            ResourceWorldEditorTable = new SQLiteTable<SQLiteResourceWorldEditorModel>("ResourceWorldEditorTable", provider);
            ResourceTypeWorldEditorTable = new SQLiteTable<SQLiteResourceTypeWorldEditorModel>("ResourceTypeWorldEditorTable", provider);
            TileWorldEditorTable = new SQLiteTable<SQLiteTileWorldEditorModel>("TileWorldEditorTable", provider);
            BuildingWorldEditorTable = new SQLiteTable<SQLiteBuildingWorldEditorModel>("BuildingWorldEditorTable", provider);
            TileTypeWorldEditorTable = new SQLiteTable<SQLiteTileTypeWorldEditorModel>("TileTypeWorldEditorTable", provider);
            WorldEditorTable = new SQLiteTable<SQLiteWorldEditorModel>("WorldEditorTable", provider);
            CampaignChapterTable = new SQLiteTable<SQLiteCampaignChapterModel>("CampaignChapterTable", provider);
            FactionTable = new SQLiteTable<SQLiteFactionModel>("FactionTable", provider);
            PassiveTable = new SQLiteTable<SQLitePassiveModel>("PassiveTable", provider);
            StatsTable = new SQLiteTable<SQLiteStatsModel>("StatsTable", provider);
            ProjectileTypeTable = new SQLiteTable<SQLiteProjectileTypeModel>("ProjectileTypeTable", provider);
        }

        private void AddRows()
        {
            ISQLiteRow knightSpearmanStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 10, 10, 10, 10, 5f, 100, 10, 2, 150f, 10, 10, 150f, 50)); // -- 1
            ISQLiteRow knightFootmanStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 10, 10, 10, 10, 5f, 100, 10, 2, 150f, 10, 10, 150f, 50)); // -- 2
            ISQLiteRow knightArcherStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 10, 10, 10, 10, 5f, 100, 10, 2, 150f, 10, 10, 150f, 50)); // -- 3
            ISQLiteRow knightWorkerStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 10, 10, 10, 10, 5f, 100, 10, 2, 150f, 10, 10, 150f, 50)); // -- 4

            ISQLiteRow vikingSpearmanStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 10, 10, 10, 10, 5f, 100, 10, 2, 150f, 10, 10, 150f, 50)); // -- 5
            ISQLiteRow vikingFootmanStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 10, 10, 10, 10, 5f, 100, 10, 2, 150f, 10, 10, 150f, 50)); // -- 6
            ISQLiteRow vikingArcherStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 10, 10, 10, 10, 5f, 100, 10, 2, 150f, 10, 10, 150f, 50)); // -- 7
            ISQLiteRow vikingWorkerStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 10, 10, 10, 10, 5f, 100, 10, 2, 150f, 10, 10, 150f, 50)); // -- 8

            ISQLiteRow knightFaction = FactionTable.Insert(new SQLiteFactionModel(FactionTable, EFaction.Knights.ToString())); // -- 1
            ISQLiteRow vikingsFaction = FactionTable.Insert(new SQLiteFactionModel(FactionTable, EFaction.Vikings.ToString())); // -- 2

            SQLiteUnitModel knightSpearmanUnit = new SQLiteUnitModel(UnitTable, knightFaction.Id, (int)EUnitType.Spearman, 0, knightSpearmanStats.Id, "Spearman");
            SQLiteUnitModel knightFootmanUnit = new SQLiteUnitModel(UnitTable, knightFaction.Id, (int)EUnitType.Footman, 0, knightFootmanStats.Id, "Footman");
            SQLiteUnitModel knightArcherUnit = new SQLiteUnitModel(UnitTable, knightFaction.Id, (int)EUnitType.Bowman, 0, knightArcherStats.Id, "Bowman");
            SQLiteUnitModel knightWorkerUnit = new SQLiteUnitModel(UnitTable, knightFaction.Id, (int)EUnitType.Worker, 0, knightWorkerStats.Id, "Worker");

            SQLiteUnitModel vikingSpearmanUnit = new SQLiteUnitModel(UnitTable, vikingsFaction.Id, (int)EUnitType.Spearman, 0, vikingSpearmanStats.Id, "Spearman");
            SQLiteUnitModel vikingFootmanUnit = new SQLiteUnitModel(UnitTable, vikingsFaction.Id, (int)EUnitType.Footman, 0, vikingFootmanStats.Id, "Footman");
            SQLiteUnitModel vikingArcherUnit = new SQLiteUnitModel(UnitTable, vikingsFaction.Id, (int)EUnitType.Bowman, 0, vikingArcherStats.Id, "Bowman");
            SQLiteUnitModel vikingWorkerUnit = new SQLiteUnitModel(UnitTable, vikingsFaction.Id, (int)EUnitType.Worker, 0, vikingWorkerStats.Id, "Worker");


            UnitTable.Insert(knightSpearmanUnit); // -- 1
            UnitTable.Insert(knightFootmanUnit); // -- 2
            UnitTable.Insert(knightArcherUnit); // -- 3
            UnitTable.Insert(knightWorkerUnit); // -- 4

            UnitTable.Insert(vikingSpearmanUnit); // -- 5
            UnitTable.Insert(vikingFootmanUnit); // -- 6
            UnitTable.Insert(vikingArcherUnit); // -- 7
            UnitTable.Insert(vikingWorkerUnit); // -- 8

            ISQLiteRow vikingArcheryRangeStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 9
            ISQLiteRow vikingBarracksStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 10
            ISQLiteRow vikingBlacksmithStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 11
            ISQLiteRow vikingFieldStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 12
            ISQLiteRow vikingGatheringStationStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 13
            ISQLiteRow vikingTowerStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 250, 125, 50, 10, 5f, 400, 40, 2, 0f, 10, 10, 0f, 50)); // -- 14
            ISQLiteRow vikingTownHallStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 15

            ISQLiteRow knightArcheryRangeStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 9
            ISQLiteRow knightBarracksStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 10
            ISQLiteRow knightBlacksmithStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 11
            ISQLiteRow knightFieldStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 12
            ISQLiteRow knightGatheringStationStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 13
            ISQLiteRow knightTowerStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 250, 125, 50, 10, 5f, 400, 40, 2, 0f, 10, 10, 0f, 50)); // -- 14
            ISQLiteRow knightTownHallStats = StatsTable.Insert(new SQLiteStatsModel(StatsTable, 500, 250, 100, 10, 5f, 800, 0, 2, 0f, 10, 10, 0f, 50)); // -- 15

            SQLiteBuildingModel vikingArcheryRangeBuilding = new SQLiteBuildingModel(BuildingTable, vikingsFaction.Id, (int)EBuildingType.ArcheryRange, 0, vikingArcheryRangeStats.Id, "ArcheryRange");
            SQLiteBuildingModel vikingBarracksBuilding = new SQLiteBuildingModel(BuildingTable, vikingsFaction.Id, (int)EBuildingType.Barracks, 0, vikingBarracksStats.Id, "Barracks");
            SQLiteBuildingModel vikingBlacksmithBuilding = new SQLiteBuildingModel(BuildingTable, vikingsFaction.Id, (int)EBuildingType.Blacksmith, 0, vikingBlacksmithStats.Id, "Blacksmith");
            SQLiteBuildingModel vikingFieldBuilding = new SQLiteBuildingModel(BuildingTable, vikingsFaction.Id, (int)EBuildingType.Field, 0, vikingFieldStats.Id, "Field");
            SQLiteBuildingModel vikingGatheringStationBuilding = new SQLiteBuildingModel(BuildingTable, vikingsFaction.Id, (int)EBuildingType.GatheringStation, 0, vikingGatheringStationStats.Id, "GatheringStation");
            SQLiteBuildingModel vikingTowerBuilding = new SQLiteBuildingModel(BuildingTable, vikingsFaction.Id, (int)EBuildingType.Tower, 0, vikingTowerStats.Id, "Tower");
            SQLiteBuildingModel vikingTownHallBuilding = new SQLiteBuildingModel(BuildingTable, vikingsFaction.Id, (int)EBuildingType.TownHall, 0, vikingTownHallStats.Id, "TownHall");

            SQLiteBuildingModel knightArcheryRangeBuilding = new SQLiteBuildingModel(BuildingTable, knightFaction.Id, (int)EBuildingType.ArcheryRange, 0, knightArcheryRangeStats.Id, "ArcheryRange");
            SQLiteBuildingModel knightBarracksBuilding = new SQLiteBuildingModel(BuildingTable, knightFaction.Id, (int)EBuildingType.Barracks, 0, knightBarracksStats.Id, "Barracks");
            SQLiteBuildingModel knightBlacksmithBuilding = new SQLiteBuildingModel(BuildingTable, knightFaction.Id, (int)EBuildingType.Blacksmith, 0, knightBlacksmithStats.Id, "Blacksmith");
            SQLiteBuildingModel knightFieldBuilding = new SQLiteBuildingModel(BuildingTable, knightFaction.Id, (int)EBuildingType.Field, 0, knightFieldStats.Id, "Field");
            SQLiteBuildingModel knightGatheringStationBuilding = new SQLiteBuildingModel(BuildingTable, knightFaction.Id, (int)EBuildingType.GatheringStation, 0, knightGatheringStationStats.Id, "GatheringStation");
            SQLiteBuildingModel knightTowerBuilding = new SQLiteBuildingModel(BuildingTable, knightFaction.Id, (int)EBuildingType.Tower, 0, knightTowerStats.Id, "Tower");
            SQLiteBuildingModel knightTownHallBuilding = new SQLiteBuildingModel(BuildingTable, knightFaction.Id, (int)EBuildingType.TownHall, 0, knightTownHallStats.Id, "TownHall");

            BuildingTable.Insert(vikingArcheryRangeBuilding); // -- 1
            BuildingTable.Insert(vikingBarracksBuilding); // -- 2
            BuildingTable.Insert(vikingBlacksmithBuilding); // -- 3
            BuildingTable.Insert(vikingFieldBuilding); // -- 4
            BuildingTable.Insert(vikingGatheringStationBuilding); // -- 5
            BuildingTable.Insert(vikingTowerBuilding); // -- 6
            BuildingTable.Insert(vikingTownHallBuilding); // -- 7

            BuildingTable.Insert(knightArcheryRangeBuilding); // -- 8
            BuildingTable.Insert(knightBarracksBuilding); // -- 9
            BuildingTable.Insert(knightBlacksmithBuilding); // -- 10
            BuildingTable.Insert(knightFieldBuilding); // -- 11
            BuildingTable.Insert(knightGatheringStationBuilding); // -- 12
            BuildingTable.Insert(knightTowerBuilding); // -- 13
            BuildingTable.Insert(knightTownHallBuilding); // -- 14
        }
    }
}
