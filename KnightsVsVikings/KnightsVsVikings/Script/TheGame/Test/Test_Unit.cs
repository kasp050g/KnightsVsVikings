using KnightsVsVikings.Script.MainSystem.In_Works_Not_Done_Animations;
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
        public override void Awake()
        {
            base.Awake();
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
            if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                Animator tmp = GameObject.GetComponent<Animator>();
                tmp.PlayAnimation("Run1");
            }
            if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.E))
            {
                Animator tmp = GameObject.GetComponent<Animator>();
                tmp.PlayAnimation("Run2");
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
