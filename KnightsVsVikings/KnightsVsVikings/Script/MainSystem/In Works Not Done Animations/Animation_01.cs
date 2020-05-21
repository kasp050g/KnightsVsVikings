using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.MainSystem.In_Works_Not_Done_Animations
{
    public class Animation_01
    {
        public Texture2D[] Sprites { get; private set; }
        public string Name { get; private set; }
        public float Fps { get; private set; }

        public Animation_01(List<Texture2D> sprites, string name, float fps)
        {
            this.Sprites = sprites.ToArray();
            this.Name = name;
            this.Fps = fps;
        }
    }
}
