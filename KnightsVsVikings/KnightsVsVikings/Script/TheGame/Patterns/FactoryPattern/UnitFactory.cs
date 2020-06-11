using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.FactoryPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    // Kasper og Lukas
    public class UnitFactory : IFactoryPlayerCreated
    {
        public GameObject Create(string type, EFaction faction, ETeam team)
        {
            // Main GameObject
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(SpriteContainer.Instance.Pixel);
            CAnimator animator = new CAnimator();
            CUnit unit = new CUnit(team, (EUnitType)Enum.Parse(typeof(EUnitType), type), faction);
            CMove move = new CMove();
            CStats stats = new CStats();
            CAstar astar = new CAstar(unit);
            CSelectable canBeSelected = new CSelectable();
            CShadow shadow = new CShadow();

            go.AddComponent<CUnit>(unit);
            go.AddComponent<CMove>(move);
            go.AddComponent<CStats>(stats);
            go.AddComponent<CShadow>(shadow);
            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<CAnimator>(animator);
            go.AddComponent<CSelectable>(canBeSelected);
            go.AddComponent<CAstar>(astar);

            switch (team)
            {
                case ETeam.Team01:
                    sr.Color = Color.LightPink;
                    break;
                case ETeam.Team02:
                    sr.Color = Color.LightBlue;
                    break;
                case ETeam.Team03:
                    sr.Color = Color.LightGreen;
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

            sr.LayerDepth = 0.3f;
            sr.OffSet = new Vector2(-0.75f * 128, -0.9f * 128);
            go.Transform.Scale *= 1.0f;

            return go;
        }
    }
}
