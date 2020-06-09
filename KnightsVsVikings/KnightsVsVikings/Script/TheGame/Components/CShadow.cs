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
    public class CShadow : Component
    {
        private Texture2D Sprite;
        private Vector2 offSet = new Vector2(1, 23);

        public override void Awake()
        {
            base.Awake();
            Sprite = SpriteContainer.Instance.Sprite["Shadow"];
        }
        public override void Start()
        {
            base.Start();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
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
                    0.25f + GameObject.Transform.Position.Y / 100000 + GameObject.Transform.Position.X / 110000
                );
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
