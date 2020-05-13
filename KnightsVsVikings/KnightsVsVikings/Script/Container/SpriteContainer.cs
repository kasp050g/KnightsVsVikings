using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class SpriteContainer
    {
        #region Singleton
        private static SpriteContainer instance;
        public static SpriteContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpriteContainer();
                }
                return instance;
            }
        }
        #endregion

        private Dictionary<string, Texture2D> sprite = new Dictionary<string, Texture2D>();
        private Dictionary<string, List<Texture2D>> spriteList = new Dictionary<string, List<Texture2D>>();
        private SpriteFont normalFont;

        public Dictionary<string, Texture2D> Sprite { get => sprite; set => sprite = value; }
        public Dictionary<string, List<Texture2D>> SpriteList { get => spriteList; set => spriteList = value; }
        public SpriteFont NormalFont { get => normalFont; set => normalFont = value; }

        public SpriteContainer()
        {

        }

        public void LoadContent(ContentManager content)
        {
            // Normal Font
            NormalFont = content.Load<SpriteFont>("Font/NormalFont");

            // The Pixel
            AddSprite(content.Load<Texture2D>("Images/MainSystem/Pixel"), "Pixel");


        }

        private void AddSprite(Texture2D texture2D, string name)
        {
            Sprite.Add(name, texture2D);
        }

        private void AddSpriteList(List<Texture2D> texture2Ds, string name)
        {
            SpriteList.Add(name, texture2Ds);
        }
    }
}
