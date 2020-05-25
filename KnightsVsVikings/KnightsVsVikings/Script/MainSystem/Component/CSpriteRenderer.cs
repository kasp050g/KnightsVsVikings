using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class CSpriteRenderer : Component
    {
        #region Fields
        private Texture2D sprite;
        private Color color = Color.White;
        private SpriteEffects spriteEffects = SpriteEffects.None;
        private float layerDepth = 0;
        private EOriginPosition originPositionEnum = EOriginPosition.TopLeft;
        private Vector2 offSet = new Vector2(0, 0);
        private Rectangle rectangle;
        #endregion

        #region Properties 
        public Texture2D Sprite { get => sprite; set => sprite = value; }
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

        public CSpriteRenderer(string spriteName)
        {
            SetSprite(spriteName);
        }

        public CSpriteRenderer(string spriteName, EOriginPosition originPositionEnum)
        {
            this.OriginPositionEnum = originPositionEnum;
            SetSprite(spriteName);
        }

        public CSpriteRenderer(string spriteName, EOriginPosition originPositionEnum, float layerDepth)
        {
            this.layerDepth = layerDepth;
            this.OriginPositionEnum = originPositionEnum;
            SetSprite(spriteName);
        }

        public CSpriteRenderer(string spriteName, EOriginPosition originPositionEnum, float layerDepth, Color color)
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
            rectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
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
                this.LayerDepth
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

        public void SetOrigin(EOriginPosition originPositionEnum)
        {
            this.originPositionEnum = originPositionEnum;
            Helper.UpdateOrigin(GameObject, sprite, originPositionEnum);
        }

        #endregion
    }
}
