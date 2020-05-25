using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class GUIButton : GUI
    {
        #region Fields
        Action onClick;

        Color color = Color.White;
        Color colorHovering = Color.White;
        Color fontColor = Color.Black;

        Texture2D image;
        Texture2D imageHovering;

        SpriteFont spriteFont;
        string text = string.Empty;
        Vector2 fontScale = new Vector2(1, 1);

        bool lastUpdate;
        bool currentUpdate;
        #endregion

        #region Properties
        public Action OnClick { get => onClick; set => onClick = value; }
        public Color Color { get => color; set => color = value; }
        public Color ColorHovering { get => colorHovering; set => colorHovering = value; }
        public Color FontColor { get => fontColor; set => fontColor = value; }
        public Texture2D Image { get => image; set => image = value; }
        public Texture2D ImageHovering { get => imageHovering; set => imageHovering = value; }
        public SpriteFont SpriteFont { get => spriteFont; set => spriteFont = value; }
        public string Text { get => text; set => text = value; }
        public Vector2 FontScale { get => fontScale; set => fontScale = value; }


        #endregion

        #region Constructors
        public GUIButton()
        {

        }
        public GUIButton(CSpriteRenderer spriteRenderer)
        {
            this.SpriteRenderer = spriteRenderer;
            this.image = spriteRenderer.Sprite;
            this.imageHovering = spriteRenderer.Sprite;
            this.color = spriteRenderer.Color;
            this.colorHovering = Color.Gray;
            BlockGUI = true;
            ConstructorMethod();
        }
        public GUIButton(Texture2D image, Texture2D imageHovering, Color color, Color colorHovering)
        {
            this.image = image;
            this.imageHovering = imageHovering;
            this.color = color;
            this.colorHovering = colorHovering;
            BlockGUI = true;
            ConstructorMethod();
        }
        public GUIButton(CSpriteRenderer spriteRenderer, Texture2D image, Texture2D imageHovering, Color color, Color colorHovering)
        {
            this.SpriteRenderer = spriteRenderer;
            this.image = image;
            this.imageHovering = imageHovering;
            this.color = color;
            this.colorHovering = colorHovering;
            BlockGUI = true;
            ConstructorMethod();
        }
        public GUIButton(Texture2D image, Color color, Color colorHovering, Vector2 fontScale, string text)
        {
            this.image = image;
            this.color = color;
            this.colorHovering = colorHovering;
            this.fontScale = fontScale;
            this.text = text;
            BlockGUI = true;
            ConstructorMethod();
        }
        public GUIButton(CSpriteRenderer spriteRenderer, Texture2D image, Texture2D imageHovering, Color color, Color colorHovering, SpriteFont spriteFont, Color fontColor, Vector2 fontScale, string text)
        {
            this.SpriteRenderer = spriteRenderer;
            this.image = image;
            this.imageHovering = imageHovering;
            this.color = color;
            this.colorHovering = colorHovering;
            this.spriteFont = spriteFont;
            this.fontColor = fontColor;
            this.fontScale = fontScale;
            this.text = text;
            BlockGUI = true;
            ConstructorMethod();
        }
        public void ConstructorMethod()
        {

        }
        #endregion



        #region Methods 
        public override void Awake()
        {
            LayerDepth = SpriteRenderer.LayerDepth;
            base.Awake();
        }
        public override void Start()
        {
            base.Start();
            if (SpriteRenderer == null)
            {
                if (GameObject.GetComponent<CSpriteRenderer>() != null)
                {
                    SpriteRenderer = GameObject.GetComponent<CSpriteRenderer>();
                }
                else
                {
                    // TODO: see if you can fix this.
                    // so if it dont got a SpriteRenderer it can make it owns
                    //GameObject.AddComponent<SpriteRenderer>(new SpriteRenderer(image));
                }
            }
            if (spriteFont == null)
            {
                this.spriteFont = SpriteContainer.Instance.NormalFont;
            }
        }

        public override void Update()
        {
            if (MouseIsHovering)
            {
                if (imageHovering != null && SpriteRenderer.Sprite != imageHovering)
                {
                    SpriteRenderer.Sprite = imageHovering;
                }

                SpriteRenderer.Color = colorHovering;
                if (Input.GetMouseButtonUp(EMyMouseButtons.LeftButton) && lastUpdate == true)
                {
                    lastUpdate = false;
                    currentUpdate = false;
                    if (OnClick != null)
                    {
                        OnClick();
                    }
                }

                if (Input.GetMouseButton(EMyMouseButtons.LeftButton))
                {
                    if (currentUpdate == true)
                    {
                        lastUpdate = true;
                    }
                    else
                    {
                        lastUpdate = false;
                    }
                    currentUpdate = true;
                }
                else
                {
                    currentUpdate = false;
                }
            }
            else
            {
                if (SpriteRenderer.Sprite != image)
                {
                    SpriteRenderer.Sprite = image;
                }
                SpriteRenderer.Color = color;
            }
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (GUImouseBlockCollision.X + (GUImouseBlockCollision.Width / 2)) - (SpriteFont.MeasureString(Text).X / 2) * FontScale.X;
                var y = (GUImouseBlockCollision.Y + (GUImouseBlockCollision.Height / 2)) - (SpriteFont.MeasureString(Text).Y / 2) * FontScale.Y;

                spriteBatch.DrawString(
                    // SpriteFont
                    SpriteFont,
                    // String text
                    Text,
                    // Position
                    new Vector2(x, y),
                    // Color
                    fontColor,
                    // Rotation
                    MathHelper.ToRadians(this.GameObject.Transform.Rotation),
                    // Origin
                    Vector2.Zero,
                    // Scale
                    FontScale,
                    // SpriteEffects
                    SpriteRenderer.SpriteEffects,
                    // LayerDepth
                    SpriteRenderer.LayerDepth + 0.00001f
                );
            }
        }

        public override void Destroy()
        {
            base.Destroy();
        }
        #endregion
    }
}
