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
    public class CAnimation
    {
        // Sprite Array
        public Texture2D[] Sprites { get; private set; }

        // Sprite Sheet
        public Texture2D SpriteSheet { get; set; }
        public List<Vector2> SpritePositions { get; set; }
        public Vector2 SpriteSize { get; set; }
        public EAnimationType animationType { get; set; }
        public string Name { get; private set; }
        public float Fps { get; private set; }
        public bool Loop { get; set; }
        public bool End { get; set; }
        public bool AnimationLock { get; set; }

        public CAnimation(List<Texture2D> sprites, string name, float fps, bool Loop, bool AnimationLock = false)
        {
            this.Sprites = sprites.ToArray();
            this.Name = name;
            this.Fps = fps;
            this.Loop = Loop;
            this.AnimationLock = AnimationLock;
            animationType = EAnimationType.SpriteArray;
        }
        public CAnimation(Texture2D SpriteSheet, List<Vector2> SpritePositions, Vector2 SpriteSize, string name, float fps, bool Loop,bool AnimationLock = false, bool End = false)
        {
            this.SpriteSheet = SpriteSheet;
            this.SpritePositions = SpritePositions;
            this.SpriteSize = SpriteSize;
            this.Name = name;
            this.Fps = fps;
            this.Loop = Loop;
            this.End = End;
            this.AnimationLock = AnimationLock;
            animationType = EAnimationType.SpriteSheet;
        }
    }
}
