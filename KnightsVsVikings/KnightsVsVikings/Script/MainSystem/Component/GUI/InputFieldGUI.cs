using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class InputFieldGUI : GUI
    {
        #region Fields
        Color color = Color.White;
        Color fontColor = Color.Black;
        Color placeholderFontColor = Color.Gray;
        Texture2D image;
        SpriteFont spriteFont;
        Vector2 fontScale = new Vector2(1, 1);
        string placeholderText = "";
        string text = string.Empty;
        bool currentSelected = false;
        float inputCooldown = 0.15f;
        float currentInputCooldown = 0;
        #endregion

        #region Properties
        public Color Color { get => color; set => color = value; }
        public Color FontColor { get => fontColor; set => fontColor = value; }
        public Texture2D Image { get => image; set => image = value; }
        public SpriteFont SpriteFont { get => spriteFont; set => spriteFont = value; }
        public Vector2 FontScale { get => fontScale; set => fontScale = value; }
        public string PlaceholderText { get => placeholderText; set => placeholderText = value; }
        public string Text { get => text; set => text = value; }
        #endregion

        #region Constructors
        public InputFieldGUI(SpriteRenderer spriteRenderer, Texture2D image, Color color, SpriteFont spriteFont, Color fontColor, Vector2 fontScale, string placeholderText)
        {
            this.SpriteRenderer = spriteRenderer;
            this.image = image;
            this.color = color;
            this.spriteFont = spriteFont;
            this.fontColor = fontColor;
            this.fontScale = fontScale;
            this.placeholderText = placeholderText;
            BlockGUI = true;
        }
        #endregion

        #region Methods 
        public override void Awake()
        {
            if (SpriteRenderer == null)
            {
                if (GameObject.GetComponent<SpriteRenderer>() != null)
                {
                    SpriteRenderer = GameObject.GetComponent<SpriteRenderer>();
                }
                else
                {
                    //GameObject.AddComponent<SpriteRenderer>(new SpriteRenderer(image));
                }
            }
            if (spriteFont == null)
            {
                this.spriteFont = SpriteContainer.Instance.NormalFont;
            }
            LayerDepth = SpriteRenderer.LayerDepth;
            base.Awake();
        }
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            SelectedInputField();
            KeyBordInputText();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var x = (GUImouseBlockCollision.X + (GUImouseBlockCollision.Width / 2)) - (SpriteFont.MeasureString((text != string.Empty ? text : (currentSelected == true ? " " : placeholderText))).X / 2) * FontScale.X;
            var y = (GUImouseBlockCollision.Y + (GUImouseBlockCollision.Height / 2)) - (SpriteFont.MeasureString((text != string.Empty ? text : (currentSelected == true ? " " : placeholderText))).Y / 2) * FontScale.Y;

            spriteBatch.DrawString(
                // SpriteFont
                SpriteFont,
                // String text
                (text != string.Empty ? text : (currentSelected == true ? "" : placeholderText)) + (currentSelected == true ? "|" : ""),
                // Position
                new Vector2(x, y),
                // Color
                (text != string.Empty ? fontColor : placeholderFontColor),
                // Rotation
                MathHelper.ToRadians(this.GameObject.Transform.Rotation),
                // Origin
                Vector2.Zero,
                // Scale
                FontScale,
                // SpriteEffects
                SpriteRenderer.SpriteEffects,
                // LayerDepth
                SpriteRenderer.LayerDepth+0.00001f
            );
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public void SelectedInputField()
        {
            if (MouseIsHovering && !Input.GetMouseButtonDown(MyMouseButtonsEnum.RightButton))
            {
                if (Input.GetMouseButtonDown(MyMouseButtonsEnum.LeftButton))
                {
                    currentSelected = true;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(MyMouseButtonsEnum.LeftButton) || Input.GetMouseButtonDown(MyMouseButtonsEnum.RightButton))
                {
                    currentSelected = false;
                }
            }
        }

        public void KeyBordInputText()
        {
            if(currentSelected == true && currentInputCooldown <= 0)
            {
                // Get current state of keyboard.
                KeyboardState keyboardState = Keyboard.GetState();
                // Check if any keys are Pressed's
                Keys[] getPressedKeys = keyboardState.GetPressedKeys();

                foreach (Keys key in getPressedKeys)
                {
                    if(key == Keys.Back && text.Length > 0)
                    {
                        text = text.Remove(text.Length - 1);
                        currentInputCooldown = inputCooldown;
                    }
                    else if (!(key == Keys.Back))
                    {
                        if(key != Keys.LeftShift && key != Keys.RightShift)
                        {
                            string keyText = key.ToString().ToLower();
                            if (AcceptableCharacters(ref keyText))
                            {
                                text += keyText;
                                currentInputCooldown = inputCooldown;
                            }
                        }
                    }                    
                }
                
            }
            else if(currentSelected == true)
            {
                if(currentInputCooldown > 0)
                {
                    currentInputCooldown -= Time.deltaTime;
                }
            }
        }

        private bool AcceptableCharacters(ref string keyText)
        {
            switch (keyText)
            {
                case "q":
                    return true;
                case "w":
                    return true;
                case "e":
                    return true;
                case "r":
                    return true;
                case "t":
                    return true;
                case "y":
                    return true;
                case "u":
                    return true;
                case "i":
                    return true;
                case "o":
                    return true;
                case "p":
                    return true;
                case "å":
                    return true;
                case "a":
                    return true;
                case "s":
                    return true;
                case "d":
                    return true;
                case "f":
                    return true;
                case "g":
                    return true;
                case "h":
                    return true;
                case "j":
                    return true;
                case "k":
                    return true;
                case "l":
                    return true;
                case "æ":
                    return true;
                case "ø":
                    return true;
                case "z":
                    return true;
                case "x":
                    return true;
                case "c":
                    return true;
                case "v":
                    return true;
                case "b":
                    return true;
                case "n":
                    return true;
                case "m":
                    return true;

                case "d1":
                    keyText = "1";
                    return true;
                case "d2":
                    keyText = "2";
                    return true;
                case "d3":
                    keyText = "3";
                    return true;
                case "d4":
                    keyText = "4";
                    return true;
                case "d5":
                    keyText = "5";
                    return true;
                case "d6":
                    keyText = "6";
                    return true;
                case "d7":
                    keyText = "7";
                    return true;
                case "d8":
                    keyText = "8";
                    return true;
                case "d9":
                    keyText = "9";
                    return true;
                case "d0":
                    keyText = "0";
                    return true;
                default:
                    break;
            }
            return false;
        }


        #endregion
    }
}
