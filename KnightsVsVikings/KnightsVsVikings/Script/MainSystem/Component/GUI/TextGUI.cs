using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class TextGUI : GUI
    {
        #region Fields
        Color fontColor = Color.Black;
        SpriteFont spriteFont;
        string text = string.Empty;
        Vector2 fontScale = new Vector2(1, 1);

        Vector2 newPosition = new Vector2(0, 0);
        Vector2 offsetPosition = new Vector2(0, 0);
        OriginPositionEnum originPositionEnum = OriginPositionEnum.TopLeft;
        #endregion

        #region Properties
        public Color FontColor { get => fontColor; set => fontColor = value; }
        public SpriteFont SpriteFont { get => spriteFont; set => spriteFont = value; }
        public string Text { get { return text; } private set { text = value;  } }
        public Vector2 FontScale { get => fontScale; set => fontScale = value; }
        
        public OriginPositionEnum OriginPositionEnum { get => originPositionEnum; set => originPositionEnum = value; }
        public Vector2 OffsetPosition { get => offsetPosition; set => offsetPosition = value; }
        public virtual Rectangle OriginRectangle
        {
            get
            {
                // returns a new rectangle based on the position, scale, sprite width and height.
                return new Rectangle(
                    (int)this.GameObject.Transform.Position.X - (int)(this.GameObject.Transform.Origin.X * this.GameObject.Transform.Scale.X),
                    (int)this.GameObject.Transform.Position.Y - (int)(this.GameObject.Transform.Origin.Y * this.GameObject.Transform.Scale.Y),
                    (int)(1 * this.GameObject.Transform.Scale.X),
                    (int)(1 * this.GameObject.Transform.Scale.Y)
                    );
            }
        }

        #endregion

        #region Constructors
        public TextGUI(string text)
        {
            this.spriteFont = SpriteContainer.Instance.NormalFont;
            this.fontColor = Color.Black;
            this.fontScale = new Vector2(0.5f,0.5f);
            this.text = text;

        }
        public TextGUI(SpriteFont spriteFont, Color fontColor, Vector2 fontScale, string text)
        {
            this.spriteFont = spriteFont;
            this.fontColor = fontColor;
            this.fontScale = fontScale;
            this.text = text;
            
        }
        public TextGUI(SpriteFont spriteFont, Color fontColor, Vector2 fontScale, string text,OriginPositionEnum originPositionEnum)
        {
            this.spriteFont = spriteFont;
            this.fontColor = fontColor;
            this.fontScale = fontScale;
            this.text = text;
            this.originPositionEnum = originPositionEnum;
        }
        public void ConstructorMethod()
        {
            
        }
        #endregion

        #region Methods 
        public override void Awake()
        {
            if (spriteFont == null)
            {
                this.spriteFont = SpriteContainer.Instance.NormalFont;
            }
            UpdateOriginPosition();
            base.Awake();
        }
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                spriteBatch.DrawString(
                    // SpriteFont
                    SpriteFont,
                    // String text
                    Text,
                    // Position
                    newPosition + OffsetPosition,
                    // Color
                    fontColor,
                    // Rotation
                    MathHelper.ToRadians(this.GameObject.Transform.Rotation),
                    // Origin
                    Vector2.Zero,
                    // Scale
                    FontScale,
                    // SpriteEffects
                    SpriteEffects.None,
                    // LayerDepth
                    LayerDepth
                );
            }
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public void UpdateOriginPosition()
        {
            Helper.UpdateOriginText(text, OriginRectangle, spriteFont, fontScale, originPositionEnum, ref newPosition);
        }

        public void SetText(string text)
        {
            this.text = text;
            UpdateOriginPosition();
        }

        #endregion
    }
}
