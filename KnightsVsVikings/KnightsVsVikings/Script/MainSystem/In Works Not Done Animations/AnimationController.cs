using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class AnimationController : Component
    {
        float fps = 10;
        float timeElapsed;
        int currentIndex;
        public SpriteRenderer sr;

        public Texture2D[] currentSprites;
        Texture2D currentSprite;

        Texture2D idlSprite;

        bool runOnetime = false;
        bool isDead = false;

        // - - -
        public bool Loop { get; set; }
        public bool StopAfter { get; set; }
        public bool AnimationLock { get; set; }

        public AnimationController(float fps)
        {
            this.fps = fps;
        }
        public AnimationController(float fps, SpriteRenderer sr)
        {
            this.fps = fps;
            this.sr = sr;
        }

        public override void Awake()
        {
            base.Awake();
            if (sr == null)
            {
                sr = GameObject.GetComponent<SpriteRenderer>();
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            Animate();
            sr.Sprite = currentSprite;
            Helper.UpdateOrigin(GameObject, sr.Sprite, sr.OriginPositionEnum);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public void OnAnimateSwit()
        {
            timeElapsed = 0;
            currentIndex = 0;
        }

        public void Animate()
        {
            // Adds time that has passed since last update.
            timeElapsed += Time.deltaTime;

            // Calculates the curent index.
            currentIndex = (int)(timeElapsed * fps);

            // Check if we need to restart the animation
            if (currentIndex >= currentSprites.Length && Loop)
            {
                // Resets the animation
                timeElapsed = 0;
                currentIndex = 0;
            }

            if (currentIndex < currentSprites.Length )
            {
                currentSprite = currentSprites[currentIndex];
            }
            else
            {
                currentSprite = currentSprites[currentSprites.Length - 1];
                AnimationLock = false;
            }
            ////Console.WriteLine(currentSprites[currentIndex].Name);
            //Console.WriteLine((int)(timeElapsed * fps));
        }
    }
}
