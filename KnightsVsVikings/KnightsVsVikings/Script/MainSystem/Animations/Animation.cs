using KnightsVsVikings.Script.MainSystem.Enum;
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
    public class Animation
    {
        // Sprite Array
        public Texture2D[] Sprites { get; private set; }

        // Sprite Sheet
        public Texture2D SpriteSheet { get; set; }
        public List<Vector2> SpritePositions { get; set; }
        public Vector2 SpriteSize { get; set; }
        public EAnimationType AnimationType { get; set; }
        public string Name { get; private set; }
        public float Fps { get; private set; }
        public bool Loop { get; set; }
        public bool End { get; set; }
        public bool AnimationLock { get; set; }

        /// <summary>
        /// Use if the Animation is made with a List of sprites
        /// </summary>
        /// <param name="sprites">List of sprites</param>
        /// <param name="name">Name of the animation</param>
        /// <param name="fps">How many pictures that is shown per second</param>
        /// <param name="Loop">Do this animation loop</param>
        /// <param name="AnimationLock">when this animation is playing it is locked, I cannot shift to another animation</param>
        /// <param name="End">when it is done playing the full animation it will end this animation and return to the default animation</param>
        public Animation(List<Texture2D> sprites, string name, float fps, bool Loop, bool AnimationLock = false,bool End = false)
        {
            this.Sprites = sprites.ToArray();
            this.Name = name;
            this.Fps = fps;
            this.Loop = Loop;
            this.End = End;
            this.AnimationLock = AnimationLock;
            AnimationType = EAnimationType.SpriteArray;
        }
        /// <summary>
        /// Use if the Animation is made with a Sprite Sheet image
        /// </summary>
        /// <param name="SpriteSheet">Sprite sheet image</param>
        /// <param name="SpritePositions">positions of the cut out images of the Sprite sheet</param>
        /// <param name="SpriteSize">the size of the cut-out images</param>
        /// <param name="name">Name of the animation</param>
        /// <param name="fps">How many pictures that is shown per second</param>
        /// <param name="Loop">Do this animation loop</param>
        /// <param name="AnimationLock">when this animation is playing it is locked, I cannot shift to another animation</param>
        /// <param name="End">when it is done playing the full animation it will end this animation and return to the default animation</param>
        public Animation(Texture2D SpriteSheet, List<Vector2> SpritePositions, Vector2 SpriteSize, string name, float fps, bool Loop,bool AnimationLock = false, bool End = false)
        {
            this.SpriteSheet = SpriteSheet;
            this.SpritePositions = SpritePositions;
            this.SpriteSize = SpriteSize;
            this.Name = name;
            this.Fps = fps;
            this.Loop = Loop;
            this.End = End;
            this.AnimationLock = AnimationLock;
            AnimationType = EAnimationType.SpriteSheet;
        }
    }
}
