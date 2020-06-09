using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class CCanBeSelected : Component
    {
        private bool isSelected;
        private Texture2D Sprite;
        private Vector2 offSet = new Vector2(1,20);
        private Texture2D textureCollisionBox;
        private int outlineThickness = 2;
        private Color outlineColor = Color.LawnGreen;

        public virtual Rectangle SelectedCollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)GameObject.Transform.Position.X - (int)(GameObject.Transform.Origin.X * GameObject.Transform.Scale.X) + 1,
                    (int)GameObject.Transform.Position.Y - (int)(GameObject.Transform.Origin.Y * GameObject.Transform.Scale.Y) + 1,
                    (int)(64),
                    (int)(64));
            }
        }

        public bool IsSelected { get => isSelected; set => isSelected = value; }

        public override void Awake()
        {
            base.Awake();
            Sprite = SpriteContainer.Instance.Sprite["IsSelected"];
            textureCollisionBox = SpriteContainer.Instance.Pixel;
        }
        public override void Start()
        {
            base.Start();
        }

        public override void Destroy()
        {
            base.Destroy();
            GameObject.MyScene.SelectedEnabled.Remove(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(isSelected)
            spriteBatch.Draw(
                // Texture2D
                this.Sprite,
                // Postion
                this.GameObject.Transform.Position + offSet,
                // Source Rectangle
                null,
                // Color
                Color.White,
                // Rotation
                MathHelper.ToRadians(this.GameObject.Transform.Rotation),
                // Origin
                this.GameObject.Transform.Origin,
                // Scale
                this.GameObject.Transform.Scale * 0.8f,
                // SpriteEffects
                SpriteEffects.None,
                // LayerDepth
                0.29f + GameObject.Transform.Position.Y / 100000 + GameObject.Transform.Position.X / 110000                
            );
#if DEBUG
            DrawCollisionBox(spriteBatch);
#endif
        }

        public override void Update()
        {
            base.Update();
        }

        private void DrawCollisionBox(SpriteBatch spriteBatch)
        {
            Rectangle collisionBox = SelectedCollisionBox;
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width + outlineThickness, outlineThickness);
            Rectangle rigthLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, outlineThickness, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, outlineThickness, collisionBox.Height);
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, outlineThickness);

            spriteBatch.Draw(textureCollisionBox, bottomLine, null, outlineColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(textureCollisionBox, rigthLine, null, outlineColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(textureCollisionBox, leftLine, null, outlineColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(textureCollisionBox, topLine, null, outlineColor, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
