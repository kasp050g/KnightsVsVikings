using KnightsVsVikings.Script.MainSystem.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.MainSystem.In_Works_Not_Done_Animations
{
    public class Animation
    {
        public Texture2D[] Sprites { get; private set; }
        public string Name { get; private set; }
        public float Fps { get; private set; }

        public Texture2D SpriteSheet { get; set; }
        public List<Vector2> SpritePositions { get; set; }
        public Vector2 SpriteSize { get; set; }
        public EAnimationType animationType { get; set; }

        public Animation(List<Texture2D> sprites, string name, float fps)
        {
            this.Sprites = sprites.ToArray();
            this.Name = name;
            this.Fps = fps;
            animationType = EAnimationType.SpriteArray;
        }
        public Animation(Texture2D SpriteSheet, List<Vector2> SpritePositions, Vector2 SpriteSize, string name, float fps)
        {
            this.SpriteSheet = SpriteSheet;
            this.SpritePositions = SpritePositions;
            this.SpriteSize = SpriteSize;
            this.Name = name;
            this.Fps = fps;
            animationType = EAnimationType.SpriteSheet;
        }
    }
}
