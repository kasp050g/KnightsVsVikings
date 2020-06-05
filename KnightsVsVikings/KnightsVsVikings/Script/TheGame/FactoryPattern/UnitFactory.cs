using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class UnitFactory : Factory
    {
        public GameObject Creaft(ETeam team, EUnitType unitType, EFaction faction)
        {
            // Main GameObject
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.Pixel);
            CAnimator animator = new CAnimator();
            CUnit unit = new CUnit(team, unitType, faction);
            CMove move = new CMove();

            go.AddComponent<CUnit>(unit);
            go.AddComponent<CMove>(move);
            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CAnimator>(animator);

            sr.LayerDepth = 0.3f;
            sr.OffSet = new Vector2(-0.75f * 128, -0.9f * 128);
            go.Transform.Scale *= 1.0f;

            return go;
        }
    }
}
