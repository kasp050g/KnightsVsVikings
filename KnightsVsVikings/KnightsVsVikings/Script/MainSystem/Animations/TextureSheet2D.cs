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
    public class TextureSheet2D
    {
        private Texture2D sprite;
        private Rectangle rectangle;

        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }

        /// <summary>
        /// A cut out of a sprite sheet.
        /// </summary>
        /// <param name="sprite">Sprite sheet image</param>
        /// <param name="position">position on the Sprite sheet</param>
        /// <param name="size">the size of the cut out image</param>
        public TextureSheet2D(Texture2D sprite, Vector2 position, Vector2 size)
        {
            this.sprite = sprite;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }
    }
}
