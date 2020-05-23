using KnightsVsVikings.Script.MainSystem.Enum;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.MainSystem.In_Works_Not_Done_Animations
{
    public class Animator : Component
    {
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Dictionary<string, BlendTree> blendTrees = new Dictionary<string, BlendTree>();

        private EFacingDirection facingDirection = EFacingDirection.Down;
        private Animation currentAnimation = null;
        private BlendTree currentBlendTree = null;
        private float timeElapsed;
        private int currentIndex;

        public override void Awake()
        {
            base.Awake();
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();

            if (currentAnimation.animationType == EAnimationType.SpriteArray)
            {
                spriteRenderer.Rectangle = new Rectangle(0, 0, (int)currentAnimation.Sprites[0].Width, (int)currentAnimation.Sprites[0].Height);
            }
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
            if (currentAnimation != null)
            {
                if (currentBlendTree != null && GameObject.Transform.Velocity != new Vector2(0, 0))
                {
                    Animation tmp = currentBlendTree.Play(GameObject.Transform.Velocity, spriteRenderer, ref facingDirection);
                    if (currentAnimation != tmp)
                    {
                        currentAnimation = tmp;
                    }
                }

                timeElapsed += Time.deltaTime;

                currentIndex = (int)(timeElapsed * currentAnimation.Fps);

                if (currentAnimation.SpriteSheet != null)
                {
                    if (currentIndex > currentAnimation.SpritePositions.Count - 1)
                    {
                        timeElapsed = 0;
                        currentIndex = 0;
                    }

                    spriteRenderer.Sprite = currentAnimation.SpriteSheet;

                    spriteRenderer.Rectangle = new Rectangle(
                    (int)currentAnimation.SpritePositions[currentIndex].X,
                    (int)currentAnimation.SpritePositions[currentIndex].Y,
                    (int)currentAnimation.SpriteSize.X,
                    (int)currentAnimation.SpriteSize.Y
                    );
                }
                else
                {
                    if (currentIndex > currentAnimation.Sprites.Length - 1)
                    {
                        timeElapsed = 0;
                        currentIndex = 0;
                    }

                    spriteRenderer.Sprite = currentAnimation.Sprites[currentIndex];
                }
            }
        }

        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);
        }
        public void AddAnimation(BlendTree blendTree)
        {
            blendTrees.Add(blendTree.Name, blendTree);
        }

        public void PlayAnimation(string animationName)
        {
            currentAnimation = null;
            currentBlendTree = null;

            timeElapsed = 0;
            currentIndex = 0;

            if (animations.ContainsKey(animationName))
            {
                currentAnimation = animations[animationName];
                if (currentAnimation.animationType == EAnimationType.SpriteArray && spriteRenderer != null)
                {
                    spriteRenderer.Rectangle = new Rectangle(0, 0, (int)currentAnimation.Sprites[0].Width, (int)currentAnimation.Sprites[0].Height);
                }
            }
            else if (blendTrees.ContainsKey(animationName))
            {
                currentBlendTree = blendTrees[animationName];
                currentAnimation = currentBlendTree.FacingCheck(facingDirection);

                if (currentAnimation.animationType == EAnimationType.SpriteArray && spriteRenderer != null)
                {
                    spriteRenderer.Rectangle = new Rectangle(0, 0, (int)currentAnimation.Sprites[0].Width, (int)currentAnimation.Sprites[0].Height);
                }
            }
        }
    }
}
