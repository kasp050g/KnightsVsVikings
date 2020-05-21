using KnightsVsVikings.Script.MainSystem.Enum;
using KnightsVsVikings.Script.MainSystem.In_Works_Not_Done_Animations;
using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Test
{
    public class Test_JAmen
    {
        public GameObject Jamen()
        {
            GameObject jamen = new GameObject();

            SpriteRenderer sp = new SpriteRenderer(SpriteContainer.Instance.Character);
            Animator animator = new Animator();
            //animator.AddAnimation(new Animation(SpriteContainer.Instance.SpriteList["Knights Bowman Up"],"Run",10,EFacingDirection.Down));
            //animator.AddAnimation(new Animation(SpriteContainer.Instance.SpriteList["Knights Bowman Down"],"Run",10,EFacingDirection.Down));
            //animator.AddAnimation(new Animation(SpriteContainer.Instance.SpriteList["Knights Bowman Side"],"Run",10,EFacingDirection.Down));


            BlendTree blendTree01 = new BlendTree
                (
                new Animation(SpriteContainer.Instance.SpriteList["Knights Bowman Up"], "Run1", 10),
                new Animation(SpriteContainer.Instance.SpriteList["Knights Bowman Down"], "Run1", 10),
                new Animation(SpriteContainer.Instance.SpriteList["Knights Bowman Side"], "Run1", 10),
                new Animation(SpriteContainer.Instance.SpriteList["Knights Bowman Side"], "Run1", 10),
                "Run1"
                );
            BlendTree blendTree02 = new BlendTree
                (
                new Animation(SpriteContainer.Instance.SpriteList["Knights Footman Up"], "Run1", 10),
                new Animation(SpriteContainer.Instance.SpriteList["Knights Footman Down"], "Run1", 10),
                new Animation(SpriteContainer.Instance.SpriteList["Knights Footman Side"], "Run1", 10),
                new Animation(SpriteContainer.Instance.SpriteList["Knights Footman Side"], "Run1", 10),
                "Run2"
                );
            animator.AddAnimation(blendTree01);
            animator.AddAnimation(blendTree02);
            animator.PlayAnimation("Run2");


            jamen.AddComponent<SpriteRenderer>(sp);
            jamen.AddComponent<Animator>(animator);
            jamen.AddComponent<Test_Unit>();

            return jamen;
        }


    }
}
