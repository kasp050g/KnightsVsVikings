using KnightsVsVikings.Script.MainSystem.Enum;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class CAnimator : Component
    {
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, CAnimation> animations = new Dictionary<string, CAnimation>();
        private Dictionary<string, CBlendTree> blendTrees = new Dictionary<string, CBlendTree>();

        private EFacingDirection facingDirection = EFacingDirection.Down;
        private CAnimation currentAnimation = null;
        private CBlendTree currentBlendTree = null;
        private float timeElapsed;
        private int currentIndex;
        private bool stopAnimator = false;

        public bool AnimationLock { get { return (currentAnimation != null ? currentAnimation.AnimationLock : false); } }
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
            // Check if we got a animation to play if so play it
            // Check to if we ask it to stop the animator
            if (currentAnimation != null && stopAnimator == false)
            {
                // this is made for this Game.
                // to 
                if (currentBlendTree != null && GameObject.Transform.Velocity != new Vector2(0, 0))
                {
                    CAnimation tmp = currentBlendTree.Play(GameObject.Transform.Velocity, spriteRenderer, ref facingDirection);
                    if (currentAnimation != tmp)
                    {
                        currentAnimation = tmp;
                    }
                }

                timeElapsed += Time.deltaTime;

                currentIndex = (int)(timeElapsed * currentAnimation.Fps);

                ChangeSprite();
            }
        }

        private void ChangeSprite()
        {
            if (currentAnimation.SpriteSheet != null)
            {
                if (currentIndex > currentAnimation.SpritePositions.Count - 1)
                {
                    timeElapsed = 0;
                    currentIndex = 0;
                    if (currentAnimation.Loop == false && currentAnimation.End == false)
                    {
                        currentBlendTree = blendTrees.First().Value;
                        currentAnimation = currentBlendTree.FacingCheck(facingDirection);
                    }
                    else if (currentAnimation.End == true)
                    {
                        stopAnimator = true;
                    }
                }

                if (stopAnimator == false)
                {
                    spriteRenderer.Sprite = currentAnimation.SpriteSheet;

                    spriteRenderer.Rectangle = new Rectangle(
                    (int)currentAnimation.SpritePositions[currentIndex].X,
                    (int)currentAnimation.SpritePositions[currentIndex].Y,
                    (int)currentAnimation.SpriteSize.X,
                    (int)currentAnimation.SpriteSize.Y
                    );
                }
            }
            else
            {
                if (currentIndex > currentAnimation.Sprites.Length - 1)
                {
                    timeElapsed = 0;
                    currentIndex = 0;
                    if (currentAnimation.Loop == false && currentAnimation.End == false)
                    {
                        currentAnimation = animations.First().Value;
                    }
                    else if (currentAnimation.End == true)
                    {
                        stopAnimator = true;
                    }
                }

                if (stopAnimator == false)
                {
                    spriteRenderer.Sprite = currentAnimation.Sprites[currentIndex];
                }
            }
        }

        public void AddAnimation(CAnimation animation)
        {
            animations.Add(animation.Name, animation);
        }
        public void AddAnimation(CBlendTree blendTree)
        {
            blendTrees.Add(blendTree.Name, blendTree);
        }

        public void PlayAnimation(string animationName)
        {
            currentAnimation = null;
            currentBlendTree = null;

            timeElapsed = 0;
            currentIndex = 0;
            stopAnimator = false;

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
            else
            {
                Console.WriteLine("Error did not find the animation: " + animationName);
            }
        }

        public void Reset()
        {
            stopAnimator = false;
            timeElapsed = 0;
            currentIndex = 0;

            if (currentAnimation.SpriteSheet != null)
            {
                currentBlendTree = blendTrees.First().Value;
                currentAnimation = currentBlendTree.FacingCheck(facingDirection);

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
                currentAnimation = animations.First().Value;
                spriteRenderer.Sprite = currentAnimation.Sprites[currentIndex];
            }
        }
    }
}
