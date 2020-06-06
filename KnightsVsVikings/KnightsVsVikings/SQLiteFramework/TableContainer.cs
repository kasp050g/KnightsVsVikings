using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Models.TheGame;
using KnightsVsVikings.SQLiteFramework.Models.WorldEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework
{
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

        }
    }
}
