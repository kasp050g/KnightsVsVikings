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
    public class CBuilding : Component
    {
        private EBuildingType buildingType;
        private ETeam team;
        private EFaction faction;

        private GameObject woodSing;
        private GameObject icon;
        private CStats stats;

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
                BuildSing();
            }
        }
        public override void Start()
        {
            base.Start();
        }

        public override void Destroy()
        {
            base.Destroy();
            if (buildingType != EBuildingType.Field)
            {
                GameObject.MyScene.Destroy(woodSing);
                GameObject.MyScene.Destroy(icon);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            base.Update();
        }

        private void BuildSing()
        {
            woodSing = new GameObject();
            CSpriteRenderer sr01 = new CSpriteRenderer(SpriteContainer.Instance.SpriteSheet["WoodSign"]);
            sr01.LayerDepth = 0.30000004f;
            sr01.OffSet = new Vector2(-1*128/2,-2*128/2);
            woodSing.AddComponent<CSpriteRenderer>(sr01);
            woodSing.Transform.Scale *= 0.5f;
            woodSing.Transform.Position = GameObject.Transform.Position;

            icon = new GameObject();
            CSpriteRenderer sr02 = new CSpriteRenderer();
            sr02.LayerDepth = 0.30000008f;
            sr02.OffSet = new Vector2(0 * 128 / 2, -1 * 128 / 2);
            icon.AddComponent<CSpriteRenderer>(sr02);
            icon.Transform.Scale *= 0.5f;
            icon.Transform.Position = woodSing.Transform.Position;

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

            GameObject.MyScene.Instantiate(woodSing);
            GameObject.MyScene.Instantiate(icon);
        }
    }
}
