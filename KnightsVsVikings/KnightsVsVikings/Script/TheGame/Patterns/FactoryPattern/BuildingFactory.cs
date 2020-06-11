using KnightsVsVikings.Script.TheGame.FactoryPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    // Kasper
    public class BuildingFactory : IFactoryPlayerCreated
    {
        public GameObject Create(string type, EFaction faction, ETeam team)
        {
            EBuildingType buildingType = (EBuildingType)Enum.Parse(typeof(EBuildingType), type);

            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.SpriteSheet["GrayTent"]);
            CBuilding building = new CBuilding(buildingType, faction, team);
            CStats stats = new CStats();

            sr.LayerDepth = 0.3f;
            sr.OriginPositionEnum = EOriginPosition.TopLeft;
            sr.OffSet = new Vector2(-1 * 128/2, -3*128/2);
            go.Transform.Scale *= 0.5f;

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CBuilding>(building);
            go.AddComponent<CStats>(stats);

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
                    sr.OffSet = new Vector2(0 * 128 / 2, 0 * 128 / 2);
                    sr.SetSprite(SpriteContainer.Instance.TileSprite.Wheatfield);
                    break;
                default:
                    break;
            }

            if (buildingType != EBuildingType.Field)
            {
                switch (team)
                {
                    case ETeam.Team01:
                        sr.Color = Color.Red;
                        break;
                    case ETeam.Team02:
                        sr.Color = Color.Blue;
                        break;
                    case ETeam.Team03:
                        sr.Color = Color.Green;
                        break;
                    case ETeam.Team04:
                        sr.Color = Color.Yellow;
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

            return go;
        }
    }
}
