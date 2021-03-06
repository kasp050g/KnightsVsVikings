﻿using KnightsVsVikings;
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
    // Kasper  Fly
    public class CAnimator : Component
    {
        private CSpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Dictionary<string, BlendTree> blendTrees = new Dictionary<string, BlendTree>();

        private EFacingDirection facingDirection = EFacingDirection.Down;
        private Animation currentAnimation = null;
        private BlendTree currentBlendTree = null;
        private float timeElapsed;
        private int currentIndex;
        private bool stopAnimator = false;

        public bool AnimationLock { get { return (currentAnimation != null ? currentAnimation.AnimationLock : false); } }

        public EFacingDirection FacingDirection { get => facingDirection; set => facingDirection = value; }

        public override void Awake()
        {
            base.Awake();
            spriteRenderer = GameObject.GetComponent<CSpriteRenderer>();

            if (currentAnimation.AnimationType == EAnimationType.SpriteArray)
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

        /// <summary>
        /// Update Animation
        /// </summary>
        private void UpdateAnimation()
        {
            // Check if we got a animation to play if so play it
            // Check to if we ask it to stop the animator
            if (currentAnimation != null && stopAnimator == false)
            {
                // this is made for this Game.
                // to 
                if (currentBlendTree != null )
                {
                    //CAnimation tmp = currentBlendTree.Play(GameObject.Transform.Velocity, spriteRenderer, ref facingDirection);
                    Animation tmp = currentBlendTree.FacingCheck(facingDirection);
                    if (currentAnimation != tmp)
                    {
                        currentAnimation = tmp;
                    }
                }

                timeElapsed += Time.DeltaTime;

                currentIndex = (int)(timeElapsed * currentAnimation.Fps);

                ChangeSprite();
            }
        }

        /// <summary>
        /// Change sprite with the animation
        /// </summary>
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

        /// <summary>
        /// Add Animation
        /// </summary>
        /// <param name="animation"></param>
        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);
        }

        /// <summary>
        /// Add BlendTree
        /// </summary>
        /// <param name="blendTree"></param>
        public void AddAnimation(BlendTree blendTree)
        {
            blendTrees.Add(blendTree.Name, blendTree);
        }

        /// <summary>
        /// Play animation
        /// </summary>
        /// <param name="animationName">Name of animation</param>
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
                if (currentAnimation.AnimationType == EAnimationType.SpriteArray && spriteRenderer != null)
                {
                    spriteRenderer.Rectangle = new Rectangle(0, 0, (int)currentAnimation.Sprites[0].Width, (int)currentAnimation.Sprites[0].Height);
                }
            }
            else if (blendTrees.ContainsKey(animationName))
            {
                currentBlendTree = blendTrees[animationName];
                currentAnimation = currentBlendTree.FacingCheck(facingDirection);

                if (currentAnimation.AnimationType == EAnimationType.SpriteArray && spriteRenderer != null)
                {
                    spriteRenderer.Rectangle = new Rectangle(0, 0, (int)currentAnimation.Sprites[0].Width, (int)currentAnimation.Sprites[0].Height);
                }
            }
            else
            {
                Console.WriteLine("Error did not find the animation: " + animationName);
            }
        }

        /// <summary>
        /// Reset the animation
        /// </summary>
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
