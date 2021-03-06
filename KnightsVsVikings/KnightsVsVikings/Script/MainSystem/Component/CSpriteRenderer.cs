﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    // Kasper  Fly
    public class CSpriteRenderer : Component
    {
        #region Fields
        private Texture2D sprite;
        private TextureSheet2D spriteSheet;
        private Color color = Color.White;
        private SpriteEffects spriteEffects = SpriteEffects.None;
        private float layerDepth = 0;
        private EOriginPosition originPositionEnum = EOriginPosition.TopLeft;
        private Vector2 offSet = new Vector2(0, 0);
        private Rectangle rectangle;
        #endregion

        #region Properties 
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public TextureSheet2D SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
        public Color Color { get => color; set => color = value; }
        public SpriteEffects SpriteEffects { get => spriteEffects; set => spriteEffects = value; }
        public float LayerDepth { get => layerDepth; set => layerDepth = value; }
        public EOriginPosition OriginPositionEnum { get => originPositionEnum; set => originPositionEnum = value; }
        public Vector2 OffSet { get => offSet; set => offSet = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        #endregion

        #region Constructors  
        public CSpriteRenderer()
        {
            if (Sprite == null)
            {
                Sprite = SpriteContainer.Instance.Sprite["Pixel"];
                rectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            }
        }

        public CSpriteRenderer(Texture2D spriteName)
        {
            sprite = spriteName;
            rectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
        }

        public CSpriteRenderer(TextureSheet2D spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            this.sprite = spriteSheet.Sprite;
            this.rectangle = spriteSheet.Rectangle;
        }

        public CSpriteRenderer(string spriteName, EOriginPosition originPositionEnum = EOriginPosition.TopLeft, float layerDepth = 0, Color? color = null)
        {
            this.color = color ?? Color.White;
            this.layerDepth = layerDepth;
            this.OriginPositionEnum = originPositionEnum;
            SetSprite(spriteName);
        }
        #endregion

        #region Methods 
        public override void Awake()
        {
            base.Awake();

            if(sprite == null)
            {
                sprite = SpriteContainer.Instance.Pixel;
            }

            if (spriteSheet != null)
            {
                Helper.UpdateOrigin(GameObject, spriteSheet, originPositionEnum);
            }
            else
            {
                Helper.UpdateOrigin(GameObject, sprite, originPositionEnum);
            }
        }

        /// <summary>
        /// Set sprite with string
        /// </summary>
        /// <param name="spriteName">String name of the sprite</param>
        public void SetSprite(string spriteName)
        {
            if (SpriteContainer.Instance.Sprite.ContainsKey(spriteName))
            {
                sprite = SpriteContainer.Instance.Sprite[spriteName];
                rectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            }
            else if (SpriteContainer.Instance.SpriteSheet.ContainsKey(spriteName))
            {
                spriteSheet = SpriteContainer.Instance.SpriteSheet[spriteName];
                sprite = spriteSheet.Sprite;
                rectangle = spriteSheet.Rectangle;
            }
        }

        /// <summary>
        /// Set sprite with a Texture2D
        /// </summary>
        /// <param name="spriteName">type with a Texture2D</param>
        public void SetSprite(Texture2D spriteName)
        {
            spriteSheet = null;
            sprite = spriteName;
            rectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
        }

        /// <summary>
        /// Set sprite with a TextureSheet2D
        /// </summary>
        /// <param name="spriteName">type with a TextureSheet2D</param>
        public void SetSprite(TextureSheet2D spriteName)
        {
            spriteSheet = spriteName;
            sprite = spriteName.Sprite;
            rectangle = spriteName.Rectangle;
        }

        public void RemoveImage()
        {
            spriteSheet = null;
            sprite = null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(sprite != null)
            spriteBatch.Draw(
                // Texture2D
                this.Sprite,
                // Postion
                this.GameObject.Transform.Position + OffSet,
                // Source Rectangle
                Rectangle,
                // Color
                this.Color,
                // Rotation
                MathHelper.ToRadians(this.GameObject.Transform.Rotation),
                // Origin
                this.GameObject.Transform.Origin,
                // Scale
                this.GameObject.Transform.Scale,
                // SpriteEffects
                this.SpriteEffects,
                // LayerDepth
                this.LayerDepth + GameObject.Transform.Position.Y / 100000 + GameObject.Transform.Position.X / 110000
            );
        }

        public override string ToString()
        {
            return "SpriteRenderer";
        }

        public CSpriteRenderer Clone()
        {
            return (CSpriteRenderer)this.MemberwiseClone();
        }

        /// <summary>
        /// Set origin of the sprite
        /// </summary>
        /// <param name="originPositionEnum">origin Position</param>
        public void SetOrigin(EOriginPosition originPositionEnum)
        {
            this.originPositionEnum = originPositionEnum;
            if (spriteSheet != null)
            {
                Helper.UpdateOrigin(GameObject, spriteSheet, originPositionEnum);
            }
            else
            {
                Helper.UpdateOrigin(GameObject, sprite, originPositionEnum);
            }
        }

        #endregion
    }
}
