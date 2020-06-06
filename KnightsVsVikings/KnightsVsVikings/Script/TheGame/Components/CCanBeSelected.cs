﻿using MainSystemFramework;
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
        private CSpriteRenderer spriteRenderer;
        private Texture2D Sprite;
        private Vector2 offSet = new Vector2(0,0);

        public virtual Rectangle SelectedCollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)GameObject.Transform.Position.X - (int)(GameObject.Transform.Origin.X * GameObject.Transform.Scale.X) + 1,
                    (int)GameObject.Transform.Position.Y - (int)(GameObject.Transform.Origin.Y * GameObject.Transform.Scale.Y) + 1,
                    (int)(1),
                    (int)(1));
            }
        }

        public override void Awake()
        {
            base.Awake();
            spriteRenderer.GameObject.GetComponent<CSpriteRenderer>();
            Sprite = SpriteContainer.Instance.Sprite["SlotNameBar"];
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
                this.GameObject.Transform.Scale,
                // SpriteEffects
                SpriteEffects.None,
                // LayerDepth
                //this.LayerDepth + GameObject.Transform.Position.Y / 100000 + GameObject.Transform.Position.X / 110000
                1
            );
        }

        public override void Update()
        {
            base.Update();
        }
    }
}