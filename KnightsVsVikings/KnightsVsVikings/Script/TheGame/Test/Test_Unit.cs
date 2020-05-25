using KnightsVsVikings;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Test
{
    public class Test_Unit : Component
    {
        CAnimator cAnimator;
        public override void Awake()
        {
            base.Awake();
            cAnimator = GameObject.GetComponent<CAnimator>();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            Test();
            NewAnimation();
        }

        public void NewAnimation()
        {
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D1) && !cAnimator.AnimationLock)
            {
                cAnimator.PlayAnimation(EUnitAnimationType.Idle.ToString());
            }
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D2) && !cAnimator.AnimationLock)
            {
                cAnimator.PlayAnimation($"{EUnitAnimationType.Run}");
            }
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D3) && !cAnimator.AnimationLock)
            {
                cAnimator.PlayAnimation($"{EUnitAnimationType.BowAttack}");
            }
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D4) && !cAnimator.AnimationLock)
            {
                cAnimator.PlayAnimation($"{EUnitAnimationType.SpearAttack}");
            }
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D5) && !cAnimator.AnimationLock)
            {
                cAnimator.PlayAnimation($"{EUnitAnimationType.SwordAttack}");
            }
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D6) && !cAnimator.AnimationLock)
            {
                cAnimator.PlayAnimation($"{EUnitAnimationType.Die}");
            }
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D7) && !cAnimator.AnimationLock)
            {
                cAnimator.PlayAnimation($"{EUnitAnimationType.Cast}");
            }
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D8))
            {
                cAnimator.Reset();
            }
        }

        public void Test()
        {
            Vector2 newVel = new Vector2(0, 0);

            if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.W))
            {
                newVel += new Vector2(0, 1);
            }
            if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.S))
            {
                newVel += new Vector2(0, -1);
            }
            if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.A))
            {
                newVel += new Vector2(-1, 0);
            }
            if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.D))
            {
                newVel += new Vector2(1, 0);
            }

            GameObject.Transform.Velocity = newVel;
        }
    }
}
