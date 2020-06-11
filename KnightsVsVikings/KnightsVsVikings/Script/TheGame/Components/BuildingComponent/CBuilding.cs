using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Models.TheGame;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    // Kasper og Lukas
    public class CBuilding : Component
    {
        private EBuildingType buildingType;
        private ETeam team;
        private EFaction faction;

        private GameObject woodSign;
        private GameObject icon;
        private CStats stats = new CStats();

        public EBuildingType BuildingType { get => buildingType; set => buildingType = value; }
        public EFaction Faction { get => faction; set => faction = value; }
        public ETeam Team { get => team; set => team = value; }

        public CBuilding(EBuildingType buildingType, EFaction faction, ETeam team)
        {
            this.buildingType = buildingType;
            this.faction = faction;
            this.team = team;
        }
        public override void Awake()
        {
            base.Awake();
            stats = GameObject.GetComponent<CStats>();
            if (buildingType != EBuildingType.Field)
            {
                BuildSign();
                AssignSQLiteValues();
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            if (buildingType != EBuildingType.Field)
            {
                GameObject.MyScene.Destroy(woodSign);
                GameObject.MyScene.Destroy(icon);
            }
        }

        private void BuildSign()
        {
            woodSign = new GameObject();
            CSpriteRenderer sr01 = new CSpriteRenderer(SpriteContainer.Instance.SpriteSheet["WoodSign"]);
            sr01.LayerDepth = 0.30000004f;
            sr01.OffSet = new Vector2(-1*128/2,-2*128/2);
            woodSign.AddComponent<CSpriteRenderer>(sr01);
            woodSign.Transform.Scale *= 0.5f;
            woodSign.Transform.Position = GameObject.Transform.Position;

            icon = new GameObject();
            CSpriteRenderer sr02 = new CSpriteRenderer();
            sr02.LayerDepth = 0.30000008f;
            sr02.OffSet = new Vector2(0 * 128 / 2, -1 * 128 / 2);
            icon.AddComponent<CSpriteRenderer>(sr02);
            icon.Transform.Scale *= 0.5f;
            icon.Transform.Position = woodSign.Transform.Position;

            switch (buildingType)
            {
                case EBuildingType.TownHall:
                    sr02.SetSprite(SpriteContainer.Instance.SpriteSheet["SignIcon_Book"]);
                    break;
                case EBuildingType.ArcheryRange:
                    sr02.SetSprite(SpriteContainer.Instance.SpriteSheet["SignIconTailoring"]);
                    break;
                case EBuildingType.Blacksmith:
                    sr02.SetSprite(SpriteContainer.Instance.SpriteSheet["SignIcon_BS"]);
                    break;
                case EBuildingType.Tower:
                    sr02.SetSprite(SpriteContainer.Instance.SpriteSheet["SignIcon_AM"]);
                    break;
                case EBuildingType.Barracks:
                    sr02.SetSprite(SpriteContainer.Instance.SpriteSheet["SignIcon_Axe"]);
                    break;
                case EBuildingType.GatheringStation:
                    sr02.SetSprite(SpriteContainer.Instance.SpriteSheet["SignIcon_Food"]);
                    break;
                case EBuildingType.Field:
                    sr02.SetSprite(SpriteContainer.Instance.SpriteSheet["SignIcon_Pot"]);
                    break;
                default:
                    break;
            }

            GameObject.MyScene.Instantiate(woodSign);
            GameObject.MyScene.Instantiate(icon);
        }

        // Lucas
        private void AssignSQLiteValues()
        {
            ISQLiteRow factionRow = Singletons.TableContainerSingleton.FactionTable.Get(PropertyFinder<SQLiteFactionModel>.Find(x => x.Name), Faction.ToString());
            List<ISQLiteRow> buildingRow = Singletons.TableContainerSingleton.BuildingTable.GetMultiple(PropertyFinder<SQLiteBuildingModel>.Find(x => x.FactionId), factionRow.Id);
            ISQLiteRow myStatsRow = null;

            foreach (SQLiteBuildingModel row in buildingRow)
                if (row.BuildingTypeId == (int)buildingType)
                {
                    myStatsRow = Singletons.TableContainerSingleton.StatsTable.Get(row.StatsId);
                    break;
                }

            List<PropertyInfo> myStatsProperties = myStatsRow.GetType().GetProperties().ToList();
            myStatsProperties.RemoveAll(property => property.Name == "Id" || property.Name == "LocatedInTable");
            List<PropertyInfo> statsProperties = stats.Stats.GetType().GetProperties().ToList();

            for (int i = 0; i < myStatsProperties.Count - 1; i++)
                statsProperties.ElementAt(i).SetValue(stats.Stats, myStatsProperties.ElementAt(i).GetValue(myStatsRow));
        }
    }
}