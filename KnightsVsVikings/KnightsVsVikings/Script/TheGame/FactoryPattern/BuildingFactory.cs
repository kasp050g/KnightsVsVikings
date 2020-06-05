using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class BuildingFactory : Factory
    {
        public GameObject Creaft(EBuildingType buildingType)
        {
            // Main GameObject
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.SpriteSheet["GrayTent"]);
            CBuilding building = new CBuilding(buildingType);

            sr.LayerDepth = 0.2f;
            go.Transform.Scale *= 0.5f;

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CBuilding>(building);

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
                    sr.SetSprite(SpriteContainer.Instance.TileSprite.Wheatfield);
                    break;
                default:
                    break;
            }

            return go;
        }
    }
}
