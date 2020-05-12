using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class Animator : Component
    {
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Animation currentAnimation;
        private float timeElapsed;
        private int currentIndex;

        public override void Awake()
        {
            base.Awake();
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
        }
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            UpdateAnimation();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void UpdateAnimation()
        {
            if(currentAnimation != null)
            {
                timeElapsed += Time.deltaTime;

                currentIndex = (int)(timeElapsed * currentAnimation.Fps);

                if(currentIndex > currentAnimation.Sprites.Length - 1)
                {
                    timeElapsed = 0;
                    currentIndex = 0;
                }

                spriteRenderer.Sprite = currentAnimation.Sprites[currentIndex];
            }
        }

        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);
        }

        public void PlayAnimation(string animationName)
        {
            currentAnimation = animations[animationName];
        }
    }
}
