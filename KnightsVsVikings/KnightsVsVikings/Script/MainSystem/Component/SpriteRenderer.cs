using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class SpriteRenderer : Component
    {
        #region Fields
        private Texture2D sprite;
        private Color color = Color.White;
        private SpriteEffects spriteEffects = SpriteEffects.None;
        private float layerDepth = 0;
        private OriginPositionEnum originPositionEnum = OriginPositionEnum.TopLeft;
        private Vector2 offSet = new Vector2(0, 0);
        #endregion

        #region Properties 
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public Color Color { get => color; set => color = value; }
        public SpriteEffects SpriteEffects { get => spriteEffects; set => spriteEffects = value; }
        public float LayerDepth { get => layerDepth; set => layerDepth = value; }
        public OriginPositionEnum OriginPositionEnum { get => originPositionEnum; set => originPositionEnum = value; }
        public Vector2 OffSet { get => offSet; set => offSet = value; }
        #endregion

        #region Constructors  
        public SpriteRenderer()
        {
            if (Sprite == null)
            {
                Sprite = SpriteContainer.Instance.Sprite["Pixel"];
            }
        }

        public SpriteRenderer(Texture2D spriteName)
        {
            sprite = spriteName;
        }

        public SpriteRenderer(string spriteName)
        {
            SetSprite(spriteName);
        }

        public SpriteRenderer(string spriteName, OriginPositionEnum originPositionEnum)
        {
            this.OriginPositionEnum = originPositionEnum;
            SetSprite(spriteName);
        }

        public SpriteRenderer(string spriteName, OriginPositionEnum originPositionEnum, float layerDepth)
        {
            this.layerDepth = layerDepth;
            this.OriginPositionEnum = originPositionEnum;
            SetSprite(spriteName);
        }

        public SpriteRenderer(string spriteName, OriginPositionEnum originPositionEnum, float layerDepth, Color color)
        {
            this.color = color;
            this.layerDepth = layerDepth;
            this.OriginPositionEnum = originPositionEnum;
            SetSprite(spriteName);
        }
        #endregion

        #region Methods 
        public override void Awake()
        {
            base.Awake();
            Helper.UpdateOrigin(GameObject, sprite, originPositionEnum);
        }
        public void SetSprite(string spriteName)
        {
            sprite = SpriteContainer.Instance.Sprite[spriteName];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                // Texture2D
                this.Sprite,
                // Postion
                this.GameObject.Transform.Position + OffSet,
                // Source Rectangle
                null,
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
                this.LayerDepth
            );
        }

        public override string ToString()
        {
            return "SpriteRenderer";
        }

        public SpriteRenderer Clone()
        {
            return (SpriteRenderer)this.MemberwiseClone();
        }

        public void SetOrigin(OriginPositionEnum originPositionEnum)
        {
            this.originPositionEnum = originPositionEnum;
            Helper.UpdateOrigin(GameObject, sprite, originPositionEnum);
        }

        #endregion
    }
}
