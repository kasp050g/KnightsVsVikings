using KnightsVsVikings;
using KnightsVsVikings.Script.TheGame.Test;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class Test_JAmen
    {
        public GameObject Jamen(EFactions factions, EUnitType unitType)
        {
            GameObject jamen = new GameObject();

            SpriteRenderer sp = new SpriteRenderer(SpriteContainer.Instance.Pixel);
            CAnimator animator = new CAnimator();


            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.Idle));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.Run));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.BowAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.SpearAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.SwordAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.Die));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.Cast));
            animator.PlayAnimation($"{EUnitAnimationType.Idle}");

            jamen.AddComponent<SpriteRenderer>(sp);
            jamen.AddComponent<CAnimator>(animator);

            CWorker worker = new CWorker();
            jamen.AddComponent<CWorker>(worker);

            Test_Unit test = new Test_Unit();
            jamen.AddComponent<Test_Unit>(test);

            return jamen;
        }


    }
}
