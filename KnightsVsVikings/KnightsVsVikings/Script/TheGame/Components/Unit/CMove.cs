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
    public class CMove : Component
    {
        private float speed = 100f;
        private Vector2 velocity = new Vector2(0,0);
        private CAnimator animator;
        private CSpriteRenderer spriteRenderer;

        public float Speed { get => speed; set => speed = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }

        public override void Awake()
        {
            base.Awake();
            if(GameObject.GetComponent<CAnimator>() != null)
            {
                animator = GameObject.GetComponent<CAnimator>();
                spriteRenderer = GameObject.GetComponent<CSpriteRenderer>();
            }
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
            Move();
            AnimationFacing();
        }

        private void Move()
        {
            GameObject.Transform.Position += speed * velocity * Time.deltaTime;
        }

        private void AnimationFacing()
        {
            Vector2 tmp = Vector2.Normalize(velocity);
            float y = tmp.Y;
            float x = tmp.X;

             if(0 < Math.Abs(x) && Math.Abs(x) >= Math.Abs(y))
            {
                if (0 > x)
                {
                    spriteRenderer.SpriteEffects = SpriteEffects.FlipHorizontally;
                    animator.FacingDirection = EFacingDirection.Left;
                }
                else
                {
                    spriteRenderer.SpriteEffects = SpriteEffects.None;
                    animator.FacingDirection = EFacingDirection.Right;
                }
            }
            else if (0 < Math.Abs(y))
            {
                if (0 < y)
                {
                    spriteRenderer.SpriteEffects = SpriteEffects.None;
                    animator.FacingDirection = EFacingDirection.Down;
                }
                else
                {
                    spriteRenderer.SpriteEffects = SpriteEffects.None;
                    animator.FacingDirection = EFacingDirection.Up;
                }
            }
        }
    }
}
