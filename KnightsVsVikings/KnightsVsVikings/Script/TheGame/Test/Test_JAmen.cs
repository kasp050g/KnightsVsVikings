using KnightsVsVikings.Script.MainSystem.Enum;
using KnightsVsVikings.Script.MainSystem.In_Works_Not_Done_Animations;
using KnightsVsVikings.Script.TheGame.Enum.AnimationsEnum;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Test
{
    public class Test_JAmen
    {
        public GameObject Jamen(EFactions factions, EUnitType unitType)
        {
            GameObject jamen = new GameObject();

            SpriteRenderer sp = new SpriteRenderer(SpriteContainer.Instance.Pixel);
            Animator animator = new Animator();


            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.Idle));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.Run));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.BowAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.SpearAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.SwordAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.Die));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(factions, unitType, EUnitAnimationType.Cast));
            animator.PlayAnimation($"{EUnitAnimationType.Idle}");

            jamen.AddComponent<SpriteRenderer>(sp);
            jamen.AddComponent<Animator>(animator);
            Test_Unit test_Unit = new Test_Unit();
            jamen.AddComponent<Test_Unit>(test_Unit);

            return jamen;
        }


    }
}
