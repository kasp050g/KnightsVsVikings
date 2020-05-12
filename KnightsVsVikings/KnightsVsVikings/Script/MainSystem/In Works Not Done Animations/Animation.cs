using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class Animation
    {
        public Texture2D[] Sprites { get; private set; }
        public string Name { get; private set; }
        public float Fps { get; private set; }

        public Animation(List<Texture2D> sprites,string name,float fps)
        {
            this.Sprites = sprites.ToArray();
            this.Name = name;
            this.Fps = fps;
        }
    }
}
